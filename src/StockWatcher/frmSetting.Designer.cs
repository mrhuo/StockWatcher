
namespace StockWatcher
{
    partial class frmSetting
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
            this.labelForRefresh = new System.Windows.Forms.Label();
            this.comboBoxForRefresh = new System.Windows.Forms.ComboBox();
            this.labelForList = new System.Windows.Forms.Label();
            this.textBoxStockList = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelForRefresh
            // 
            this.labelForRefresh.AutoSize = true;
            this.labelForRefresh.Location = new System.Drawing.Point(12, 18);
            this.labelForRefresh.Name = "labelForRefresh";
            this.labelForRefresh.Size = new System.Drawing.Size(127, 15);
            this.labelForRefresh.TabIndex = 3;
            this.labelForRefresh.Text = "刷新间隔（秒）：";
            // 
            // comboBoxForRefresh
            // 
            this.comboBoxForRefresh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForRefresh.Items.AddRange(new object[] {
            "3",
            "5",
            "8",
            "10",
            "15"});
            this.comboBoxForRefresh.Location = new System.Drawing.Point(145, 15);
            this.comboBoxForRefresh.Name = "comboBoxForRefresh";
            this.comboBoxForRefresh.Size = new System.Drawing.Size(151, 23);
            this.comboBoxForRefresh.TabIndex = 4;
            // 
            // labelForList
            // 
            this.labelForList.AutoSize = true;
            this.labelForList.Location = new System.Drawing.Point(12, 58);
            this.labelForList.Name = "labelForList";
            this.labelForList.Size = new System.Drawing.Size(172, 15);
            this.labelForList.TabIndex = 5;
            this.labelForList.Text = "监控列表（一行一个）：";
            // 
            // textBoxStockList
            // 
            this.textBoxStockList.Location = new System.Drawing.Point(15, 79);
            this.textBoxStockList.Multiline = true;
            this.textBoxStockList.Name = "textBoxStockList";
            this.textBoxStockList.Size = new System.Drawing.Size(281, 218);
            this.textBoxStockList.TabIndex = 6;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(183, 304);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(113, 37);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "保存设置";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 353);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxStockList);
            this.Controls.Add(this.labelForList);
            this.Controls.Add(this.comboBoxForRefresh);
            this.Controls.Add(this.labelForRefresh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelForRefresh;
        private System.Windows.Forms.ComboBox comboBoxForRefresh;
        private System.Windows.Forms.Label labelForList;
        private System.Windows.Forms.TextBox textBoxStockList;
        private System.Windows.Forms.Button buttonSave;
    }
}