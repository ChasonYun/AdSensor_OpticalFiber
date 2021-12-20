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
    public struct ChannelSettingInfo
    {
        /// <summary>
        /// 通道测量时间
        /// </summary>
        public short Time { get; set; }
        /// <summary>
        /// 设定温度间隔
        /// </summary>
        public short TempInterval { get; set; }
        /// <summary>
        /// 设定温度精度
        /// </summary>
        public short Accuracy { get; set; }
    }

    /// <summary>
    /// 分区配置信息
    /// </summary>
    public struct Part_SettingInfo
    {
        /// <summary>
        /// 分区开始点位
        /// </summary>
        public short StartPos { get; set; }
        /// <summary>
        /// 分区结束点位
        /// </summary>
        public short EndPos { get; set; }
        /// <summary>
        /// 定温阈值
        /// </summary>
        public short DingWen { get; set; }
        /// <summary>
        /// 升温阈值
        /// </summary>
        public short WenSheng { get; set; }
        /// <summary>
        /// 差温阈值
        /// </summary>
        public short ChaWen { get; set; }
    }

    #endregion

    #region//输入寄存器

    /// <summary>
    /// 通道基本信息
    /// </summary>
    public struct Channel_BaseInfo
    {
        /// <summary>
        /// 通道号
        /// </summary>
        public short Id { get; set; }
        /// <summary>
        /// 分区数
        /// </summary>
        public short PartCount { get; set; }
        /// <summary>
        /// 断纤报警状态
        /// </summary>
        public short IsBrokenAlarm { get; set; }
        /// <summary>
        /// 定温报警数量
        /// </summary>
        public short DingWenAlarmCount { get; set; }
        /// <summary>
        /// 温升报警数量
        /// </summary>
        public short WenShengAlarmCount { get; set; }
        /// <summary>
        /// 差温报警数量
        /// </summary>
        public short ChaWenAlarmCount { get; set; }
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

    public struct Channel_BrokenInfo
    {
        /// <summary>
        /// 断纤位置
        /// </summary>
        public short BrokenPos { get; set; }

        /// <summary>
        /// 断纤 年 月
        /// </summary>
        public short Broken_Year_Month { get; set; }
        /// <summary>
        /// 断纤 日 时
        /// </summary>
        public short Broken_Day_Hour { get; set; }
        /// <summary>
        /// 断纤 分 秒
        /// </summary>
        public short Broken_Min_Sec { get; set; }
    }

    /// <summary>
    /// 设备故障信息
    /// </summary>

    public struct Device_FaultInfo
    {
        /// <summary>
        /// 采集模块通讯故障
        /// </summary>
        public short IsCommunctionFault { get; set; }

        /// <summary>
        /// 主电故障
        /// </summary>
        public short IsMainPowerFault { get; set; }

        /// <summary>
        /// 备电故障
        /// </summary>
        public short IsBackUpPowerFault { get; set; }

        /// <summary>
        /// 备电故障
        /// </summary>
        public short IsChargeFault { get; set; }
    }


    public struct Channel_TempInfo
    {
        /// <summary>
        /// 分区报警状态 0 正常 1bit高温报警 2bit差温报警 3bit区域差温报警
        /// </summary>
        public short AlarmState { get; set; }
        /// <summary>
        /// 分区最高温
        /// </summary>
        public short MaxTemp { get; set; }
        /// <summary>
        /// 平均温度
        /// </summary>
        public short AverageTemp { get; set; }
        /// <summary>
        /// 最低温度
        /// </summary>
        public short MinTemp { get; set; }
        /// <summary>
        /// 最高温点位
        /// </summary>
        public short MaxTempPos { get; set; }
        /// <summary>
        /// 最低温点位
        /// </summary>
        public short MinTempPos { get; set; }
    }

    /// <summary>
    /// 定温报警信息
    /// </summary>
    public struct Channel_DingWenAlarmInfo
    {
        /// <summary>
        /// 起点位置
        /// </summary>
        public short StartPos { get; set; }
        /// <summary>
        /// 终点位置
        /// </summary>
        public short EndPos { get; set; }
        /// <summary>
        /// 年 月
        /// </summary>
        public short Alarm_Year_Month { get; set; }
        /// <summary>
        /// 日 时
        /// </summary>
        public short Alarm_Day_Hour { get; set; }
        /// <summary>
        /// 分 秒
        /// </summary>
        public short Alarm_Min_Sec { get; set; }

        /// <summary>
        /// 分区号
        /// </summary>
        public short ChannelId { get; set; }
    }


    /// <summary>
    /// 温升报警信息
    /// </summary>
    public struct Channel_WenShengAlarmInfo
    {
        /// <summary>
        /// 起点位置
        /// </summary>
        public short StartPos { get; set; }
        /// <summary>
        /// 终点位置
        /// </summary>
        public short EndPos { get; set; }
        /// <summary>
        /// 年 月
        /// </summary>
        public short Alarm_Year_Month { get; set; }
        /// <summary>
        /// 日 时
        /// </summary>
        public short Alarm_Day_Hour { get; set; }
        /// <summary>
        /// 分 秒
        /// </summary>
        public short Alarm_Min_Sec { get; set; }
        /// <summary>
        /// 分区号
        /// </summary>
        public short ChannelId { get; set; }
    }

    /// <summary>
    /// 差温报警信息
    /// </summary>
    public struct Channel_ChaWenAlarmInfo
    {
        /// <summary>
        /// 起点位置
        /// </summary>
        public short StartPos { get; set; }
        /// <summary>
        /// 终点位置
        /// </summary>
        public short EndPos { get; set; }
        /// <summary>
        /// 年 月
        /// </summary>
        public short Alarm_Year_Month { get; set; }
        /// <summary>
        /// 日 时
        /// </summary>
        public short Alarm_Day_Hour { get; set; }
        /// <summary>
        /// 分 秒
        /// </summary>
        public short Alarm_Min_Sec { get; set; }
        /// <summary>
        /// 分区号
        /// </summary>
        public short ChannelId { get; set; }
    }

    #endregion

    public class DtsChannleDataModel
    {

        Channel_BaseInfo channel_BaseInfo;
        Channel_CollectionsInfo channel_CollectionsInfo;
        public DtsChannleDataModel()
        {
            channel_BaseInfo = new Channel_BaseInfo();
            channel_CollectionsInfo = new Channel_CollectionsInfo();

        }
    }
}
