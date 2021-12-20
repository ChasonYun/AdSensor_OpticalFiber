using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpticalFiber
{
    /// <summary>
    /// 调用流程  实例化 MobusRTU类 》Init》RTU_Open》RTU_Write》RTU_Read》RTU_Close
    /// </summary>
    public abstract class RTU
    {
        public abstract void Init(string PortName, int BaudRate,int ReadTimeout);//初始化
        public abstract bool RTU_Open();//打开串口
        public abstract bool RTU_Close();//关闭串口
        public abstract void RTU_Write(int sensorAdd,byte[] byteWrite);//写串口数据
        public abstract byte[] RTU_Read(int sensorAdd,int length);//读串口数据
       
    }
    public class ModBusRTU:RTU
    {
        private string portName;
        private int baudRate;
        private int readTimeout;

        public ModBusRTU(string name,int rate,int timeout)
        {
            this.baudRate = rate;
            this.portName = name;
            this.readTimeout = timeout;
            Init(portName, baudRate, readTimeout);
        }

        /// <summary>
        /// 串口出初始化
        /// </summary>
        /// <param name="PortName">串口名称</param>
        /// <param name="BaudRate">波特率</param>
        /// <param name="ReadTimeout">读超时</param>
        public override void Init(string PortName, int BaudRate, int ReadTimeout)
        {
            PortName = this.portName;
            BaudRate = this.baudRate;
            ReadTimeout = this.readTimeout;
            _SerialPort.GetPort().PortName = PortName;//串口名称
            _SerialPort.GetPort().BaudRate = BaudRate;//波特率
            _SerialPort.GetPort().ReadTimeout = ReadTimeout;
            _SerialPort.GetPort().WriteTimeout = ReadTimeout;
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public override bool RTU_Open()
        {
            try
            {
                if (!_SerialPort.GetPort().IsOpen)
                {
                    _SerialPort.GetPort().Open();
                }
                return _SerialPort.GetPort().IsOpen;
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("对端口的访问被拒绝。 或 - 当前进程或系统上的另一个进程已经打开了指定的 COM 端口");
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("此实例的一个或多个属性无效");
            }
            catch (ArgumentException)
            {
                throw new Exception("端口名称不是以“COM”开始的。 或 - 端口的文件类型不受支持");
            }
            catch (IOException)
            {
                throw new Exception("此端口处于无效状态");
            }
            catch (InvalidOperationException)
            {
                throw new Exception("当前实例上的指定端口已经打开");
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public override bool RTU_Close()
        {
            try
            {
                if (_SerialPort.GetPort().IsOpen)
                {
                    _SerialPort.GetPort().Close();
                }
                return _SerialPort.GetPort().IsOpen;
            }
            catch (IOException)
            {
                throw new Exception("此端口处于无效状态");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }

        /// <summary>
        /// 串口写数据
        /// </summary>
        public override void RTU_Write(int sensorAdd,byte[] byteWrite)
        {
            try
            {
                _SerialPort.GetPort().DiscardInBuffer();
                _SerialPort.GetPort().DiscardOutBuffer();
                Thread.Sleep(200);//暂停200  下位机需要处理数据
                _SerialPort.GetPort().Write(byteWrite, 0, byteWrite.Length);
            }
            catch (ArgumentNullException)
            {
                throw new Exception("写入的数为空");
            }
            catch (InvalidOperationException)
            {
                throw new Exception("指定的端口未打开");
            }
            catch (TimeoutException)
            {
                throw new Exception("该操作未在超时时间到期之前完成");
            }
            catch (Exception ex)
            {
                if (ex.Message == "正在中止线程。")//正常的信息  通过抛出异常结束线程的方式 就会抛出这个异常
                {

                }
                else
                {
                    DataClass.commFault_Count[sensorAdd]++;
                    throw new Exception(ex.Message);
                }
            }
        }

        public override byte[] RTU_Read(int sensorAdd,int length)
        {
            byte[] byteRead = new byte[length];//124*2=248,248+1+1+2=252
            int reclen = 0;
            try
            {
                while (reclen < length)
                {
                    int _reclen = _SerialPort.GetPort().Read(byteRead, reclen, byteRead.Length-reclen);
                    reclen += _reclen;
                }
                    //_SerialPort.GetPort().Read(byteRead, 0, byteRead.Length);
            }
            catch (TimeoutException)//超时
            {
                DataClass.commFault_Count[sensorAdd]++;
            }
           
            catch (ArgumentNullException)
            {
                throw new Exception("传递的 buffer 为 null");
            }
            catch (InvalidOperationException)
            {
                throw new Exception("指定的端口未打开");
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception(" offset 或 count 参数超出了所传递的 buffer 的有效区域。 offset 或 count 小于零");
            }
            catch (ArgumentException)
            {
                throw new Exception(" offset 加上 count 大于 buffer 的长度");
            }
            catch(Exception ex)
            {
                if(ex.Message== "由于线程退出或应用程序请求，已中止 I/O 操作。\r\n"||ex.Message== "正在中止线程。")
                {

                }
                else
                {
                    throw new Exception(ex.Message + ex.StackTrace);
                }
            }
            return byteRead;
        }

      
    }
}
