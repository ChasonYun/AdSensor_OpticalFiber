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
        public struct DeviceIsBroken
        {
            public ChannelIsBroken[] channelIsBrokens;
        }
        public struct ChannelIsBroken//断纤状态结构体
        {
            public DateTime brokenTime;
            public bool isbroken;
            public int brokenposition;
            public int threshold;
        }
        public class IsBroken
        {
            public DeviceIsBroken[] deviceIsBrokens = new DeviceIsBroken[9];
            public  IsBroken()
            {
                ChannelIsBroken[] channelIsBrokens;
                for(int i = 0; i < 9; i++)
                {
                    channelIsBrokens = new ChannelIsBroken[5];
                    deviceIsBrokens[i].channelIsBrokens = channelIsBrokens;
                }
            }
           
        }

        public struct DeviceAlarm
        {
            public ChannelAlarm[] channelAlarms;//4个
        }
        public struct ChannelAlarm
        {
            public PartitionAlarm[] partitionAlarms;//50个
        }

        public struct PartitionAlarm
        {
            public bool isFireAlarm;
            public int fireAlarmPosition;
            public DateTime fireAlarmTime;

            public bool isRiseAlarm;
            public int riseAlarnPosition;
            public DateTime riseAlarmTime;

            public int relayNo;
            public double maxRiseValue;
            public double maxRealValue;
            public double riseThreshold;
            public double realThreshold;
        }
        public class IsAlarm
        {
            public DeviceAlarm[] deviceAlarms = new DeviceAlarm[9];
            private ChannelAlarm[] channelAlarms;
            public IsAlarm()
            {
                PartitionAlarm[] partitionAlarms;
                for(int i = 0; i < 9; i++)
                {
                    channelAlarms = new ChannelAlarm[5];
                    for (int j = 0; j < 5; j++)
                    {
                        partitionAlarms = new PartitionAlarm[51];
                        channelAlarms[j].partitionAlarms = partitionAlarms;
                    }
                    deviceAlarms[i].channelAlarms = channelAlarms;
                }
            }
        }
        static Thread thread;
        public static IsBroken isBroken = new IsBroken();//八台设备 32个通道断纤状态
        public static IsAlarm isAlarm = new IsAlarm();
        static AlarmStatus()
        {
            thread = new Thread(CheckAlarmStatus);
            Thread.Sleep(500);
            thread.Start();
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
        private static void CheckAlarmStatus()
        {
            try
            {
                while (true)
                {
                    //Stopwatch stopwatch = new Stopwatch();
                    //stopwatch.Start();
                    for (int i = 1; i <= 8; i++)//八台设备
                    {
                        for(int j = 1; j <= 4; j++)//四个通道
                        {
                            isBroken.deviceIsBrokens[i].channelIsBrokens[j].isbroken = Convert.ToBoolean(DataClass.list_DeviceChannelParam[i].struct_DeviceChannelParam.struct_ChannelParams[j].isBroken);
                            isBroken.deviceIsBrokens[i].channelIsBrokens[j].brokenposition = DataClass.list_DeviceChannelParam[i].struct_DeviceChannelParam.struct_ChannelParams[j].brokenPoint;
                            isBroken.deviceIsBrokens[i].channelIsBrokens[j].threshold = DataClass.list_DeviceChannelParam[i].struct_DeviceChannelParam.struct_ChannelParams[j].length;
                            year = DataClass.list_DeviceParam[i].struct_deivceParam.year;
                            month = DataClass.list_DeviceParam[i].struct_deivceParam.month;
                            day = DataClass.list_DeviceParam[i].struct_deivceParam.day;
                            hour = DataClass.list_DeviceParam[i].struct_deivceParam.hour;
                            minute = DataClass.list_DeviceParam[i].struct_deivceParam.minute;
                            second = DataClass.list_DeviceParam[i].struct_deivceParam.second;
                            DateTime.TryParse(year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second, out isBroken.deviceIsBrokens[i].channelIsBrokens[j].brokenTime);
                            for (int k = 1; k < 51; k++)
                            {
                                //if (Convert.ToBoolean(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmStatus))
                                //{
                                    isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].isFireAlarm = Convert.ToBoolean(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmStatus);
                                    isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].realThreshold = (double)DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmThreshold;
                                    isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].fireAlarmPosition = DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmPosition;
                                    year_month = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmYear_Month);
                                    day_hour = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmDay_Hour);
                                    minute_second = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmMinute_Second);
                                    YM = 2000 + (year_month >> 8) + "/" + (year_month & 0xff);
                                    DH = "/" + (day_hour >> 8) + " " + (day_hour & 0xff);
                                    MS = ":" + (minute_second >> 8) + ":" + (minute_second & 0xff);
                                    DateTime.TryParse(YM + DH + MS, out isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].fireAlarmTime);
                                    if (isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].fireAlarmPosition > 0)
                                    {
                                        //isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].maxRealValue = (double)DataClass.list_DeviceTemper[i].channelTempers[j].realTemper[isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].fireAlarmPosition];
                                        isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].maxRealValue = (double)DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].maxTemper;

                                    }
                                //}
                                //if (Convert.ToBoolean(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmStaus))
                                //{
                                    isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].isRiseAlarm = Convert.ToBoolean(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmStaus);
                                    isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].riseThreshold = (double)DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmThreshold;
                                    isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].riseAlarnPosition = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmPosition);
                                    year_month = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmYear_Month);
                                    day_hour = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmDay_Hour);
                                    minute_second = Convert.ToInt32(DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmMinute_Second);
                                    YM = 2000 + (year_month >> 8) + "/" + (year_month & 0xff);
                                    DH = "/" + (day_hour >> 8) + " " + (day_hour & 0xff);
                                    MS = ":" + (minute_second >> 8) + ":" + (minute_second & 0xff);
                                    DateTime.TryParse(YM + DH + MS, out isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].riseAlarmTime);
                                    if (isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].riseAlarnPosition > 0)
                                    {
                                        //isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].maxRiseValue = (double)DataClass.list_DeviceTemper[i].channelTempers[j].riseTemper[isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].riseAlarnPosition];
                                        isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].maxRiseValue = (double)DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].maxRise;
                                    }
                                //}
                                isAlarm.deviceAlarms[i].channelAlarms[j].partitionAlarms[k].relayNo = DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].relayNumber;
                            }
                        }
                        //stopwatch.Stop();
                        //string time = stopwatch.ElapsedMilliseconds.ToString();
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("报警信息巡检状态异常！——"+ex.Message);
            }
        }

        public static void Reset()
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    DataClass.list_DeviceChannelParam[i].struct_DeviceChannelParam.struct_ChannelParams[j].isBroken = 0;//通道断纤故障
                    for (int k = 1; k <= 50; k++)
                    {
                        DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].fireAlarmStatus = 0;//分区火警
                        DataClass.list_DevicePartition[i].struct_devicePartition.struct_ChannelPartitions[j].struct_Partitions[k].riseAlarmStaus = 0;//分区升警
                    }
                }
            }
        }
    }
}
