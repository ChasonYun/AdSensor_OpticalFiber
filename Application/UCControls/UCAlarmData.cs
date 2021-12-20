using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpticalFiber
{
    public partial class UCAlarmData : UserControl
    {
        SQL_Select sql_Select = new SQL_Select();
        List<Struct_AlarmMsg> list_Temp = new List<Struct_AlarmMsg>();
        public UCAlarmData()
        {
            InitializeComponent();
            dtpStart.MaxDate = DateTime.Now.Date;

            dtpEnd.MaxDate = DateTime.Now.Date;

            cbxAlarmType.SelectedIndex = 0;
            list_Temp = sql_Select.Select_Alarm(cbxAlarmType.SelectedIndex, dtpStart.Value.Date.ToShortDateString(), dtpStart.Value.AddDays(1).Date.ToShortDateString());
            UpdateDgv();
        }

        private void UpdateDgv()
        {
            try
            {
                int i = 1;
                dgvAlarmData.Rows.Clear();
                foreach(Struct_AlarmMsg AlarmMsg in list_Temp)
                {
                    dgvAlarmData.Rows.Add(i++, AlarmMsg.DateTime, AlarmMsg.DeviceNo, AlarmMsg.ChannelNo, AlarmMsg.PartitionNo, AlarmMsg.Position + "米", AlarmMsg.Illustrate, AlarmMsg.Relay, AlarmMsg.Type, AlarmMsg.AlarmValue, AlarmMsg.Threshold);
                }
            }
            catch (Exception  e)
            {
                DataClass.ShowErrMsg("查询报警记录异常" + e.Message);
            }
        }

        private void btnQuary_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpEnd.Value < dtpStart.Value)
                {
                    MessageBox.Show("开始时间小于结束时间！");
                    return;
                }
                list_Temp = sql_Select.Select_Alarm(cbxAlarmType.SelectedIndex, dtpStart.Value.Date.ToShortDateString(), dtpEnd.Value.AddDays(1).Date.ToShortDateString());

            }
            catch (Exception ex)
            {

                DataClass.ShowErrMsg("查询报警记录异常" + ex.Message);
            }
            finally
            {
                UpdateDgv();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string[] filePath = null;
            filePath = ShowSaveFileDialog();
            if (filePath[0] != null && filePath[1] != null)
            {
                DataTable dt = new DataTable();
                // 列强制转换
                for (int count = 0; count < dgvAlarmData.Columns.Count; count++)
                {
                    DataColumn dc = new DataColumn(dgvAlarmData.Columns[count].Name.ToString());
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int count = 0; count < dgvAlarmData.Rows.Count; count++)
                {
                    DataRow dr = dt.NewRow();
                    for (int countsub = 0; countsub < dgvAlarmData.Columns.Count; countsub++)
                    {
                        dr[countsub] = Convert.ToString(dgvAlarmData.Rows[count].Cells[countsub].Value);
                    }
                    dt.Rows.Add(dr);
                }
                dt.Columns[0].ColumnName = "序号";
                dt.Columns[1].ColumnName = "时间";
                dt.Columns[2].ColumnName = "设备";
                dt.Columns[3].ColumnName = "通道";
                dt.Columns[4].ColumnName = "分布";
                dt.Columns[5].ColumnName = "位置";
                dt.Columns[6].ColumnName = "说明";
                dt.Columns[7].ColumnName = "继电器";
                dt.Columns[8].ColumnName = "类型";
                dt.Columns[9].ColumnName = "报警值";
                dt.Columns[10].ColumnName = "限定值";
                Excel.ExportToExcel(dt, filePath[0], filePath[1]);
                MessageBox.Show("导出成功！");
            }
        }

        private string[] ShowSaveFileDialog()
        {
            string localFilePath = "";
            string[] strPathFile = new string[2];
            //string localFilePath, fileNameExt, newFileName, FilePath; 
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel表格（*.xls）|*.xls";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString();
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                string path = localFilePath.Substring(0, localFilePath.LastIndexOf("\\") + 1);
                strPathFile[0] = path;
                strPathFile[1] = fileNameExt;
            }
            return strPathFile;
        }
    }
}
