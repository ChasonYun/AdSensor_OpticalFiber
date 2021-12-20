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
    
    public partial class FrmLogin : Form
    {
        SQL_Insert sql_Insert = new SQL_Insert();
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

        DataInit dataInit = new DataInit();
        SQL_Select sql_Select = new SQL_Select();
        public FrmLogin()
        {
            InitializeComponent();
            cbxUser.SelectedIndex = 0;
        }

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }

        private void lblTiltle_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }

        private void lblName_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (cbxUser.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户！");
                return;
            }
            if (string.IsNullOrEmpty(tbxPwd.Text))
            {
                MessageBox.Show("密码不能为空！");
                return;
            }
            switch (cbxUser.SelectedIndex)
            {
                case 0:
                    if (sql_Select.Select_Pwd(0) == MD5.GetMD5Hash(tbxPwd.Text))
                    {
                        this.DialogResult = DialogResult.OK;
                        DataClass.userLevel = 0;
                        this.Close();
                        sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = 0, record = "登录系统" });
                    }
                    else
                    {
                        MessageBox.Show("密码错误！");
                        tbxPwd.SelectAll();
                    }
                    break;
                case 1:
                    if (sql_Select.Select_Pwd(1) == MD5.GetMD5Hash(tbxPwd.Text))
                    {
                        this.DialogResult = DialogResult.OK;
                        DataClass.userLevel = 1;
                        this.Close();
                        sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = 1, record = "登录系统" });
                    }
                    else
                    {
                        MessageBox.Show("密码错误！");
                        tbxPwd.SelectAll();
                    }
                    break;
                case 2:
                    if (sql_Select.Select_Pwd(2) == MD5.GetMD5Hash(tbxPwd.Text))
                    {
                        this.DialogResult = DialogResult.OK;
                        DataClass.userLevel = 2;
                        this.Close();
                        sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = 2, record = "登录系统" });
                    }
                    else
                    {
                        MessageBox.Show("密码错误！");
                        tbxPwd.SelectAll();
                    }
                    break;
            }
        }

        private void pbxUser_MouseDown(object sender, MouseEventArgs e)
        {
            Frm_Move();
        }
     
    }
}
