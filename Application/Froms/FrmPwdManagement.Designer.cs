namespace OpticalFiber
{
    partial class FrmPwdManagement
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChangeUserPwd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxUser_ = new System.Windows.Forms.TextBox();
            this.tbxUser = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnChangeOperatorPwd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxOperator_ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxOperator = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnChangeAdminPwd = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxAdmin_ = new System.Windows.Forms.TextBox();
            this.tbxAdmin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.btnQuit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(773, 50);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnQuit
            // 
            this.btnQuit.BackgroundImage = global::OpticalFiber.Properties.Resources.Frm_Close;
            this.btnQuit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnQuit.FlatAppearance.BorderSize = 0;
            this.btnQuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuit.Location = new System.Drawing.Point(723, 0);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(0);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(50, 50);
            this.btnQuit.TabIndex = 1;
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(333, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "密码管理";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnChangeUserPwd);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxUser_);
            this.groupBox1.Controls.Add(this.tbxUser);
            this.groupBox1.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(38, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 285);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户密码";
            // 
            // btnChangeUserPwd
            // 
            this.btnChangeUserPwd.Location = new System.Drawing.Point(81, 212);
            this.btnChangeUserPwd.Name = "btnChangeUserPwd";
            this.btnChangeUserPwd.Size = new System.Drawing.Size(75, 25);
            this.btnChangeUserPwd.TabIndex = 2;
            this.btnChangeUserPwd.Text = "确定";
            this.btnChangeUserPwd.UseVisualStyleBackColor = true;
            this.btnChangeUserPwd.Click += new System.EventHandler(this.btnChangeUserPwd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "再次输入";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "新密码";
            // 
            // tbxUser_
            // 
            this.tbxUser_.Location = new System.Drawing.Point(81, 126);
            this.tbxUser_.Name = "tbxUser_";
            this.tbxUser_.PasswordChar = '*';
            this.tbxUser_.ShortcutsEnabled = false;
            this.tbxUser_.Size = new System.Drawing.Size(138, 26);
            this.tbxUser_.TabIndex = 0;
            this.tbxUser_.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxUser
            // 
            this.tbxUser.Location = new System.Drawing.Point(81, 74);
            this.tbxUser.Name = "tbxUser";
            this.tbxUser.PasswordChar = '*';
            this.tbxUser.ShortcutsEnabled = false;
            this.tbxUser.Size = new System.Drawing.Size(138, 26);
            this.tbxUser.TabIndex = 0;
            this.tbxUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnChangeOperatorPwd);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbxOperator_);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbxOperator);
            this.groupBox2.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(283, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 285);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "系统操作员密码";
            // 
            // btnChangeOperatorPwd
            // 
            this.btnChangeOperatorPwd.Location = new System.Drawing.Point(88, 212);
            this.btnChangeOperatorPwd.Name = "btnChangeOperatorPwd";
            this.btnChangeOperatorPwd.Size = new System.Drawing.Size(75, 25);
            this.btnChangeOperatorPwd.TabIndex = 2;
            this.btnChangeOperatorPwd.Text = "确定";
            this.btnChangeOperatorPwd.UseVisualStyleBackColor = true;
            this.btnChangeOperatorPwd.Click += new System.EventHandler(this.btnChangeOperatorPwd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "再次输入";
            // 
            // tbxOperator_
            // 
            this.tbxOperator_.Location = new System.Drawing.Point(81, 126);
            this.tbxOperator_.Name = "tbxOperator_";
            this.tbxOperator_.PasswordChar = '*';
            this.tbxOperator_.ShortcutsEnabled = false;
            this.tbxOperator_.Size = new System.Drawing.Size(138, 26);
            this.tbxOperator_.TabIndex = 0;
            this.tbxOperator_.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "新密码";
            // 
            // tbxOperator
            // 
            this.tbxOperator.Location = new System.Drawing.Point(81, 74);
            this.tbxOperator.Name = "tbxOperator";
            this.tbxOperator.PasswordChar = '*';
            this.tbxOperator.ShortcutsEnabled = false;
            this.tbxOperator.Size = new System.Drawing.Size(138, 26);
            this.tbxOperator.TabIndex = 0;
            this.tbxOperator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnChangeAdminPwd);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tbxAdmin_);
            this.groupBox3.Controls.Add(this.tbxAdmin);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(525, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(225, 285);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "系统管理员密码";
            // 
            // btnChangeAdminPwd
            // 
            this.btnChangeAdminPwd.Location = new System.Drawing.Point(92, 212);
            this.btnChangeAdminPwd.Name = "btnChangeAdminPwd";
            this.btnChangeAdminPwd.Size = new System.Drawing.Size(75, 25);
            this.btnChangeAdminPwd.TabIndex = 2;
            this.btnChangeAdminPwd.Text = "确定";
            this.btnChangeAdminPwd.UseVisualStyleBackColor = true;
            this.btnChangeAdminPwd.Click += new System.EventHandler(this.btnChangeAdminPwd_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "再次输入";
            // 
            // tbxAdmin_
            // 
            this.tbxAdmin_.Location = new System.Drawing.Point(81, 126);
            this.tbxAdmin_.Name = "tbxAdmin_";
            this.tbxAdmin_.PasswordChar = '*';
            this.tbxAdmin_.ShortcutsEnabled = false;
            this.tbxAdmin_.Size = new System.Drawing.Size(138, 26);
            this.tbxAdmin_.TabIndex = 0;
            this.tbxAdmin_.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbxAdmin
            // 
            this.tbxAdmin.Location = new System.Drawing.Point(81, 74);
            this.tbxAdmin.Name = "tbxAdmin";
            this.tbxAdmin.PasswordChar = '*';
            this.tbxAdmin.ShortcutsEnabled = false;
            this.tbxAdmin.Size = new System.Drawing.Size(138, 26);
            this.tbxAdmin.TabIndex = 0;
            this.tbxAdmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "新密码";
            // 
            // FrmPwdManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 380);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPwdManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPwdManagement";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnChangeUserPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxUser_;
        private System.Windows.Forms.TextBox tbxUser;
        private System.Windows.Forms.Button btnChangeOperatorPwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxOperator_;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxOperator;
        private System.Windows.Forms.Button btnChangeAdminPwd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxAdmin_;
        private System.Windows.Forms.TextBox tbxAdmin;
        private System.Windows.Forms.Label label6;
    }
}