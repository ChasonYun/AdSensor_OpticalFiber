using ModBus.ModBus;
using ModBus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModBus
{
    public class DtsModBus
    {
        string ip;
        int port;
        IModBus modeBusClient;


        int channelCount = 8;//通道数量

        public DtsDeviceDataModel dtsDeviceDataModel;

        string response;

        ushort[] channelSettingInfo = new ushort[3];
        ushort[] deviceFaultInfo = new ushort[4];
        ushort[] channelBaseInfo = new ushort[6];
        ushort[] channelCollectionInfo = new ushort[5];
        ushort[] channelBrokenInfo = new ushort[4];
        ushort[] partSettingInfo = new ushort[5];
        ushort[] partTempInfo = new ushort[6];
        ushort[] dingWenAlarm = new ushort[6];
        ushort[] chaWenAlarm = new ushort[6];
        ushort[] wenShengAlarm = new ushort[6];
        ushort[] channelTemps = new ushort[5000];
        Action<string, string> EvAlarm;

        public DtsModBus(string ip, int port, int channelCount)
        {
            this.ip = ip;
            this.port = port;
            this.channelCount = channelCount;
            if (this.channelCount > 255) this.channelCount = 255;
            modeBusClient = new ModBusTcp(ip, port);
            dtsDeviceDataModel = new DtsDeviceDataModel();
        }

        private void Connect()
        {
            string response = string.Empty;
            try
            {
                modeBusClient.Connect(out response);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }

        #region//写
        /// <summary>
        /// 复位
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool Reset(out string response)
        {
            try
            {
                modeBusClient.WriteSingleRegister(1, 1, 1, out response);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 静音
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool Mute(out string response)
        {
            try
            {
                modeBusClient.WriteSingleRegister(1, 2, 1, out response);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 自检
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool SelfCheck(out string response)
        {
            try
            {
                modeBusClient.WriteSingleRegister(1, 3, 1, out response);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }
        #endregion

        #region//读
        /// <summary>
        /// 通道配置信息
        /// </summary>
        private void PraseChannelSettingInfo()
        {
            try
            {
                for (int i = 1; i <= channelCount; i++)
                {
                    if (modeBusClient.ReadHoldingRegisters((byte)i, 51, 3, out channelSettingInfo, out response))
                    {
                        dtsDeviceDataModel.ChannelSettingInfo.Time = channelSettingInfo[0];
                        dtsDeviceDataModel.ChannelSettingInfo.TempInterval = channelSettingInfo[1];
                        dtsDeviceDataModel.ChannelSettingInfo.Accuracy = channelSettingInfo[2];
                    }
                }

            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }

        /// <summary>
        /// 通道基本信息
        /// </summary>
        private void PraseChannelBaseInfo()
        {
            try
            {
                for (int i = 1; i <= channelCount; i++)
                {
                    if (modeBusClient.ReadInputRegisters((byte)i, 10, 6, out channelBaseInfo, out response))
                    {
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.Id = channelBaseInfo[0];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.PartCount = channelBaseInfo[1];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.IsBrokenAlarm = channelBaseInfo[2];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.DingWenAlarmCount = channelBaseInfo[3];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.ChaWenAlarmCount = channelBaseInfo[4];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.WenShengAlarmCount = channelBaseInfo[5];
                    }
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }

        /// <summary>
        /// 通道采集信息
        /// </summary>
        private void PraseChannelCollectionInfo()
        {
            try
            {
                for (int i = 1; i <= channelCount; i++)
                {
                    if (modeBusClient.ReadInputRegisters((byte)i, 50, 5, out channelCollectionInfo, out response))
                    {
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.Id = channelCollectionInfo[0];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.PartCount = channelCollectionInfo[1];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.IsBrokenAlarm = channelCollectionInfo[2];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.DingWenAlarmCount = channelCollectionInfo[3];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.ChaWenAlarmCount = channelCollectionInfo[4];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.WenShengAlarmCount = channelCollectionInfo[5];
                    }
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }

        /// <summary>
        /// 通道断纤信息
        /// </summary>
        private void PraseChannelBrokenInfo()
        {
            try
            {
                for (int i = 1; i <= channelCount; i++)
                {
                    if (modeBusClient.ReadInputRegisters((byte)i, 100, 4, out channelBrokenInfo, out response))
                    {
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BrokenInfo.BrokenPos = channelBrokenInfo[0];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BrokenInfo.Broken_Year_Month = channelBrokenInfo[1];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BrokenInfo.Broken_Day_Hour = channelBrokenInfo[2];
                        dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BrokenInfo.Broken_Min_Sec = channelBrokenInfo[3];
                    }
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }

        /// <summary>
        /// 设备故障信息
        /// </summary>
        private void PraseDeviceFaultInfo()
        {
            try
            {
                if (modeBusClient.ReadInputRegisters(1, 200, 4, out deviceFaultInfo, out response))
                {
                    dtsDeviceDataModel.Device_FaultInfo.IsCommunctionFault = deviceFaultInfo[0];
                    dtsDeviceDataModel.Device_FaultInfo.IsMainPowerFault = deviceFaultInfo[1];
                    dtsDeviceDataModel.Device_FaultInfo.IsBackUpPowerFault = deviceFaultInfo[2];
                    dtsDeviceDataModel.Device_FaultInfo.IsChargeFault = deviceFaultInfo[3];
                    dtsDeviceDataModel.Device_FaultInfo.IsNetCommunFault = 0;
                }
                else
                {
                    dtsDeviceDataModel.Device_FaultInfo.IsNetCommunFault = 1;
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }

        /// <summary>
        /// 分区配置信息
        /// </summary>
        private void PrasePartSettingInfo()
        {
            try
            {
                for (int i = 1; i <= channelCount; i++)
                {
                    for (int j = 0; j < dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.PartCount; j++)
                    {
                        if (modeBusClient.ReadHoldingRegisters((byte)i, 1000, 5, out partSettingInfo, out response))
                        {
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_SettingInfo.StartPos = partSettingInfo[0];
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_SettingInfo.EndPos = partSettingInfo[1];
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_SettingInfo.DingWen = partSettingInfo[2];
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_SettingInfo.ChaWen = partSettingInfo[3];
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_SettingInfo.WenSheng = partSettingInfo[4];
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }

        /// <summary>
        /// 分区温度信息
        /// </summary>
        private void PrasePartTempInfo()
        {
            try
            {
                for (int i = 1; i <= channelCount; i++)
                {
                    for (int j = 0; j < dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.PartCount; j++)
                    {
                        if (modeBusClient.ReadInputRegisters((byte)i, 1000, 5, out partTempInfo, out response))
                        {
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_TempInfo.AlarmState = partTempInfo[0];
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_TempInfo.MaxTemp = partTempInfo[1];
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_TempInfo.AverageTemp = partTempInfo[2];
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_TempInfo.MinTemp = partTempInfo[3];
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_TempInfo.MaxTempPos = partTempInfo[4];
                            dtsDeviceDataModel.DtsChannelDataModels[i].DtsPartDataModels[j].Part_TempInfo.MinTempPos = partTempInfo[5];
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }
        /// <summary>
        /// 定温报警
        /// </summary>
        private void PraseChannelDingWenAlarmInfo()
        {
            try
            {
                for (int i = 1; i <= channelCount; i++)
                {
                    for (int j = 0; j < dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.DingWenAlarmCount; j++)
                    {
                        if (modeBusClient.ReadInputRegisters((byte)i, (ushort)(10000 + j * 1000), 5, out dingWenAlarm, out response))
                        {
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_DingWenAlarmInfos[j].StartPos = dingWenAlarm[0];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_DingWenAlarmInfos[j].EndPos = dingWenAlarm[1];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_DingWenAlarmInfos[j].Alarm_Year_Month = dingWenAlarm[2];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_DingWenAlarmInfos[j].Alarm_Day_Hour = dingWenAlarm[3];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_DingWenAlarmInfos[j].Alarm_Min_Sec = dingWenAlarm[4];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_DingWenAlarmInfos[j].PartId = dingWenAlarm[5];
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }

        /// <summary>
        /// 差温报警
        /// </summary>
        private void PraseChannelChaWenAlarmInfo()
        {
            try
            {
                for (int i = 1; i <= channelCount; i++)
                {
                    for (int j = 0; j < dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.ChaWenAlarmCount; j++)
                    {
                        if (modeBusClient.ReadInputRegisters((byte)i, (ushort)(14000 + j * 1000), 5, out chaWenAlarm, out response))
                        {
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_ChaWenAlarmInfos[j].StartPos = chaWenAlarm[0];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_ChaWenAlarmInfos[j].EndPos = chaWenAlarm[1];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_ChaWenAlarmInfos[j].Alarm_Year_Month = chaWenAlarm[2];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_ChaWenAlarmInfos[j].Alarm_Day_Hour = chaWenAlarm[3];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_ChaWenAlarmInfos[j].Alarm_Min_Sec = chaWenAlarm[4];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_ChaWenAlarmInfos[j].PartId = chaWenAlarm[5];
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }

        /// <summary>
        /// 温升报警
        /// </summary>
        private void PraseChannelWenShengAlarmInfo()
        {
            try
            {
                for (int i = 1; i <= channelCount; i++)
                {
                    for (int j = 0; j < dtsDeviceDataModel.DtsChannelDataModels[i].Channel_BaseInfo.WenShengAlarmCount; j++)
                    {
                        if (modeBusClient.ReadInputRegisters((byte)i, (ushort)(12000 + j * 1000), 5, out wenShengAlarm, out response))
                        {
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_WenShengAlarmInfos[j].StartPos = wenShengAlarm[0];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_WenShengAlarmInfos[j].EndPos = wenShengAlarm[1];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_WenShengAlarmInfos[j].Alarm_Year_Month = wenShengAlarm[2];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_WenShengAlarmInfos[j].Alarm_Day_Hour = wenShengAlarm[3];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_WenShengAlarmInfos[j].Alarm_Min_Sec = wenShengAlarm[4];
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_WenShengAlarmInfos[j].PartId = wenShengAlarm[5];
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }

        private void PraseChannelTemp()
        {
            try
            {
                for (int i = 1; i <= channelCount; i++)
                {
                    if (modeBusClient.ReadInputRegisters((byte)i, 20000, 5000, out channelTemps, out response))
                    {
                        for (int j = 0; j < dtsDeviceDataModel.DtsChannelDataModels[i].Channel_CollectionsInfo.TempPosCount; j++)
                        {
                            dtsDeviceDataModel.DtsChannelDataModels[i].Channel_Temps[j] = channelTemps[j];
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                EvAlarm?.Invoke(ip, response);
            }
        }
        #endregion

        public bool isStop;
        public void StartMonitor()
        {
            Task.Factory.StartNew(() =>
            {
                //Connect();
                while (!isStop)
                {
                    if (!modeBusClient.IsConnect)
                    {
                        Connect();
                        Thread.Sleep(1000);
                        continue;
                    }
                    PraseChannelSettingInfo();
                    PraseChannelBaseInfo();
                    PraseChannelCollectionInfo();
                    PraseChannelBrokenInfo();
                    PraseDeviceFaultInfo();
                    PrasePartSettingInfo();
                    PrasePartTempInfo();
                    PraseChannelDingWenAlarmInfo();
                    PraseChannelChaWenAlarmInfo();
                    PraseChannelWenShengAlarmInfo();
                    PraseChannelTemp();
                    Thread.Sleep(10);
                }

            });
            isStop = false;
            //modeBusClient.Close();
        }

        public void StopMonitor()
        {
            isStop = true;
        }
    }
}
