using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticalFiber
{
    public partial class FrmInitDB : Form
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
        public FrmInitDB()
        {
            try
            {
                InitializeComponent();
                InitBackGroundWorker();
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库初始化异常！" + ex.Message + ex.StackTrace);
            }
           
        }

        private void InitBackGroundWorker()
        {
            try
            {
                backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
                backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
                backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库初始化异常！" + ex.Message + ex.StackTrace);
            }
           
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                progressBar1.Value = e.ProgressPercentage;
                lblPercent.Text = "已完成：" + progressBar1.Value.ToString() + "%";
                lblWorker.Text = e.UserState.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库初始化异常！" + ex.Message + ex.StackTrace);
            }
           
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                e.Result = new SQL_Create(worker);
            }
            catch (Exception ex)
            {

                MessageBox.Show("数据库初始化异常！" + ex.Message + ex.StackTrace);
            }
          
        }

        private void FrmInitDB_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
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
