using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBus.Model
{
    /// <summary>
    /// 光纤 通道 参数
    /// </summary>
    public class DtsChannelDataModel
    {

        Channel_BaseInfo channel_BaseInfo;
        Channel_CollectionsInfo channel_CollectionsInfo;
        Channel_BrokenInfo channel_BrokenInfo;
        public DtsChannelDataModel()
        {
            channel_BaseInfo = new Channel_BaseInfo();
            channel_CollectionsInfo = new Channel_CollectionsInfo();
            channel_BrokenInfo = new Channel_BrokenInfo();
        }
    }
}
