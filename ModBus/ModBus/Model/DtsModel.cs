using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ModBus.Model
{
    #region// 保持寄存器
    /// <summary>
    /// 通道配置信息
    /// </summary>
    public class ChannelSettingInfo
    {
        /// <summary>
        /// 通道测量时间
        /// </summary>
        public int Time { get; set; }
        /// <summary>
        /// 设定温度间隔
        /// </summary>
        public int TempInterval { get; set; }
        /// <summary>
        /// 设定温度精度
        /// </summary>
        public int Accuracy { get; set; }
    }

    /// <summary>
    /// 分区配置信息
    /// </summary>
    public class Part_SettingInfo
    {
        /// <summary>
        /// 分区开始点位
        /// </summary>
        public int StartPos { get; set; }
        /// <summary>
        /// 分区结束点位
        /// </summary>
        public int EndPos { get; set; }
        /// <summary>
        /// 定温阈值
        /// </summary>
        public int DingWen { get; set; }
        /// <summary>
        /// 升温阈值
        /// </summary>
        public int WenSheng { get; set; }
        /// <summary>
        /// 差温阈值
        /// </summary>
        public int ChaWen { get; set; }
    }

    #endregion

    #region//输入寄存器

    /// <summary>
    /// 通道基本信息
    /// </summary>
    public class Channel_BaseInfo
    {
        /// <summary>
        /// 通道号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 分区数
        /// </summary>
        public int PartCount { get; set; }
        /// <summary>
        /// 断纤报警状态
        /// </summary>
        public int IsBrokenAlarm { get; set; }
        /// <summary>
        /// 定温报警数量
        /// </summary>
        public int DingWenAlarmCount { get; set; }
        /// <summary>
        /// 温升报警数量
        /// </summary>
        public int WenShengAlarmCount { get; set; }
        /// <summary>
        /// 差温报警数量
        /// </summary>
        public int ChaWenAlarmCount { get; set; }
    }

    /// <summary>
    /// 通道采集信息
    /// </summary>
    public struct Channel_CollectionsInfo
    {
        /// <summary>
        /// 温度点间距
        /// </summary>
        public short TempPosInterval { get; set; }
        /// <summary>
        /// 温度点数量
        /// </summary>
        public short TempPosCount { get; set; }
        /// <summary>
        /// 温度刷新 年 月
        /// </summary>
        public short Refresh_Year_Month { get; set; }
        /// <summary>
        /// 温度刷新 日 时
        /// </summary>
        public short Refresh_Day_Hour { get; set; }
        /// <summary>
        /// 温度刷新 分 秒
        /// </summary>
        public short Refresh_Min_Sec { get; set; }
    }

    /// <summary>
    /// 通道 断纤信息
    /// </summary>

    public class Channel_BrokenInfo
    {
        string YM;
        string DH;
        string MS;
        int broken_Year_Month;
        int broken_Day_Hour;
        int broken_Min_Sec;
        DateTime alarmTime;

        /// <summary>
        /// 断纤位置
        /// </summary>
        public int BrokenPos { get; set; }

        /// <summary>
        /// 断纤 年 月
        /// </summary>
        public int Broken_Year_Month
        {
            get => broken_Year_Month;
            set
            {
                broken_Year_Month = value;
                YM = 2000 + (Broken_Year_Month >> 8) + "/" + (Broken_Year_Month & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }
        /// <summary>
        /// 断纤 日 时
        /// </summary>
        public int Broken_Day_Hour
        {
            get => broken_Day_Hour;
            set
            {
                broken_Day_Hour = value;
                YM = 2000 + (Broken_Day_Hour >> 8) + "/" + (Broken_Day_Hour & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }
        /// <summary>
        /// 断纤 分 秒
        /// </summary>
        public int Broken_Min_Sec
        {
            get => broken_Min_Sec;
            set
            {
                broken_Min_Sec = value;
                YM = 2000 + (Broken_Min_Sec >> 8) + "/" + (Broken_Min_Sec & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }



        public DateTime AlarmTime { get => alarmTime; set => alarmTime = value; }
    }

    /// <summary>
    /// 设备故障信息
    /// </summary>

    public class Device_FaultInfo
    {
        /// <summary>
        /// 采集模块通讯故障
        /// </summary>
        public int IsCommunctionFault { get; set; }

        /// <summary>
        /// 主电故障
        /// </summary>
        public int IsMainPowerFault { get; set; }

        /// <summary>
        /// 备电故障
        /// </summary>
        public int IsBackUpPowerFault { get; set; }

        /// <summary>
        /// 备电故障
        /// </summary>
        public int IsChargeFault { get; set; }

        /// <summary>
        /// 通讯故障
        /// </summary>
        public int IsNetCommunFault { get; set; }
    }


    public class Part_TempInfo
    {
        /// <summary>
        /// 分区报警状态 0 正常 1bit高温报警 2bit差温报警 3bit区域差温报警
        /// </summary>
        public int AlarmState { get; set; }
        /// <summary>
        /// 分区最高温
        /// </summary>
        public int MaxTemp { get; set; }
        /// <summary>
        /// 平均温度
        /// </summary>
        public int AverageTemp { get; set; }
        /// <summary>
        /// 最低温度
        /// </summary>
        public int MinTemp { get; set; }
        /// <summary>
        /// 最高温点位
        /// </summary>
        public int MaxTempPos { get; set; }
        /// <summary>
        /// 最低温点位
        /// </summary>
        public int MinTempPos { get; set; }
    }

    /// <summary>
    /// 定温报警信息
    /// </summary>
    public class Channel_DingWenAlarmInfo
    {
        string YM;
        string DH;
        string MS;
        int alarm_Year_Month;
        int alarm_Day_Hour;
        int alarm_Min_Sec;
        DateTime alarmTime;
        /// <summary>
        /// 起点位置
        /// </summary>
        public int StartPos { get; set; }
        /// <summary>
        /// 终点位置
        /// </summary>
        public int EndPos { get; set; }
        /// <summary>
        /// 年 月
        /// </summary>
        public int Alarm_Year_Month
        {
            get => alarm_Year_Month;
            set
            {
                alarm_Year_Month = value;
                YM = 2000 + (Alarm_Year_Month >> 8) + "/" + (Alarm_Year_Month & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }
        /// <summary>
        /// 日 时
        /// </summary>
        public int Alarm_Day_Hour
        {
            get => alarm_Day_Hour; set
            {
                alarm_Day_Hour = value;
                DH = "/" + (Alarm_Day_Hour >> 8) + " " + (Alarm_Day_Hour & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }
        /// <summary>
        /// 分 秒
        /// </summary>
        public int Alarm_Min_Sec
        {
            get => alarm_Min_Sec; set
            {
                alarm_Min_Sec = value;
                MS = ":" + (Alarm_Min_Sec >> 8) + ":" + (Alarm_Min_Sec & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }

        /// <summary>
        /// 分区号
        /// </summary>
        public int PartId { get; set; }

        public DateTime AlarmTime { get => alarmTime; set => alarmTime = value; }

    }


    /// <summary>
    /// 温升报警信息
    /// </summary>
    public class Channel_WenShengAlarmInfo
    {
        string YM;
        string DH;
        string MS;
        int alarm_Year_Month;
        int alarm_Day_Hour;
        int alarm_Min_Sec;
        DateTime alarmTime;
        /// <summary>
        /// 起点位置
        /// </summary>
        public int StartPos { get; set; }
        /// <summary>
        /// 终点位置
        /// </summary>
        public int EndPos { get; set; }
        /// <summary>
        /// 年 月
        /// </summary>
        public int Alarm_Year_Month
        {
            get => alarm_Year_Month;
            set
            {
                alarm_Year_Month = value;
                YM = 2000 + (Alarm_Year_Month >> 8) + "/" + (Alarm_Year_Month & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }
        /// <summary>
        /// 日 时
        /// </summary>
        public int Alarm_Day_Hour
        {
            get => alarm_Day_Hour; set
            {
                alarm_Day_Hour = value;
                DH = "/" + (Alarm_Day_Hour >> 8) + " " + (Alarm_Day_Hour & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }
        /// <summary>
        /// 分 秒
        /// </summary>
        public int Alarm_Min_Sec
        {
            get => alarm_Min_Sec; set
            {
                alarm_Min_Sec = value;
                MS = ":" + (Alarm_Min_Sec >> 8) + ":" + (Alarm_Min_Sec & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }

        /// <summary>
        /// 分区号
        /// </summary>
        public int PartId { get; set; }

        public DateTime AlarmTime { get => alarmTime; set => alarmTime = value; }
    }

    /// <summary>
    /// 差温报警信息
    /// </summary>
    public class Channel_ChaWenAlarmInfo
    {
        string YM;
        string DH;
        string MS;
        int alarm_Year_Month;
        int alarm_Day_Hour;
        int alarm_Min_Sec;
        DateTime alarmTime;
        /// <summary>
        /// 起点位置
        /// </summary>
        public int StartPos { get; set; }
        /// <summary>
        /// 终点位置
        /// </summary>
        public int EndPos { get; set; }
        /// <summary>
        /// 年 月
        /// </summary>
        public int Alarm_Year_Month
        {
            get => alarm_Year_Month;
            set
            {
                alarm_Year_Month = value;
                YM = 2000 + (Alarm_Year_Month >> 8) + "/" + (Alarm_Year_Month & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }
        /// <summary>
        /// 日 时
        /// </summary>
        public int Alarm_Day_Hour
        {
            get => alarm_Day_Hour; set
            {
                alarm_Day_Hour = value;
                DH = "/" + (Alarm_Day_Hour >> 8) + " " + (Alarm_Day_Hour & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }
        /// <summary>
        /// 分 秒
        /// </summary>
        public int Alarm_Min_Sec
        {
            get => alarm_Min_Sec; set
            {
                alarm_Min_Sec = value;
                MS = ":" + (Alarm_Min_Sec >> 8) + ":" + (Alarm_Min_Sec & 0xff);
                DateTime.TryParse(YM + DH + MS, out alarmTime);
            }
        }

        /// <summary>
        /// 分区号
        /// </summary>
        public int PartId { get; set; }

        public DateTime AlarmTime { get => alarmTime; set => alarmTime = value; }
    }

    #endregion


}
