using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockWatcher
{
    public partial class StockPreviewControl : UserControl
    {
        private Timer timer = null;
        private static int currentIndex = -1;
        private static int maxIndex = -1;
        public StockPreviewControl(CSDeskBand.CSDeskBandWin w)
        {
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();

            this.ContextMenu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("联系作者", new EventHandler((s, e) =>
                {
                    Util.Info($"QQ: 491217650\r\nGithub: https://github.com/mrhuo\r\nEmail: admin@mrhuo.com", "联系作者");
                })),
                new MenuItem("设置", new EventHandler((s, e) =>
                {
                    new frmSetting(this).ShowDialog();
                })),
            });
            ResetTimer();

            w.ShowDW(true);
        }

        public void ResetTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
                timer.Dispose();
                currentIndex = maxIndex = -1;
                timer = null;
            }
            timer = new Timer()
            {
                Interval = StockConfig.RefreshInterval,
                Enabled = true
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LoadSetting();
            if (currentIndex == -1)
            {
                UpdateStatus($"暂无监视对象");
            }
            else
            {
                Task.Run(async () =>
                {
                    var currentCode = StockConfig.StockList[currentIndex];
                    var stockModel = await GetStockInfo(currentCode);
                    if (stockModel == null)
                    {
                        UpdateStatus($"查询{currentCode}失败");
                    }
                    else
                    {
                        labelForStatus.Tag = stockModel;
                        UpdateStatus($"{stockModel.Name} {stockModel.CurrentPrice.ToString("F2")}\r\n{stockModel.PricePercent}{(stockModel.IsUp ? "↑" : "↓")}", stockModel.IsUp ? StockColor.Red : StockColor.Green);
                    }
                });
            }
        }

        private void UpdateStatus(string text, StockColor color = StockColor.Disabled)
        {
            switch (color)
            {
                case StockColor.Green:
                    labelForStatus.ForeColor = Color.FromArgb(0, 255, 0);
                    break;
                case StockColor.Red:
                    labelForStatus.ForeColor = Color.Red;
                    break;
                case StockColor.Disabled:
                    labelForStatus.ForeColor = Color.White;
                    labelForStatus.Tag = null;
                    break;
            }
            labelForStatus.Text = text;
        }

        private void LoadSetting()
        {
            StockConfig.LoadSetting();
            maxIndex = StockConfig.StockList.Count - 1;
            if (maxIndex <= 0)
            {
                currentIndex = -1;
            }
            else if (currentIndex > maxIndex)
            {
                currentIndex = 0;
            }
            else
            {
                ++currentIndex;
            }
        }

        private async Task<StockModel> GetStockInfo(string code)
        {
            try
            {
                var res = await InvokeSinaApi(code);
#if DEBUG
                Util.Log($"服务器返回值【{res}】");
#endif
                var startAt = res.IndexOf("\"") + 1;
                if (startAt == -1)
                {
                    throw new Exception($"服务器返回数据异常：{res}");
                }
                var length = res.LastIndexOf("\"") - res.IndexOf("\"") - 1;
                var result = res.Substring(startAt, length);
                if (string.IsNullOrEmpty(result))
                {
                    throw new Exception($"服务器返回数据异常：{res}");
                }
                var arr = result.Split(',');
                if (arr.Length < 3)
                {
                    throw new Exception($"服务器返回数据异常：{res}");
                }
                return new StockModel()
                {
                    Code = code.Substring(code.Length - 6),
                    Name = arr[0],
                    CurrentPrice = float.Parse(arr[1]),
                    PricePercent = float.Parse(arr[2])
                };
            }
            catch (Exception ex)
            {
                //记录日志
                Util.Log($"查询股票【{code}】信息出错！", ex);
                return null;
            }
        }

        private async Task<string> InvokeSinaApi(string code)
        {
            var api = $"http://hq.sinajs.cn/list={code}";
            try
            {
                using (var client = new HttpClient()
                {
                    Timeout = TimeSpan.FromSeconds(5),
                })
                {
                    client.DefaultRequestHeaders.Add("Referer", "http://finance.sina.com.cn");
                    return await client.GetStringAsync(api);
                }
            }
            catch (Exception ex)
            {
                //记录日志
                Util.Log($"请求API【{api}】出错！", ex);
                return "";
            }
        }

        private void labelForStatus_DoubleClick(object sender, EventArgs e)
        {
            if (labelForStatus.Tag != null && labelForStatus.Tag is StockModel model)
            {
                Util.Info(
                    $"代码：{model.Code}\r\n" +
                    $"名称：{model.Name}\r\n" +
                    $"现价：{model.CurrentPrice}\r\n" +
                    $"涨跌：{model.PricePercent}"
                , "查看详情");
            }
        }
    }

    class StockModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public float CurrentPrice { get; set; }
        public float PricePercent { get; set; }
        public bool IsUp
        {
            get
            {
                return PricePercent > 0;
            }
        }
    }

    enum StockColor
    {
        Green,
        Red,
        Disabled
    }
}
