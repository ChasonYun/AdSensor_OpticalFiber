using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticalFiber
{
    class SQL_Update
    {
        MySqlCommand cmd;
        MySqlConnection conn;

        /// <summary>
        /// 新增探测器 按顺序 添加
        /// </summary>
        public void Update_Sensor_Add(int sensorid,string sesnsorname,int startadd,int nodenumber)
        {
            try
            {
                string connstr = "data source =localhost;database=SM9003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "update sensor set sensorName='" + sesnsorname + "',startAdd='" + startadd + "',nodeNumber='" + nodenumber + "',isEnable='1' where sensorId='" + sensorid + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("新增探测器失败！——" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
        }

        /// <summary>
        /// 删除探测器 参数初始化  isenable =0
        /// </summary>
        /// <param name="sensorid"></param>
        /// <param name="sesnsorname"></param>
        /// <param name="startadd"></param>
        /// <param name="nodenumber"></param>
        public void Update_Sensor_Delete(int sensorid)
        {
            try
            {
                string connstr = "data source =localhost;database=SM9003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "update sensor set sensorName='探测器" + sensorid + "',startAdd='1',nodeNumber='60',isEnable='0' where sensorId='" + sensorid + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("删除探测器失败！——" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
        }

        /// <summary>
        /// 修改探测器信息 
        /// </summary>
        /// <param name="sensorid"></param>
        /// <param name="sesnsorname"></param>
        /// <param name="startadd"></param>
        /// <param name="nodenumber"></param>
        public void Update_Sensor_Update(int sensorid, string sesnsorname, int startadd, int nodenumber)
        {
            try
            {
                string connstr = "data source =localhost;database=SM9003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "update sensor set sensorName='" + sesnsorname + "',startAdd='" + startadd + "',nodeNumber='" + nodenumber + "',isEnable='1' where sensorId='" + sensorid + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("删除探测器失败！——" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
        }

        /// <summary>
        /// 修改项目名称
        /// </summary>
        /// <param name="prjid"></param>
        /// <param name="prjname"></param>
        public void Update_PrjName(int prjid, string prjname)
        {
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "update prjname set prjname='" + prjname + "' where prjid='" + prjid + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("修改项目名称失败！——" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
        }

        public void Update_Device(int deviceNo,struct_DeviceEnable struct_DeviceEnable)
        {
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "update device set deviceName='" + struct_DeviceEnable.name + "',ipAddress='" + struct_DeviceEnable.ipEndPoint.Address.ToString() + "',port='" + struct_DeviceEnable.ipEndPoint.Port + "',enable='" + Convert.ToInt32(struct_DeviceEnable.enable) + "' where deviceNo='" + deviceNo + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new Exception("更新设备启用失败！——");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("更新设备启用失败！——" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
        }

        public void Update_PrtName(struct_PrtName prtName)
        {
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "update prtname set prtName='" + prtName.prtName + "' where deviceNo='" + prtName.deviceNo + "' and  channelNo='" + prtName.channelNo + "' and prtNo='" + prtName.prtNo + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new Exception("更新设备名称失败！——");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("更新设备名称失败！——" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
        }

        /// <summary>
        /// 更新主页通道信息
        /// </summary>
        /// <param name="struct_ChannelMsg"></param>
        public void Update_ChannelMsg(struct_ChannelMsg struct_ChannelMsg)
        {
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "update channel set channelName='" + struct_ChannelMsg.channelName + "',locationX='" + struct_ChannelMsg.locationX + "' ,locationY='" + struct_ChannelMsg.locationY + "',isEnable='" + Convert.ToInt32(struct_ChannelMsg.isEnable) + "' where deviceNo='" + struct_ChannelMsg.deviceNo + "' and  channelNo='" + struct_ChannelMsg.channelNo + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new Exception("更新通道信息失败！——");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("更新通道信息失败！——" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
        }

        /// <summary>
        /// 更新主页图片
        /// </summary>
        /// <param name="picNo"></param>
        /// <param name="bytes"></param>
        public void Update_PagePic(int picNo, byte[] bytes)
        {
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "update pagepic set pic=@image where picNo='" + picNo + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@image", MySqlDbType.Binary, bytes.Length);
                cmd.Parameters["@image"].Value = bytes;
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new Exception("更新主页图片失败！——");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("更新主页图片失败！——" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
        }

        public void Update_Pwd(string userName, string pwd)
        {
            try
            {
                string connstr = "data source =localhost;database=SM2003;user id=root;password=adsensor;pooling=false;charset=utf8";
                string sql = "update pwd set  passWord='" + MD5.GetMD5Hash(pwd) + "' where userName='" + userName + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new Exception("修改密码失败！——");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("修改密码失败！——" + ex.Message);
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
