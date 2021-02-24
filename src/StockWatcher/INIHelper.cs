using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StockWatcher
{
    class INIHelper
    {
        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="filePath">INI文件完整路径</param>
        /// <param name="key">键</param>
        /// <param name="def">未取到值时返回的默认值</param>
        /// <returns>读取的值</returns>
        public static string Read(string filePath, string key, string def = "")
        {
            if (!File.Exists(filePath))
            {
                return def;
            }
            var all = GetINIFileContents(filePath);
            if (all.ContainsKey(key))
            {
                return all[key];
            }
            return def;
        }

        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="filePath">INI文件完整路径</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的新字符串</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public static void Write(string filePath, string key, string value)
        {
            var all = GetINIFileContents(filePath);
            if (all.ContainsKey(key))
            {
                all[key] = value;
            }
            else
            {
                all.Add(key, value);
            }
            WriteDictionaryToINIFile(filePath, all);
        }

        private static Dictionary<string, string> GetINIFileContents(string filePath)
        {
            var dict = new Dictionary<string, string>();
            if (File.Exists(filePath))
            {
                var values = from a in File.ReadAllLines(filePath)
                             let arr = a.Split('=')
                             let _key = (arr.Length == 2 ? arr[0] : "").Trim()
                             let _value = (arr.Length == 2 ? arr[1] : "").Trim()
                             where !string.IsNullOrEmpty(_key)
                             select new { Key = _key, Value = _value };
                foreach (var item in values)
                {
                    dict.Add(item.Key, item.Value);
                }
            }
            return dict;
        }

        private static void WriteDictionaryToINIFile(string filePath, Dictionary<string, string> dict)
        {
            var content = (from a in dict
                           select $"{a.Key}={a.Value}").ToList();
            File.WriteAllLines(filePath, content);
        }
    }
}
