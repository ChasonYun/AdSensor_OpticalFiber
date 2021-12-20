using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticalFiber
{
    public partial class UCLines : UserControl
    {
        public UCLines(struct_TvwMsg struct_tvwMsg)
        {
            InitializeComponent();
            this.struct_tvwMsg = struct_tvwMsg;
            GetParam();
        }

        private struct_TvwMsg struct_tvwMsg;
        string title;

        private void GetParam()
        {
            tlp.Controls.Clear();
            tlp.Dock = DockStyle.Fill;
            if (struct_tvwMsg.channelNo == 0 && struct_tvwMsg.partitionNo == 0)//这是一个设备的信息  展示通道数 对应的曲线信息
            {
                int channelNo = DataClass.list_DeviceParam[struct_tvwMsg.deviceNo].struct_deivceParam.channelNum;
                if (channelNo == 1)
                {
                    tlp.RowCount = 1;
                    tlp.ColumnCount = 1;
                }
                else if (channelNo == 2)
                {
                    tlp.RowCount = 1;
                    tlp.ColumnCount = 2;
                }
                else
                {
                    tlp.RowCount = 2;
                    tlp.ColumnCount = 2;
                }
                for(int i = 1; i <= channelNo; i++)
                {
                    foreach (struct_DeviceEnable _DeviceEnable in DataClass.list_DeviceEnables)
                    {
                        if (_DeviceEnable.deviceNo == struct_tvwMsg.deviceNo)
                        {
                            title = _DeviceEnable.name + "—通道" + i;
                        }
                    }
                    //title = "设备" + struct_tvwMsg.deviceNo + "—通道" + i;
                    UCChart ucChart = new UCChart(struct_tvwMsg.deviceNo, i, 0, title);
                    ucChart.Dock = DockStyle.Fill;
                    tlp.Controls.Add(ucChart);
                }
            }
            else if(struct_tvwMsg.partitionNo == 0)//一个通道
            {
                foreach (struct_DeviceEnable _DeviceEnable in DataClass.list_DeviceEnables)
                {
                    if (_DeviceEnable.deviceNo == struct_tvwMsg.deviceNo)
                    {
                        title = _DeviceEnable.name + "—通道" + struct_tvwMsg.channelNo;
                    }
                }
                //title = "设备" + struct_tvwMsg.deviceNo + "—通道" + struct_tvwMsg.channelNo;
                tlp.RowCount = 1;
                tlp.ColumnCount = 1;
                UCChart ucChart = new UCChart(struct_tvwMsg.deviceNo, struct_tvwMsg.channelNo, 0, title);
                ucChart.Dock = DockStyle.Fill;
                tlp.Controls.Add(ucChart);
            }
            else//这是一个分区
            {
                foreach (struct_DeviceEnable _DeviceEnable in DataClass.list_DeviceEnables)
                {
                    if (_DeviceEnable.deviceNo == struct_tvwMsg.deviceNo)
                    {
                        title = _DeviceEnable.name + "—通道" + struct_tvwMsg.channelNo + "—" + struct_tvwMsg.treeNodeName;
                    }
                }
                //title = "设备" + struct_tvwMsg.deviceNo + "—通道" + struct_tvwMsg.channelNo + "—" + struct_tvwMsg.treeNodeName;
                tlp.RowCount = 1;
                tlp.ColumnCount = 1;
                UCChart ucChart = new UCChart(struct_tvwMsg.deviceNo, struct_tvwMsg.channelNo, struct_tvwMsg.partitionNo, title);
                ucChart.Dock = DockStyle.Fill;
                tlp.Controls.Add(ucChart);
            }
        }
    }
}
