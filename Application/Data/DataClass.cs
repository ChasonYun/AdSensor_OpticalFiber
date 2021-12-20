using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace OpticalFiber
{
    static class DataClass
    {
        #region
        /// <summary>
        /// 错误消息提示 日志
        /// </summary>
        /// <param name="msg"></param>
        public static void ShowErrMsg(string msg)
        {
#if DEBUG
            MessageBox.Show(msg);
#else
            MessageBox.Show(msg);
            string path = "D:" + "\\" + "SM2003";
            string name = path + "\\" + "SM2003Log.txt";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(name))
            {
                File.CreateText(name);
            }
            if (!IsLogInUse(name))
            {
                FileStream fileStream = new FileStream(name, FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(DateTime.Now.ToString() + ":--" + msg);
                streamWriter.Flush();
                streamWriter.Close();
                fileStream.Close();
            }
#endif
        }

        private static bool IsLogInUse(string path)
        {
            bool IsInuse = true;
            FileStream fs = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                IsInuse = false;
            }
            catch
            {

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return IsInuse;
        }
        #endregion

        ///Model
        public static int userLevel = 0;
        public static string projName;
        public static bool IsRunning;
        public static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ///容量为8套
        public static List<struct_DeviceEnable> list_DeviceEnables;//配置文件读取  设备启用情况
        public static List<Class_DeviceStatus> list_DeviceStatus = new List<Class_DeviceStatus>();//设备 运行状态
        public static List<Class_DeviceParam> list_DeviceParam = new List<Class_DeviceParam>();//设备 参数

        public static List<Class_DeviceChannelParam> list_DeviceChannelParam = new List<Class_DeviceChannelParam>();//设备通道参数

        //public static int[,,] deviceTemper = new int[9, 5, 10000]; //温度  设备编号  通道号  三个温度 数组 int10000
        public static List<Class_DevcieTemper> list_DeviceTemper = new List<Class_DevcieTemper>();//设备温度

        public static List<Class_DevicePartition> list_DevicePartition = new List<Class_DevicePartition>();//设备分区参数


        public static List<struct_PrtName> list_PrtName = new List<struct_PrtName>();

        public static bool[] list_TcpCommFault = new bool[9];//通讯异常 判断标志位

        public static int[,] temper_Now = new int[257, 257];//记录 实时温度
        public static int[] commFault_Count = new int[257];//通讯故障计数

       
    }

    public struct struct_DeviceEnable
    {
        public int deviceNo;//设备编号
        public bool enable;//是否启用
        public IPEndPoint ipEndPoint;//终结点
        public string name;//设备名称
    }

    /// <summary>
    /// 设备状态结构体
    /// </summary>
    public class Class_DeviceStatus
    {
        /// <summary>
        /// 100 01 00 01 00 48
        /// </summary>
        private bool controlStatus;//控制状态 0本地 1远程                 0001 
        private bool errorStatus;//主机状态 0正常 1异常                   0002
        private bool runStatus;//运行状态 0 停止 1运行                    0003
        private bool voiceStatus;//是否处于消音状态直到再次报警           0004
        private bool resetStatus;//是否处于报警复位中（复位时10秒内置1）  0005
        private bool acquireBoardErr;//采集板故障                         0006
        private bool alarmBoardErr;//报警板故障                           0007
        private bool chassisTemSensorErr;//机箱温度传感器故障             0008
        private bool referTemSensorErr;//参考温度传感器故障               0009

        private bool relay1_Status;//继电器1状态                          0041
        private bool relay2_Status;//继电器2状态                          0042
        private bool relay3_Status;//继电器3状态                          0043
        private bool relay4_Status;//继电器4状态                          0044
        private bool relay5_Status;//继电器5状态                          0045
        private bool relay6_Status;//继电器6状态                          0046
        private bool relay7_Status;//继电器7状态                          0047
        private bool relay8_Status;//继电器8状态                          0048

        public bool ControlStatus { get => controlStatus; set => controlStatus = value; }
        public bool ErrorStatus { get => errorStatus; set => errorStatus = value; }
        public bool VoiceStatus { get => voiceStatus; set => voiceStatus = value; }
        public bool RunStatus { get => runStatus; set => runStatus = value; }
        public bool ResetStatus { get => resetStatus; set => resetStatus = value; }
        public bool AcquireBoardErr { get => acquireBoardErr; set => acquireBoardErr = value; }
        public bool AlarmBoardErr { get => alarmBoardErr; set => alarmBoardErr = value; }
        public bool ChassisTemSensorErr { get => chassisTemSensorErr; set => chassisTemSensorErr = value; }
        public bool ReferTemSensorErr { get => referTemSensorErr; set => referTemSensorErr = value; }
        public bool Relay1_Status { get => relay1_Status; set => relay1_Status = value; }
        public bool Relay2_Status { get => relay2_Status; set => relay2_Status = value; }
        public bool Relay3_Status { get => relay3_Status; set => relay3_Status = value; }
        public bool Relay4_Status { get => relay4_Status; set => relay4_Status = value; }
        public bool Relay5_Status { get => relay5_Status; set => relay5_Status = value; }
        public bool Relay6_Status { get => relay6_Status; set => relay6_Status = value; }
        public bool Relay7_Status { get => relay7_Status; set => relay7_Status = value; }
        public bool Relay8_Status { get => relay8_Status; set => relay8_Status = value; }
    }

  
    /// <summary>
    /// 设备参数 结构体
    /// </summary>
    public class Class_DeviceParam
    {

        public struct_DeivceParam struct_deivceParam;
        public Class_DeviceParam()
        {
            struct_deivceParam = new struct_DeivceParam();
        }
        public struct struct_DeivceParam
        {
            /// <summary>
            /// 100 0X 01 35
            /// </summary>
            public int referLength;//参考光纤                                0001
            public int dataPoint;//数据点长度                                0002
            public int chassisTemper;//机箱温度 0.01℃                       0003
            public int referTemper;//参考温度0.01                            0004
            public int compensationCoefficient;//补偿系数0.001℃             0005
            public int partitionNum;//设备分区总数                           0006
            public int channelNum;//设备通道个数                             0007
            public int scanModel;//扫描模式                                  0008
            public int year;//主机时间  年                                   0009
            public int month;//主机时间  月                                  0010
            public int day;// 主机时间 日                                    0011
            public int hour;// 主机时间 时                                   0012
            public int minute;// 主机时间 分                                 0013
            public int second;// 主机时间 秒                                 0014

            public int runTimeH_L;//主机累计运行时间 高16位 低16位  组合     0031_0032
            public int runTimeThis;//主机本次运行时间 16位                   0033
            public int faultCount;//故障总数                                 0034
            public int fireAlarmCount;//火警总数                  
        }
    }

    public class Class_DevcieTemper
    {
        private struct_ChannelTemper channeltemper;
        public struct_ChannelTemper[] channelTempers = new struct_ChannelTemper[5];
        public Class_DevcieTemper()
        {
            for (int i = 0; i < 5; i++)
            {
                channeltemper = new struct_ChannelTemper();
                channeltemper.averageTemper = new int[10000];
                channeltemper.realTemper = new int[10000];
                channeltemper.riseTemper = new int[10000];
                channelTempers[i] = channeltemper;
            }
        }

        public struct struct_ChannelTemper
        {
            public int[] averageTemper;//每米平均温度
            public int[] realTemper;//每米实时温度
            public int[] riseTemper;//每米没分温升速率
        }
    }

    public class Class_DeviceChannelParam
    {
        public struct_DeviceChannelParam struct_DeviceChannelParam = new struct_DeviceChannelParam();
        public struct_ChannelParam[] struct_ChannelParams = new struct_ChannelParam[5];


        public Class_DeviceChannelParam()
        {
            struct_DeviceChannelParam.struct_ChannelParams = struct_ChannelParams;
        }

    }


    /// <summary>
    /// 通道 参数 结构体
    /// </summary>
    public struct struct_ChannelParam
    {
        /// <summary>
        /// 1～4 04 01 26
        /// </summary>
        public int length;//通道光纤长度                                0001
        public int startPosition;//通道起始点位置                       0002
        public int slope;//通道斜率                                     0003
        public int intercept;//截距                                     0004
        public int offsetTemper;//温度偏置                              0005
        public int averageTimes;//平均次数                              0006
        public int brokenPoint;//断纤位置                               0007
        public int partition;//分区个数                                 0008
        public int status;//状态                                        0009
        public int checkStatus;//报警检测状态 0未检测 1检测中           0010
        public int faultStatus;//断纤状态                               0011
        public int isBroken;//是否断纤 0无 断纤 1 断                    0012
        public int fireAlarmStatus;//火警状态                           0013
        public int riseAlarmStatus;//升警状态                           0014
    }

    /// <summary>
    /// 设备所有 通道参数
    /// </summary>
    public struct struct_DeviceChannelParam
    {
        public struct_ChannelParam[] struct_ChannelParams;
    }

   
    /// <summary>
    /// 设备分区 参数
    /// </summary>
    public class Class_DevicePartition
    {
        public struct_DevicePartition struct_devicePartition = new struct_DevicePartition();

        private struct_Partition[] struct_Partitions;
        private struct_ChannelPartition[] struct_ChannelPartitions;
        public Class_DevicePartition()
        {
            struct_Partitions = new struct_Partition[51];//每个通道分区有50个
            struct_ChannelPartitions = new struct_ChannelPartition[5];//每台设备有4个通道
            for (int i = 0; i <= 4; i++)
            {
                struct_ChannelPartitions[i].struct_Partitions = (struct_Partition[])struct_Partitions.Clone();//分区赋值给通道
            }
            struct_devicePartition.struct_ChannelPartitions = (struct_ChannelPartition[])struct_ChannelPartitions.Clone();
        }

        /// <summary>
        /// 设备所有分区参数
        /// </summary>
        public struct struct_DevicePartition
        {
            public struct_ChannelPartition[] struct_ChannelPartitions;
        }

        /// <summary>
        /// 通道分区参数
        /// </summary>
        public struct struct_ChannelPartition
        {
            public struct_Partition[] struct_Partitions;
        }
       
    }

    /// <summary>
    /// 分区 参数  状态结构体 
    /// </summary>
    public struct struct_Partition
    {
        /// <summary>
        /// 分区  +100  使用200+通道号
        /// </summary>
        public int startPosition;//开始位置                             0101
        public int endPosition;//结束位置                               0102
        public int relayNumber;//接点编号                               0103
        public int fireAlarmThreshold;//火警阈值                        0104
        public int riseAlarmThreshold;//升温报警阈值                    0105

        public int alarmStatus;//报警状态                               0121
        public int fireAlarmStatus;//火警 0 无 1有                      0122
        public int fireAlarmPosition;//火警位置                         0123
        public int riseAlarmStaus;//温升报警  0无 1有                   0124
        public int riseAlarmPosition;//温升位置                         0125

        public int fireAlarmYear_Month;//火警年  月                     0131
        public int fireAlarmDay_Hour;//火警 日 时                       0132
        public int fireAlarmMinute_Second;//火警 分 秒                  0133
        public int riseAlarmYear_Month;//升警年  月                     0134
        public int riseAlarmDay_Hour;//升警 日 时                       0135
        public int riseAlarmMinute_Second;//升警 分 秒                  0136

        public int maxRise;//最大温升                                   0141
        public int maxRisePosition;//最大温升距离                       0142
        public int maxTemper;//最高温度                                 0143
        public int maxTemperPosition;//最高温度距离                     0144
    }


    public struct struct_PrtName
    {
        public int deviceNo;
        public int channelNo;
        public int prtNo;
        public string prtName;
    }

    public struct struct_ChannelMsg
    {
        public int deviceNo;
        public int channelNo;
        public string channelName;
        public double locationX;
        public double locationY;
        public bool isEnable;
    }

    public struct struct_TvwMsg
    {
        public int deviceNo;
        public int channelNo;
        public int partitionNo;
        public string treeNodeName;
    }

    public class Struct_AlarmMsg : IComparer<Struct_AlarmMsg>, IComparable<Struct_AlarmMsg>
    {
        private DateTime dateTime;
        private int deviceNo;
        private int channelNo;
        private int partitionNo;
        private string illustrate;
        private int position;
        private int relay;
        private string type;
        private double alarmValue;
        private double threshold;

        public DateTime DateTime { get => dateTime; set => dateTime = value; }
        public int DeviceNo { get => deviceNo; set => deviceNo = value; }
        public int ChannelNo { get => channelNo; set => channelNo = value; }
        public int PartitionNo { get => partitionNo; set => partitionNo = value; }
        public string Illustrate { get => illustrate; set => illustrate = value; }
        public int Position { get => position; set => position = value; }
        public int Relay { get => relay; set => relay = value; }
        public string Type { get => type; set => type = value; }
        public double AlarmValue { get => alarmValue; set => alarmValue = value; }
        public double Threshold { get => threshold; set => threshold = value; }
        public int Compare(Struct_AlarmMsg AlarmMsg1, Struct_AlarmMsg AlarmMsg2)
        {
            if (AlarmMsg1.DateTime > AlarmMsg2.DateTime)
            {
                return 1;
            }
            if (AlarmMsg1.DateTime < AlarmMsg2.DateTime)
            {
                return -1;
            }
            return 0;
        } 

        public int CompareTo(Struct_AlarmMsg Alarm)
        {
            return Compare(this, Alarm);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator == (Struct_AlarmMsg alarm1, Struct_AlarmMsg alarm2)
        {
            if(alarm1.DeviceNo == alarm2.DeviceNo &&
               alarm1.ChannelNo == alarm2.ChannelNo &&
               alarm1.PartitionNo == alarm2.PartitionNo &&
               alarm1.Illustrate == alarm2.Illustrate &&
               alarm1.Position == alarm2.Position &&
               alarm1.Relay == alarm2.Relay &&
               alarm1.Type == alarm2.Type &&
               alarm1.AlarmValue == alarm2.AlarmValue &&
               alarm1.Threshold == alarm2.Threshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator !=(Struct_AlarmMsg alarm1, Struct_AlarmMsg alarm2)
        {
            if (alarm1.DeviceNo == alarm2.DeviceNo &&
               alarm1.ChannelNo == alarm2.ChannelNo &&
               alarm1.PartitionNo == alarm2.PartitionNo &&
               alarm1.Illustrate == alarm2.Illustrate &&
               alarm1.Position == alarm2.Position &&
               alarm1.Relay == alarm2.Relay &&
               alarm1.Type == alarm2.Type)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    public struct OperationRecord
    {
        public DateTime dateTime;
        public int user;
        public string record;
    }
}
