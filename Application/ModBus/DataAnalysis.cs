using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticalFiber
{
    public abstract class  DataAnalysis
    {
        /// <summary>
        /// 解析更新 设备状态
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        public abstract void DataAnalyse_DeviceStatus(byte[] recBytes);

        /// <summary>
        /// 解析更新 设备参数
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        public abstract void DataAnalyse_DeviceParam(byte[] recBytes);

        /// <summary>
        /// 解析更新 设备通道参数
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        public abstract void DataAnalyse_DeviceChannelParam(byte[] recBytes);

        /// <summary>
        /// 解析更新 设备通道平均温度
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        public abstract void DataAnalyse_ChannelAverageTemper(byte[] recBytes);

        /// <summary>
        /// 解析更新 设备通道实时温度
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        public abstract void DataAnalyse_ChannelRealTemper(byte[] recBytes);

        /// <summary>
        /// 解析更新 设备通道温升
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        public abstract void DataAnalyse_ChannelRiseTemper(byte[] recBytes);

        /// <summary>
        /// 解析分区参数
        /// </summary>
        public abstract void DataAnalyse_PartitionParam(byte[] recBytes);
    }

    public class ModBusTcpDataAnalysis : DataAnalysis
    {

        /// <summary>
        /// 解析更新 设备状态
        /// </summary>
        /// <param name="bytesDeviceStatus"></param>
        public override void DataAnalyse_DeviceStatus(byte[] recBytes)
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
        public override void DataAnalyse_DeviceParam(byte[] recBytes)
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
        public override void DataAnalyse_DeviceChannelParam(byte[] recBytes)
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
        public override void DataAnalyse_ChannelAverageTemper(byte[] recBytes)
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
        public override void DataAnalyse_ChannelRealTemper(byte[] recBytes)
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
        public override void DataAnalyse_ChannelRiseTemper(byte[] recBytes)
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
        public override void DataAnalyse_PartitionParam(byte[] recBytes)
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

