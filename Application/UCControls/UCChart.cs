using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Diagnostics;

namespace OpticalFiber
{
    public partial class UCChart : UserControl
    {
        //Stopwatch stopwatch;
        public UCChart(int deviceNo, int channelNo, int partitionNo, string title)
        {
            InitializeComponent();
            this.title = title;
            this.deviceNo = deviceNo;
            this.channelNo = channelNo;
            this.partitionNo = partitionNo;

            chart1.Series.Clear();

            series_AvgTemper.BorderColor = Color.Blue;
            series_AvgTemper.BorderWidth = 2;
            series_AvgTemper.ChartType = SeriesChartType.Line;

            series_RealTemper.BorderColor = Color.Green;
            series_RealTemper.BorderWidth = 2;
            series_RealTemper.ChartType = SeriesChartType.Line;

            series_RiseTemper.BorderColor = Color.Red;
            series_RiseTemper.BorderWidth = 2;
            series_RiseTemper.ChartType = SeriesChartType.Line;

            chart1.Series.Add(series_AvgTemper);
            chart1.Series.Add(series_RealTemper);
            chart1.Series.Add(series_RiseTemper);

            series_AvgTemper.Enabled = toolStripMenuItem1.Checked;
            series_RealTemper.Enabled = toolStripMenuItem2.Checked;
            series_RiseTemper.Enabled = toolStripMenuItem3.Checked;

            chart1.MouseWheel += MouseEventHandler;
            Init();
        }

        Series series_AvgTemper = new Series("平均温度");
        Series series_RealTemper = new Series("实时温度");
        Series series_RiseTemper = new Series("温升速率");

        string title;
        int deviceNo;
        int channelNo;
        int partitionNo;

        int startPosition;
        int endPosition;
        int lenght;

        private int minmumX;
        private int maxmumX;

        private int minmumY = -50;
        private int maxmumY = 200;



        public int MinmumX { get => minmumX; set => minmumX = value; }
        public int MaxmumX { get => maxmumX; set => maxmumX = value; }
        public int MinmumY { get => minmumY; set => minmumY = value; }
        public int MaxmumY { get => maxmumY; set => maxmumY = value; }


        private void Init()
        {

            if (partitionNo == 0)//是通道的  横轴起始位-200
            {
                startPosition = 1;
                endPosition = DataClass.list_DeviceChannelParam[deviceNo].struct_ChannelParams[channelNo].length;//通道光纤长度
                MaxmumX = endPosition;
                MinmumX = -200;
            }
            else
            {
                startPosition = DataClass.list_DevicePartition[deviceNo].struct_devicePartition.struct_ChannelPartitions[channelNo].struct_Partitions[partitionNo].startPosition;
                endPosition = DataClass.list_DevicePartition[deviceNo].struct_devicePartition.struct_ChannelPartitions[channelNo].struct_Partitions[partitionNo].endPosition;
                lenght = DataClass.list_DeviceChannelParam[deviceNo].struct_ChannelParams[channelNo].length;
                MaxmumX = endPosition;
                MinmumX = startPosition;
            }

            timer1.Start();

            chart1.ChartAreas[0].AxisX.Maximum = MaxmumX;
            chart1.ChartAreas[0].AxisX.Minimum = MinmumX;

            chart1.ChartAreas[0].AxisY.Maximum = MaxmumY;
            chart1.ChartAreas[0].AxisY.Minimum = MinmumY;
            lblTitle.Text = title;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //stopwatch = new Stopwatch();
            //stopwatch.Start();
            double avgMaxtemper = 0;
            int avgMaxposition = 0;
            double realMaxtemper = 0;
            int realMaxposition = 0;
            series_AvgTemper.Points.Clear();
            series_RealTemper.Points.Clear();
            series_RiseTemper.Points.Clear();
            double avgTempTemper = 0.0;
            double realTempTemper = 0.0;
            if (endPosition - startPosition <= 0)
            {
                return;
            }
            for (int i = startPosition; i < endPosition; i++)
            {
                if (DataClass.list_TcpCommFault[deviceNo])//断纤 IsBroken()||  通讯故障  全部置0 
                {
                    avgTempTemper = 0;
                    realTempTemper = 0;
                    series_AvgTemper.Points.AddXY(i, 0);
                    series_RealTemper.Points.AddXY(i, 0);
                }
                else
                {
                    if (IsBroken() && i > DataClass.list_DeviceChannelParam[deviceNo].struct_ChannelParams[channelNo].brokenPoint)
                    {
                        series_AvgTemper.Points.AddXY(i, 0);
                        series_RealTemper.Points.AddXY(i, 0);
                    }
                    else
                    {
                        avgTempTemper = (double)DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].averageTemper[i] / 10;
                        series_AvgTemper.Points.AddXY(i, avgTempTemper);
                        realTempTemper = (double)DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].realTemper[i] / 10;
                        series_RealTemper.Points.AddXY(i, realTempTemper);
                    }
                }

                if (avgTempTemper > avgMaxtemper)
                {
                    avgMaxtemper = avgTempTemper;
                    avgMaxposition = i;
                }
                if (realTempTemper > realMaxtemper)
                {
                    realMaxtemper = realTempTemper;
                    realMaxposition = i;
                }
            }

            if (IsBroken() || DataClass.list_TcpCommFault[deviceNo])// 
            {
                series_AvgTemper.Points.AddXY(endPosition, 0);
                series_RealTemper.Points.AddXY(endPosition, 0);
            }
            else
            {
                if (partitionNo == 0)//通道
                {
                    avgTempTemper = (double)DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].averageTemper[endPosition - 1] / 10;
                    series_AvgTemper.Points.AddXY(endPosition, avgTempTemper);
                    realTempTemper = (double)DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].realTemper[endPosition - 1] / 10;
                    series_RealTemper.Points.AddXY(endPosition, realTempTemper);
                }
                else//分区
                {
                    if (endPosition == lenght)
                    {
                        avgTempTemper = (double)DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].averageTemper[endPosition - 1] / 10;
                        series_AvgTemper.Points.AddXY(endPosition, avgTempTemper);
                        realTempTemper = (double)DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].realTemper[endPosition - 1] / 10;
                        series_RealTemper.Points.AddXY(endPosition, realTempTemper);
                    }
                    else
                    {
                        avgTempTemper = (double)DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].averageTemper[endPosition] / 10;
                        series_AvgTemper.Points.AddXY(endPosition, avgTempTemper);
                        realTempTemper = (double)DataClass.list_DeviceTemper[deviceNo].channelTempers[channelNo].realTemper[endPosition] / 10;
                        series_RealTemper.Points.AddXY(endPosition, realTempTemper);
                    }
                }


            }
            if (toolStripMenuItem1.Checked)
            {
                lblMax.Text = "最高温度：" + avgMaxtemper + "℃ 距离：" + avgMaxposition + "米";
            }
            else if (toolStripMenuItem2.Checked)
            {
                lblMax.Text = "最高温度：" + realMaxtemper + "℃ 距离：" + realMaxposition + "米";
            }
        }

        private bool IsBroken()
        {
            bool isBroken = false;
            int alarmStatus = DataClass.list_DeviceChannelParam[deviceNo].struct_ChannelParams[channelNo].isBroken;
            if (alarmStatus == 1)
            {
                isBroken = true;
            }
            return isBroken;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked = !toolStripMenuItem1.Checked;
            series_AvgTemper.Enabled = toolStripMenuItem1.Checked;

            toolStripMenuItem2.Checked = !toolStripMenuItem1.Checked;
            series_RealTemper.Enabled = toolStripMenuItem2.Checked;

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = !toolStripMenuItem2.Checked;
            series_RealTemper.Enabled = toolStripMenuItem2.Checked;

            toolStripMenuItem1.Checked = !toolStripMenuItem2.Checked;
            series_AvgTemper.Enabled = toolStripMenuItem1.Checked;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            toolStripMenuItem3.Checked = !toolStripMenuItem3.Checked;
            series_RiseTemper.Enabled = toolStripMenuItem3.Checked;
        }


        private void MouseEventHandler(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    chart1.ChartAreas[0].AxisY.Maximum = chart1.ChartAreas[0].AxisY.Maximum - 10;
                    chart1.ChartAreas[0].AxisY.Minimum = chart1.ChartAreas[0].AxisY.Minimum - 10;
                }
                if (e.Delta > 0)
                {
                    chart1.ChartAreas[0].AxisY.Maximum = chart1.ChartAreas[0].AxisY.Maximum + 10;
                    chart1.ChartAreas[0].AxisY.Minimum = chart1.ChartAreas[0].AxisY.Minimum + 10;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        ToolTip toolTip = new ToolTip();

        Point tempPoint = new Point();
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip.ForeColor = Color.Black;
            Point point = new Point(e.X, e.Y);
            if (tempPoint != point)
            {
                chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(point, true);
                chart1.ChartAreas[0].CursorY.SetCursorPixelPosition(point, true);

                tempPoint = point;

                int locatonX = (int)chart1.ChartAreas[0].CursorX.Position;
                if (toolStripMenuItem1.Checked)
                {
                    if (locatonX > 0)
                    {
                        if (partitionNo == 0)//通道
                        {
                            if (series_AvgTemper.Points.Count > 0)
                            {
                                toolTip.SetToolTip(chart1, "温度" + series_AvgTemper.Points[locatonX - 1].YValues[0] + "℃\r\n" + "距离" + (locatonX) + "米");
                            }
                        }
                        else//分区
                        {
                            if (series_AvgTemper.Points.Count > 0)
                            {
                                toolTip.SetToolTip(chart1, "温度" + series_AvgTemper.Points[locatonX - startPosition].YValues[0] + "℃\r\n" + "距离" + (locatonX) + "米");
                            }
                        }
                    }
                }
                if (toolStripMenuItem2.Checked)
                {
                    if (locatonX > 0)
                    {
                        if (partitionNo == 0)//通道
                        {
                            if (series_RealTemper.Points.Count > 0)
                            {
                                toolTip.SetToolTip(chart1, "温度" + series_RealTemper.Points[locatonX - 1].YValues[0] + "℃\r\n" + "距离" + (locatonX) + "米");
                            }
                        }
                        else//分区
                        {
                            if (series_RealTemper.Points.Count > 0)
                            {
                                toolTip.SetToolTip(chart1, "温度" + series_RealTemper.Points[locatonX - startPosition].YValues[0] + "℃\r\n" + "距离" + (locatonX) + "米");
                            }
                        }
                    }
                }
            }
        }

        Point point1 = new Point();
        Point point2 = new Point();
        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            point1 = new Point(e.X, e.Y);
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            point2 = new Point(e.X, e.Y);
            if (point1.X - point2.X > 150 && point1.Y - point2.Y > 150)
            {
                chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(2);
                chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset(2);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
            }
            catch (Exception)
            {

            }

        }
    }
}
