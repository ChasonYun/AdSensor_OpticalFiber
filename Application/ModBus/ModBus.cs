using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticalFiber
{
    public  abstract class ModBusSendData
    {
        public abstract byte[] Write_Data(int add, int funcode, int startAdd, int num);
    }

    public  class ModBusSensdata_RTU : ModBusSendData
    {
        public override byte[] Write_Data(int add, int funcode, int startAdd, int num)
        {
            byte[] bytes = new byte[8];
            try
            {
                bytes[0] = Convert.ToByte(add);
                bytes[1] = Convert.ToByte(funcode);
                bytes[2] = Convert.ToByte(startAdd >> 8);
                bytes[3] = Convert.ToByte(startAdd & 0xff);
                bytes[4] = Convert.ToByte(num >> 8);
                bytes[5] = Convert.ToByte(num & 0xff);
                bytes[6] = CRC16.crc16(bytes, 6)[0];
                bytes[7] = CRC16.crc16(bytes, 6)[1];
                return bytes;
            }
            catch (Exception)
            {
                return bytes;
            }
        }
    }
   
    public class ModBusSendData_TCP : ModBusSendData
    {
        /// <summary>
        /// modbustcp写数据
        /// </summary>
        /// <param name="add">从机地址</param>
        /// <param name="funcode">功能码</param>
        /// <param name="startAdd">开始地址</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public override byte[] Write_Data(int add, int funcode, int startAdd, int num)
        {
            byte[] bytes = new byte[12];
            try
            {
                bytes[0] = 0x00;
                bytes[1] = 0x00;
                bytes[2] = 0x00;
                bytes[3] = 0x00;
                bytes[4] = 0x00;
                bytes[5] = 0x06;//前面是固定的
                bytes[6] = Convert.ToByte(add);
                bytes[7] = Convert.ToByte(funcode);
                bytes[8] = Convert.ToByte(startAdd >> 8);
                bytes[9] = Convert.ToByte(startAdd & 0xff);
                bytes[10] = Convert.ToByte(num >> 8);
                bytes[11] = Convert.ToByte(num & 0xff);//不需要校验
                return bytes;
            }
            catch (Exception ex)
            {
                throw new Exception("数据转换失败"+ex.Message);
            }
        }
    }
}
