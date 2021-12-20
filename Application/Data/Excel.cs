using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

namespace OpticalFiber
{
    class Excel
    {

        public static string ExportToExcel(System.Data.DataTable dt, string path, string filename)
        {
            KillSpecialExcel();
            string result = string.Empty;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                StreamWriter sw = new StreamWriter(Path.Combine(path,filename), false, Encoding.GetEncoding("gb2312"));
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    sb.Append(dt.Columns[k].ColumnName.ToString() + "\t");
                }
                sb.Append(Environment.NewLine);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sb.Append(row[j].ToString() + "\t");
                    }
                    sb.Append(Environment.NewLine);
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
            catch (Exception ex)
            {
                result = "请保存或关闭可能已打开的Excel文件" + ex.Message + ex.StackTrace;
            }
            finally
            {
                dt.Dispose();
            }
            return result;
        }
        private static void KillSpecialExcel()
        {
            foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                if (!theProc.HasExited)
                {
                    bool b = theProc.CloseMainWindow();
                    if (b == false)
                    {
                        theProc.Kill();
                    }
                    theProc.Close();
                }
            }
        }

    }
}
