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

        bool Close();
        /// <summary>
        /// 写单个寄存器
        /// </summary>
        /// <param name="startAdd">开始地址</param>
        /// <param name="value">值</param>
        /// <param name="response">异常时 信息</param>
        /// <returns></returns>
        bool WriteSingleRegister(int startAdd, int value, out string response);

        /// <summary>
        /// 写多个寄存器
        /// </summary>
        /// <param name="startAdd">开始地址</param>
        /// <param name="values">值</param>
        /// <param name="response">异常时 信息</param>
        /// <returns></returns>
        bool WriteMultipleRegisters(int startAdd, int[] values, out string response);
        /// <summary>
        /// 读多个寄存器
        /// </summary>
        /// <param name="startAdd">开始地址</param>
        /// <param name="count">数量</param>
        /// <param name="values">值</param>
        /// <param name="response">异常时 信息</param>
        /// <returns></returns>
        bool ReadInputRegisters(int startAdd, int count, out int[] values, out string response);
    }
}
