using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace OpticalFiber
{
    public partial class UCTreeView : UserControl
    {

        List<struct_PrtName> struct_PrtNames;


        /// <summary>
        /// 结构体树视图 用于绘制 树视图
        /// </summary>
        struct struct_Tvw
        {
            public struct_TvwDevice[] struct_TvwDevices;
        }

        /// <summary>
        /// 根据启用的设备数  去获取  该设备的 通道数组
        /// </summary>
        struct struct_TvwDevice
        {
            public int deviceNo;
            public struct_TvwChannel[] struct_TvwChannels;
        }

        /// <summary>
        /// 根据通道号  获取  分区数组
        /// </summary>
        struct struct_TvwChannel
        {
            public int channelNo;
            public int partitions;
        }





        List<struct_DeviceEnable> struct_DeviceEnables;
        struct_DeviceEnable struct_DeviceEnable;

        List<int> list_EnableDeviceNo = new List<int>();




        struct_Tvw struct_tvw;

        public UCTreeView()
        {
            InitializeComponent();

            Init();

            struct_PrtNames = (new SQL_Select()).Select_PrtName();

            BuildTvw(struct_tvw);
        }

        private void Init()
        {
            try
            {
                //struct_DeviceEnables = ConfigClass.GetStruct_DeviceEnables();
                struct_DeviceEnables = (new SQL_Select()).Select_DeviceEnable();
                foreach (struct_DeviceEnable _DeviceEnable in struct_DeviceEnables)
                {
                    if (_DeviceEnable.enable)
                    {
                        list_EnableDeviceNo.Add(_DeviceEnable.deviceNo);
                    }
                }
                struct_TvwDevice[] struct_TvwDevices = new struct_TvwDevice[list_EnableDeviceNo.Count];//设备的 


                Thread.Sleep(500);
                for (int j = 0; j < list_EnableDeviceNo.Count; j++)
                {
                    int channelNum = DataClass.list_DeviceEnables[list_EnableDeviceNo[j] - 1].channelCount;
                    struct_TvwChannel[] struct_TvwChannels = new struct_TvwChannel[channelNum + 1];//创建通道数组


                    for (int k = 0; k < channelNum; k++)
                    {
                        int partitionNo = ModBusService.Instance().dtsModBuses[list_EnableDeviceNo[j] - 1].dtsDeviceDataModel.DtsChannelDataModels[k].Channel_BaseInfo.PartCount;
                        struct_TvwChannels[k].channelNo = k;
                        struct_TvwChannels[k].partitions = partitionNo;//把分区数赋值给通道
                    }
                    struct_TvwDevices[j].deviceNo = list_EnableDeviceNo[j];
                    struct_TvwDevices[j].struct_TvwChannels = struct_TvwChannels;//把通道赋值给 设备
                }
                struct_tvw.struct_TvwDevices = struct_TvwDevices;//把设备赋值给 结构体
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("获取树视图结构体异常！——" + ex.Message);
            }

        }

        TreeNode treeNodeDevice;
        TreeNode treeNodeChannel;
        TreeNode treeNodePartition;
        struct_TvwMsg struct_tvwMsg;
        private void BuildTvw(struct_Tvw struct_tvw)
        {
            try
            {
                for (int i = 0; i < struct_tvw.struct_TvwDevices.Length; i++)
                {
                    treeNodeDevice = new TreeNode();//设备层
                    foreach (struct_DeviceEnable _DeviceEnable in DataClass.list_DeviceEnables)
                    {
                        if (_DeviceEnable.deviceNo == struct_tvw.struct_TvwDevices[i].deviceNo)
                        {
                            treeNodeDevice.Text = _DeviceEnable.name;
                        }
                    }
                    //treeNodeDevice.Text = "设备" + struct_tvw.struct_TvwDevices[i].deviceNo;
                    treeNodeDevice.ImageIndex = 0;
                    for (int j = 1; j < struct_tvw.struct_TvwDevices[i].struct_TvwChannels.Length; j++)
                    {
                        treeNodeChannel = new TreeNode();//通道层
                        treeNodeChannel.Text = "通道" + (struct_tvw.struct_TvwDevices[i].struct_TvwChannels[j].channelNo + 1);
                        treeNodeChannel.ImageIndex = 1;
                        for (int k = 1; k <= struct_tvw.struct_TvwDevices[i].struct_TvwChannels[j].partitions; k++)
                        {
                            treeNodePartition = new TreeNode();
                            foreach (struct_PrtName prtName in struct_PrtNames)
                            {
                                if (prtName.deviceNo == struct_tvw.struct_TvwDevices[i].deviceNo && prtName.channelNo == struct_tvw.struct_TvwDevices[i].struct_TvwChannels[j].channelNo && prtName.prtNo == k)
                                {
                                    treeNodePartition.Text = prtName.prtName;
                                }
                            }
                            treeNodePartition.ImageIndex = 2;

                            struct_tvwMsg.deviceNo = struct_tvw.struct_TvwDevices[i].deviceNo;//添加tag信息 
                            struct_tvwMsg.channelNo = struct_tvw.struct_TvwDevices[i].struct_TvwChannels[j].channelNo;
                            struct_tvwMsg.partitionNo = k;
                            struct_tvwMsg.treeNodeName = treeNodePartition.Text;
                            treeNodePartition.Tag = struct_tvwMsg;

                            treeNodeChannel.Nodes.Add(treeNodePartition);
                        }

                        struct_tvwMsg.deviceNo = struct_tvw.struct_TvwDevices[i].deviceNo;//添加tag信息 
                        struct_tvwMsg.channelNo = struct_tvw.struct_TvwDevices[i].struct_TvwChannels[j].channelNo;
                        struct_tvwMsg.partitionNo = 0;
                        struct_tvwMsg.treeNodeName = treeNodeChannel.Text;
                        treeNodeChannel.Tag = struct_tvwMsg;

                        treeNodeDevice.Nodes.Add(treeNodeChannel);
                    }

                    struct_tvwMsg.deviceNo = struct_tvw.struct_TvwDevices[i].deviceNo;//添加tag信息 
                    struct_tvwMsg.channelNo = 0;
                    struct_tvwMsg.partitionNo = 0;
                    struct_tvwMsg.treeNodeName = treeNodeDevice.Text;
                    treeNodeDevice.Tag = struct_tvwMsg;

                    tvw.Nodes.Add((TreeNode)treeNodeDevice.Clone());
                }
                tvw.ExpandAll();
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("构建树视图异常！——" + ex.Message);
            }
        }

        public event GetTvwMsg getTvwMsg;

        private void tvw_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void tvw_MouseDown(object sender, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                tvw.SelectedNode = tvw.GetNodeAt(point);
                if (tvw.SelectedNode == null)
                {
                    tvw.SelectedNode = null;
                }
                else
                {
                    getTvwMsg((struct_TvwMsg)tvw.SelectedNode.Tag);
                }
            }
        }
    }
    public delegate void GetTvwMsg(struct_TvwMsg struct_TvwMsg);
}
