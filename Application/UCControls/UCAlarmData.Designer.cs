namespace OpticalFiber
{
    partial class UCAlarmData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.spcAlarmData = new System.Windows.Forms.SplitContainer();
            this.pnlHeader_Title = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnQuary = new System.Windows.Forms.Button();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxAlarmType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAlarmData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.spcAlarmData)).BeginInit();
            this.spcAlarmData.Panel1.SuspendLayout();
            this.spcAlarmData.Panel2.SuspendLayout();
            this.spcAlarmData.SuspendLayout();
            this.pnlHeader_Title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmData)).BeginInit();
            this.SuspendLayout();
            // 
            // spcAlarmData
            // 
            this.spcAlarmData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcAlarmData.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spcAlarmData.IsSplitterFixed = true;
            this.spcAlarmData.Location = new System.Drawing.Point(0, 0);
            this.spcAlarmData.Margin = new System.Windows.Forms.Padding(0);
            this.spcAlarmData.Name = "spcAlarmData";
            this.spcAlarmData.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcAlarmData.Panel1
            // 
            this.spcAlarmData.Panel1.Controls.Add(this.pnlHeader_Title);
            // 
            // spcAlarmData.Panel2
            // 
            this.spcAlarmData.Panel2.Controls.Add(this.dgvAlarmData);
            this.spcAlarmData.Size = new System.Drawing.Size(1719, 709);
            this.spcAlarmData.SplitterDistance = 90;
            this.spcAlarmData.SplitterWidth = 1;
            this.spcAlarmData.TabIndex = 0;
            // 
            // pnlHeader_Title
            // 
            this.pnlHeader_Title.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlHeader_Title.Controls.Add(this.btnExport);
            this.pnlHeader_Title.Controls.Add(this.btnQuary);
            this.pnlHeader_Title.Controls.Add(this.dtpEnd);
            this.pnlHeader_Title.Controls.Add(this.dtpStart);
            this.pnlHeader_Title.Controls.Add(this.label2);
            this.pnlHeader_Title.Controls.Add(this.label4);
            this.pnlHeader_Title.Controls.Add(this.label3);
            this.pnlHeader_Title.Controls.Add(this.cbxAlarmType);
            this.pnlHeader_Title.Controls.Add(this.label1);
            this.pnlHeader_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader_Title.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader_Title.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader_Title.Name = "pnlHeader_Title";
            this.pnlHeader_Title.Size = new System.Drawing.Size(1719, 90);
            this.pnlHeader_Title.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Red;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Location = new System.Drawing.Point(1568, 33);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(95, 30);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnQuary
            // 
            this.btnQuary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnQuary.FlatAppearance.BorderSize = 0;
            this.btnQuary.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.btnQuary.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnQuary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuary.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuary.Location = new System.Drawing.Point(1328, 28);
            this.btnQuary.Name = "btnQuary";
            this.btnQuary.Size = new System.Drawing.Size(95, 30);
            this.btnQuary.TabIndex = 11;
            this.btnQuary.Text = "查询";
            this.btnQuary.UseVisualStyleBackColor = false;
            this.btnQuary.Click += new System.EventHandler(this.btnQuary_Click);
            // 
            // dtpEnd
            // 
            this.dtpEnd.CalendarFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEnd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEnd.Location = new System.Drawing.Point(1115, 32);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(135, 23);
            this.dtpEnd.TabIndex = 9;
            // 
            // dtpStart
            // 
            this.dtpStart.CalendarFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStart.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpStart.Location = new System.Drawing.Point(855, 31);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(135, 23);
            this.dtpStart.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(553, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "报警类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(1020, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "结束日期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(759, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "开始日期";
            // 
            // cbxAlarmType
            // 
            this.cbxAlarmType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAlarmType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxAlarmType.FormattingEnabled = true;
            this.cbxAlarmType.Items.AddRange(new object[] {
            "全部",
            "火灾报警",
            "故障报警",
            "通讯报警"});
            this.cbxAlarmType.Location = new System.Drawing.Point(634, 32);
            this.cbxAlarmType.Name = "cbxAlarmType";
            this.cbxAlarmType.Size = new System.Drawing.Size(96, 22);
            this.cbxAlarmType.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "报警记录";
            // 
            // dgvAlarmData
            // 
            this.dgvAlarmData.AllowUserToAddRows = false;
            this.dgvAlarmData.AllowUserToDeleteRows = false;
            this.dgvAlarmData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            this.dgvAlarmData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAlarmData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAlarmData.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAlarmData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAlarmData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlarmData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlarmData.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAlarmData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlarmData.Location = new System.Drawing.Point(0, 0);
            this.dgvAlarmData.Margin = new System.Windows.Forms.Padding(0);
            this.dgvAlarmData.Name = "dgvAlarmData";
            this.dgvAlarmData.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAlarmData.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAlarmData.RowTemplate.Height = 23;
            this.dgvAlarmData.Size = new System.Drawing.Size(1719, 618);
            this.dgvAlarmData.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "时间";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "设备";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "通道";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "分布";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "位置";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "说明";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "继电器";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "类型";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "报警值";
            this.Column10.Name = "Column10";
            this.Column10.Visible = false;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "限定值";
            this.Column11.Name = "Column11";
            // 
            // UCAlarmData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spcAlarmData);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCAlarmData";
            this.Size = new System.Drawing.Size(1719, 709);
            this.spcAlarmData.Panel1.ResumeLayout(false);
            this.spcAlarmData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcAlarmData)).EndInit();
            this.spcAlarmData.ResumeLayout(false);
            this.pnlHeader_Title.ResumeLayout(false);
            this.pnlHeader_Title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spcAlarmData;
        private System.Windows.Forms.DataGridView dgvAlarmData;
        private System.Windows.Forms.Panel pnlHeader_Title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnQuary;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxAlarmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}
