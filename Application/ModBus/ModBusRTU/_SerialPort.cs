using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace OpticalFiber
{
    class _SerialPort
    {
        //单例模式
        private static SerialPort serialPort;
        private static readonly object lockObj = new object();

        private _SerialPort()
        {

        }

        public  static SerialPort GetPort()
        {
            if (serialPort == null)
            {
                lock (lockObj)
                {
                    if (serialPort == null)
                    {
                        serialPort = new SerialPort();
                        serialPort.Parity = 0;//校验位
                        serialPort.DataBits = 8;//数据位
                        serialPort.StopBits = StopBits.One;//停止位
                    }
                }
            }
            return serialPort;
        }
    }
}
