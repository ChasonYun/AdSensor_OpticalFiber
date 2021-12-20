using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpticalFiber
{
  
    public class SyncSocket
    {
        Thread thread;
        Thread threadCommCheck;
        Socket clientSocket;
        IPEndPoint iPEndPoint;
        int deviceNo;

        public SyncSocket(IPEndPoint iPEndPoint)
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
                clientSocket = new Socket(iPEndPoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.SendTimeout = 200;
                clientSocket.ReceiveTimeout = 200;
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
                    Send(100, 1, 1, 48);//发送
                    Receive(bytesDeviceStatus);
                    tempBytes.AddRange(bytesDeviceStatus);
                    DataAnalyse_DeviceStatus(tempBytes.ToArray());
                    #endregion

                    #region//获取设备参数
                    tempBytes.Clear();
                    tempBytes.AddRange(tempbyte);
                    Send(100, 3, 1, 35);//发送
                    Receive(bytesDeviceParam);
                    tempBytes.AddRange(bytesDeviceParam);
                    DataAnalyse_DeviceParam(tempBytes.ToArray());
                    #endregion

                    #region//获取设备通道参数
                    for (int i = 1; i <= DataClass.list_DeviceParam[deviceNo].struct_deivceParam.channelNum; i++)
                    {
                        tempBytes.Clear();
                        tempBytes.AddRange(tempbyte);//添加 设备编号
                        tempBytes.AddRange(new byte[] { (byte)i });//添加 通道号 
                        Send(i, 3, 1, 14);//发送
                        Receive(byteChannelParam);
                        tempBytes.AddRange(byteChannelParam);
                        DataAnalyse_DeviceChannelParam(tempBytes.ToArray());
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
                            Send(i, 4, (j - 1) * 125 + 1, 125);
                            Receive(byteAvgTemper);
                            tempBytes.AddRange(byteAvgTemper);
                            DataAnalyse_ChannelAverageTemper(tempBytes.ToArray());
                            #endregion

                            #region//实时温度
                            tempBytes.Clear();
                            tempBytes.AddRange(tempbyte);
                            tempBytes.AddRange(new byte[] { (byte)i, (byte)j });
                            Send(i, 4, 10001 + (j - 1) * 125, 125);
                            Receive(byteRealTemper);
                            tempBytes.AddRange(byteRealTemper);
                            DataAnalyse_ChannelRealTemper(tempBytes.ToArray());
                            #endregion

                            #region//温升
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
                            Send(i + 200, 3, 100 * j + 1, 44);
                            Receive(bytePartParam);
                            tempBytes.AddRange(bytePartParam);
                            DataAnalyse_PartitionParam(tempBytes.ToArray());
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
            catch (Exception e)
            {
                DataClass.ShowErrMsg(e.Message);
            }
            finally
            {
                clientSocket.Shutdown(SocketShutdown.Both);//关闭
                clientSocket.Close();
                DataClass.list_TcpCommFault[deviceNo] = true;
            }
        }

        private byte[] IntsToBytes(int add, int funcode, int startAdd, int num)
        {
            byte[] bytes = new byte[12];
            try
            {
                bytes[0] = 0x00;
                bytes[1] = 0x00;
                bytes[2] = 0x00;
                bytes[3] = 0x00;
                bytes[4] = 0x00;
                bytes[5] = 0x06;//前面是固定的
                bytes[6] = Convert.ToByte(add);
                bytes[7] = Convert.ToByte(funcode);
                bytes[8] = Convert.ToByte(startAdd >> 8);
                bytes[9] = Convert.ToByte(startAdd & 0xff);
                bytes[10] = Convert.ToByte(num >> 8);
                bytes[11] = Convert.ToByte(num & 0xff);//不需要校验
                return bytes;
            }
            catch (Exception ex)
            {
                throw new Exception("数据转换失败" + ex.Message);
            }
        }

        private void Connect(IPEndPoint iPEndPoint)
        {
            try
            {
                //clientSocket.Connect(iPEndPoint);//连接
                IAsyncResult asyncResult = clientSocket.BeginConnect(iPEndPoint, null, null);
                bool success = asyncResult.AsyncWaitHandle.WaitOne(1000);
                if (!success)
                {
                    throw new SocketException();
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
        private void Send(int add, int funcode, int startAdd, int num)
        {
            try
            {
                clientSocket.Send(IntsToBytes(add, funcode, startAdd, num));
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

        private byte[] Receive(byte[] recByte)
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

        /// <summary>
        /// 解析更新 设备状态
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        private void DataAnalyse_DeviceStatus(byte[] recBytes)
        {
            try
            {
                Class_DeviceStatus deviceStatus = new Class_DeviceStatus();
                if (recBytes != null)
                {
                    int deviceNo = (int)recBytes[0];//开头为设备编号
                    ///处理设备状态  8个bit为一个byte 高低位倒置 10-15 6byte 等于 48bit  结构体无法直接改变  可以修改为class或者生成先的结构 重新赋值
                    deviceStatus.ControlStatus = Convert.ToBoolean(recBytes[10] & (0x01 << 0x00));
                    deviceStatus.ErrorStatus = Convert.ToBoolean(recBytes[10] & (0x01 << 0x001));
                    deviceStatus.RunStatus = Convert.ToBoolean(recBytes[10] & (0x01 << 0x02));
                    deviceStatus.VoiceStatus = Convert.ToBoolean(recBytes[10] & (0x01 << 0x03));
                    deviceStatus.ResetStatus = Convert.ToBoolean(recBytes[10] & (0x01 << 0x04));
                    deviceStatus.AcquireBoardErr = Convert.ToBoolean(recBytes[10] & (0x01 << 0x05));
                    deviceStatus.AlarmBoardErr = Convert.ToBoolean(recBytes[10] & (0x01 << 0x06));
                    deviceStatus.ChassisTemSensorErr = Convert.ToBoolean(recBytes[10] & (0x01 << 0x07));

                    deviceStatus.ReferTemSensorErr = Convert.ToBoolean(recBytes[11] & (0x01 << 0x00));

                    deviceStatus.Relay1_Status = Convert.ToBoolean(recBytes[15] & (0x01 << 0x00));
                    deviceStatus.Relay2_Status = Convert.ToBoolean(recBytes[15] & (0x01 << 0x01));
                    deviceStatus.Relay3_Status = Convert.ToBoolean(recBytes[15] & (0x01 << 0x02));
                    deviceStatus.Relay4_Status = Convert.ToBoolean(recBytes[15] & (0x01 << 0x03));
                    deviceStatus.Relay5_Status = Convert.ToBoolean(recBytes[15] & (0x01 << 0x04));
                    deviceStatus.Relay6_Status = Convert.ToBoolean(recBytes[15] & (0x01 << 0x05));
                    deviceStatus.Relay7_Status = Convert.ToBoolean(recBytes[15] & (0x01 << 0x06));
                    deviceStatus.Relay8_Status = Convert.ToBoolean(recBytes[15] & (0x01 << 0x07));
                    DataClass.list_DeviceStatus[deviceNo] = deviceStatus;
                }
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("设备状态处理异常" + ex.Message);
            }
        }


        /// <summary>
        /// 解析更新 设备参数
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        private void DataAnalyse_DeviceParam(byte[] recBytes)
        {
            try
            {

                if (recBytes != null)
                {
                    int deviceNo = (int)recBytes[0];//开头为设备编号

                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.referLength = (short)(recBytes[10] << 8 | recBytes[11]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.dataPoint = (short)(recBytes[12] << 8 | recBytes[13]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.chassisTemper = (short)(recBytes[14] << 8 | recBytes[15]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.referTemper = (short)(recBytes[16] << 8 | recBytes[17]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.compensationCoefficient = (short)(recBytes[18] << 8 | recBytes[19]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.partitionNum = (short)(recBytes[20] << 8 | recBytes[21]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.channelNum = (short)(recBytes[22] << 8 | recBytes[23]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.scanModel = (short)(recBytes[24] << 8 | recBytes[25]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.year = (short)(recBytes[26] << 8 | recBytes[27]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.month = (short)(recBytes[28] << 8 | recBytes[29]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.day = (short)(recBytes[30] << 8 | recBytes[31]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.hour = (short)(recBytes[32] << 8 | recBytes[33]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.minute = (short)(recBytes[34] << 8 | recBytes[35]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.second = (short)(recBytes[36] << 8 | recBytes[37]);

                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.runTimeH_L = (short)((recBytes[70] << 8 | recBytes[71]) | (recBytes[72] << 8 | recBytes[73]));
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.runTimeThis = (short)(recBytes[74] << 8 | recBytes[75]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.faultCount = (short)(recBytes[76] << 8 | recBytes[77]);
                    DataClass.list_DeviceParam[deviceNo].struct_deivceParam.fireAlarmCount = (short)(recBytes[78] << 8 | recBytes[79]);
                }

            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("设备参数处理异常" + ex.Message);
            }
        }


        /// <summary>
        /// 解析更新 设备通道参数
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        private void DataAnalyse_DeviceChannelParam(byte[] recBytes)
        {
            try
            {
                struct_ChannelParam deviceChannelParam = new struct_ChannelParam();
                if (recBytes != null)
                {
                    int deviceNo = (int)recBytes[0];//开头为设备编号
                    int channelNo = (int)recBytes[1];//通道编号
                    deviceChannelParam.length = (short)(recBytes[11] << 8 | recBytes[12]);
                    deviceChannelParam.startPosition = (short)(recBytes[13] << 8 | recBytes[14]);
                    deviceChannelParam.slope = (short)(recBytes[15] << 8 | recBytes[16]);
                    deviceChannelParam.intercept = (short)(recBytes[17] << 8 | recBytes[18]);
                    deviceChannelParam.offsetTemper = (short)(recBytes[19] << 8 | recBytes[20]);
                    deviceChannelParam.averageTimes = (short)(recBytes[21] << 8 | recBytes[22]);
                    deviceChannelParam.brokenPoint = (short)(recBytes[23] << 8 | recBytes[24]);
                    deviceChannelParam.partition = (short)(recBytes[25] << 8 | recBytes[26]);
                    deviceChannelParam.status = (short)(recBytes[27] << 8 | recBytes[28]);
                    deviceChannelParam.checkStatus = (short)(recBytes[29] << 8 | recBytes[30]);
                    deviceChannelParam.faultStatus = (short)(recBytes[31] << 8 | recBytes[32]);
                    deviceChannelParam.isBroken = (short)(recBytes[33] << 8 | recBytes[34]);
                    deviceChannelParam.fireAlarmStatus = (short)(recBytes[35] << 8 | recBytes[36]);
                    deviceChannelParam.riseAlarmStatus = (short)(recBytes[37] << 8 | recBytes[38]);
                    DataClass.list_DeviceChannelParam[deviceNo].struct_DeviceChannelParam.struct_ChannelParams[channelNo] = deviceChannelParam;
                }
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("设备通道参数处理异常" + ex.Message);
            }
        }

        #region//处理温度
        /// <summary>
        /// 解析更新 设备通道平均温度
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        private void DataAnalyse_ChannelAverageTemper(byte[] recBytes)
        {
            try
            {
                if (recBytes != null)
                {
                    int deviceNo = (int)recBytes[0];//开头为设备编号
                    int channelNo = (int)recBytes[1];//通道号
                    int count = (int)recBytes[2];//巡检编号
                    for (int i = 1; i <= 125; i++)
                    {
                        //DataClass.deviceTemper[deviceNo, channelNo, (count - 1) * 125 + i] = recBytes[i * 2 + 10] << 8 | recBytes[i * 2 + 11];
                        DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].averageTemper[(count - 1) * 125 + i] = (short)(recBytes[i * 2 + 10] << 8 | recBytes[i * 2 + 11]);
                    }
                }
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("设备平均温度处理异常" + ex.Message);
            }
        }

        /// <summary>
        /// 解析更新 设备通道实时温度
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        private void DataAnalyse_ChannelRealTemper(byte[] recBytes)
        {
            try
            {
                if (recBytes != null)
                {
                    int deviceNo = (int)recBytes[0];//开头为设备编号
                    int channelNo = (int)recBytes[1];//通道号
                    int count = (int)recBytes[2];//巡检编号
                    for (int i = 1; i <= 125; i++)
                    {
                        //DataClass.deviceTemper[deviceNo, channelNo, (count - 1) * 125 + i] = recBytes[i * 2 + 10] << 8 | recBytes[i * 2 + 11];
                        DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].realTemper[(count - 1) * 125 + i] = (short)(recBytes[i * 2 + 10] << 8 | recBytes[i * 2 + 11]);
                    }
                }
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("设备实时温度处理异常" + ex.Message);
            }
        }

        /// <summary>
        /// 解析更新 设备通道温升
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        private void DataAnalyse_ChannelRiseTemper(byte[] recBytes)
        {
            try
            {
                if (recBytes != null)
                {
                    int deviceNo = (int)recBytes[0];//开头为设备编号
                    int channelNo = (int)recBytes[1];//通道号
                    int count = (int)recBytes[2];//巡检编号
                    for (int i = 1; i <= 125; i++)
                    {
                        //DataClass.deviceTemper[deviceNo, channelNo, (count - 1) * 125 + i] = recBytes[i * 2 + 10] << 8 | recBytes[i * 2 + 11];
                        DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].riseTemper[(count - 1) * 125 + i] = (short)(recBytes[i * 2 + 10] << 8 | recBytes[i * 2 + 11]);
                    }
                }
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("设备升温温度处理异常" + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 解析分区参数
        /// </summary>
        private void DataAnalyse_PartitionParam(byte[] recBytes)
        {
            try
            {
                struct_Partition struct_Partition = new struct_Partition();

                if (recBytes != null)
                {
                    int deviceNo = (int)recBytes[0];//开头为设备编号
                    int channelNo = (int)recBytes[1];//通道编号
                    int partitionNo = (int)recBytes[2];//分区编号

                    struct_Partition.startPosition = (short)(recBytes[12] << 8 | recBytes[13]);
                    struct_Partition.endPosition = (short)(recBytes[14] << 8 | recBytes[15]);
                    struct_Partition.relayNumber = (short)(recBytes[16] << 8 | recBytes[17]);
                    struct_Partition.fireAlarmThreshold = (short)(recBytes[18] << 8 | recBytes[19]);
                    struct_Partition.riseAlarmThreshold = (short)(recBytes[20] << 8 | recBytes[21]);

                    struct_Partition.alarmStatus = (short)(recBytes[50] << 8 | recBytes[51]);
                    struct_Partition.fireAlarmStatus = (short)(recBytes[52] << 8 | recBytes[53]);
                    struct_Partition.fireAlarmPosition = (short)(recBytes[54] << 8 | recBytes[55]);
                    struct_Partition.riseAlarmStaus = (short)(recBytes[56] << 8 | recBytes[57]);
                    struct_Partition.riseAlarmPosition = (short)(recBytes[58] << 8 | recBytes[59]);

                    struct_Partition.fireAlarmYear_Month = (short)(recBytes[72] << 8 | recBytes[73]);
                    struct_Partition.fireAlarmDay_Hour = (short)(recBytes[74] << 8 | recBytes[75]);
                    struct_Partition.fireAlarmMinute_Second = (short)(recBytes[76] << 8 | recBytes[77]);
                    struct_Partition.riseAlarmYear_Month = (short)(recBytes[78] << 8 | recBytes[79]);
                    struct_Partition.riseAlarmDay_Hour = (short)(recBytes[80] << 8 | recBytes[81]);
                    struct_Partition.riseAlarmMinute_Second = (short)(recBytes[82] << 8 | recBytes[83]);

                    struct_Partition.maxRise = (short)(recBytes[92] << 8 | recBytes[93]);
                    struct_Partition.maxRisePosition = (short)(recBytes[94] << 8 | recBytes[95]);

                    struct_Partition.maxTemper = (short)(recBytes[96] << 8 | recBytes[97]);
                    struct_Partition.maxTemperPosition = (short)(recBytes[98] << 8 | recBytes[99]);
                    DataClass.list_DevicePartition[deviceNo].struct_devicePartition.struct_ChannelPartitions[channelNo].struct_Partitions[partitionNo] = struct_Partition;
                }

            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("设备分区参数处理异常" + ex.Message + ex.StackTrace);
            }
        }
    }
}
