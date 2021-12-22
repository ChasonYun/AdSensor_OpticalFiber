using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticalFiber
{
    public partial class FrmMain : Form
    {
        SQL_Select sql_Select = new SQL_Select();
        SQL_Insert sql_Insert = new SQL_Insert();
        ModBusService modBusService;
        int tempDingWenCount;
        int tempChaWenCount;
        int tempWenShengCount;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        public FrmMain()
        {
            try
            {
                InitializeComponent();
                DataClass.list_PrtName = sql_Select.Select_PrtName();
                timerMain.Start();
                timerAlarm.Start();
                timerVoice.Start();
                DataClass.projName = sql_Select.Select_PrjName(1);

                modBusService = ModBusService.Instance();
                InitTreeView();
                InitRunStatus();

                LoadMainPage();
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("frmmain初始化失败！——" + ex.Message);
            }

        }

        private void InitTreeView()
        {
            try
            {
                pnlTvw.Controls.Clear();
                UCTreeView uCTreeView = new UCTreeView();
                uCTreeView.getTvwMsg += new GetTvwMsg(ShowLines);
                uCTreeView.Dock = DockStyle.Fill;
                pnlTvw.Controls.Add(uCTreeView);
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("设备列表初始化失败！——" + ex.Message);
            }

        }

        private void InitRunStatus()
        {
            try
            {
                tlpRunStatus.Controls.Clear();
                UCRunStatus ucRunStatus;
                for (int i = 0; i < 8; i++)
                {
                    ucRunStatus = new UCRunStatus(i);
                    ucRunStatus.Dock = DockStyle.Fill;
                    tlpRunStatus.Controls.Add(ucRunStatus);
                }
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("设备状态初始化失败！——" + ex.Message);
            }
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            try
            {
                lblProjName.Text = DataClass.projName;
                lblTimeNow.Text = DateTime.Now.ToString();
                if (!DataClass.IsRunning)
                {
                    pbxRunStatus.BackgroundImage = Properties.Resources.runStatus_Stop;
                }
                else
                {
                    pbxRunStatus.BackgroundImage = Properties.Resources.runStatus_Run;
                }

                if (list_AlarmCons.Count > 0 || list_AlarmRise.Count > 0 || list_AlarmChaWen.Count > 0)
                {
                    pbxFireAlarm.BackgroundImage = Properties.Resources.status_FireAlarm;
                }
                else
                {
                    pbxFireAlarm.BackgroundImage = Properties.Resources.status_FireAlarm_;
                }

                if (list_BrokenMsg.Count > 0 || list_CommFault.Count > 0)
                {
                    pbxFaultAlarm.BackgroundImage = Properties.Resources.status_FaultAlarm;
                }
                else
                {
                    pbxFaultAlarm.BackgroundImage = Properties.Resources.status_FaultAlarm_;
                }

                switch (DataClass.userLevel)
                {
                    case 0:
                        pbxUser.BackgroundImage = Properties.Resources.userNormal;
                        lblWelcome.Text = "欢迎！普通用户";
                        设备管理ToolStripMenuItem.Visible = false;
                        主页设置ToolStripMenuItem.Visible = false;
                        密码管理ToolStripMenuItem.Visible = false;
                        操作记录ToolStripMenuItem.Visible = false;
                        break;
                    case 1:
                        pbxUser.BackgroundImage = Properties.Resources.userOperator;
                        设备管理ToolStripMenuItem.Visible = true;
                        主页设置ToolStripMenuItem.Visible = true;
                        密码管理ToolStripMenuItem.Visible = false;
                        操作记录ToolStripMenuItem.Visible = false;
                        lblWelcome.Text = "你好！系统操作员";
                        break;
                    case 2:
                        pbxUser.BackgroundImage = Properties.Resources.userAdmin;
                        设备管理ToolStripMenuItem.Visible = true;
                        主页设置ToolStripMenuItem.Visible = true;
                        密码管理ToolStripMenuItem.Visible = true;
                        操作记录ToolStripMenuItem.Visible = true;
                        lblWelcome.Text = "您好！系统管理员";
                        break;
                }

            }
            catch (Exception ex)
            {

                DataClass.ShowErrMsg("主时钟事件错误！——" + ex.Message);
            }
        }

        bool Ismute = false;
        SoundPlayer soundPlayer = new SoundPlayer();
        private void timerVoice_Tick(object sender, EventArgs e)
        {
            if (!Ismute)//没有消音
            {
                if (list_AlarmCons.Count > 0 || list_AlarmRise.Count > 0)
                {
                    soundPlayer = new SoundPlayer(Properties.Resources.alarmVoice);
                    soundPlayer.PlayLooping();
                }
                else if (list_BrokenMsg.Count > 0 || list_CommFault.Count > 0)
                {
                    soundPlayer = new SoundPlayer(Properties.Resources.faultVoice);
                    soundPlayer.PlayLooping();
                }
                else
                {
                    soundPlayer.Stop();
                }
            }
            else//静音
            {
                soundPlayer.Stop();
            }
        }


        Struct_AlarmMsg temp_CommFault;//通讯故障
        Struct_AlarmMsg temp_TempBroken;//断纤故障
        Struct_AlarmMsg temp_TempCons;//定温报警
        Struct_AlarmMsg temp_TempRise;//差温报警

        Struct_AlarmMsg compareAlarmMsg;

        /// <summary>
        /// 断纤故障
        /// </summary>
        Dictionary<int, Struct_AlarmMsg> dicBroken = new Dictionary<int, Struct_AlarmMsg>();
        /// <summary>
        /// 定温报警
        /// </summary>
        Dictionary<int, Struct_AlarmMsg> dicCons = new Dictionary<int, Struct_AlarmMsg>();
        /// <summary>
        /// 温升报警
        /// </summary>
        Dictionary<int, Struct_AlarmMsg> dicRise = new Dictionary<int, Struct_AlarmMsg>();
        /// <summary>
        /// 差温报警
        /// </summary>
        Dictionary<int, Struct_AlarmMsg> dicChaWen = new Dictionary<int, Struct_AlarmMsg>();


        List<Struct_AlarmMsg> list_AlarmCons = new List<Struct_AlarmMsg>();//定温报警
        List<Struct_AlarmMsg> list_AlarmChaWen = new List<Struct_AlarmMsg>();//差温报警
        List<Struct_AlarmMsg> list_AlarmRise = new List<Struct_AlarmMsg>();//温升
        List<Struct_AlarmMsg> list_BrokenMsg = new List<Struct_AlarmMsg>();//断纤故障
        List<Struct_AlarmMsg> list_CommFault = new List<Struct_AlarmMsg>();//通讯故障
        private bool[] commFault = new bool[8];
        private void timerAlarm_Tick(object sender, EventArgs e)
        {
            try
            {
                DataClass.list_DeviceEnables = (new SQL_Select()).Select_DeviceEnable();
                for (int i = 0; i < 8; i++)
                {
                    if (DataClass.list_DeviceEnables[i].enable)
                    {
                        #region//通讯故障
                        if (commFault[i] && ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.Device_FaultInfo.IsNetCommunFault <= 0)//有故障
                        {
                            InitTreeView();
                            commFault[i] = false;
                            temp_CommFault = new Struct_AlarmMsg();
                            temp_CommFault.DateTime = DateTime.Now;
                            temp_CommFault.DeviceNo = i;
                            temp_CommFault.ChannelNo = 0;
                            temp_CommFault.PartitionNo = 0;
                            temp_CommFault.Position = 0;
                            foreach (struct_DeviceEnable _DeviceEnable in DataClass.list_DeviceEnables)
                            {
                                if (_DeviceEnable.deviceNo == i)
                                {
                                    temp_CommFault.Illustrate = _DeviceEnable.name;
                                }
                            }
                            temp_CommFault.Relay = 0;
                            temp_CommFault.Type = "通讯恢复";
                            temp_CommFault.AlarmValue = 0;
                            temp_CommFault.Threshold = 0;

                            list_CommFault.Add(temp_CommFault);
                            UpdateDgvMsg();
                            sql_Insert.Insert_Alarm(temp_CommFault);
                        }
                        else
                        {
                            if (ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.Device_FaultInfo.IsNetCommunFault > 0 && !commFault[i])
                            {
                                InitTreeView();
                                temp_CommFault = new Struct_AlarmMsg();
                                temp_CommFault.DateTime = DateTime.Now;
                                temp_CommFault.DeviceNo = i;
                                temp_CommFault.ChannelNo = 0;
                                temp_CommFault.PartitionNo = 0;
                                temp_CommFault.Position = 0;
                                foreach (struct_DeviceEnable _DeviceEnable in DataClass.list_DeviceEnables)
                                {
                                    if (_DeviceEnable.deviceNo == i)
                                    {
                                        temp_CommFault.Illustrate = _DeviceEnable.name;
                                    }
                                }
                                //temp_CommFault.Illustrate = "设备" + i;
                                temp_CommFault.Relay = 0;
                                temp_CommFault.Type = "通讯故障";
                                temp_CommFault.AlarmValue = 0;
                                temp_CommFault.Threshold = 0;
                                list_CommFault.Add(temp_CommFault);
                                Ismute = false;
                                UpdateDgvMsg();
                                sql_Insert.Insert_Alarm(temp_CommFault);
                            }
                        }
                        if (ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.Device_FaultInfo.IsNetCommunFault > 0)
                        {
                            commFault[i] = true;
                        }
                        #endregion


                        for (int j = 0; j < DataClass.list_DeviceEnables[i].channelCount; j++)
                        {
                            #region//断纤故障
                            if (ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_BaseInfo.IsBrokenAlarm > 0)
                            {
                                temp_TempBroken = new Struct_AlarmMsg();
                                temp_TempBroken.DateTime = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_BrokenInfo.AlarmTime;
                                temp_TempBroken.DeviceNo = i;
                                temp_TempBroken.ChannelNo = j;
                                temp_TempBroken.PartitionNo = 0;
                                temp_TempBroken.Position = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_BrokenInfo.BrokenPos;
                                temp_TempBroken.Illustrate = "通道" + j + 1;
                                temp_TempBroken.Relay = 0;
                                temp_TempBroken.Type = "断纤故障";
                                temp_TempBroken.AlarmValue = 0;
                                //temp_TempBroken.Threshold = AlarmStatus.isBroken.deviceIsBrokens[i].channelIsBrokens[j].threshold;
                                if (!dicBroken.ContainsKey((i - 1) * 8 + j))
                                {
                                    Ismute = false;
                                    list_BrokenMsg.Add(temp_TempBroken);
                                    UpdateDgvMsg();
                                    dicBroken.Add((i - 1) * 8 + j, temp_TempBroken);
                                    sql_Insert.Insert_Alarm(temp_TempBroken);
                                }
                                else
                                {
                                    dicBroken.TryGetValue((i - 1) * 8 + j, out compareAlarmMsg);
                                    if (compareAlarmMsg != temp_TempBroken)
                                    {
                                        dicBroken.Remove((i - 1) * 8 + j);
                                        Ismute = false;
                                        list_BrokenMsg.Add(temp_TempBroken);
                                        UpdateDgvMsg();
                                        dicBroken.Add((i - 1) * 8 + j, temp_TempBroken);
                                        sql_Insert.Insert_Alarm(temp_TempBroken);
                                    }
                                }
                            }
                            #endregion
                            for (int k = 0; k < ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_BaseInfo.DingWenAlarmCount; k++)//定温报警
                            {
                                {
                                    temp_TempCons = new Struct_AlarmMsg();
                                    temp_TempCons.DateTime = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_DingWenAlarmInfos[k].AlarmTime;
                                    temp_TempCons.DeviceNo = i;
                                    temp_TempCons.ChannelNo = j;
                                    temp_TempCons.PartitionNo = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_DingWenAlarmInfos[k].PartId;
                                    temp_TempCons.Position = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_DingWenAlarmInfos[k].StartPos;
                                    foreach (struct_PrtName prtName in DataClass.list_PrtName)
                                    {
                                        if (prtName.deviceNo == i && prtName.channelNo == j && prtName.prtNo == k)
                                        {
                                            temp_TempCons.Illustrate = prtName.prtName;
                                        }
                                    }
                                    temp_TempCons.Relay = 0;
                                    temp_TempCons.Type = "定温报警";
                                    temp_TempCons.AlarmValue = 0;
                                    temp_TempCons.Threshold = 0;
                                    if (!dicCons.ContainsKey((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo))
                                    {
                                        Ismute = false;
                                        list_AlarmCons.Add(temp_TempCons);
                                        dicCons.Add((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo, temp_TempCons);
                                        UpdateDgvMsg();
                                        sql_Insert.Insert_Alarm(temp_TempCons);
                                    }
                                    else
                                    {
                                        dicCons.TryGetValue((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo, out compareAlarmMsg);
                                        if (compareAlarmMsg != temp_TempCons)
                                        {
                                            dicCons.Remove((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo);
                                            Ismute = false;
                                            list_AlarmCons.Add(temp_TempCons);
                                            UpdateDgvMsg();
                                            dicCons.Add((i - 1) * 8 + (j - 1) * 50 + k, temp_TempCons);
                                            sql_Insert.Insert_Alarm(temp_TempCons);
                                        }
                                    }
                                }
                            }
                            for (int k = 0; k < ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_BaseInfo.ChaWenAlarmCount; k++)//差温火警
                            {
                                temp_TempCons = new Struct_AlarmMsg();
                                temp_TempCons.DateTime = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_ChaWenAlarmInfos[k].AlarmTime;
                                temp_TempCons.DeviceNo = i;
                                temp_TempCons.ChannelNo = j;
                                temp_TempCons.PartitionNo = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_ChaWenAlarmInfos[k].PartId;
                                temp_TempCons.Position = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_ChaWenAlarmInfos[k].StartPos;
                                foreach (struct_PrtName prtName in DataClass.list_PrtName)
                                {
                                    if (prtName.deviceNo == i && prtName.channelNo == j && prtName.prtNo == k)
                                    {
                                        temp_TempCons.Illustrate = prtName.prtName;
                                    }
                                }
                                temp_TempCons.Relay = 0;
                                temp_TempCons.Type = "差温报警";
                                temp_TempCons.AlarmValue = 0;
                                temp_TempCons.Threshold = 0;
                                if (!dicChaWen.ContainsKey((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo))
                                {
                                    Ismute = false;
                                    list_AlarmChaWen.Add(temp_TempRise);
                                    dicChaWen.Add((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo, temp_TempRise);
                                    UpdateDgvMsg();
                                    sql_Insert.Insert_Alarm(temp_TempRise);
                                }
                                else
                                {
                                    dicChaWen.TryGetValue((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo, out compareAlarmMsg);
                                    if (compareAlarmMsg != temp_TempRise)
                                    {
                                        dicChaWen.Remove((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo);
                                        Ismute = false;
                                        list_AlarmChaWen.Add(temp_TempRise);
                                        UpdateDgvMsg();
                                        dicChaWen.Add((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo, temp_TempRise);
                                        sql_Insert.Insert_Alarm(temp_TempRise);
                                    }
                                }
                            }
                            for (int k = 0; k < ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_BaseInfo.WenShengAlarmCount; k++)//温升火警
                            {
                                temp_TempCons = new Struct_AlarmMsg();
                                temp_TempCons.DateTime = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_WenShengAlarmInfos[k].AlarmTime;
                                temp_TempCons.DeviceNo = i;
                                temp_TempCons.ChannelNo = j;
                                temp_TempCons.PartitionNo = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_WenShengAlarmInfos[k].PartId;
                                temp_TempCons.Position = ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.DtsChannelDataModels[j].Channel_WenShengAlarmInfos[k].StartPos;
                                foreach (struct_PrtName prtName in DataClass.list_PrtName)
                                {
                                    if (prtName.deviceNo == i && prtName.channelNo == j && prtName.prtNo == k)
                                    {
                                        temp_TempCons.Illustrate = prtName.prtName;
                                    }
                                }
                                temp_TempCons.Relay = 0;
                                temp_TempCons.Type = "温升报警";
                                temp_TempCons.AlarmValue = 0;
                                temp_TempCons.Threshold = 0;
                                if (!dicRise.ContainsKey((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo))
                                {
                                    Ismute = false;
                                    list_AlarmRise.Add(temp_TempRise);
                                    dicRise.Add((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo, temp_TempRise);
                                    UpdateDgvMsg();
                                    sql_Insert.Insert_Alarm(temp_TempRise);
                                }
                                else
                                {
                                    dicRise.TryGetValue((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo, out compareAlarmMsg);
                                    if (compareAlarmMsg != temp_TempRise)
                                    {
                                        dicRise.Remove((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo);
                                        Ismute = false;
                                        list_AlarmRise.Add(temp_TempRise);
                                        UpdateDgvMsg();
                                        dicRise.Add((i - 1) * 8 + (j - 1) * 8 + temp_TempCons.PartitionNo, temp_TempRise);
                                        sql_Insert.Insert_Alarm(temp_TempRise);
                                    }
                                }
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("报警时事件间错误！——" + ex.Message + ex.StackTrace);
            }
        }

        private void 设备管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UCDeviceManagement uCDeviceManagement = new UCDeviceManagement();
            //uCDeviceManagement.Dock = DockStyle.Fill;
            //uCDeviceManagement.BackColor = Color.White;
            //pnlMain.Controls.Clear();
            //pnlMain.Controls.Add(uCDeviceManagement);
            try
            {
                if (DataClass.IsRunning)
                {
                    modBusService.StopMonitor();
                    Thread.Sleep(500);
                    timerAlarm.Stop();
                }

                UCSysConfig ucSysConfig = new UCSysConfig();
                ucSysConfig.Dock = DockStyle.Fill;
                pnlMain.Controls.Clear();
                pnlMain.Controls.Add(ucSysConfig);
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg(ex.Message);
            }
        }

        private void btnMainPage_Click(object sender, EventArgs e)
        {
            LoadMainPage();
        }

        private void LoadMainPage()
        {
            if (!DataClass.IsRunning)
            {
                modBusService = ModBusService.Instance();
                modBusService.StartMonitor();
                DataClass.list_PrtName = sql_Select.Select_PrtName();
                timerAlarm.Start();
                Thread.Sleep(500);
                InitTreeView();
            }
            pnlMain.Controls.Clear();
            UCMainPage ucMainPage = new UCMainPage(1);
            ucMainPage.spcPagePic.Panel2Collapsed = true;
            ucMainPage.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(ucMainPage);
        }

        private void btnSpline_Click(object sender, EventArgs e)
        {
            if (!DataClass.IsRunning)
            {
                modBusService = ModBusService.Instance();
                modBusService.StartMonitor();
                DataClass.list_PrtName = sql_Select.Select_PrtName();
                timerAlarm.Start();
                Thread.Sleep(500);
                InitTreeView();
            }
            ShowLines(new struct_TvwMsg() { deviceNo = 1, channelNo = 0, partitionNo = 0, treeNodeName = "设备1" });
        }

        private void 主页设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            UCMainPage ucMainPage = new UCMainPage(0);
            ucMainPage.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(ucMainPage);
        }

        private void ShowLines(struct_TvwMsg struct_tvwMsg)
        {
            if (!DataClass.IsRunning)
            {
                modBusService = ModBusService.Instance();
                modBusService.StartMonitor();
                DataClass.list_PrtName = sql_Select.Select_PrtName();
                timerAlarm.Start();
                Thread.Sleep(500);
                InitTreeView();
            }
            pnlMain.Controls.Clear();
            UCLines ucLines = new UCLines(struct_tvwMsg);
            ucLines.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(ucLines);
        }

        private void 切换用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();
        }

        private void 密码管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPwdManagement frmPwdManagement = new FrmPwdManagement();
            frmPwdManagement.ShowDialog();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要退出吗？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
                sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = DataClass.userLevel, record = "退出系统" });
                Environment.Exit(0);
            }
        }

        private void 复位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataClass.userLevel == 0)
                {
                    FrmCheckPwd frmCheckPwd = new FrmCheckPwd();
                    if (frmCheckPwd.ShowDialog() == DialogResult.OK)
                    {
                        Reset();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show("确定要复位吗？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        Reset();
                    }
                }

            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg(ex.Message);
            }

        }

        //FrmReset frmReset;
        private void Reset()
        {
            try
            {
                //frmReset = new FrmReset();
                //frmReset.Show();
                Ismute = false;
                timerAlarm.Stop();
                Thread.Sleep(1000);
                Array.Clear(commFault, 0, commFault.Length);
                Array.Clear(DataClass.list_TcpCommFault, 0, DataClass.list_TcpCommFault.Length);
                list_AlarmRise.Clear();
                list_AlarmCons.Clear();
                list_AlarmChaWen.Clear();
                list_CommFault.Clear();
                list_BrokenMsg.Clear();
                AlarmStatus.Reset();

                dicBroken.Clear();
                dicCons.Clear();
                dicRise.Clear();
                dicChaWen.Clear();

                if (DataClass.IsRunning)
                {
                    modBusService.StopMonitor();
                    Thread.Sleep(500);

                }
                Thread.Sleep(500);

                modBusService.StartMonitor();
                DataClass.list_PrtName = sql_Select.Select_PrtName();
                Thread.Sleep(500);
                InitTreeView();
                dgvAlarmMsg.Rows.Clear();
                Thread.Sleep(500);
                timerAlarm.Start();
                //frmReset.Close();
                MessageBox.Show("复位成功！");
                sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = DataClass.userLevel, record = "系统复位" });
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("复位失败！" + ex.Message);
            }
            finally
            {

            }
        }


        private void UpdateDgvMsg()
        {
            try
            {
                int i = 1;
                List<Struct_AlarmMsg> allMsg = new List<Struct_AlarmMsg>();
                dgvAlarmMsg.Rows.Clear();
                foreach (Struct_AlarmMsg AlarmMsg in list_BrokenMsg)
                {
                    allMsg.Add(AlarmMsg);
                }
                foreach (Struct_AlarmMsg AlarmMsg in list_AlarmRise)
                {
                    allMsg.Add(AlarmMsg);
                }
                foreach (Struct_AlarmMsg AlarmMsg in list_AlarmCons)
                {
                    allMsg.Add(AlarmMsg);
                }
                foreach (Struct_AlarmMsg AlarmMsg in list_CommFault)
                {
                    allMsg.Add(AlarmMsg);
                }
                foreach (Struct_AlarmMsg AlarmMsg in list_AlarmChaWen)
                {
                    allMsg.Add(AlarmMsg);
                }
                allMsg.Sort();//按时间排序  这个类需要实现  IComparer  IComparable接口  声明排序条件
                foreach (Struct_AlarmMsg AlarmMsg in allMsg)
                {
                    AddRows(i++, AlarmMsg);
                }
                for (int j = 0; j < dgvAlarmMsg.Rows.Count; j++)
                {
                    if (dgvAlarmMsg.Rows[j].Cells[8].Value.ToString() == "通讯故障")
                    {
                        dgvAlarmMsg.Rows[j].Cells[8].Style.BackColor = Color.Yellow;
                        dgvAlarmMsg.Rows[j].Cells[9].Style.BackColor = Color.Yellow;
                        dgvAlarmMsg.Rows[j].Cells[10].Style.BackColor = Color.Yellow;
                    }
                    if (dgvAlarmMsg.Rows[j].Cells[8].Value.ToString() == "通讯恢复")
                    {
                        dgvAlarmMsg.Rows[j].Cells[8].Style.BackColor = Color.Green;
                        dgvAlarmMsg.Rows[j].Cells[9].Style.BackColor = Color.Green;
                        dgvAlarmMsg.Rows[j].Cells[10].Style.BackColor = Color.Green;
                    }
                    if (dgvAlarmMsg.Rows[j].Cells[8].Value.ToString() == "定温报警")
                    {
                        dgvAlarmMsg.Rows[j].Cells[8].Style.BackColor = Color.Red;
                        dgvAlarmMsg.Rows[j].Cells[9].Style.BackColor = Color.Red;
                        dgvAlarmMsg.Rows[j].Cells[10].Style.BackColor = Color.Red;
                    }
                    if (dgvAlarmMsg.Rows[j].Cells[8].Value.ToString() == "差温报警")
                    {
                        dgvAlarmMsg.Rows[j].Cells[8].Style.BackColor = Color.Red;
                        dgvAlarmMsg.Rows[j].Cells[9].Style.BackColor = Color.Red;
                        dgvAlarmMsg.Rows[j].Cells[10].Style.BackColor = Color.Red;
                    }
                    if (dgvAlarmMsg.Rows[j].Cells[8].Value.ToString() == "温升报警")
                    {
                        dgvAlarmMsg.Rows[j].Cells[8].Style.BackColor = Color.Red;
                        dgvAlarmMsg.Rows[j].Cells[9].Style.BackColor = Color.Red;
                        dgvAlarmMsg.Rows[j].Cells[10].Style.BackColor = Color.Red;
                    }
                    if (dgvAlarmMsg.Rows[j].Cells[8].Value.ToString() == "断纤故障")
                    {
                        dgvAlarmMsg.Rows[j].Cells[8].Style.BackColor = Color.Yellow;
                        dgvAlarmMsg.Rows[j].Cells[9].Style.BackColor = Color.Yellow;
                        dgvAlarmMsg.Rows[j].Cells[10].Style.BackColor = Color.Yellow;
                    }
                }
                tabMsg.SelectedTab = tabAlarmMsg;
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("报警信息更新异常！——" + ex.Message);
            }
        }
        private void AddRows(int i, Struct_AlarmMsg AlarmMsg)
        {
            dgvAlarmMsg.Rows.Add(i, AlarmMsg.DateTime, AlarmMsg.DeviceNo, AlarmMsg.ChannelNo, AlarmMsg.PartitionNo, AlarmMsg.Position + "米", AlarmMsg.Illustrate, AlarmMsg.Relay, AlarmMsg.Type, AlarmMsg.AlarmValue, AlarmMsg.Threshold);
        }

        private void 报警查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            UCAlarmData ucAlarmData = new UCAlarmData();
            ucAlarmData.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(ucAlarmData);
        }

        private void 消音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ismute = true;
            sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = DataClass.userLevel, record = "静音" });
        }

        private void 历史曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂未开放！");
        }

        private void 操作记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            UCAudit ucAudit = new UCAudit();
            ucAudit.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(ucAudit);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要退出吗？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
                sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = DataClass.userLevel, record = "退出系统" });
                Environment.Exit(0);
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
