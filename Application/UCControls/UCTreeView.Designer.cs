namespace OpticalFiber
{
    partial class UCTreeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTreeView));
            this.tvw = new System.Windows.Forms.TreeView();
            this.img = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tvw
            // 
            this.tvw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvw.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvw.ImageIndex = 0;
            this.tvw.ImageList = this.img;
            this.tvw.Location = new System.Drawing.Point(0, 0);
            this.tvw.Margin = new System.Windows.Forms.Padding(0);
            this.tvw.Name = "tvw";
            this.tvw.SelectedImageIndex = 3;
            this.tvw.Size = new System.Drawing.Size(200, 595);
            this.tvw.TabIndex = 0;
            this.tvw.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvw_AfterSelect);
            this.tvw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvw_MouseDown);
            // 
            // img
            // 
            this.img.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img.ImageStream")));
            this.img.TransparentColor = System.Drawing.Color.Transparent;
            this.img.Images.SetKeyName(0, "device.png");
            this.img.Images.SetKeyName(1, "channel.png");
            this.img.Images.SetKeyName(2, "partition.png");
            this.img.Images.SetKeyName(3, "checked.png");
            // 
            // UCTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvw);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCTreeView";
            this.Size = new System.Drawing.Size(200, 595);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvw;
        private System.Windows.Forms.ImageList img;
    }
}
