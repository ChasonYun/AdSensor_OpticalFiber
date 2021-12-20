using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace OpticalFiber
{
    public partial class UCSysConfig : UserControl
    {
        SQL_Select sql_Select = new SQL_Select();
        SQL_Update sql_Update = new SQL_Update();
        SQL_Insert sql_Insert = new SQL_Insert();
        List<struct_DeviceEnable> struct_DeviceEnables;
        List<struct_PrtName> struct_PrtNames;

        struct_DeviceEnable struct_DeviceEnable;
        struct_PrtName struct_prtName;
        public UCSysConfig()
        {
            InitializeComponent();
          
            cbxDevice.SelectedIndex = 0;
            cbxChannel.SelectedIndex = 0;
            Init_DgvDevice();
            Init_DgvPartition();
        }

        private void Init_DgvDevice()
        {
            try
            {
                DataClass.list_DeviceEnables = sql_Select.Select_DeviceEnable();
                dgvDevice.Rows.Clear();
                struct_DeviceEnables = new List<struct_DeviceEnable>();
                //struct_DeviceEnables = ConfigClass.GetStruct_DeviceEnables();
                struct_DeviceEnables = sql_Select.Select_DeviceEnable();
                int i = 1;
                foreach (struct_DeviceEnable struct_DeviceEnable in struct_DeviceEnables)
                {
                    dgvDevice.Rows.Add(i++, struct_DeviceEnable.name, struct_DeviceEnable.ipEndPoint.Address, struct_DeviceEnable.ipEndPoint.Port, struct_DeviceEnable.enable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取探测器配置信息失败！——" + ex.Message);
            }
        }

        private void Init_DgvPartition()
        {
            try
            {
                dgvPartition.Rows.Clear();
                struct_PrtNames = sql_Select.Select_PrtName();
                foreach(struct_PrtName struct_PrtName in struct_PrtNames)
                {
                    dgvPartition.Rows.Add("设备" + struct_PrtName.deviceNo, "通道" + struct_PrtName.channelNo, struct_PrtName.prtNo, struct_PrtName.prtName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取分区配置信息失败！——" + ex.Message);
            }
        }

        private void Upate_DgvPartition(int deviceNo,int channelNo)
        {
            try
            {
                if (deviceNo == 0)
                {
                    struct_PrtNames = sql_Select.Select_PrtName();
                    dgvPartition.Rows.Clear();
                    foreach (struct_PrtName struct_PrtName in struct_PrtNames)
                    {
                        dgvPartition.Rows.Add("设备" + struct_PrtName.deviceNo, "通道" + struct_PrtName.channelNo, struct_PrtName.prtNo, struct_PrtName.prtName);
                    }
                }
                else
                {
                    struct_PrtNames = sql_Select.Select_PrtName(deviceNo, channelNo);
                    dgvPartition.Rows.Clear();
                    foreach (struct_PrtName struct_PrtName in struct_PrtNames)
                    {
                        dgvPartition.Rows.Add("设备" + struct_PrtName.deviceNo, "通道" + struct_PrtName.channelNo, struct_PrtName.prtNo, struct_PrtName.prtName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取分区配置信息失败！——" + ex.Message);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Upate_DgvPartition(cbxDevice.SelectedIndex, cbxChannel.SelectedIndex + 1);
        }

        private void dgvDevice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 5)
                    {
                       
                        if (MessageBox.Show("确定要修改设备信息吗？","操作提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            struct_DeviceEnable.deviceNo = Convert.ToInt32(dgvDevice.CurrentRow.Cells[0].Value);
                            struct_DeviceEnable.name = dgvDevice.CurrentRow.Cells[1].Value.ToString();
                            IPAddress iPAddress;
                            IPAddress.TryParse(dgvDevice.CurrentRow.Cells[2].Value.ToString(), out iPAddress);
                            int port = Convert.ToInt32(dgvDevice.CurrentRow.Cells[3].Value);
                            struct_DeviceEnable.ipEndPoint = new IPEndPoint(iPAddress, port);
                            struct_DeviceEnable.enable = Convert.ToBoolean(dgvDevice.CurrentRow.Cells[4].Value.ToString());
                            sql_Update.Update_Device(struct_DeviceEnable.deviceNo, struct_DeviceEnable);
                            MessageBox.Show("修改成功！");
                            sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = DataClass.userLevel, record = "修改设备信息" + (struct_DeviceEnable.deviceNo) });
                            Init_DgvDevice();
                        }
                        
                        //if (!Convert.ToBoolean(dgvDevice.CurrentRow.Cells[3].Value.ToString()))
                        //{
                        //    if (MessageBox.Show("确定要关闭该设备吗？","操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        //    {

                        //        struct_DeviceEnable.name = dgvDevice.CurrentRow.Cells[0].Value.ToString();
                        //        IPAddress iPAddress = IPAddress.Parse(dgvDevice.CurrentRow.Cells[1].Value.ToString());
                        //        int port = Convert.ToInt32(dgvDevice.CurrentRow.Cells[2].Value);
                        //        struct_DeviceEnable.ipEndPoint = new IPEndPoint(iPAddress, port);
                        //        struct_DeviceEnable.enable = Convert.ToBoolean(dgvDevice.CurrentRow.Cells[3].Value.ToString());
                        //        //ConfigClass.SetStruct_DeviceEnables(e.RowIndex + 1, struct_DeviceEnable);
                        //        sql_Update.Update_Device(e.RowIndex + 1, struct_DeviceEnable);
                        //        MessageBox.Show("关闭成功！");
                        //        sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = DataClass.userLevel, record = "关闭设备" + (e.RowIndex + 1) });
                        //        Init_DgvDevice();
                        //    }
                        //}
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新设备配置失败！——" + ex.Message);
            }
        }

        private void dgvPartition_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 4)
                    {
                      
                        if (MessageBox.Show("确定要修改该分区名称吗？", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            struct_prtName.deviceNo = Convert.ToInt32(dgvPartition.CurrentRow.Cells[0].Value.ToString().Split('备')[1]);
                            struct_prtName.channelNo = Convert.ToInt32(dgvPartition.CurrentRow.Cells[1].Value.ToString().Split('道')[1]);
                            struct_prtName.prtNo = Convert.ToInt32(dgvPartition.CurrentRow.Cells[2].Value);
                            struct_prtName.prtName = Convert.ToString(dgvPartition.CurrentRow.Cells[3].Value);
                            sql_Update.Update_PrtName(struct_prtName);
                            //struct_DeviceEnable.name = dgvDevice.CurrentRow.Cells[0].Value.ToString();
                            //IPAddress iPAddress = IPAddress.Parse(dgvDevice.CurrentRow.Cells[1].Value.ToString());
                            //int port = Convert.ToInt32(dgvDevice.CurrentRow.Cells[2].Value);
                            //struct_DeviceEnable.ipEndPoint = new IPEndPoint(iPAddress, port);
                            //struct_DeviceEnable.enable = Convert.ToBoolean(dgvDevice.CurrentRow.Cells[3].Value.ToString());
                            ////ConfigClass.SetStruct_DeviceEnables(e.RowIndex + 1, struct_DeviceEnable);
                            //sql_Update.Update_Device(e.RowIndex + 1, struct_DeviceEnable);
                            MessageBox.Show("修改成功！");
                            sql_Insert.Insert_Audit(new OperationRecord() { dateTime = DateTime.Now, user = DataClass.userLevel, record = "修改分区名称" });
                            Upate_DgvPartition(cbxDevice.SelectedIndex, cbxChannel.SelectedIndex + 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更分区配置失败！——" + ex.Message);
            }
        }
    }
}
