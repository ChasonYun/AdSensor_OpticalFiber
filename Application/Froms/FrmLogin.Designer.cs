namespace OpticalFiber
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.lblTiltle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxUser = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxPwd = new System.Windows.Forms.TextBox();
            this.pbxUser = new System.Windows.Forms.PictureBox();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.btnSignOut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxUser)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTiltle
            // 
            this.lblTiltle.AutoSize = true;
            this.lblTiltle.BackColor = System.Drawing.Color.Transparent;
            this.lblTiltle.Font = new System.Drawing.Font("黑体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTiltle.ForeColor = System.Drawing.Color.Black;
            this.lblTiltle.Location = new System.Drawing.Point(181, 126);
            this.lblTiltle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTiltle.Name = "lblTiltle";
            this.lblTiltle.Size = new System.Drawing.Size(452, 48);
            this.lblTiltle.TabIndex = 0;
            this.lblTiltle.Text = "分布式光纤测温系统";
            this.lblTiltle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTiltle_MouseDown);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(20, 18);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(346, 24);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "无锡圣敏传感科技股份有限公司";
            this.lblName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblName_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(307, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "请选择用户：";
            // 
            // cbxUser
            // 
            this.cbxUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUser.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbxUser.FormattingEnabled = true;
            this.cbxUser.Items.AddRange(new object[] {
            "普通用户",
            "系统操作员",
            "系统管理员"});
            this.cbxUser.Location = new System.Drawing.Point(417, 233);
            this.cbxUser.Name = "cbxUser";
            this.cbxUser.Size = new System.Drawing.Size(121, 24);
            this.cbxUser.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(307, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "请输入密码：";
            // 
            // tbxPwd
            // 
            this.tbxPwd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxPwd.Location = new System.Drawing.Point(417, 276);
            this.tbxPwd.Name = "tbxPwd";
            this.tbxPwd.PasswordChar = '*';
            this.tbxPwd.ShortcutsEnabled = false;
            this.tbxPwd.Size = new System.Drawing.Size(120, 26);
            this.tbxPwd.TabIndex = 4;
            this.tbxPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pbxUser
            // 
            this.pbxUser.BackgroundImage = global::OpticalFiber.Properties.Resources.user;
            this.pbxUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxUser.Location = new System.Drawing.Point(244, 242);
            this.pbxUser.Margin = new System.Windows.Forms.Padding(0);
            this.pbxUser.Name = "pbxUser";
            this.pbxUser.Size = new System.Drawing.Size(60, 60);
            this.pbxUser.TabIndex = 5;
            this.pbxUser.TabStop = false;
            this.pbxUser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxUser_MouseDown);
            // 
            // btnSignIn
            // 
            this.btnSignIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSignIn.FlatAppearance.BorderSize = 0;
            this.btnSignIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignIn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSignIn.Location = new System.Drawing.Point(421, 361);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(117, 31);
            this.btnSignIn.TabIndex = 6;
            this.btnSignIn.Text = "登录";
            this.btnSignIn.UseVisualStyleBackColor = false;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // btnSignOut
            // 
            this.btnSignOut.BackColor = System.Drawing.Color.Green;
            this.btnSignOut.FlatAppearance.BorderSize = 0;
            this.btnSignOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignOut.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSignOut.Location = new System.Drawing.Point(259, 361);
            this.btnSignOut.Name = "btnSignOut";
            this.btnSignOut.Size = new System.Drawing.Size(117, 31);
            this.btnSignOut.TabIndex = 6;
            this.btnSignOut.Text = "退出";
            this.btnSignOut.UseVisualStyleBackColor = false;
            this.btnSignOut.Click += new System.EventHandler(this.btnSignOut_Click);
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.btnSignIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(169)))), ((int)(((byte)(230)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSignOut);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.pbxUser);
            this.Controls.Add(this.tbxPwd);
            this.Controls.Add(this.cbxUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblTiltle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbxUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTiltle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxPwd;
        private System.Windows.Forms.PictureBox pbxUser;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Button btnSignOut;
    }
}