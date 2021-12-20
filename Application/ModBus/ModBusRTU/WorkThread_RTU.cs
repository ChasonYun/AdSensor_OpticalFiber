//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace AD_Sensor
//{
//    class WorkThread_RTU
//    {
//        RTU Rtu;
//        Thread thread;
//        List<ModbusRTU_cmd> cmds;
//        byte[] recBytes;
//        byte[] crcModbusBytes;
//        int recLength;
//        int[] temp;
//        ModbusRTUSensorStatus sensorStatus;
//        public WorkThread_RTU(RTU Rtu, List<ModbusRTU_cmd> cmds)
//        {
//            for (int i = 1; i < 257; i++)
//            {
//                StaticClass.list_ModbusRTU_Status.Add(sensorStatus);
//                for (int j = 0; j < 257; j++)
//                {
//                    StaticClass.temper_ModbusRTU[i, j] = -16;
//                }
//            }
//            this.Rtu = Rtu;
//            this.cmds = cmds;
//            thread = new Thread(Start_ModbusRTU);
//            thread.Start();
//        }

//        public void Start_ModbusRTU()
//        {
//            try
//            {
//                if (!_SerialPort.GetPort().IsOpen)
//                {
//                    _SerialPort.GetPort().Open();
//                }
//                while (_SerialPort.GetPort().IsOpen)
//                {
//                    foreach (ModbusRTU_cmd modbusRTU_Cmd in cmds)//巡检温度 
//                    {
//                        StaticClass.Running_Sensor_Add = modbusRTU_Cmd.sensorAdd;
//                        Rtu.RTU_Write(modbusRTU_Cmd.sensorAdd, RTU_WriteCmd(modbusRTU_Cmd.sensorAdd, 4, modbusRTU_Cmd.startAdd, modbusRTU_Cmd.num));//查温度数据
//                        recLength = 5 + modbusRTU_Cmd.num * 2;
//                        recBytes = Rtu.RTU_Read(modbusRTU_Cmd.sensorAdd, recLength);//接收数据
//                        crcModbusBytes = CRC16.crc16(recBytes, recLength - 2);
//                        if (recBytes[recLength - 2] == crcModbusBytes[0] || recBytes[recLength - 1] == crcModbusBytes[1])//CRC校验通过
//                        {
//                            temp = Temp_Analyze(recBytes);
//                            for (int i = 1; i <= modbusRTU_Cmd.num; i++)
//                            {
//                                StaticClass.temper_ModbusRTU[modbusRTU_Cmd.sensorAdd, i + modbusRTU_Cmd.startAdd - 1] = temp[i];//更新温度数据
//                                StaticClass.commFault_ModbusRTU[modbusRTU_Cmd.sensorAdd] = 0;
//                            }
//                        }
//                        else//crc校验不通过
//                        {
//                            StaticClass.commFault_ModbusRTU[modbusRTU_Cmd.sensorAdd]++;
//                        }

//                        foreach (ModbusRTU_cmd modbusRTU_cmd in cmds)//巡检状态
//                        {
//                            StaticClass.Running_Sensor_Add = modbusRTU_Cmd.sensorAdd;
//                            Rtu.RTU_Write(modbusRTU_Cmd.sensorAdd, RTU_WriteCmd(modbusRTU_Cmd.sensorAdd, 3, 1, 6));//查状态
//                            recLength = 5 + 2 * 6;
//                            recBytes = Rtu.RTU_Read(modbusRTU_Cmd.sensorAdd, recLength);
//                            crcModbusBytes = CRC16.crc16(recBytes, recLength - 2);
//                            if (recBytes[recLength - 2] == crcModbusBytes[0] || recBytes[recLength - 1] == crcModbusBytes[1])//通过
//                            {
//                                SensorStatus_Analyze(recBytes);//更新探测器状态
//                                StaticClass.commFault_ModbusRTU[modbusRTU_Cmd.sensorAdd] = 0;
//                            }
//                            else//crc校验不通过
//                            {
//                                StaticClass.commFault_ModbusRTU[modbusRTU_Cmd.sensorAdd]++;
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                if (ex.Message == "正在中止线程。")//正常的信息  通过抛出异常结束线程的方式 就会抛出这个异常
//                {

//                }
//                else
//                {
//                    MessageBox.Show(ex.Message + ex.StackTrace);
//                }
//            }
//        }

//        /// <summary>
//        /// modbus写入数据协议转换 
//        /// </summary>
//        /// <param name="add">从机地址</param>
//        /// <param name="func">功能码</param>
//        /// <param name="num"></param>
//        /// <returns></returns>
//        private byte[] RTU_WriteCmd(int add, int funcode, int startAdd, int num)
//        {
//            byte[] bytes = new byte[8];
//            try
//            {
//                bytes[0] = Convert.ToByte(add);
//                bytes[1] = Convert.ToByte(funcode);
//                bytes[2] = Convert.ToByte(startAdd >> 8);
//                bytes[3] = Convert.ToByte(startAdd & 0xff);
//                bytes[4] = Convert.ToByte(num >> 8);
//                bytes[5] = Convert.ToByte(num & 0xff);
//                bytes[6] = CRC16.crc16(bytes, 6)[0];
//                bytes[7] = CRC16.crc16(bytes, 6)[1];
//                return bytes;
//            }
//            catch (Exception)
//            {
//                return bytes;
//            }
//        }

//        private int[] Temp_Analyze(byte[] bytes)
//        {
//            int[] tempAnalyze = new int[((int)bytes[2] / 2) + 1];
//            int tempTemp;
//            try
//            {
//                for (int i = 1; i <= tempAnalyze.Length - 1; i++)
//                {
//                    tempTemp = (bytes[i * 2 + 1] << 8 ^ bytes[i * 2 + 2]);
//                    if (tempTemp >= -10 && tempTemp <= 260)
//                    {
//                        tempAnalyze[i] = (bytes[i * 2 + 1] << 8 ^ bytes[i * 2 + 2]);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                string str = ex.Message + ex.StackTrace;

//            }
//            return tempAnalyze;
//        }

//        private void SensorStatus_Analyze(byte[] bytes)
//        {
//            try
//            {
//                sensorStatus.sensorAdd = bytes[0];
//                sensorStatus.sensorIsFire = (bytes[3] << 8) ^ bytes[4];
//                sensorStatus.sensorIsFault = (bytes[5] << 8) ^ bytes[6];
//                sensorStatus.maxTemp = (bytes[7] << 8) ^ bytes[8];
//                sensorStatus.maxTempLength = (bytes[9] << 8) ^ bytes[10];
//                sensorStatus.firstFireLength = (bytes[11] << 8) ^ bytes[12];
//                sensorStatus.firstFaultLength = (bytes[13] << 8) ^ bytes[14];
//                StaticClass.list_ModbusRTU_Status[sensorStatus.sensorAdd] = sensorStatus;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.ToString());
//            }
//        }

//        public void Rest_HardWare()
//        {
//            try
//            {
//                if (cmds.Count == 0)
//                {
//                    throw new Exception("当前无启用的探测器！");
//                }
//                foreach (ModbusRTU_cmd modbusRTU_Cmd in cmds)
//                {
//                    Rtu.RTU_Write(modbusRTU_Cmd.sensorAdd, RTU_WriteCmd(modbusRTU_Cmd.sensorAdd, 6, 0, 2));
//                }
//            }
//            catch (Exception ex)
//            {
//                throw new Exception("复位失败！" + ex.Message);
//            }
//        }

//        public void Abort_WorkThread()
//        {
//            try
//            {
//                thread.Abort();
//                thread = null;
//                _SerialPort.GetPort().Close();
//            }
//            catch (Exception ex)
//            {
//                if (ex.Message == "正在中止线程。")
//                {

//                }
//                else
//                {
//                    throw new Exception(ex.Message);
//                }
//            }
//        }

//        public void Start_WorkThread()
//        {
//            try
//            {
//                thread = null;
//                thread = new Thread(Start_ModbusRTU);
//                thread.Start();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//    }
//}
