
namespace StockWatcher
{
    partial class StockPreviewControl
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
            this.ContextMenu = null;
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
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
            this.labelForStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelForStatus
            // 
            this.labelForStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelForStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelForStatus.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelForStatus.ForeColor = System.Drawing.Color.White;
            this.labelForStatus.Location = new System.Drawing.Point(0, 0);
            this.labelForStatus.Name = "labelForStatus";
            this.labelForStatus.Size = new System.Drawing.Size(150, 45);
            this.labelForStatus.TabIndex = 0;
            this.labelForStatus.Text = "正在加载行情";
            this.labelForStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelForStatus.DoubleClick += new System.EventHandler(this.labelForStatus_DoubleClick);
            // 
            // StockPreviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.labelForStatus);
            this.Name = "StockPreviewControl";
            this.Size = new System.Drawing.Size(150, 45);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelForStatus;
    }
}
