using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace StockWatcher
{
    public partial class frmSetting : Form
    {
        private readonly StockPreviewControl userControl;
        public frmSetting(StockPreviewControl userControl)
        {
            InitializeComponent();
            this.userControl = userControl;
        }

        private void LoadStockList()
        {
            var list = (from a in StockConfig.StockList
                        select a.Substring(a.Length - 6)).ToList();
            this.textBoxStockList.Text = string.Join("\r\n", list);
        }

        private void buttonForAddStock_Click(object sender, EventArgs e)
        {
            var input = Util.Input("请输入正确的股票代码：");
            var stockCode = Util.ReturnFixStockCode(input);
            if (stockCode == null)
            {
                Util.Error($"您输入的股票代码【{input}】不是正确的股票代码或已存在！");
                return;
            }
            if (StockConfig.AddStock(stockCode))
            {
                LoadStockList();
            }
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            var defaultInterval = (StockConfig.RefreshInterval / 1000).ToString();
            this.comboBoxForRefresh.SelectedItem = defaultInterval;

            LoadStockList();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (this.comboBoxForRefresh.SelectedItem == null)
            {
                Util.Alert("请选择刷新间隔！");
                return;
            }
            var refreshInterval = int.Parse(this.comboBoxForRefresh.SelectedItem.ToString());
            var stockList = this.textBoxStockList.Text.Trim();
            var arr = stockList.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<string>();
            foreach (var item in arr)
            {
                var _code = Util.ReturnFixStockCode(item);
                if (string.IsNullOrEmpty(_code))
                {
                    Util.Alert($"您输入的股票代码【{item}】不是正确的代码，请检查！");
                    return;
                }
                list.Add(_code);
            }
            StockConfig.RefreshInterval = refreshInterval;
            StockConfig.StockList = list;
            userControl.ResetTimer();
            Util.Alert("保存成功！");
            this.Close();
        }
    }
}
