namespace OpticalFiber
{
    partial class UCRunStatus
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblReferTemper = new System.Windows.Forms.Label();
            this.lblChasissTemper = new System.Windows.Forms.Label();
            this.lblCompnsation = new System.Windows.Forms.Label();
            this.lblScanChannel_1 = new System.Windows.Forms.Label();
            this.lblDetectionStatus_1 = new System.Windows.Forms.Label();
            this.timerRunStatus = new System.Windows.Forms.Timer(this.components);
            this.lblScanChannel_2 = new System.Windows.Forms.Label();
            this.lblDetectionStatus_2 = new System.Windows.Forms.Label();
            this.lblScanChannel_3 = new System.Windows.Forms.Label();
            this.lblDetectionStatus_3 = new System.Windows.Forms.Label();
            this.lblScanChannel_4 = new System.Windows.Forms.Label();
            this.lblDetectionStatus_4 = new System.Windows.Forms.Label();
            this.pnlAlarmMsg = new System.Windows.Forms.Panel();
            this.lblAlarmMsg = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.pnlAlarmMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblReferTemper
            // 
            this.lblReferTemper.AutoSize = true;
            this.lblReferTemper.Location = new System.Drawing.Point(3, 11);
            this.lblReferTemper.Name = "lblReferTemper";
            this.lblReferTemper.Size = new System.Drawing.Size(53, 12);
            this.lblReferTemper.TabIndex = 0;
            this.lblReferTemper.Text = "参考温度";
            // 
            // lblChasissTemper
            // 
            this.lblChasissTemper.AutoSize = true;
            this.lblChasissTemper.Location = new System.Drawing.Point(104, 11);
            this.lblChasissTemper.Name = "lblChasissTemper";
            this.lblChasissTemper.Size = new System.Drawing.Size(53, 12);
            this.lblChasissTemper.TabIndex = 0;
            this.lblChasissTemper.Text = "机箱温度";
            // 
            // lblCompnsation
            // 
            this.lblCompnsation.AutoSize = true;
            this.lblCompnsation.Location = new System.Drawing.Point(3, 31);
            this.lblCompnsation.Name = "lblCompnsation";
            this.lblCompnsation.Size = new System.Drawing.Size(53, 12);
            this.lblCompnsation.TabIndex = 0;
            this.lblCompnsation.Text = "补偿系数";
            // 
            // lblScanChannel_1
            // 
            this.lblScanChannel_1.AutoSize = true;
            this.lblScanChannel_1.Location = new System.Drawing.Point(3, 53);
            this.lblScanChannel_1.Name = "lblScanChannel_1";
            this.lblScanChannel_1.Size = new System.Drawing.Size(59, 12);
            this.lblScanChannel_1.TabIndex = 0;
            this.lblScanChannel_1.Text = "通道号：1";
            // 
            // lblDetectionStatus_1
            // 
            this.lblDetectionStatus_1.AutoSize = true;
            this.lblDetectionStatus_1.Location = new System.Drawing.Point(3, 73);
            this.lblDetectionStatus_1.Name = "lblDetectionStatus_1";
            this.lblDetectionStatus_1.Size = new System.Drawing.Size(59, 12);
            this.lblDetectionStatus_1.TabIndex = 0;
            this.lblDetectionStatus_1.Text = "准备中...";
            // 
            // timerRunStatus
            // 
            this.timerRunStatus.Interval = 500;
            this.timerRunStatus.Tick += new System.EventHandler(this.timerRunStatus_Tick);
            // 
            // lblScanChannel_2
            // 
            this.lblScanChannel_2.AutoSize = true;
            this.lblScanChannel_2.Location = new System.Drawing.Point(104, 53);
            this.lblScanChannel_2.Name = "lblScanChannel_2";
            this.lblScanChannel_2.Size = new System.Drawing.Size(59, 12);
            this.lblScanChannel_2.TabIndex = 0;
            this.lblScanChannel_2.Text = "通道号：2";
            // 
            // lblDetectionStatus_2
            // 
            this.lblDetectionStatus_2.AutoSize = true;
            this.lblDetectionStatus_2.Location = new System.Drawing.Point(104, 73);
            this.lblDetectionStatus_2.Name = "lblDetectionStatus_2";
            this.lblDetectionStatus_2.Size = new System.Drawing.Size(59, 12);
            this.lblDetectionStatus_2.TabIndex = 0;
            this.lblDetectionStatus_2.Text = "准备中...";
            // 
            // lblScanChannel_3
            // 
            this.lblScanChannel_3.AutoSize = true;
            this.lblScanChannel_3.Location = new System.Drawing.Point(3, 96);
            this.lblScanChannel_3.Name = "lblScanChannel_3";
            this.lblScanChannel_3.Size = new System.Drawing.Size(59, 12);
            this.lblScanChannel_3.TabIndex = 0;
            this.lblScanChannel_3.Text = "通道号：3";
            // 
            // lblDetectionStatus_3
            // 
            this.lblDetectionStatus_3.AutoSize = true;
            this.lblDetectionStatus_3.Location = new System.Drawing.Point(3, 116);
            this.lblDetectionStatus_3.Name = "lblDetectionStatus_3";
            this.lblDetectionStatus_3.Size = new System.Drawing.Size(59, 12);
            this.lblDetectionStatus_3.TabIndex = 0;
            this.lblDetectionStatus_3.Text = "准备中...";
            // 
            // lblScanChannel_4
            // 
            this.lblScanChannel_4.AutoSize = true;
            this.lblScanChannel_4.Location = new System.Drawing.Point(104, 96);
            this.lblScanChannel_4.Name = "lblScanChannel_4";
            this.lblScanChannel_4.Size = new System.Drawing.Size(59, 12);
            this.lblScanChannel_4.TabIndex = 0;
            this.lblScanChannel_4.Text = "通道号：4";
            // 
            // lblDetectionStatus_4
            // 
            this.lblDetectionStatus_4.AutoSize = true;
            this.lblDetectionStatus_4.Location = new System.Drawing.Point(104, 116);
            this.lblDetectionStatus_4.Name = "lblDetectionStatus_4";
            this.lblDetectionStatus_4.Size = new System.Drawing.Size(59, 12);
            this.lblDetectionStatus_4.TabIndex = 0;
            this.lblDetectionStatus_4.Text = "准备中...";
            // 
            // pnlAlarmMsg
            // 
            this.pnlAlarmMsg.BackColor = System.Drawing.Color.Transparent;
            this.pnlAlarmMsg.Controls.Add(this.lblDevice);
            this.pnlAlarmMsg.Controls.Add(this.lblAlarmMsg);
            this.pnlAlarmMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAlarmMsg.Location = new System.Drawing.Point(0, 0);
            this.pnlAlarmMsg.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAlarmMsg.Name = "pnlAlarmMsg";
            this.pnlAlarmMsg.Size = new System.Drawing.Size(210, 143);
            this.pnlAlarmMsg.TabIndex = 1;
            // 
            // lblAlarmMsg
            // 
            this.lblAlarmMsg.AutoSize = true;
            this.lblAlarmMsg.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAlarmMsg.Location = new System.Drawing.Point(57, 54);
            this.lblAlarmMsg.Name = "lblAlarmMsg";
            this.lblAlarmMsg.Size = new System.Drawing.Size(89, 19);
            this.lblAlarmMsg.TabIndex = 0;
            this.lblAlarmMsg.Text = "报警信息";
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDevice.Location = new System.Drawing.Point(20, 16);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(49, 14);
            this.lblDevice.TabIndex = 1;
            this.lblDevice.Text = "设备：";
            // 
            // UCRunStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.pnlAlarmMsg);
            this.Controls.Add(this.lblDetectionStatus_4);
            this.Controls.Add(this.lblDetectionStatus_3);
            this.Controls.Add(this.lblScanChannel_4);
            this.Controls.Add(this.lblScanChannel_3);
            this.Controls.Add(this.lblDetectionStatus_2);
            this.Controls.Add(this.lblScanChannel_2);
            this.Controls.Add(this.lblDetectionStatus_1);
            this.Controls.Add(this.lblScanChannel_1);
            this.Controls.Add(this.lblCompnsation);
            this.Controls.Add(this.lblChasissTemper);
            this.Controls.Add(this.lblReferTemper);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCRunStatus";
            this.Size = new System.Drawing.Size(210, 143);
            this.pnlAlarmMsg.ResumeLayout(false);
            this.pnlAlarmMsg.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReferTemper;
        private System.Windows.Forms.Label lblChasissTemper;
        private System.Windows.Forms.Label lblCompnsation;
        private System.Windows.Forms.Label lblScanChannel_1;
        private System.Windows.Forms.Label lblDetectionStatus_1;
        private System.Windows.Forms.Timer timerRunStatus;
        private System.Windows.Forms.Label lblScanChannel_2;
        private System.Windows.Forms.Label lblDetectionStatus_2;
        private System.Windows.Forms.Label lblScanChannel_3;
        private System.Windows.Forms.Label lblDetectionStatus_3;
        private System.Windows.Forms.Label lblScanChannel_4;
        private System.Windows.Forms.Label lblDetectionStatus_4;
        private System.Windows.Forms.Panel pnlAlarmMsg;
        private System.Windows.Forms.Label lblAlarmMsg;
        private System.Windows.Forms.Label lblDevice;
    }
}
