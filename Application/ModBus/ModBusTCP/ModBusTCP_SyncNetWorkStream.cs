using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpticalFiber
{
    public class ModBusTCP_SyncNetWorkStream:TCP
    {
        ModBusSendData sendData = new ModBusSendData_TCP();
        DataAnalysis dataAnalysis = new ModBusTcpDataAnalysis();
        Thread thread;
        Thread threadCommCheck;
        NetworkStream networkStream;
        IPEndPoint iPEndPoint;
        TcpClient tcpClient = new TcpClient();
        Socket socket;
        int deviceNo;

        public ModBusTCP_SyncNetWorkStream(IPEndPoint iPEndPoint)
        {
            try
            {
                this.iPEndPoint = iPEndPoint;
                if (iPEndPoint == null)
                {
                    throw new Exception();
                }
                DataClass.list_DeviceEnables = (new SQL_Select()).Select_DeviceEnable();
                foreach(struct_DeviceEnable _DeviceEnable in DataClass.list_DeviceEnables)
                {
                    if(_DeviceEnable.ipEndPoint.ToString()== iPEndPoint.ToString())
                    {
                        deviceNo = _DeviceEnable.deviceNo;
                    }
                }
                Start();
            }
            catch (Exception)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;
            }
        }

        private void Start()
        {
            try
            {
                try
                {
                    Connect(iPEndPoint);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (threadCommCheck == null)
                    {
                        threadCommCheck = new Thread(CommCheck);
                        threadCommCheck.Start();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void Connect(IPEndPoint iPEndPoint)
        {
            try
            {
                if (socket == null)
                {
                    socket = new Socket(iPEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    IAsyncResult asyncResult = socket.BeginConnect(iPEndPoint, null, null);
                    bool success = asyncResult.AsyncWaitHandle.WaitOne(1000);
                    if (!success)
                    {
                        throw new SocketException();
                    }
                    //socket.Connect(iPEndPoint);
                    if (socket.Connected)
                    {
                        networkStream = new NetworkStream(socket);
                        networkStream.ReadTimeout = 30000;
                        networkStream.WriteTimeout = 30000;
                        thread = new Thread(WorkThread);
                        thread.Start();
                    }
                }
                else
                {
                    if (!socket.Connected)
                    {
                        socket = new Socket(iPEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                        IAsyncResult asyncResult = socket.BeginConnect(iPEndPoint, null, null);
                        bool success = asyncResult.AsyncWaitHandle.WaitOne(1000);
                        if (!success)
                        {
                            throw new SocketException();
                        }
                        //socket.Connect(iPEndPoint);
                        if (socket.Connected)
                        {
                            networkStream = new NetworkStream(socket);
                            networkStream.ReadTimeout = 30000;
                            networkStream.WriteTimeout = 30000;
                            thread = new Thread(WorkThread);
                            thread.Start();
                        }
                    }
                    else
                    {
                        if (thread == null)
                        {
                            thread = new Thread(WorkThread);
                        }
                        if (!thread.IsAlive)
                        {
                            if (thread.ThreadState == System.Threading.ThreadState.Stopped)
                            {
                                thread = new Thread(WorkThread);
                            }
                            thread.Start();
                        }
                    }
                }
                //IAsyncResult asyncResult = tcpClient.BeginConnect(iPEndPoint.Address, iPEndPoint.Port, null, null);
                //bool success = asyncResult.AsyncWaitHandle.WaitOne(1000);
                //if (!success)
                //{
                //    throw new SocketException();
                //}
                //else
                //{
                //    networkStream = tcpClient.GetStream();
                //}
            }
            catch (ArgumentNullException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//参数 为 null
            }
            catch (SocketException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//尝试访问套接字时出错
            }
            catch (ObjectDisposedException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//TcpClient 已关闭
            }
            catch (Exception)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;
            }
        }

        public override void Send(byte[] sendBytes)
        {
            try
            {
                networkStream.Write(sendBytes, 0, sendBytes.Length);
            }
            catch (ArgumentNullException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//参数 为 null
            }
            catch (ArgumentOutOfRangeException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//参数超出范围
            }
            catch (IOException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//写入到网络时失败
            }
            catch (ObjectDisposedException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//Socket 已关闭
            }
            catch (Exception)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//
            }
        }

        public override byte[] Receive(byte[] recByte)
        {
            try
            {
                networkStream.Read(recByte, 0, recByte.Length);
            }
            catch (ArgumentNullException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//参数 为 null
            }
            catch (ArgumentOutOfRangeException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//长度出错
            }
            catch (IOException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//Socket 已关闭
            }
            catch (ObjectDisposedException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//NetworkStream已关闭
            }
            catch (Exception)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//
            }
            return recByte;
        }

        public override void Close()
        {
            try
            {
                socket.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
   

        private void CommCheck()
        {
            while (!DataClass.cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    try
                    {
                        if (DataClass.list_TcpCommFault[deviceNo])
                        {
                            Start();
                        }
                    }
                    catch (Exception)
                    {
                        //不做处理
                    }
                    Thread.Sleep(2000);
                }
                catch (Exception ex)
                {
                    DataClass.ShowErrMsg(ex.Message);
                }
            }
        }

        private void WorkThread()
        {
            try
            {
                //Stopwatch stopwatch;
                List<byte> tempBytes = new List<byte>();
                int channelLength = 0;
                byte[] tempbyte = new byte[] { (byte)deviceNo };//识别设备编号
                byte[] bytesDeviceStatus = new byte[15];
                byte[] bytesDeviceParam = new byte[79];
                byte[] byteChannelParam = new byte[37];
                byte[] byteAvgTemper = new byte[259];
                byte[] byteRealTemper = new byte[259];
                byte[] bytePartParam = new byte[97];
                byte[] byteDateTime = new byte[14];
                while (!DataClass.cancellationTokenSource.IsCancellationRequested)
                {
                    //stopwatch = new Stopwatch();
                    //stopwatch.Start();
                    #region//获取设备状态
                    tempBytes.Clear();
                    tempBytes.AddRange(tempbyte);
                    Send(sendData.Write_Data(100, 1, 1, 48));//发送
                    Receive(bytesDeviceStatus);
                    tempBytes.AddRange(bytesDeviceStatus);
                    dataAnalysis.DataAnalyse_DeviceStatus(tempBytes.ToArray());
                    #endregion

                    #region//获取设备参数
                    tempBytes.Clear();
                    tempBytes.AddRange(tempbyte);
                    Send(sendData.Write_Data(100, 3, 1, 35));//发送
                    Receive(bytesDeviceParam);
                    tempBytes.AddRange(bytesDeviceParam);
                    dataAnalysis.DataAnalyse_DeviceParam(tempBytes.ToArray());
                    #endregion

                    #region//获取设备通道参数
                    for (int i = 1; i <= DataClass.list_DeviceParam[deviceNo].struct_deivceParam.channelNum; i++)
                    {
                        tempBytes.Clear();
                        tempBytes.AddRange(tempbyte);//添加 设备编号
                        tempBytes.AddRange(new byte[] { (byte)i });//添加 通道号 
                        Send(sendData.Write_Data(i, 3, 1, 14));//发送
                        Receive(byteChannelParam);
                        tempBytes.AddRange(byteChannelParam);
                        dataAnalysis.DataAnalyse_DeviceChannelParam(tempBytes.ToArray());
                    }
                    #endregion

                    #region//获取通道温度 

                    for (int i = 1; i <= DataClass.list_DeviceParam[deviceNo].struct_deivceParam.channelNum; i++)
                    {
                        channelLength = DataClass.list_DeviceChannelParam[deviceNo].struct_DeviceChannelParam.struct_ChannelParams[i].length;
                        int count = channelLength / 125 + (channelLength % 125 == 0 ? 0 : 1);
                        for (int j = 1; j <= count; j++)
                        {
                            #region//平均温度
                            tempBytes.Clear();
                            tempBytes.AddRange(tempbyte);//添加 设备编号
                            tempBytes.AddRange(new byte[] { (byte)i, (byte)j });//添加 通道号 巡检编号
                            Send(sendData.Write_Data(i, 4, (j - 1) * 125 + 1, 125));
                            Receive(byteAvgTemper);
                            tempBytes.AddRange(byteAvgTemper);
                            dataAnalysis.DataAnalyse_ChannelAverageTemper(tempBytes.ToArray());
                            #endregion

                            #region//实时温度
                            tempBytes.Clear();
                            tempBytes.AddRange(tempbyte);
                            tempBytes.AddRange(new byte[] { (byte)i, (byte)j });
                            Send(sendData.Write_Data(i, 4, 10001 + (j - 1) * 125, 125));
                            Receive(byteRealTemper);
                            tempBytes.AddRange(byteRealTemper);
                            dataAnalysis.DataAnalyse_ChannelRealTemper(tempBytes.ToArray());
                            #endregion

                            #region//温升
                            tempBytes.Clear();
                            tempBytes.AddRange(tempbyte);
                            tempBytes.AddRange(new byte[] { (byte)i, (byte)j });
                            Send(sendData.Write_Data(i, 4, 20001 + (j - 1) * 125, 125));
                            Receive(byteRealTemper);
                            tempBytes.AddRange(byteRealTemper);
                            dataAnalysis.DataAnalyse_ChannelRiseTemper(tempBytes.ToArray());
                            //Send(i, 4, 20001 + (j - 1) * 125, 125);
                            //tempBytes.AddRange(tcp.Receive(9 + 125 * 2));
                            //lock (objLock_channelRiseTemper)
                            //{
                            //    queue_channelRiseTemper.Enqueue(tempBytes.ToArray());
                            //}
                            #endregion
                        }
                    }
                    #endregion

                    #region//获取分区参数
                    for (int i = 1; i <= DataClass.list_DeviceParam[deviceNo].struct_deivceParam.channelNum; i++)
                    {
                        for (int j = 1; j <= DataClass.list_DeviceChannelParam[deviceNo].struct_DeviceChannelParam.struct_ChannelParams[i].partition; j++)
                        {
                            tempBytes.Clear();
                            tempBytes.AddRange(tempbyte);//添加 设备编号
                            tempBytes.AddRange(new byte[] { (byte)i, (byte)j });//添加 通道号 分区 号
                            Send(sendData.Write_Data(i + 200, 3, 100 * j + 1, 44));
                            Receive(bytePartParam);
                            tempBytes.AddRange(bytePartParam);
                            dataAnalysis.DataAnalyse_PartitionParam(tempBytes.ToArray());
                        }
                    }
                    #endregion

                    #region//校时 一天校时 一次
                    if (DateTime.Now.ToLongTimeString().ToString() == "2:00:00") 
                    {
                        Send(sendData.Write_Data(100, 6, 22, DateTime.Now.Year));
                        Receive(byteDateTime);
                        Send(sendData.Write_Data(100, 6, 23, DateTime.Now.Month));
                        Receive(byteDateTime);
                        Send(sendData.Write_Data(100, 6, 24, DateTime.Now.Day));
                        Receive(byteDateTime);
                        Send(sendData.Write_Data(100, 6, 25, DateTime.Now.Hour));
                        Receive(byteDateTime);
                        Send(sendData.Write_Data(100, 6, 26, DateTime.Now.Minute));
                        Receive(byteDateTime);
                        Send(sendData.Write_Data(100, 6, 27, DateTime.Now.Second));
                        Receive(byteDateTime);
                    }
                    #endregion
                    if (DataClass.list_TcpCommFault[deviceNo])
                    {
                        DataClass.list_TcpCommFault[deviceNo] = false;

                    }
                    //stopwatch.Stop();
                    //string time = stopwatch.ElapsedMilliseconds.ToString();
                    Thread.Sleep(100);
                }
            }
            catch (Exception)
            {
               //不作处理
            }
            finally
            {
                if (DataClass.IsRunning)
                {
                    DataClass.list_TcpCommFault[deviceNo] = true;
                }
                Close();
            }
        }
    }
}
