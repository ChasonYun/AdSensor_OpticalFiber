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
    public partial class UCAudit : UserControl
    {
        SQL_Select sql_Select = new SQL_Select();
        List<OperationRecord> list_Temp = new List<OperationRecord>();
        public UCAudit()
        {
            InitializeComponent();
            dtpStart.MaxDate = DateTime.Now.Date;

            dtpEnd.MaxDate = DateTime.Now.Date;
            list_Temp = sql_Select.Select_OpRecord(dtpStart.Value.Date.ToShortDateString(), dtpStart.Value.AddDays(1).Date.ToShortDateString());
            UpdateDgv();
        }

        private void UpdateDgv()
        {
            try
            {
                int i = 1;
                dgvAdudit.Rows.Clear();
                foreach (OperationRecord temp in list_Temp)
                {
                    if (temp.user == 0)
                    {
                        dgvAdudit.Rows.Add(i++, temp.dateTime, "普通用户", temp.record);
                    }
                    if (temp.user == 1)
                    {
                        dgvAdudit.Rows.Add(i++, temp.dateTime, "系统操作员", temp.record);
                    }
                    if (temp.user == 2)
                    {
                        dgvAdudit.Rows.Add(i++, temp.dateTime, "系统管理员", temp.record);
                    }
                }
            }
            catch (Exception e)
            {
                DataClass.ShowErrMsg("查询操作记录异常" + e.Message);
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
                list_Temp = sql_Select.Select_OpRecord(dtpStart.Value.Date.ToShortDateString(), dtpStart.Value.AddDays(1).Date.ToShortDateString());

            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("查询操作记录异常" + ex.Message);
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
                for (int count = 0; count < dgvAdudit.Columns.Count; count++)
                {
                    DataColumn dc = new DataColumn(dgvAdudit.Columns[count].Name.ToString());
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int count = 0; count < dgvAdudit.Rows.Count; count++)
                {
                    DataRow dr = dt.NewRow();
                    for (int countsub = 0; countsub < dgvAdudit.Columns.Count; countsub++)
                    {
                        dr[countsub] = Convert.ToString(dgvAdudit.Rows[count].Cells[countsub].Value);
                    }
                    dt.Rows.Add(dr);
                }
                dt.Columns[0].ColumnName = "序号";
                dt.Columns[1].ColumnName = "时间";
                dt.Columns[2].ColumnName = "用户";
                dt.Columns[3].ColumnName = "操作记录";
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
