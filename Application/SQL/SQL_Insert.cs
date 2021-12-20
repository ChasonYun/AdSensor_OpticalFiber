using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticalFiber
{
    class SQL_Insert
    {
        MySqlCommand cmd;
        MySqlConnection conn;


        public void Insert_Alarm(Struct_AlarmMsg alarmMsg)
        {
            try
            {
                //主键消除重复
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "insert ignore into  alarm(datetime,deviceNo,channelNo,partitionNo,position,illustrate,relay,type,alarmvalue,threshold) values ('" + alarmMsg.DateTime + "','" + alarmMsg.DeviceNo + "','" + alarmMsg.ChannelNo + "','" + alarmMsg.PartitionNo + "','" + alarmMsg.Position + "','" + alarmMsg.Illustrate + "','" + alarmMsg.Relay + "','" + alarmMsg.Type + "','" + alarmMsg.AlarmValue + "', '" + alarmMsg.Threshold + "')";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("告警信息插入失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
        }

        public void Insert_Audit(OperationRecord OpRecord)
        {
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "insert  into  audit(datetime,user,record) values ('" + OpRecord.dateTime + "','" + OpRecord.user + "','" + OpRecord.record + "')";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("操作信息插入失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
        }
    }
}
