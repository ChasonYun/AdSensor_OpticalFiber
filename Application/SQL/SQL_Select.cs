using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace OpticalFiber
{
    public class SQL_Select
    {
        MySqlCommand cmd;
        MySqlConnection conn;

        /// <summary>
        /// 查询 项目名称
        /// </summary>
        /// <param name="prjId"></param>
        /// <returns></returns>
        public string Select_PrjName(int prjId)
        {
            string name = null;
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "select * from prjname where prjId='" + prjId + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader mySqlDataReader;
                conn.Open();
                mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    name = mySqlDataReader.GetValue(1).ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取项目名称失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return name;
        }

        /// <summary>
        /// 查询密码 MD5值
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string Select_Pwd(int userName)
        {
            string password = null;
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = null;
                switch (userName)
                {
                    case 0:
                        sql = "select passWord from pwd where userName='generalUser'";
                        break;
                    case 1:
                        sql = "select passWord from pwd where userName='sysOperator'";
                        break;
                    case 2:
                        sql = "select passWord from pwd where userName='sysAdministrator'";
                        break;
                }
                
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                password = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("获取项目名称失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return password;
        }

        /// <summary>
        /// 获取设备信息表
        /// </summary>
        /// <returns></returns>
        public List<struct_DeviceEnable> Select_DeviceEnable()
        {
            List<struct_DeviceEnable> struct_DeviceEnables = new List<struct_DeviceEnable>();
            struct_DeviceEnable struct_DeviceEnable;
            IPAddress iPAddress;
            int port;
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "select *  from device  order by deviceNo ";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    struct_DeviceEnable.enable = Convert.ToBoolean(Convert.ToString(mySqlDataReader.GetValue(4)));
                    iPAddress = IPAddress.Parse(Convert.ToString(mySqlDataReader.GetValue(2)));
                    port = Convert.ToInt32(Convert.ToString(mySqlDataReader.GetValue(3)));
                    struct_DeviceEnable.ipEndPoint = new IPEndPoint(iPAddress, port);
                    struct_DeviceEnable.name = Convert.ToString(Convert.ToString(mySqlDataReader.GetValue(1)));
                    struct_DeviceEnable.deviceNo = Convert.ToInt32(Convert.ToString(mySqlDataReader.GetValue(0)));
                    struct_DeviceEnables.Add(struct_DeviceEnable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取设备信息表失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return struct_DeviceEnables;
        }

        /// <summary>
        /// 获取分区名称
        /// </summary>
        /// <param name="deviceNo">设备编号</param>
        /// <param name="channlNo">通道编号</param>
        /// <returns></returns>
        public List<struct_PrtName> Select_PrtName(int deviceNo,int channlNo)
        {
            List<struct_PrtName> list_prtname = new List<struct_PrtName>();
            struct_PrtName struct_PrtName;
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "select *  from prtname where deviceNo='" + deviceNo + "' and channelNo='" + channlNo + "' order by prtNo ";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    struct_PrtName.deviceNo = Convert.ToInt32(mySqlDataReader.GetValue(0));
                    struct_PrtName.channelNo = Convert.ToInt32(mySqlDataReader.GetValue(1));
                    struct_PrtName.prtNo = Convert.ToInt32(mySqlDataReader.GetValue(2));
                    struct_PrtName.prtName = Convert.ToString(mySqlDataReader.GetValue(3));
                    list_prtname.Add(struct_PrtName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取分区信息表失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return list_prtname;
        }

        /// <summary>
        /// 获取所有的分区名称
        /// </summary>
        /// <returns></returns>
        public List<struct_PrtName> Select_PrtName()
        {
            List<struct_PrtName> list_prtname = new List<struct_PrtName>();
            struct_PrtName struct_PrtName;
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "select *  from prtname order by deviceNo,channelNo,prtNo ";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    struct_PrtName.deviceNo = Convert.ToInt32(mySqlDataReader.GetValue(0));
                    struct_PrtName.channelNo = Convert.ToInt32(mySqlDataReader.GetValue(1));
                    struct_PrtName.prtNo = Convert.ToInt32(mySqlDataReader.GetValue(2));
                    struct_PrtName.prtName = Convert.ToString(mySqlDataReader.GetValue(3));
                    list_prtname.Add(struct_PrtName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取分区信息表失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return list_prtname;
        }

        public List<struct_ChannelMsg> Select_ChannelMsg()
        {
            List<struct_ChannelMsg> list_channelmsg = new List<struct_ChannelMsg>();
            struct_ChannelMsg struct_ChannelMsg;
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "select *  from channel order by deviceNo,channelNo ";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    struct_ChannelMsg.deviceNo = Convert.ToInt32(mySqlDataReader.GetValue(0));
                    struct_ChannelMsg.channelNo = Convert.ToInt32(mySqlDataReader.GetValue(1));
                    struct_ChannelMsg.channelName = Convert.ToString(mySqlDataReader.GetValue(2));
                    struct_ChannelMsg.locationX = Convert.ToDouble(mySqlDataReader.GetValue(3));
                    struct_ChannelMsg.locationY = Convert.ToDouble(mySqlDataReader.GetValue(4));
                    struct_ChannelMsg.isEnable = Convert.ToBoolean(mySqlDataReader.GetValue(5));
                    list_channelmsg.Add(struct_ChannelMsg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取通道信息表失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return list_channelmsg;
        }

        public byte[] Select_PagePic(int picNo)
        {
            byte[] bytes;
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "select pic  from pagepic where picNo='" + picNo + "' ";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                bytes= (byte[])cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("获取通道信息表失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return bytes;
        }

        /// <summary>
        /// 查询报警信息
        /// </summary>
        /// <returns></returns>
        public List<Struct_AlarmMsg> Select_Alarm(int type,string start,string end)
        {
            List<Struct_AlarmMsg> list_AlarmMsg = new List<Struct_AlarmMsg>();
            Struct_AlarmMsg alarmMsg;
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "select   *  from alarm order by datetime desc limit 0,1000 ";
                switch (type)
                {
                    case 0:
                        sql = "select   *  from alarm where datetime between '" + start + "' and '" + end + "' order by datetime desc  ";
                        break;
                    case 1:
                        sql = "select   *  from alarm where datetime  between '" + start + "' and '" + end + "' and type like '%报警' order by datetime desc ";
                        break;
                    case 2:
                        sql = "select   *  from alarm where datetime between '" + start + "' and '" + end + "' and type like '断纤%' order by datetime desc ";
                        break;
                    case 3:
                        sql = "select   *  from alarm where datetime between '" + start + "' and '" + end + "' and type like '通讯%' order by datetime desc ";
                        break;
                }
                
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    alarmMsg = new Struct_AlarmMsg();
                    alarmMsg.DateTime = Convert.ToDateTime(mySqlDataReader.GetValue(0));
                    alarmMsg.DeviceNo = Convert.ToInt32(mySqlDataReader.GetValue(1));
                    alarmMsg.ChannelNo = Convert.ToInt32(mySqlDataReader.GetValue(2));
                    alarmMsg.PartitionNo = Convert.ToInt32(mySqlDataReader.GetValue(3));
                    alarmMsg.Position = Convert.ToInt32(mySqlDataReader.GetValue(4));
                    alarmMsg.Illustrate = (string)mySqlDataReader.GetValue(5);
                    alarmMsg.Relay = Convert.ToInt32(mySqlDataReader.GetValue(6));
                    alarmMsg.Type = Convert.ToString(mySqlDataReader.GetValue(7));
                    alarmMsg.AlarmValue = (double)mySqlDataReader.GetValue(8);
                    alarmMsg.Threshold = (double)mySqlDataReader.GetValue(9);
                    list_AlarmMsg.Add(alarmMsg);
                }
                //MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sql, conn);
                //mySqlDataAdapter.Fill(dataSet, "Alarm");
            }
            catch (Exception ex)
            {
                throw new Exception("获取报警信息表失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return list_AlarmMsg;
        }

        public List<OperationRecord> Select_OpRecord(string start, string end)
        {
            List<OperationRecord> operationRecords = new List<OperationRecord>();
            OperationRecord operationRecord;
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "select *  from audit  where datetime between '" + start + "' and '" + end + "' order by datetime ";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    operationRecord.dateTime = Convert.ToDateTime(mySqlDataReader.GetValue(0));
                    operationRecord.user = Convert.ToInt32(mySqlDataReader.GetValue(1));
                    operationRecord.record = Convert.ToString(mySqlDataReader.GetValue(2));
                    operationRecords.Add(operationRecord);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取操作信息表失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return operationRecords;
        }
    }
}
