using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBus.Model
{
    /// <summary>
    /// 光纤 通道 参数 一个通道 对应多个分区
    /// </summary>
    public class DtsChannelDataModel
    {
        int partCount = 0;
        public ChannelSettingInfo ChannelSettingInfo { get; set; }
        public Channel_BaseInfo Channel_BaseInfo { get; set; }
        public Channel_CollectionsInfo Channel_CollectionsInfo { get; set; }
        public Channel_BrokenInfo Channel_BrokenInfo { get; set; }
        public List<DtsPartDataModel> DtsPartDataModels { get; set; }
        public List<Channel_DingWenAlarmInfo> Channel_DingWenAlarmInfos { get; set; }
        public List<Channel_ChaWenAlarmInfo> Channel_ChaWenAlarmInfos { get; set; }
        public List<Channel_WenShengAlarmInfo> Channel_WenShengAlarmInfos { get; set; }
        public List<int> Channel_Temps { get; set; }
        public DtsChannelDataModel()
        {
            ChannelSettingInfo = new ChannelSettingInfo();
            Channel_BaseInfo = new Channel_BaseInfo();
            Channel_CollectionsInfo = new Channel_CollectionsInfo();
            Channel_BrokenInfo = new Channel_BrokenInfo();
            DtsPartDataModels = new List<DtsPartDataModel>();
            Channel_DingWenAlarmInfos = new List<Channel_DingWenAlarmInfo>();
            Channel_ChaWenAlarmInfos = new List<Channel_ChaWenAlarmInfo>();
            Channel_WenShengAlarmInfos = new List<Channel_WenShengAlarmInfo>();
            Channel_Temps = new List<int>();
            for (int i = 0; i < 800; i++)//800个 分区
            {
                DtsPartDataModels.Add(new DtsPartDataModel());
            }
            for (int i = 0; i < 200; i++)//200 个报警 定温 差温 温升
            {
                Channel_DingWenAlarmInfos.Add(new Channel_DingWenAlarmInfo());
                Channel_ChaWenAlarmInfos.Add(new Channel_ChaWenAlarmInfo());
                Channel_WenShengAlarmInfos.Add(new Channel_WenShengAlarmInfo());
            }
            for (int i = 0; i < 44536; i++)//44536 个温度点
            {
                Channel_Temps.Add(0);
            }

        }

    }
}
