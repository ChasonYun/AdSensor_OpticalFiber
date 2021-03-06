using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace OpticalFiber
{
    public partial class UCChannelMsg : UserControl
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void Frm_Move()
        {
            if (mode == 1)
            {
                return;
            }
            int WM_SYSCOMMAND = 0x0112;
            int SC_MOVE = 0xF010;
            int HTCAPTION = 0x0002;
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        int mode;

        public UCChannelMsg(int deviceNo, int channelNo, int mode)
        {
            InitializeComponent();
            this.DeviceNo = deviceNo;
            this.ChannelNo = channelNo;
            this.mode = mode;
            switch (mode)
            {
                case 0://调试模式
                    lblMaxTemper.Text = "设备" + deviceNo;
                    lblPosition.Text = "通道" + channelNo;
                    break;
                case 1://运行模式
                    timer1.Start();
                    break;
            }
        }

        private int deviceNo;
        private int channelNo;
        private bool isEnable = true;


        public string ChannelName { get => lblName.Text; set => lblName.Text = value; }
        public int DeviceNo { get => deviceNo; set => deviceNo = value; }
        public int ChannelNo { get => channelNo; set => channelNo = value; }
        public bool IsEnable { get => isEnable; set => isEnable = value; }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblMaxTemper.Text = "最高温度：" + (decimal)Channel_Max(deviceNo,channelNo)[0] / 10 + "℃";
            lblPosition.Text = "位置：" + (decimal)Channel_Max(deviceNo, channelNo)[1] + "米";
            if (IsChannelDingWenAlarm(deviceNo, channelNo) || IsChannelChaWenAlarm(deviceNo, channelNo) || IsChannelWenShengAlarm(deviceNo, channelNo))//优先级最高
            {
                this.BackColor = Color.Black;
                panel1.BackColor = Color.Red;
                panel2.BackColor = Color.Red;
            }
            else
            {
                if (IsBroken(deviceNo, channelNo))
                {
                    this.BackColor = Color.Black;
                    panel1.BackColor = Color.Yellow;
                    panel2.BackColor = Color.Yellow;
                }
                if (IsNetCommunFault(deviceNo))//通讯故障覆盖 断纤
                {
                    this.BackColor = Color.Black;
                    panel1.BackColor = Color.Yellow;
                    panel2.BackColor = Color.Yellow;
                    lblMaxTemper.Text = "最高温度：" + 0 + "℃";
                    lblPosition.Text = "位置：" + 0 + "米";
                }
            }

            if (!IsChannelDingWenAlarm(deviceNo, channelNo) && !IsChannelChaWenAlarm(deviceNo, channelNo) && !IsChannelWenShengAlarm(deviceNo, channelNo) && !IsNetCommunFault(deviceNo))
            {
                this.BackColor = Color.Black;
                panel1.BackColor = Color.Cyan;
                panel2.BackColor = Color.Cyan; ;

            }
        }

        private int[] Channel_Max(int deviceIndex, int channelIndex)
        {
            int maxTemper = 0;
            int maxTemperPosition = 0;
            int partitionNo = ModBusService.Instance().dtsModBuses[deviceIndex].dtsDeviceDataModel.DtsChannelDataModels[channelIndex].Channel_BaseInfo.PartCount;
            for (int i = 0; i < partitionNo; i++)
            {
                int partTemper = ModBusService.Instance().dtsModBuses[deviceIndex].dtsDeviceDataModel.DtsChannelDataModels[channelIndex].DtsPartDataModels[i].Part_TempInfo.MaxTemp;
                int partPosition = ModBusService.Instance().dtsModBuses[deviceIndex].dtsDeviceDataModel.DtsChannelDataModels[channelIndex].DtsPartDataModels[i].Part_TempInfo.MaxTempPos;
                if (partTemper >= maxTemper)
                {
                    maxTemper = partTemper;
                    maxTemperPosition = partPosition;
                }
            }
            return new int[] { maxTemper, maxTemperPosition };
        }

        /// <summary>
        /// 查询通道 定温报警
        /// </summary>
        /// <param name="deviceIndex"></param>
        /// <param name="channelIndex"></param>
        /// <returns></returns>
        private bool IsChannelDingWenAlarm(int deviceIndex, int channelIndex)
        {
            bool isFireAlarm = false;
            if (ModBusService.Instance().dtsModBuses[deviceIndex].dtsDeviceDataModel.DtsChannelDataModels[channelIndex].Channel_BaseInfo.DingWenAlarmCount > 0)
            {
                isFireAlarm = true;
            }
            return isFireAlarm;
        }

        /// <summary>
        /// 查询通道 差温报警
        /// </summary>
        /// <param name="deviceIndex"></param>
        /// <param name="channelIndex"></param>
        /// <returns></returns>
        private bool IsChannelChaWenAlarm(int deviceIndex, int channelIndex)
        {
            bool isFireAlarm = false;
            if (ModBusService.Instance().dtsModBuses[deviceIndex].dtsDeviceDataModel.DtsChannelDataModels[channelIndex].Channel_BaseInfo.ChaWenAlarmCount > 0)
            {
                isFireAlarm = true;
            }
            return isFireAlarm;
        }

        /// <summary>
        /// 查询通道 温升报警
        /// </summary>
        /// <param name="deviceIndex"></param>
        /// <param name="channelIndex"></param>
        /// <returns></returns>
        private bool IsChannelWenShengAlarm(int deviceIndex, int channelIndex)
        {
            bool isFireAlarm = false;
            if (ModBusService.Instance().dtsModBuses[deviceIndex].dtsDeviceDataModel.DtsChannelDataModels[channelIndex].Channel_BaseInfo.WenShengAlarmCount > 0)
            {
                isFireAlarm = true;
            }
            return isFireAlarm;
        }

        /// <summary>
        /// 查询通道 断纤报警
        /// </summary>
        /// <param name="deviceIndex"></param>
        /// <param name="channelIndex"></param>
        /// <returns></returns>
        private bool IsBroken(int deviceIndex, int channelIndex)
        {
            bool isFireAlarm = false;
            if (ModBusService.Instance().dtsModBuses[deviceIndex].dtsDeviceDataModel.DtsChannelDataModels[channelIndex].Channel_BaseInfo.IsBrokenAlarm > 0)
            {
                isFireAlarm = true;
            }
            return isFireAlarm;
        }

        private bool IsNetCommunFault(int deviceIndex)
        {
            bool isFireAlarm = false;
            if (ModBusService.Instance().dtsModBuses[deviceIndex].dtsDeviceDataModel.Device_FaultInfo.IsNetCommunFault > 0)
            {
                isFireAlarm = true;
            }
            return isFireAlarm;
        }
        private void lblMaxTemper_MouseDown(object sender, MouseEventArgs e)
        {
            getChannelMsg?.Invoke(DeviceNo, ChannelNo, ChannelName);
            Frm_Move();
        }

        private void lblPosition_MouseDown(object sender, MouseEventArgs e)
        {
            getChannelMsg?.Invoke(DeviceNo, ChannelNo, ChannelName);
            Frm_Move();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            getChannelMsg?.Invoke(DeviceNo, ChannelNo, ChannelName);
            Frm_Move();
        }

        private void lblName_MouseDown(object sender, MouseEventArgs e)
        {
            getChannelMsg?.Invoke(DeviceNo, ChannelNo, ChannelName);
            Frm_Move();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            getChannelMsg?.Invoke(DeviceNo, ChannelNo, ChannelName);
            Frm_Move();
        }

        private void UCChannelMsg_MouseDown(object sender, MouseEventArgs e)
        {
            getChannelMsg?.Invoke(DeviceNo, ChannelNo, ChannelName);
            Frm_Move();
        }
        public event GetChannelMsg getChannelMsg;

    }
    public delegate void GetChannelMsg(int deviceNo, int channelNo, string channelName);
}
