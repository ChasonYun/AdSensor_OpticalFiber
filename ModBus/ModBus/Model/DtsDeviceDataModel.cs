using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBus.Model
{
    /// <summary>
    /// 设备 信息
    /// </summary>
    public class DtsDeviceDataModel
    {
        public Device_FaultInfo Device_FaultInfo { get; set; }
        public ChannelSettingInfo ChannelSettingInfo { get; set; }
        public List<DtsChannelDataModel> DtsChannelDataModels { get; set; }
        public DtsDeviceDataModel()
        {
            Device_FaultInfo = new Device_FaultInfo();
            ChannelSettingInfo = new ChannelSettingInfo();
            DtsChannelDataModels = new List<DtsChannelDataModel>();
            for (int i = 0; i < 8; i++)//8个通道
            {
                DtsChannelDataModels.Add(new DtsChannelDataModel());
            }
        }
    }
}
