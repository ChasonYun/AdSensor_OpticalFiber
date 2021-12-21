using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpticalFiber
{
    static class AlarmStatus
    {
        static AlarmStatus()
        {

            //Task.Factory.StartNew(() =>
            //{
            //    CheckAlarmStatus();

            //});
        }

        static int year;
        static int month;
        static int day;
        static int hour;
        static int minute;
        static int second;
        static int year_month;
        static int day_hour;
        static int minute_second;
        static string YM;
        static string DH;
        static string MS;
        //private static void CheckAlarmStatus()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            //Stopwatch stopwatch = new Stopwatch();
        //            //stopwatch.Start();
        //            for (int i = 0; i < 8; i++)//八台设备
        //            {
        //                for (int j = 0; j < 8; j++)//八个通道
        //                {
                           
        //                    year = DataClass.list_DeviceParam[i].struct_deivceParam.year;
        //                    month = DataClass.list_DeviceParam[i].struct_deivceParam.month;
        //                    day = DataClass.list_DeviceParam[i].struct_deivceParam.day;
        //                    hour = DataClass.list_DeviceParam[i].struct_deivceParam.hour;
        //                    minute = DataClass.list_DeviceParam[i].struct_deivceParam.minute;
        //                    second = DataClass.list_DeviceParam[i].struct_deivceParam.second;
        //                    DateTime.TryParse(year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second, out isBroken.deviceIsBrokens[i].channelIsBrokens[j].brokenTime);
        //                    for (int k = 0; k < 800; k++)
        //                    {
        //                        //if (Convert.ToBoolean(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmStatus))
        //                        //{
        //                        isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].isFireAlarm = Convert.ToBoolean(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmStatus);
        //                        isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].realThreshold = (double)DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmThreshold;
        //                        isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].fireAlarmPosition = DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmPosition;
        //                        year_month = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmYear_Month);
        //                        day_hour = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmDay_Hour);
        //                        minute_second = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmMinute_Second);
        //                        YM = 2000 + (year_month >> 8) + "/" + (year_month & 0xff);
        //                        DH = "/" + (day_hour >> 8) + " " + (day_hour & 0xff);
        //                        MS = ":" + (minute_second >> 8) + ":" + (minute_second & 0xff);
        //                        DateTime.TryParse(YM + DH + MS, out isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].fireAlarmTime);
        //                        if (isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].fireAlarmPosition > 0)
        //                        {
        //                            //isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].maxRealValue = (double)DataClass.list_DeviceTemper[i].channelTempers[j].realTemper[isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].fireAlarmPosition];
        //                            isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].maxRealValue = (double)DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].maxTemper;

        //                        }
        //                        //}
        //                        //if (Convert.ToBoolean(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmStaus))
        //                        //{
        //                        isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].isRiseAlarm = Convert.ToBoolean(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmStaus);
        //                        isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].riseThreshold = (double)DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmThreshold;
        //                        isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].riseAlarnPosition = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmPosition);
        //                        year_month = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmYear_Month);
        //                        day_hour = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmDay_Hour);
        //                        minute_second = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmMinute_Second);
        //                        YM = 2000 + (year_month >> 8) + "/" + (year_month & 0xff);
        //                        DH = "/" + (day_hour >> 8) + " " + (day_hour & 0xff);
        //                        MS = ":" + (minute_second >> 8) + ":" + (minute_second & 0xff);
        //                        DateTime.TryParse(YM + DH + MS, out isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].riseAlarmTime);
        //                        if (isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].riseAlarnPosition > 0)
        //                        {
        //                            //isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].maxRiseValue = (double)DataClass.list_DeviceTemper[i].channelTempers[j].riseTemper[isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].riseAlarnPosition];
        //                            isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].maxRiseValue = (double)DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].maxRise;
        //                        }
        //                        //}
        //                        isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].relayNo = DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].relayNumber;
        //                    }
        //                }
        //                //stopwatch.Stop();
        //                //string time = stopwatch.ElapsedMilliseconds.ToString();
        //                Thread.Sleep(100);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DataClass.ShowErrMsg("报警信息巡检状态异常！——" + ex.Message);
        //    }
        //}

        public static void Reset()
        {
            string response = string.Empty;
            DataClass.list_DeviceEnables = (new SQL_Select()).Select_DeviceEnable();
            struct_DeviceEnable _Device;
            for (int i = 0; i < 8; i++)
            {
                _Device = DataClass.list_DeviceEnables[i];
                if (_Device.enable)
                {
                    ModBusService.Instance().dtsModBuses[i].Reset(out response);
                }
            }
        }
    }
}
