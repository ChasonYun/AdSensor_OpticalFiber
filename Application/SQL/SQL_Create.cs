using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace OpticalFiber
{
    /// <summary>
    /// 检查数据库 创建数据库 数据表
    /// 直接使用root账户进行操作  应该为 其创建单独的用户与数据库  然后授予用户最小权限   
    /// </summary>
    public class SQL_Create
    {
        const string userName = "root";//root
        const string password = "123456";//adsensor
        const string dbName = "SM2003_1";
        MySqlCommand cmd;
        MySqlConnection conn;
        public SQL_Create(BackgroundWorker worker)
        {
            try
            {
                if (!IsDBExit($"{dbName}"))
                {
                    worker.ReportProgress(10, $"创建数据库{dbName}");
                    if (Create_DB($"{dbName}"))
                    {

                    }
                    else
                    {
                        throw new Exception("创建数据库失败");
                    }
                }
                if (!IsTabExit("pwd"))
                {
                    worker.ReportProgress(20, "创建数据表pwd");
                    if (!Create_Table_Pwd())
                    {
                        throw new Exception("创建数据表Pwd失败");
                    }
                }
                if (!IsTabExit("prtname"))
                {
                    worker.ReportProgress(30, "创建数据表prtname");
                    if (!Create_Table_prtName())
                    {
                        throw new Exception("创建数据表prtName失败");
                    }
                }
                if (!IsTabExit("prjname"))
                {
                    worker.ReportProgress(40, "创建数据表prjname");
                    if (!Create_Table_prjName())
                    {
                        throw new Exception("创建数据表prtName失败");
                    }
                }
                if (!IsTabExit("device"))
                {
                    worker.ReportProgress(50, "创建数据表device");
                    if (!Create_Table_device())
                    {
                        throw new Exception("创建数据表device失败");
                    }
                }
                if (!IsTabExit("channel"))
                {
                    worker.ReportProgress(60, "创建数据表channel");
                    if (!Create_Table_channel())
                    {
                        throw new Exception("创建数据表channel失败");
                    }
                }
                if (!IsTabExit("pagepic"))
                {
                    worker.ReportProgress(70, "创建数据表pagepic");
                    if (!Create_Table_PagePic())
                    {
                        throw new Exception("创建数据表pagePic失败");
                    }
                }
                if (!IsTabExit("alarm"))
                {
                    worker.ReportProgress(80, "创建数据表alarm");
                    if (!Create_Table_alarm())
                    {
                        throw new Exception("创建数据表alarm失败");
                    }
                }
                if (!IsTabExit("audit"))
                {
                    worker.ReportProgress(90, "创建数据表audit");
                    if (!Create_Table_audit())
                    {
                        throw new Exception("创建数据表audit失败");
                    }
                }
            }
            catch (Exception ex)
            {
                DataClass.ShowErrMsg("数据库创建失败" + ex.Message);
            }
            finally
            {
                worker.CancelAsync();
            }
        }

        private bool IsDBExit(string dbName)
        {
            bool result = false;
            try
            {
                string connstr = $"data source =localhost;user id={userName};password={password};pooling=false;charset=utf8";
                string sql = "SELECT * FROM information_schema.SCHEMATA where SCHEMA_NAME='" + dbName + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = true;
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(" Cannot open a connection without specifying a data source or server" + ex.Message + ex.Source);
            }
            catch (MySqlException ex)
            {
                throw new Exception(" A connection-level error occurred while opening the connection." + ex.Message + ex.Source);
            }
            catch (Exception ex)
            {
                throw new Exception("数据库存在判断失败" + ex.Message + ex.Source);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }

        private bool IsTabExit(string tabName)
        {
            bool result = false;
            try
            {
                string connstr = $"data source =localhost;user id={userName};password={password};pooling=false;charset=utf8";
                string sql = $"SELECT table_name FROM information_schema.TABLES where table_schema='{dbName}' and table_name='" + tabName + "'";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("数据表存在判断失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }

        private bool Create_DB(string dbName)
        {
            bool result = false;
            try
            {
                string connstr = $"data source =localhost;user id={userName};password={password};pooling=false;charset=utf8";
                string sql = "create database " + dbName + "";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据库失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }

        private bool Create_Table_Pwd()
        {
            bool result = false;
            try
            {
                string connstr = $"data source =localhost;database={dbName};user id={userName};password={password};pooling=false;charset=utf8";
                string sql = "create table pwd(userName varchar(20) ,passWord varchar(32)) DEFAULT CHARSET=utf8";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("insert into pwd(userName,passWord) values ('generalUser','" + MD5.GetMD5Hash("userSM2003") + "')", conn);//初始密码
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("insert into pwd(userName,passWord) values ('sysOperator','" + MD5.GetMD5Hash("operatorSM2003") + "')", conn);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("insert into pwd(userName,passWord) values ('sysAdministrator','" + MD5.GetMD5Hash("adminSM2003") + "')", conn);
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据表pwd失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }

        /// <summary>
        /// 创建设备表
        /// </summary>
        /// <returns></returns>
        private bool Create_Table_device()
        {
            bool result = false;
            try
            {
                string connstr = $"data source =localhost;database={dbName};user id={userName};password={password};pooling=false;charset=utf8";
                string sql = "create table device(deviceNo tinyint unsigned,deviceName varchar(20),ipAddress varchar(15),port smallint,channelCount smallint,enable tinyint(1)) DEFAULT CHARSET=utf8";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();

                for (int k = 1; k <= 8; k++)
                {
                    cmd = new MySqlCommand("insert into device(deviceNo,deviceName,ipAddress,port,channelCount,enable) values ('" + k + "','设备" + k + "','10.10.10." + k + "','502','1','" + 0 + "')", conn);
                    cmd.ExecuteNonQuery();
                }

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据表device失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }

        private bool Create_Table_prjName()
        {
            bool result = false;
            try
            {
                string connstr = $"data source =localhost;database={dbName};user id={userName};password={password};pooling=false;charset=utf8";
                string sql = "create table prjname(prjId  tinyint unsigned,prjName varchar(32)) DEFAULT CHARSET=utf8";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("insert into prjname(prjId,prjName) values ('1','无锡圣敏传感科技股份有限公司')", conn);//
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据表prjName失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }

        private bool Create_Table_prtName()
        {
            bool result = false;
            try
            {
                string connstr = $"data source =localhost;database={dbName};user id={userName};password={password};pooling=false;charset=utf8";
                string sql = "create table prtName(deviceNo tinyint unsigned,channelNo tinyint unsigned,prtNo tinyint unsigned,prtName varchar(32)) DEFAULT CHARSET=utf8";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                for (int i = 1; i <= 8; i++)
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        for (int k = 1; k <= 200; k++)
                        {
                            cmd = new MySqlCommand("insert into prtName(deviceNo,channelNo,prtNo,prtName) values ('" + i + "','" + j + "','" + k + "','分布" + k + "')", conn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据表prtName失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }

        /// <summary>
        /// 创建用户显示主页的 信息
        /// </summary>
        /// <returns></returns>
        private bool Create_Table_channel()
        {
            bool result = false;
            try
            {
                string connstr = $"data source =localhost;database={dbName};user id={userName};password={password};pooling=false;charset=utf8";
                string sql = "create table channel(deviceNo tinyint unsigned,channelNo tinyint unsigned,channelName varchar(32),locationX double,locationY double,isEnable tinyint(1)) DEFAULT CHARSET=utf8";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                for (int i = 1; i <= 8; i++)
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        cmd = new MySqlCommand("insert into channel(deviceNo,channelNo,channelName,locationX,locationY,isEnable) values ('" + i + "','" + j + "','通道" + j + "','0','0','0')", conn);
                        cmd.ExecuteNonQuery();
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据表channel失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }

        private bool Create_Table_PagePic()
        {
            bool result = false;
            try
            {
                Image image = Properties.Resources.powerStation;
                byte[] bytes = ConfigClass.ImgToBytes(image);
                string connstr = $"data source =localhost;database={dbName};user id={userName};password={password};pooling=false;charset=utf8";
                string sql = "create table pagePic(picNo tinyint unsigned,pic mediumblob) DEFAULT CHARSET=utf8";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("insert into pagePic(picNo,pic) values ('1',@image)", conn);
                cmd.Parameters.Add("@image", MySqlDbType.Binary, bytes.Length);
                cmd.Parameters["@image"].Value = bytes;
                cmd.ExecuteNonQuery();

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据表pagePic失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }

        private bool Create_Table_alarm()
        {
            bool result = false;
            try
            {
                string connstr = $"data source =localhost;database={dbName};user id={userName};password={password};pooling=false;charset=utf8";
                string sql = "create table alarm(dateTime datetime,deviceNo tinyint unsigned,channelNo tinyint unsigned,partitionNo tinyint unsigned,position smallint,illustrate varchar(32),relay tinyint unsigned,type varchar(10),alarmvalue double,threshold double,primary key(datetime,deviceNo,channelNo,partitionNo)) DEFAULT CHARSET=utf8";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据表alarm失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }

        private bool Create_Table_audit()
        {
            bool result = false;
            try
            {
                string connstr = $"data source =localhost;database={dbName};user id={userName};password={password};pooling=false;charset=utf8";
                string sql = "create table audit(dateTime datetime,user tinyint unsigned,record varchar(32)) DEFAULT CHARSET=utf8";
                conn = new MySqlConnection(connstr);
                cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("创建数据表audit失败" + ex.Message);
            }
            finally
            {
                conn.Close();
                conn = null;
                cmd = null;
            }
            return result;
        }
    }
}
