using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Authorize;

namespace OpticalFiber
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmMain());
            //FrmLogin frmLogin = new FrmLogin();
            //if (frmLogin.ShowDialog() == DialogResult.OK)
            //{
            //    Application.Run(new FrmMain());

            //}
            //FrmInitDB frmInitDB = new FrmInitDB();
            //if (frmInitDB.ShowDialog() == DialogResult.OK)
            //{
            //    Application.Run(new FrmLogin());
            //}


            bool ret;
            Mutex mutex = new Mutex(true, Application.ProductName, out ret);
            if (ret)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
#else
                if ( ConfigurationManager.AppSettings["code"].ToString()!= GetAuthorizeCode(new Authorized().GetCode()))
                {
                    FrmAuthorize frmAuthorize = new FrmAuthorize();
                    if (frmAuthorize.ShowDialog() == DialogResult.OK)
                    {

                    }
                    else
                    {
                        return;
                    }
                }
             
#endif
                FrmInitDB frmInitDB = new FrmInitDB();
                if (frmInitDB.ShowDialog() == DialogResult.OK)
                {
                    FrmLogin frmLogin = new FrmLogin();
                    if (frmLogin.ShowDialog()== DialogResult.OK)
                    {
                        Application.Run(new FrmMain());
                    }
                }
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("软件运行中，请忽重复运行！");
                Application.Exit();
            }
        }

        private static string GetAuthorizeCode(string code)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                string AuthorizeCode = string.Empty;
                byte[] bytes = Encoding.Default.GetBytes(code);
                string CRC_1 = Encoding.Default.GetString(CRC16.crc16(bytes, bytes.Length));
                Array.Reverse(code.ToCharArray());
                bytes = Encoding.Default.GetBytes(code);
                string CRC_2 = Encoding.Default.GetString(CRC16.crc16(bytes, bytes.Length));
                AuthorizeCode = MD5.GetMD5Hash(CRC_1 + CRC_2);
                int count = 0;
                foreach (char i in MD5.GetMD5Hash(AuthorizeCode).ToUpper())
                {
                    count++;
                    if (count % 2 != 0)
                    {
                        stringBuilder.Append(i);
                    }
                }
                return stringBuilder.ToString();
            }
            catch (Exception)
            {
                throw new Exception("获取硬件信息失败");
            }
        }
    }
}
