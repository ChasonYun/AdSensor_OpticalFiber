using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace OpticalFiber
{
    public class ConfigClass
    {
   
        /// <summary>
        /// 获取保存的串口参数
        /// </summary>
        /// <param name="Parity"></param>
        /// <param name="DataBits"></param>
        /// <param name="StopBits"></param>
        /// <param name="PortName"></param>
        /// <param name="BaudRate"></param>
        //public static void Get_PortParam(out int Parity, out int DataBits, out int StopBits, out string PortName, out int BaudRate)
        //{
        //    Parity = Convert.ToInt32(ConfigurationManager.AppSettings["Parity"]);
        //    DataBits = Convert.ToInt32(ConfigurationManager.AppSettings["DataBits"]);
        //    StopBits = Convert.ToInt32(ConfigurationManager.AppSettings["StopBits"]);
        //    PortName = Convert.ToString(ConfigurationManager.AppSettings["PortName"]);
        //    BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["BaudRate"]);
        //}

        /// <summary>
        /// 更新配置文件的串口参数
        /// </summary>
        /// <param name="Parity"></param>
        /// <param name="DataBits"></param>
        /// <param name="StopBits"></param>
        /// <param name="PortName"></param>
        /// <param name="BaudRate"></param>
        //public static void Set_PortParam(int Parity, int DataBits, int StopBits, string PortName, int BaudRate)
        //{
        //    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    configuration.AppSettings.Settings["Parity"].Value = Parity.ToString();
        //    configuration.AppSettings.Settings["DataBits"].Value = DataBits.ToString();
        //    configuration.AppSettings.Settings["StopBits"].Value = StopBits.ToString();
        //    configuration.AppSettings.Settings["PortName"].Value = PortName.ToString();
        //    configuration.AppSettings.Settings["BaudRate"].Value = BaudRate.ToString();
        //    configuration.Save(ConfigurationSaveMode.Modified);
        //    ConfigurationManager.RefreshSection("AppSettings");
        //}

        /// <summary>
        /// 获取 配置文件中的IP地址端口号 的信息
        /// </summary>
        /// <returns></returns>
        //public static IPEndPoint Get_TCPParam()
        //{
        //    try
        //    {
        //        IPEndPoint endPoint;
        //        IPAddress iPAddress = IPAddress.Parse(ConfigurationManager.AppSettings["IPAddress"]);
        //        endPoint = new IPEndPoint(iPAddress, Convert.ToInt32(ConfigurationManager.AppSettings["Port"]));
        //        return endPoint;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("获取配置文件IP信息错误！" + ex.Message);
        //    }
        //}

        /// <summary>
        /// 设置 配置文件中IP地址 端口号的信息 
        /// </summary>
        /// <param name="endPoint"></param>
        //public  static void Set_TCPParam(IPEndPoint endPoint)
        //{
        //    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    configuration.AppSettings.Settings["IPAddress"].Value = endPoint.Address.ToString();
        //    configuration.AppSettings.Settings["Port"].Value = endPoint.ToString();
        //    configuration.Save(ConfigurationSaveMode.Modified);
        //    ConfigurationManager.RefreshSection("AppSettings");
        //}

        //public static List<struct_DeviceEnable> GetStruct_DeviceEnables()
        //{
        //    List<struct_DeviceEnable> struct_DeviceEnables = new List<struct_DeviceEnable>();
        //    struct_DeviceEnable deviceEnable;
        //    IPAddress iPAddress;
        //    int port;
        //    try
        //    {
        //        for (int i = 1; i <= 8; i++)
        //        {
        //            deviceEnable.enable = Convert.ToBoolean(Convert.ToInt32(ConfigurationManager.AppSettings["device" + i + "_Enable"]));
        //            iPAddress = IPAddress.Parse(ConfigurationManager.AppSettings["device" + i + "_IP"]);
        //            port = Convert.ToInt32(ConfigurationManager.AppSettings["device" + i + "_Port"]);
        //            deviceEnable.ipEndPoint = new IPEndPoint(iPAddress, port);
        //            deviceEnable.name = Convert.ToString(ConfigurationManager.AppSettings["device" + i + "_Name"]);
        //            struct_DeviceEnables.Add(deviceEnable);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("获取设备配置文件失败！——" + ex.Message);
        //    }
           
           
        //    return struct_DeviceEnables;
        //}

        public static void SetStruct_DeviceEnables(int i, struct_DeviceEnable struct_DeviceEnables)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["device" + i + "_Enable"].Value = (Convert.ToInt32(struct_DeviceEnables.enable).ToString());
                configuration.AppSettings.Settings["device" + i + "_IP"].Value = struct_DeviceEnables.ipEndPoint.Address.ToString();
                configuration.AppSettings.Settings["device" + i + "_Port"].Value = struct_DeviceEnables.ipEndPoint.Port.ToString();
                configuration.AppSettings.Settings["device" + i + "_Name"].Value = struct_DeviceEnables.name.ToString();
                configuration.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("AppSettings");
            }
            catch (Exception ex)
            {
                throw new Exception("保存设备配置文件失败！——" + ex.Message);
            }
        }

        public static string Get_ProjName()
        {
            string prjName = null;
            try
            {
                prjName = ConfigurationManager.AppSettings["proj_Name"].ToString();
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg(ex.Message);
            }
            return prjName;
        }

        public static void Set_ProjName(string projName)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["proj_Name"].Value = projName;
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("AppSettings");
        }

        public static byte[] ImgToBytes(Image image)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Bmp);
            byte[] bytes = new byte[memoryStream.Length];
            try
            {
                memoryStream.Position = 0;
                memoryStream.Read(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                memoryStream.Close();
            }
            return bytes;
        }

        public static byte[] ImgToBytes(string path)
        {
            Bitmap bitmap = new Bitmap(path);
            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Bmp);
            byte[] bytes = new byte[memoryStream.Length];
            try
            {
                memoryStream.Position = 0;
                memoryStream.Read(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                memoryStream.Close();
            }
            return bytes;
        }

        public static Image BytesToImg(byte[] bytes)
        {
            MemoryStream memoryStream = new MemoryStream();
            Image image;
            try
            {
                memoryStream = new MemoryStream(bytes);
                image = Image.FromStream(memoryStream);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                memoryStream.Close();
            }
            return image;

        }
    }
}
