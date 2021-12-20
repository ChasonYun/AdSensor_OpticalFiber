using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticalFiber
{
    public class WorkTread_TCP
    {
        //Controller
        struct_DeviceEnable _Device;

        public WorkTread_TCP(int type)
        {
            try
            {
                DataClass.list_DeviceEnables = (new SQL_Select()).Select_DeviceEnable();
                switch (type) 
                {
                    case 0:
                        //iPEndPoint = ConfigClass.Get_TCPParam();
                         
                        //cp = new ModBusTCP_Socket(socket, iPEndPoint);
                        //socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);
                        //Thread workThread_0 = new Thread(new ParameterizedThreadStart(MainWork));
                        //workThread_0.Start(tcp);
                        break;
                    case 1:
                        //DataClass.list_DeviceEnables = ConfigClass.GetStruct_DeviceEnables();
                        for(int i = 0; i < 8; i++)
                        {
                            _Device = DataClass.list_DeviceEnables[i];
                            if (_Device.enable)
                            {
                                DataClass.IsRunning = true;
                                try
                                {
                                    //TCP modbusTCP = new ModBusTCP_SyncSocket(_Device.ipEndPoint);//
                                    TCP modbusTCP = new ModBusTCP_SyncNetWorkStream(_Device.ipEndPoint);//
                                }
                                catch (Exception)
                                {
                                    //DataClass.ShowErrMsg("通讯异常——" + ex.Message);
                                    DataClass.list_TcpCommFault[i + 1] = true;
                                }
                            }
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                DataClass.ShowErrMsg("通讯建立失败——" + ex.Message);
            }
           
        }
    }
}
