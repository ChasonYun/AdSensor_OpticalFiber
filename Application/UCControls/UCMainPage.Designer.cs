namespace OpticalFiber
{
    partial class UCMainPage
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
            this.spcPagePic = new System.Windows.Forms.SplitContainer();
            this.pnlMainPage = new System.Windows.Forms.Panel();
            this.grpPrjName = new System.Windows.Forms.GroupBox();
            this.tbxPrjName = new System.Windows.Forms.TextBox();
            this.btnChangePrjName = new System.Windows.Forms.Button();
            this.grpPic = new System.Windows.Forms.GroupBox();
            this.btnChangePic = new System.Windows.Forms.Button();
            this.grpChannel = new System.Windows.Forms.GroupBox();
            this.cbxChannelNo = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbxChannelName = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDeviceNo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.spcPagePic)).BeginInit();
            this.spcPagePic.Panel1.SuspendLayout();
            this.spcPagePic.Panel2.SuspendLayout();
            this.spcPagePic.SuspendLayout();
            this.grpPrjName.SuspendLayout();
            this.grpPic.SuspendLayout();
            this.grpChannel.SuspendLayout();
            this.SuspendLayout();
            // 
            // spcPagePic
            // 
            this.spcPagePic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcPagePic.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spcPagePic.IsSplitterFixed = true;
            this.spcPagePic.Location = new System.Drawing.Point(0, 0);
            this.spcPagePic.Margin = new System.Windows.Forms.Padding(0);
            this.spcPagePic.Name = "spcPagePic";
            // 
            // spcPagePic.Panel1
            // 
            this.spcPagePic.Panel1.Controls.Add(this.pnlMainPage);
            // 
            // spcPagePic.Panel2
            // 
            this.spcPagePic.Panel2.BackColor = System.Drawing.Color.SteelBlue;
            this.spcPagePic.Panel2.Controls.Add(this.grpPrjName);
            this.spcPagePic.Panel2.Controls.Add(this.grpPic);
            this.spcPagePic.Panel2.Controls.Add(this.grpChannel);
            this.spcPagePic.Panel2.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.spcPagePic.Size = new System.Drawing.Size(1719, 800);
            this.spcPagePic.SplitterDistance = 1400;
            this.spcPagePic.TabIndex = 0;
            // 
            // pnlMainPage
            // 
            this.pnlMainPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMainPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainPage.Location = new System.Drawing.Point(0, 0);
            this.pnlMainPage.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMainPage.Name = "pnlMainPage";
            this.pnlMainPage.Size = new System.Drawing.Size(1400, 800);
            this.pnlMainPage.TabIndex = 0;
            // 
            // grpPrjName
            // 
            this.grpPrjName.Controls.Add(this.tbxPrjName);
            this.grpPrjName.Controls.Add(this.btnChangePrjName);
            this.grpPrjName.Location = new System.Drawing.Point(33, 601);
            this.grpPrjName.Name = "grpPrjName";
            this.grpPrjName.Size = new System.Drawing.Size(259, 183);
            this.grpPrjName.TabIndex = 7;
            this.grpPrjName.TabStop = false;
            this.grpPrjName.Text = "项目名称设置";
            // 
            // tbxPrjName
            // 
            this.tbxPrjName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxPrjName.Location = new System.Drawing.Point(20, 58);
            this.tbxPrjName.Name = "tbxPrjName";
            this.tbxPrjName.Size = new System.Drawing.Size(221, 23);
            this.tbxPrjName.TabIndex = 1;
            this.tbxPrjName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnChangePrjName
            // 
            this.btnChangePrjName.Location = new System.Drawing.Point(78, 121);
            this.btnChangePrjName.Name = "btnChangePrjName";
            this.btnChangePrjName.Size = new System.Drawing.Size(117, 32);
            this.btnChangePrjName.TabIndex = 0;
            this.btnChangePrjName.Text = "确定";
            this.btnChangePrjName.UseVisualStyleBackColor = true;
            this.btnChangePrjName.Click += new System.EventHandler(this.btnChangePrjName_Click);
            // 
            // grpPic
            // 
            this.grpPic.Controls.Add(this.btnChangePic);
            this.grpPic.Location = new System.Drawing.Point(33, 397);
            this.grpPic.Name = "grpPic";
            this.grpPic.Size = new System.Drawing.Size(259, 161);
            this.grpPic.TabIndex = 7;
            this.grpPic.TabStop = false;
            this.grpPic.Text = "主页图片设置";
            // 
            // btnChangePic
            // 
            this.btnChangePic.Location = new System.Drawing.Point(51, 69);
            this.btnChangePic.Name = "btnChangePic";
            this.btnChangePic.Size = new System.Drawing.Size(156, 41);
            this.btnChangePic.TabIndex = 0;
            this.btnChangePic.Text = "更换图片";
            this.btnChangePic.UseVisualStyleBackColor = true;
            this.btnChangePic.Click += new System.EventHandler(this.btnChangePic_Click);
            // 
            // grpChannel
            // 
            this.grpChannel.Controls.Add(this.cbxChannelNo);
            this.grpChannel.Controls.Add(this.btnSave);
            this.grpChannel.Controls.Add(this.tbxChannelName);
            this.grpChannel.Controls.Add(this.btnDelete);
            this.grpChannel.Controls.Add(this.btnAdd);
            this.grpChannel.Controls.Add(this.label1);
            this.grpChannel.Controls.Add(this.label2);
            this.grpChannel.Controls.Add(this.label3);
            this.grpChannel.Controls.Add(this.cbxDeviceNo);
            this.grpChannel.Location = new System.Drawing.Point(33, 32);
            this.grpChannel.Name = "grpChannel";
            this.grpChannel.Size = new System.Drawing.Size(259, 309);
            this.grpChannel.TabIndex = 6;
            this.grpChannel.TabStop = false;
            this.grpChannel.Text = "通道设置";
            // 
            // cbxChannelNo
            // 
            this.cbxChannelNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChannelNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxChannelNo.FormattingEnabled = true;
            this.cbxChannelNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbxChannelNo.Location = new System.Drawing.Point(134, 92);
            this.cbxChannelNo.Name = "cbxChannelNo";
            this.cbxChannelNo.Size = new System.Drawing.Size(99, 24);
            this.cbxChannelNo.TabIndex = 3;
            this.cbxChannelNo.SelectedIndexChanged += new System.EventHandler(this.cbxChannelNo_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(95, 254);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存修改";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbxChannelName
            // 
            this.tbxChannelName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxChannelName.Location = new System.Drawing.Point(95, 133);
            this.tbxChannelName.Name = "tbxChannelName";
            this.tbxChannelName.Size = new System.Drawing.Size(161, 26);
            this.tbxChannelName.TabIndex = 5;
            this.tbxChannelName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.Location = new System.Drawing.Point(163, 187);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Location = new System.Drawing.Point(32, 187);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 30);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(17, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "设备编号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(17, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "通道编号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(17, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "通道名称：";
            // 
            // cbxDeviceNo
            // 
            this.cbxDeviceNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDeviceNo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxDeviceNo.FormattingEnabled = true;
            this.cbxDeviceNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbxDeviceNo.Location = new System.Drawing.Point(134, 50);
            this.cbxDeviceNo.Name = "cbxDeviceNo";
            this.cbxDeviceNo.Size = new System.Drawing.Size(99, 24);
            this.cbxDeviceNo.TabIndex = 3;
            this.cbxDeviceNo.SelectedIndexChanged += new System.EventHandler(this.cbxDeviceNo_SelectedIndexChanged);
            // 
            // UCMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spcPagePic);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCMainPage";
            this.Size = new System.Drawing.Size(1719, 800);
            this.spcPagePic.Panel1.ResumeLayout(false);
            this.spcPagePic.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcPagePic)).EndInit();
            this.spcPagePic.ResumeLayout(false);
            this.grpPrjName.ResumeLayout(false);
            this.grpPrjName.PerformLayout();
            this.grpPic.ResumeLayout(false);
            this.grpChannel.ResumeLayout(false);
            this.grpChannel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlMainPage;
        public System.Windows.Forms.SplitContainer spcPagePic;
        private System.Windows.Forms.ComboBox cbxChannelNo;
        private System.Windows.Forms.ComboBox cbxDeviceNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxChannelName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpPic;
        private System.Windows.Forms.Button btnChangePic;
        private System.Windows.Forms.GroupBox grpChannel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox grpPrjName;
        private System.Windows.Forms.TextBox tbxPrjName;
        private System.Windows.Forms.Button btnChangePrjName;
    }
}
