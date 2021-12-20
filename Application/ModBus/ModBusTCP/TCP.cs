using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.IO;
using System.Threading;

namespace OpticalFiber
{
    public abstract class TCP
    {
        public abstract void Connect(IPEndPoint iPEndPoint);
        public abstract void Send(byte[] sendBytes);
        public abstract byte[] Receive(byte[] recBytes);
        public abstract void Close();
    }

    public class ModBusTCP_SyncSocket : TCP//该方案不适合使用异步方式  需要在接收数据后进行解析 使用套接字 
    {
        ModBusSendData sendData = new ModBusSendData_TCP();
        DataAnalysis dataAnalysis = new ModBusTcpDataAnalysis();
        Thread thread;
        Thread threadCommCheck;
        Socket clientSocket;
        IPEndPoint iPEndPoint;
        int deviceNo;
        public ModBusTCP_SyncSocket(IPEndPoint iPEndPoint)
        {
            try
            {
                this.iPEndPoint = iPEndPoint;
                if (iPEndPoint == null)
                {
                    throw new Exception();
                }
                string[] temp = iPEndPoint.Address.ToString().Split('.');
                deviceNo = Convert.ToInt32(value: temp[3]);
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
                Connect(iPEndPoint);//连接
                if (clientSocket.Connected)
                {
                    thread = new Thread(WorkThread);
                    thread.Start();
                }
                threadCommCheck = new Thread(CommCheck);
                threadCommCheck.Start();
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
                List<byte> tempBytes = new List<byte>();
                int channelLength = 0;
                byte[] tempbyte = new byte[] { (byte)deviceNo };//识别设备编号
                byte[] bytesDeviceStatus = new byte[15];
                byte[] bytesDeviceParam = new byte[79];
                byte[] byteChannelParam = new byte[37];
                byte[] byteAvgTemper = new byte[259];
                byte[] byteRealTemper = new byte[259];
                byte[] bytePartParam = new byte[97];
                while (!DataClass.cancellationTokenSource.IsCancellationRequested)
                {
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

                    for (int i = 1; i < DataClass.list_DeviceParam[deviceNo].struct_deivceParam.channelNum; i++)
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
                            //tempBytes = new List<byte>();
                            //tempBytes.AddRange(tempbyte);
                            //tempBytes.AddRange(new byte[] { (byte)i, (byte)j });
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
                    if (DataClass.list_TcpCommFault[deviceNo])
                    {
                        DataClass.list_TcpCommFault[deviceNo] = false;

                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                //DataClass.ShowErrMsg(e.Message);
            }
            finally
            {
                //Close();
            }
        }


        public override void Connect(IPEndPoint iPEndPoint)
        {
            try
            {
                if (clientSocket == null)
                {
                    //clientSocket.Connect(iPEndPoint);//连接
                    clientSocket = new Socket(iPEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    IAsyncResult asyncResult = clientSocket.BeginConnect(iPEndPoint, null, null);
                    bool success = asyncResult.AsyncWaitHandle.WaitOne(1000);
                    if (!success)
                    {
                        throw new SocketException();
                    }
                    if (clientSocket.Connected)
                    {
                        clientSocket.SendTimeout = 200;
                        clientSocket.ReceiveTimeout = 200;
                        thread = new Thread(WorkThread);
                        thread.Start();
                    }
                }
                else
                {
                    if (!clientSocket.Connected)
                    {
                        clientSocket = new Socket(iPEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                        IAsyncResult asyncResult = clientSocket.BeginConnect(iPEndPoint, null, null);
                        bool success = asyncResult.AsyncWaitHandle.WaitOne(1000);
                        if (!success)
                        {
                            throw new SocketException();
                        }
                        if (clientSocket.Connected)
                        {
                            clientSocket.SendTimeout = 200;
                            clientSocket.ReceiveTimeout = 200;
                            thread = new Thread(WorkThread);
                            thread.Start();
                        }
                    }
                    else
                    {
                        if (!thread.IsAlive)
                        {
                            thread.Start();
                        }
                    }
                }
                
            }
            catch (ArgumentNullException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//buffer 为 null
            }
            catch (SocketException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//尝试访问套接字时出错
            }
            catch (ObjectDisposedException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//Socket 已关闭
            }
            catch (SecurityException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//调用堆栈中的调用方没有所需的权限
            }
            catch (InvalidOperationException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//正在进行 System.Net.Sockets.Socket.Listen(System.Int32)操作。
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
                clientSocket.Send(sendBytes);
            }
            catch (ArgumentNullException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//buffer 为 null
            }
            catch (SocketException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//尝试访问套接字时出错
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
                clientSocket.Receive(recByte);
            }
            catch (ArgumentNullException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//buffer 为 null
            }
            catch (SocketException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//尝试访问套接字时出错
            }
            catch (ObjectDisposedException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//Socket 已关闭
            }
            catch (SecurityException)
            {
                DataClass.list_TcpCommFault[deviceNo] = true;
                throw;//调用堆栈中的调用方没有所需的权限
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
                clientSocket.Shutdown(SocketShutdown.Both);//关闭
            }
            catch (SocketException e)
            {
                DataClass.ShowErrMsg("尝试访问套接字时出错" + e.Message);
            }
            catch (ObjectDisposedException e)
            {
                DataClass.ShowErrMsg("已关闭" + e.Message);
            }
            finally
            {
                clientSocket.Close();
            }
        }
    }
}
