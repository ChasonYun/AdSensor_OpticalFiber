namespace OpticalFiber
{
    partial class FrmCheckPwd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnQuit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxPwd = new System.Windows.Forms.TextBox();
            this.btnChangeOperatorPwd = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlHeader.Controls.Add(this.btnQuit);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(449, 50);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeader_MouseDown);
            // 
            // btnQuit
            // 
            this.btnQuit.BackgroundImage = global::OpticalFiber.Properties.Resources.Frm_Close;
            this.btnQuit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnQuit.FlatAppearance.BorderSize = 0;
            this.btnQuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuit.Location = new System.Drawing.Point(399, 0);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(50, 50);
            this.btnQuit.TabIndex = 2;
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(124, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "请输入操作员或管理员密码";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(108, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "密码";
            // 
            // tbxPwd
            // 
            this.tbxPwd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxPwd.Location = new System.Drawing.Point(158, 114);
            this.tbxPwd.Name = "tbxPwd";
            this.tbxPwd.PasswordChar = '*';
            this.tbxPwd.ShortcutsEnabled = false;
            this.tbxPwd.Size = new System.Drawing.Size(149, 23);
            this.tbxPwd.TabIndex = 2;
            this.tbxPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnChangeOperatorPwd
            // 
            this.btnChangeOperatorPwd.Location = new System.Drawing.Point(195, 177);
            this.btnChangeOperatorPwd.Name = "btnChangeOperatorPwd";
            this.btnChangeOperatorPwd.Size = new System.Drawing.Size(75, 25);
            this.btnChangeOperatorPwd.TabIndex = 3;
            this.btnChangeOperatorPwd.Text = "确定";
            this.btnChangeOperatorPwd.UseVisualStyleBackColor = true;
            this.btnChangeOperatorPwd.Click += new System.EventHandler(this.btnChangeOperatorPwd_Click);
            // 
            // FrmCheckPwd
            // 
            this.AcceptButton = this.btnChangeOperatorPwd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 235);
            this.Controls.Add(this.btnChangeOperatorPwd);
            this.Controls.Add(this.tbxPwd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCheckPwd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmCheckPwd";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmCheckPwd_MouseDown);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxPwd;
        private System.Windows.Forms.Button btnChangeOperatorPwd;
    }
}