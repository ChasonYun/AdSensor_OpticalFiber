using ModBus.ModBus;
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

       const int CHANNELCOUNT = 8;

        public DtsModBus(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            modeBusClient = new ModBusTcp(ip, port);
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
                modeBusClient.WriteSingleRegister(1, 1, out response);
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
                modeBusClient.WriteSingleRegister(2, 1, out response);
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
                modeBusClient.WriteSingleRegister(3, 1, out response);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }
        #endregion

        public void StartMonitor()
        {
            Task.Factory.StartNew(() =>
            {
                Connect();
                while (true)
                {


                    Thread.Sleep(10);
                }

            });
        }
    }
}
