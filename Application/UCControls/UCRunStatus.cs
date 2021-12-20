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
                lblDevice.Text = "设备" + deviceNo;
                double referTemper = (double)DataClass.list_DeviceParam[deviceNo].struct_deivceParam.referTemper / 100;
                double chassisTemper = (double)DataClass.list_DeviceParam[deviceNo].struct_deivceParam.chassisTemper / 100;
                double compensation = (double)DataClass.list_DeviceParam[deviceNo].struct_deivceParam.compensationCoefficient / 1000;
                lblReferTemper.Text = "参考温度：" + referTemper + "℃";
                lblChasissTemper.Text = "机箱温度：" + chassisTemper + "℃";
                lblCompnsation.Text = "补偿系数：" + compensation;
                int channelNo = DataClass.list_DeviceParam[deviceNo].struct_deivceParam.scanModel & 0xff;
                ChangeVisiable(channelNo);
                for (int i = 1; i <= channelNo; i++)
                {
                    if (DataClass.list_DeviceChannelParam[deviceNo].struct_DeviceChannelParam.struct_ChannelParams[i].checkStatus == 0)
                    {
                        this.Controls.Find("lblDetectionStatus_" + i, false)[0].Text = "准备中...";
                    }
                    if (DataClass.list_DeviceChannelParam[deviceNo].struct_DeviceChannelParam.struct_ChannelParams[i].checkStatus == 1)
                    {
                        this.Controls.Find("lblDetectionStatus_" + i, false)[0].Text = "检测中...";
                    }
                }
                if (!DataClass.list_DeviceEnables[deviceNo - 1].enable ||  !DataClass.IsRunning)//未启用 停止
                {
                    this.BackColor = Color.Gray;
                    pnlAlarmMsg.Visible = true;
                    lblAlarmMsg.Text = "未运行";
                }
                else
                {
                    this.BackColor = Color.Green;
                    pnlAlarmMsg.Visible = false;
                    if (DataClass.list_TcpCommFault[deviceNo])
                    {
                        this.BackColor = Color.Yellow;
                        pnlAlarmMsg.Visible = true;
                        lblAlarmMsg.Text = "通讯故障";
                    }
                    for (int j = 1; j <= 4; j++)
                    {
                        if (AlarmStatus.isBroken.deviceIsBrokens[deviceNo].channelIsBrokens[j].isbroken)//有断纤 故障 
                        {
                            this.BackColor = Color.Yellow;
                            pnlAlarmMsg.Visible = true;
                            lblAlarmMsg.Text = "断纤故障";
                            break;
                        }
                    }

                    for (int j = 1; j <= 4; j++)
                    {
                        for (int k = 1; k <= 50; k++)
                        {
                            if (AlarmStatus.isAlarm.deviceAlarms[deviceNo].channelAlarms[j].partitionAlarms[k].isFireAlarm || AlarmStatus.isAlarm.deviceAlarms[deviceNo].channelAlarms[j].partitionAlarms[k].isRiseAlarm)//有火警
                            {
                                this.BackColor = Color.Red;
                                pnlAlarmMsg.Visible = true;
                                lblAlarmMsg.Text = "火警";
                                break;
                            }
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
