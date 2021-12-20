using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Authorize;

namespace OpticalFiber
{
    public partial class FrmAuthorize : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void Frm_Move()
        {
            int WM_SYSCOMMAND = 0x0112;
            int SC_MOVE = 0xF010;
            int HTCAPTION = 0x0002;
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        Authorized authorized = new Authorized();
        public FrmAuthorize()
        {
            InitializeComponent();
            tbxCode.Text = authorized.GetCode();
        }

        private void btnAuthorize_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbxAuthorize.Text == GetAuthorizeCode(tbxCode.Text))
                {
                    MessageBox.Show("激活成功，欢迎使用！");
                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configuration.AppSettings.Settings["code"].Value = tbxAuthorize.Text;
                    configuration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("AppSettings");
                    this.Close();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("激活失败，请联系技术人员！");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GetAuthorizeCode(string code)
        {
            string AuthorizeCode = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(code);
                string CRC_1 = Encoding.Default.GetString(CRC16.crc16(bytes, bytes.Length));
                Array.Reverse(code.ToCharArray());
                bytes = Encoding.Default.GetBytes(code);
                string CRC_2 = Encoding.Default.GetString(CRC16.crc16(bytes, bytes.Length));
                AuthorizeCode = authorized.GetMD5Hash(CRC_1 + CRC_2);
                int count = 0;
                foreach (char i in authorized.GetMD5Hash(AuthorizeCode).ToUpper())
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }
    }
}
