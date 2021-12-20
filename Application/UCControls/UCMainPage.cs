using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace OpticalFiber
{
    public partial class UCMainPage : UserControl
    {
        SQL_Select sql_Select = new SQL_Select();
        SQL_Update sqlUpdate = new SQL_Update();
        SQL_Insert sql_Insert = new SQL_Insert();

        List<struct_ChannelMsg> struct_ChannelMsgs;//数据库中保存的所有的分区信息

        List<struct_ChannelMsg> list_DisPlayMsgs=new List<struct_ChannelMsg>();//需要展示的   启用的 添加的

        List<struct_ChannelMsg> list_NewAdd = new List<struct_ChannelMsg>();
        struct_ChannelMsg struct_ChannelMsg;

        int mode;
        int locationx = 50;
        int locationy = 50;
        public UCMainPage(int mode)
        {
            InitializeComponent();
            this.mode = mode;
            switch (mode)
            {
                case 0://设置模式
                    spcPagePic.Panel2Collapsed = false;
                    cbxDeviceNo.SelectedIndex = 0;
                    cbxChannelNo.SelectedIndex = 0;
                    break;
                case 1://运行模式
                    spcPagePic.Panel2Collapsed = true;
                    break;
            }
            Update_Pic();
            DisPlayMsg();
            Update_PrjName();
        }

        private void Update_PrjName()
        {
            try
            {
                tbxPrjName.Text = sql_Select.Select_PrjName(1);
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("项目名称显示失败！" + ex.Message + ex.StackTrace);
            }
        }

        public void Update_Pic()
        {
            try
            {
                pnlMainPage.BackgroundImage = ConfigClass.BytesToImg(sql_Select.Select_PagePic(1));
            }
            catch (Exception ex)
            {

                MessageBox.Show("获取主页图片失败！——" + ex.Message);
            }
        }

        private void Init_List()
        {
            struct_ChannelMsgs = sql_Select.Select_ChannelMsg();
            list_NewAdd.Clear();
            list_DisPlayMsgs.Clear();
            foreach (struct_ChannelMsg struct_ChannelMsg in struct_ChannelMsgs)
            {
                if (struct_ChannelMsg.isEnable)
                {
                    list_DisPlayMsgs.Add(struct_ChannelMsg);
                }
            }
        }
     

        private void PnlAdd_Msg(int deviceNo,int channelNo)
        {
            UCChannelMsg uCChannelMsg = new UCChannelMsg(cbxDeviceNo.SelectedIndex+1, cbxChannelNo.SelectedIndex+1, mode)
            {
                ChannelName = tbxChannelName.Text,
                DeviceNo = deviceNo,
                ChannelNo = channelNo

            };
            uCChannelMsg.BackColor = Color.Green;
            uCChannelMsg.Location = new Point(locationx += 40, locationy += 40);
            pnlMainPage.Controls.Add(uCChannelMsg);

        }

        /// <summary>
        /// 新增 通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbxChannelName.Text))
                {
                    MessageBox.Show("名称不可以空！");
                    return;
                }
                if (Encoding.Default.GetBytes(tbxChannelName.Text).Length >= 32)
                {
                    MessageBox.Show("名称长度过长！不能大于32个字符");
                    return;
                }
                foreach (struct_ChannelMsg struct_ChannelMsg in list_DisPlayMsgs)
                {
                    if (struct_ChannelMsg.deviceNo == cbxDeviceNo.SelectedIndex + 1 && struct_ChannelMsg.channelNo == cbxChannelNo.SelectedIndex + 1)
                    {
                        MessageBox.Show("该分区已经存在！");
                        return;
                       //if(MessageBox.Show("该分区已经存在！您是要修改该分区名称吗？","操作提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
                       //{

                        //}
                        //else
                        //{
                        //    return;
                        //}
                    }
                }
                foreach (struct_ChannelMsg struct_ChannelMsg in list_NewAdd)
                {
                    if (struct_ChannelMsg.deviceNo == cbxDeviceNo.SelectedIndex + 1 && struct_ChannelMsg.channelNo == cbxChannelNo.SelectedIndex + 1)
                    {
                        MessageBox.Show("该分区已经存在！");
                        return;
                      
                    }
                }
                struct_ChannelMsg.deviceNo = cbxDeviceNo.SelectedIndex + 1;
                struct_ChannelMsg.channelNo = cbxChannelNo.SelectedIndex + 1;
                struct_ChannelMsg.channelName = tbxChannelName.Text;
                list_NewAdd.Add(struct_ChannelMsg);
                PnlAdd_Msg(cbxDeviceNo.SelectedIndex + 1, cbxChannelNo.SelectedIndex + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败！——" + ex.Message);
            }
           
        }

        private void cbxDeviceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxChannelName.Text = "设备" + (cbxDeviceNo.SelectedIndex + 1) + "通道" + (cbxChannelNo.SelectedIndex + 1);
        }

        private void cbxChannelNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxChannelName.Text = "设备" + (cbxDeviceNo.SelectedIndex + 1) + "通道" + (cbxChannelNo.SelectedIndex + 1);
        }

        private void DisPlayMsg()
        {
            try
            {
                Init_List();
                pnlMainPage.Controls.Clear();
                foreach (struct_ChannelMsg struct_ChannelMsg in list_DisPlayMsgs)
                {
                    UCChannelMsg uCChannelMsg = new UCChannelMsg(struct_ChannelMsg.deviceNo, struct_ChannelMsg.channelNo, mode)
                    {
                        ChannelName = struct_ChannelMsg.channelName,
                        DeviceNo = struct_ChannelMsg.deviceNo,
                        ChannelNo = struct_ChannelMsg.channelNo
                    };
                    uCChannelMsg.getChannelMsg += new GetChannelMsg(GetMsg);
                    uCChannelMsg.IsEnable = struct_ChannelMsg.isEnable;
                    uCChannelMsg.Location = new Point((int)((double)struct_ChannelMsg.locationX * (double)pnlMainPage.Width), (int)((double)struct_ChannelMsg.locationY * (double)pnlMainPage.Height));
                    if (uCChannelMsg.IsEnable)
                    {
                        pnlMainPage.Controls.Add(uCChannelMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        private void Update_ChannelMsgName(int decviceNo,int channelNo)
        {
            try
            {
                for(int i=0;i< list_DisPlayMsgs.Count; i++)
                {
                    struct_ChannelMsg = list_DisPlayMsgs[i];
                    if (struct_ChannelMsg.deviceNo == cbxDeviceNo.SelectedIndex + 1 && struct_ChannelMsg.channelNo == cbxChannelNo.SelectedIndex + 1)
                    {
                        struct_ChannelMsg.channelName = tbxChannelName.Text;
                        list_DisPlayMsgs[i] = struct_ChannelMsg;
                    }
                }
                foreach (struct_ChannelMsg struct_ChannelMsg in list_DisPlayMsgs)
                {
                    
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("修改名称失败！" + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                list_DisPlayMsgs.Clear();
                foreach (Control control in pnlMainPage.Controls)
                {
                    UCChannelMsg channelMsg = (UCChannelMsg)control;
                    struct_ChannelMsg.deviceNo = channelMsg.DeviceNo;
                    struct_ChannelMsg.channelNo = channelMsg.ChannelNo;
                    if(cbxDeviceNo.SelectedIndex+1 == struct_ChannelMsg.deviceNo&&cbxChannelNo.SelectedIndex+1== struct_ChannelMsg.channelNo)
                    {
                        channelMsg.ChannelName = tbxChannelName.Text;
                    }
                    struct_ChannelMsg.channelName = channelMsg.ChannelName;
                    struct_ChannelMsg.locationX = (double)channelMsg.Location.X / (double)pnlMainPage.Width;
                    struct_ChannelMsg.locationY = (double)channelMsg.Location.Y / (double)pnlMainPage.Height;
                    if (struct_ChannelMsg.locationX < 0)
                    {
                        struct_ChannelMsg.locationX = 0;
                    }
                    if (struct_ChannelMsg.locationY < 0)
                    {
                        struct_ChannelMsg.locationY = 0;
                    }
                    if (struct_ChannelMsg.locationY > 0.92)
                    {
                        struct_ChannelMsg.locationY = 0.92;
                    }
                    struct_ChannelMsg.isEnable = channelMsg.IsEnable;
                   
                    list_DisPlayMsgs.Add(struct_ChannelMsg);
                }
                foreach (struct_ChannelMsg struct_ChannelMsg in list_DisPlayMsgs)
                {
                    sqlUpdate.Update_ChannelMsg(struct_ChannelMsg);
                }
                DisPlayMsg();
                sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = DataClass.userLevel, record = "修改通道信息" });
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！——" + ex.Message);
            }
        }

        private void btnChangePic_Click(object sender, EventArgs e)
        {
            string path;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JPG图片(*.jpg)|*.jpg|PNG图片（*.png）|*.png|BMP图片(*.bmp)|*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog.FileName;
                    if (string.IsNullOrEmpty(path))
                    {
                        MessageBox.Show("请选择正确的图片路径！");
                        return;
                    }
                    sqlUpdate.Update_PagePic(1, ConfigClass.ImgToBytes(path));
                    sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = DataClass.userLevel, record = "更换主页图片" });
                    MessageBox.Show("更换成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更换失败！——" + ex.Message);
            }
            finally
            {
                Update_Pic();
            }
        }

        private void GetMsg( int deviceNo, int channelNo,string channelName)
        {
            cbxDeviceNo.SelectedIndex = deviceNo - 1;
            cbxChannelNo.SelectedIndex = channelNo - 1;
            tbxChannelName.Text = channelName;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (Control control in pnlMainPage.Controls)
            {
                UCChannelMsg channelMsg = (UCChannelMsg)control;
                if (cbxDeviceNo.SelectedIndex + 1 == channelMsg.DeviceNo && cbxChannelNo.SelectedIndex + 1 == channelMsg.ChannelNo)
                {
                    channelMsg.BackColor = Color.Red;
                    channelMsg.IsEnable = false;
                    MessageBox.Show("删除成功！");
                    return;
                }
            }
            MessageBox.Show("通道不存在！");
        }

        private void btnChangePrjName_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbxPrjName.Text))
                {
                    MessageBox.Show("项目名称不能为空！");
                    return;
                }
                if(!Regex.IsMatch(tbxPrjName.Text, @"^[\u4e00-\u9fa5_a-zA-Z0-9]+$"))
                {
                    MessageBox.Show("非法的名称！只允许包含汉字、数字、字母、下划线。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxPrjName.SelectAll();
                    return;
                }
                if (tbxPrjName.Text.Trim().Length > 32)
                {
                    MessageBox.Show("名称过长，必须在32字以内！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                sqlUpdate.Update_PrjName(1, tbxPrjName.Text);
                sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = DataClass.userLevel, record = "修改项目名称" + tbxPrjName.Text });
                MessageBox.Show("修改成功！");
            }
            catch (Exception ex)
            {

                DataClass.ShowErrMsg("修改项目名称失败！" + ex.Message + ex.StackTrace);
            }
            finally
            {
                DataClass.projName = sql_Select.Select_PrjName(1);
            }
        }

       
    }
}
