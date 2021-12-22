using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBus.ModBus
{
    interface IModBus
    {
        /// <summary>
        /// 连接状态
        /// </summary>
        bool IsConnect { get; }

        /// <summary>
        /// 端口号
        /// </summary>
        int Port { get; }
        /// <summary>
        /// IP地址
        /// </summary>
        string IpAddress { get; }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="response">异常时信息</param>
        /// <returns></returns>
        bool Connect(out string response);

        //bool Close();
        /// <summary>
        /// 写单个寄存器
        /// </summary>
        /// <param name="slaveAdd"></param>
        /// <param name="startAdd"></param>
        /// <param name="value"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        bool WriteSingleRegister(byte slaveAdd, ushort startAdd, ushort value, out string response);

        /// <summary>
        /// 写多个寄存器
        /// </summary>
        /// <param name="slaveAdd"></param>
        /// <param name="startAdd"></param>
        /// <param name="values"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        bool WriteMultipleRegisters(byte slaveAdd, ushort startAdd, ushort[] values, out string response);
        /// <summary>
        /// 读多个输入寄存器
        /// </summary>
        /// <param name="slaveAdd"></param>
        /// <param name="startAdd"></param>
        /// <param name="count"></param>
        /// <param name="values"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        bool ReadInputRegisters(byte slaveAdd, ushort startAdd, ushort count, out ushort[] values, out string response);

        /// <summary>
        /// 读多个保持寄存器
        /// </summary>
        /// <param name="slaveAdd"></param>
        /// <param name="startAdd"></param>
        /// <param name="count"></param>
        /// <param name="values"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        bool ReadHoldingRegisters(byte slaveAdd, ushort startAdd, ushort count, out ushort[] values, out string response);
    }
}
