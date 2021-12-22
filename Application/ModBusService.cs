using ModBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticalFiber
{
    public class ModBusService
    {
        struct_DeviceEnable _Device;
        public List<DtsModBus> dtsModBuses;
        object lockObj = new object();

        public static ModBusService Instance()
        {
            return InnerClass.inner;
        }

        private class InnerClass
        {
            internal static readonly ModBusService inner = new ModBusService();
        }
        private ModBusService()
        {
            try
            {
                lock (lockObj)
                {
                    dtsModBuses = new List<DtsModBus>();
                    DataClass.list_DeviceEnables = (new SQL_Select()).Select_DeviceEnable();
                    for (int i = 0; i < 8; i++)
                    {
                        _Device = DataClass.list_DeviceEnables[i];
                        DtsModBus dtsModBus = new DtsModBus(_Device.ipEndPoint.Address.ToString(), _Device.ipEndPoint.Port, _Device.channelCount);
                        dtsModBuses.Add(dtsModBus);
                    }
                }


            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("通讯建立失败——" + ex.Message);
            }
        }

        public void ReInit()
        {
            try
            {
                lock (lockObj)
                {
                    DataClass.list_DeviceEnables = (new SQL_Select()).Select_DeviceEnable();
                    dtsModBuses.Clear();
                    for (int i = 0; i < 8; i++)
                    {
                        _Device = DataClass.list_DeviceEnables[i];
                        if (_Device.enable)
                        {
                            try
                            {
                                DtsModBus dtsModBus = new DtsModBus(_Device.ipEndPoint.Address.ToString(), _Device.ipEndPoint.Port, _Device.channelCount);
                                dtsModBuses.Add(dtsModBus);
                            }
                            catch (Exception)
                            {
                                //DataClass.ShowErrMsg("通讯异常——" + ex.Message);
                                ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.Device_FaultInfo.IsNetCommunFault = 1;
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("通讯建立失败——" + ex.Message);
            }
        }

        public void StartMonitor()
        {
            lock (lockObj)
            {
                DataClass.IsRunning = true;
                DataClass.list_DeviceEnables = (new SQL_Select()).Select_DeviceEnable();
                for (int i = 0; i < 8; i++)
                {
                    _Device = DataClass.list_DeviceEnables[i];
                    if (_Device.enable)
                    {
                        try
                        {
                            dtsModBuses[i].StartMonitor();
                        }
                        catch (Exception)
                        {
                            //DataClass.ShowErrMsg("通讯异常——" + ex.Message);
                            ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.Device_FaultInfo.IsNetCommunFault = 1;
                        }
                    }
                }
            }

        }

        public void StopMonitor()
        {
            lock (lockObj)
            {
                DataClass.IsRunning = false;
                DataClass.list_DeviceEnables = (new SQL_Select()).Select_DeviceEnable();
                for (int i = 0; i < 8; i++)
                {
                    _Device = DataClass.list_DeviceEnables[i];
                    if (_Device.enable)
                    {
                        try
                        {
                            dtsModBuses[i].StopMonitor();
                        }
                        catch (Exception)
                        {
                            //DataClass.ShowErrMsg("通讯异常——" + ex.Message);
                            ModBusService.Instance().dtsModBuses[i].dtsDeviceDataModel.Device_FaultInfo.IsNetCommunFault = 1;
                        }
                    }
                }
            }

        }
    }
}
