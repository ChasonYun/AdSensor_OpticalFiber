using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyModbus;
using ModBus.ModBus;

namespace ModBus
{
    public class ModBusTcp : IModBus
    {
        int port;
        string ip;
        ModbusClient modbusClient;

        public bool IsConnect { get => modbusClient.Connected; }
        public int Port { get => port; }
        public string IpAddress { get => ip; }


        public ModBusTcp(string ip, int port)
        {
            this.port = port;
            this.ip = ip;
            modbusClient = new ModbusClient();
        }

        public bool Close()
        {
            try
            {
                modbusClient.Disconnect();
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
                modbusClient.Connect(ip, port);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }

        public bool ReadInputRegisters(int startAdd, int count, out int[] values, out string response)
        {
            values = null;
            response = string.Empty;
            try
            {
                values = modbusClient.ReadInputRegisters(startAdd, count);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }

        public bool WriteMultipleRegisters(int startAdd, int[] values, out string response)
        {
            response = string.Empty;
            try
            {
                modbusClient.WriteMultipleRegisters(startAdd, values);
                return true;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return false;
            }
        }

        public bool WriteSingleRegister(int startAdd, int value, out string response)
        {
            response = string.Empty;
            try
            {
                modbusClient.WriteSingleRegister(startAdd, value);
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
