using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBus.Model
{
    /// <summary>
    /// 分区 参数 
    /// </summary>
    public class DtsPartDataModel
    {
        public Part_SettingInfo Part_SettingInfo { get; set; }
        public Part_TempInfo Part_TempInfo { get; set; }

        public DtsPartDataModel()
        {
            Part_SettingInfo = new Part_SettingInfo();
            Part_TempInfo = new Part_TempInfo();
        }
    }
}
