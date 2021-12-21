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
    public partial class UCRunStatus : UserControl
    {
        private int deviceNo;
        public UCRunStatus(int deviceNo)
        {
            InitializeComponent();
            this.deviceNo = deviceNo;
            timerRunStatus.Start();
        }

        private void timerRunStatus_Tick(object sender, EventArgs e)
        {
            try
            {
                lblDevice.Text = "设备" + (deviceNo + 1);
                //double referTemper = (double)DataClass.list_DeviceParam[deviceNo].struct_deivceParam.referTemper / 100;
                //double chassisTemper = (double)DataClass.list_DeviceParam[deviceNo].struct_deivceParam.chassisTemper / 100;
                //double compensation = (double)DataClass.list_DeviceParam[deviceNo].struct_deivceParam.compensationCoefficient / 1000;
                lblReferTemper.Text = "通道测量时间：" + ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.ChannelSettingInfo.Time / 10 + "ms";
                lblChasissTemper.Text = "设定温度间隔：" + ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.ChannelSettingInfo.TempInterval + "m";
                lblCompnsation.Text = "设定温度精度：" + ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.ChannelSettingInfo.Accuracy;
                //int channelNo = DataClass.list_DeviceParam[deviceNo].struct_deivceParam.scanModel & 0xff;
                //ChangeVisiable(channelNo);
                //for (int i = 1; i <= channelNo; i++)
                //{
                //    if (DataClass.list_DeviceChannelParam[deviceNo].struct_DeviceChannelParam.struct_ChannelParams[i].checkStatus == 0)
                //    {
                //        this.Controls.Find("lblDetectionStatus_" + i, false)[0].Text = "准备中...";
                //    }
                //    if (DataClass.list_DeviceChannelParam[deviceNo].struct_DeviceChannelParam.struct_ChannelParams[i].checkStatus == 1)
                //    {
                //        this.Controls.Find("lblDetectionStatus_" + i, false)[0].Text = "检测中...";
                //    }
                //}
                if (!DataClass.list_DeviceEnables[deviceNo].enable || !DataClass.IsRunning)//未启用 停止
                {
                    this.BackColor = Color.Gray;
                    pnlAlarmMsg.Visible = true;
                    lblAlarmMsg.Text = "未运行";
                }
                else
                {
                    this.BackColor = Color.Green;
                    pnlAlarmMsg.Visible = false;
                    if (ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.Device_FaultInfo.IsNetCommunFault > 0)
                    {
                        this.BackColor = Color.Yellow;
                        pnlAlarmMsg.Visible = true;
                        lblAlarmMsg.Text = "通讯故障";
                    }
                    if (ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.Device_FaultInfo.IsCommunctionFault > 0)
                    {
                        this.BackColor = Color.Yellow;
                        pnlAlarmMsg.Visible = true;
                        lblAlarmMsg.Text = "采集模块通讯故障";
                    }
                    if (ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.Device_FaultInfo.IsMainPowerFault > 0)
                    {
                        this.BackColor = Color.Yellow;
                        pnlAlarmMsg.Visible = true;
                        lblAlarmMsg.Text = "主电故障";
                    }
                    if (ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.Device_FaultInfo.IsBackUpPowerFault > 0)
                    {
                        this.BackColor = Color.Yellow;
                        pnlAlarmMsg.Visible = true;
                        lblAlarmMsg.Text = "备电故障";
                    }
                    if (ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.Device_FaultInfo.IsChargeFault > 0)
                    {
                        this.BackColor = Color.Yellow;
                        pnlAlarmMsg.Visible = true;
                        lblAlarmMsg.Text = "充电故障";
                    }
                    for (int j = 0; j < 8; j++)
                    {
                        if (ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_BaseInfo.IsBrokenAlarm > 0)//有断纤 故障 
                        {
                            this.BackColor = Color.Yellow;
                            pnlAlarmMsg.Visible = true;
                            lblAlarmMsg.Text = "断纤故障";
                            break;
                        }
                    }

                    for (int j = 0; j < 8; j++)
                    {
                        if (ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_BaseInfo.DingWenAlarmCount > 0
                            || ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_BaseInfo.ChaWenAlarmCount > 0
                            || ModBusService.Instance().dtsModBuses[deviceNo].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_BaseInfo.WenShengAlarmCount > 0)//有火警
                        {
                            this.BackColor = Color.Red;
                            pnlAlarmMsg.Visible = true;
                            lblAlarmMsg.Text = "火警";
                            break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("运行信息更新异常！——" + ex.Message);
            }
        }

        private void ChangeVisiable(int channelNo)
        {
            switch (channelNo)
            {
                case 0:
                    lblDetectionStatus_1.Visible = false;
                    lblScanChannel_1.Visible = false;
                    lblDetectionStatus_2.Visible = false;
                    lblScanChannel_2.Visible = false;
                    lblDetectionStatus_3.Visible = false;
                    lblScanChannel_3.Visible = false;
                    lblDetectionStatus_4.Visible = false;
                    lblScanChannel_4.Visible = false;
                    break;
                case 1:
                    lblDetectionStatus_1.Visible = true;
                    lblScanChannel_1.Visible = true;
                    lblDetectionStatus_2.Visible = false;
                    lblScanChannel_2.Visible = false;
                    lblDetectionStatus_3.Visible = false;
                    lblScanChannel_3.Visible = false;
                    lblDetectionStatus_4.Visible = false;
                    lblScanChannel_4.Visible = false;
                    break;
                case 2:
                    lblDetectionStatus_1.Visible = true;
                    lblScanChannel_1.Visible = true;
                    lblDetectionStatus_2.Visible = true;
                    lblScanChannel_2.Visible = true;
                    lblDetectionStatus_3.Visible = false;
                    lblScanChannel_3.Visible = false;
                    lblDetectionStatus_4.Visible = false;
                    lblScanChannel_4.Visible = false;
                    break;
                case 3:
                    lblDetectionStatus_1.Visible = true;
                    lblScanChannel_1.Visible = true;
                    lblDetectionStatus_2.Visible = true;
                    lblScanChannel_2.Visible = true;
                    lblDetectionStatus_3.Visible = true;
                    lblScanChannel_3.Visible = true;
                    lblDetectionStatus_4.Visible = false;
                    lblScanChannel_4.Visible = false;
                    break;
                case 4:
                    lblDetectionStatus_1.Visible = true;
                    lblScanChannel_1.Visible = true;
                    lblDetectionStatus_2.Visible = true;
                    lblScanChannel_2.Visible = true;
                    lblDetectionStatus_3.Visible = true;
                    lblScanChannel_3.Visible = true;
                    lblDetectionStatus_4.Visible = true;
                    lblScanChannel_4.Visible = true;
                    break;
            }
        }
    }
}
