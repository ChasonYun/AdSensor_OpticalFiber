using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NModbus;
using ModBus.ModBus;
using System.Net.Sockets;

namespace ModBus
{
    public class ModBusTcp : IModBus
    {
        int port;
        string ip;
        IModbusMaster modbusMaster;

        public bool IsConnect
        {
            get
            {
                if (modbusMaster == null) return false;
                return true;
            }
        }
        public int Port { get => port; }
        public string IpAddress { get => ip; }


        public ModBusTcp(string ip, int port)
        {
            try
            {
                this.port = port;
                this.ip = ip;
            }
            catch (Exception ex)
            {

            }

        }

        public bool Close()
        {
            try
            {
                modbusMaster.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Connect(out string response)
        {
            response = string.Empty;
            try
            {
                modbusMaster = new ModbusFactory().CreateMaster(new TcpClient(ip, port));
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }

        public bool ReadInputRegisters(byte slaveAdd, ushort startAdd, ushort count, out ushort[] values, out string response)
        {
            values = null;
            response = string.Empty;
            try
            {
                values = modbusMaster.ReadInputRegisters(slaveAdd, startAdd, count);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }

        public bool ReadHoldingRegisters(byte slaveAdd, ushort startAdd, ushort count, out ushort[] values, out string response)
        {
            values = null;
            response = string.Empty;
            try
            {
                values = modbusMaster.ReadHoldingRegisters(slaveAdd, startAdd, count);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }

        public bool WriteMultipleRegisters(byte slaveAdd, ushort startAdd, ushort[] values, out string response)
        {
            response = string.Empty;
            try
            {
                modbusMaster.WriteMultipleRegisters(slaveAdd, startAdd, values);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }

        public bool WriteSingleRegister(byte slaveAdd, ushort startAdd, ushort value, out string response)
        {
            response = string.Empty;
            try
            {
                modbusMaster.WriteSingleRegister(slaveAdd, startAdd, value);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }
    }
}
