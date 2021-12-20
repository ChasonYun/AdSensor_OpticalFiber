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
    public partial class FrmPwdManagement : Form
    {
        SQL_Update sql_Update = new SQL_Update();

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

        public FrmPwdManagement()
        {
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangeUserPwd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbxUser.Text))
                {
                    MessageBox.Show("新密码不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(tbxUser_.Text))
                {
                    MessageBox.Show("确认密码不能为空！");
                    return;

                }
                if (tbxUser.Text != tbxUser_.Text)
                {
                    MessageBox.Show("请输入相同的密码！");
                    return;
                }
                sql_Update.Update_Pwd("generalUser", tbxUser_.Text);
                MessageBox.Show("修改成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeOperatorPwd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbxOperator.Text))
                {
                    MessageBox.Show("新密码不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(tbxOperator_.Text))
                {
                    MessageBox.Show("确认密码不能为空！");
                    return;

                }
                if (tbxOperator.Text != tbxOperator_.Text)
                {
                    MessageBox.Show("请输入相同的密码！");
                    return;
                }
                sql_Update.Update_Pwd("generalUser", tbxOperator_.Text);
                MessageBox.Show("修改成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeAdminPwd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbxAdmin.Text))
                {
                    MessageBox.Show("新密码不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(tbxAdmin_.Text))
                {
                    MessageBox.Show("确认密码不能为空！");
                    return;

                }
                if (tbxAdmin.Text != tbxAdmin_.Text)
                {
                    MessageBox.Show("请输入相同的密码！");
                    return;
                }
                sql_Update.Update_Pwd("generalUser", tbxAdmin_.Text);
                MessageBox.Show("修改成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
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
