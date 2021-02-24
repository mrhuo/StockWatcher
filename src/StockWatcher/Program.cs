using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockWatcher
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine($"Windows 任务栏看盘神器 v1.0");
            Console.WriteLine($"Email：admin@mruo.com");
            Console.WriteLine();

            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (!principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                Console.WriteLine($"您需要使用管理员权限执行此程序，否则无法安装或卸载！");
                return;
            }
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var appName = Process.GetCurrentProcess().ProcessName;
            var appFullPath = Path.Combine(appPath, appName + ".exe");
            var regasm64App = Path.Combine(appPath, "RegAsm64.exe");
            var regasmApp = Path.Combine(appPath, "RegAsm.exe");
            if (args.Length != 1 || (args[0] != "i" && args[0] != "u"))
            {
                Console.WriteLine(
                    $"使用方法：\r\n" +
                    $"    {appName}.exe i\t安装组件\r\n" +
                    $"    {appName}.exe u\t卸载组件\r\n"
                );
                return;
            }
            var regasmPath = "";
            if (Environment.Is64BitOperatingSystem)
            {
                if (File.Exists(regasm64App))
                {
                    File.Delete(regasm64App);
                }
                File.WriteAllBytes(regasm64App, Properties.Resources.RegAsm64);
                regasmPath = regasm64App;
            }
            else
            {
                if (File.Exists(regasmApp))
                {
                    File.Delete(regasmApp);
                }
                File.WriteAllBytes(regasmApp, Properties.Resources.RegAsm);
                regasmPath = regasmApp;
            }
            if (args[0] == "i")
            {
                RegisterSelf(regasmPath, appFullPath);
            }
            else if (args[0] == "u")
            {
                UnRegisterSelf(regasmPath, appFullPath);
            }
        }

        private static void RegisterSelf(string regasmPath, string appPath)
        {
            StartProcess($"\"{regasmPath}\" /silent /nologo /codebase \"{appPath}\"", $"del \"{regasmPath}\"");
        }

        private static void UnRegisterSelf(string regasmPath, string appPath)
        {
            StartProcess($"\"{regasmPath}\" /silent /nologo /u \"{appPath}\"", $"del \"{regasmPath}\"", "taskkill /im explorer.exe /f", "explorer.exe");
        }

        private static void StartProcess(params string[] cmds)
        {
            try
            {
                var process = Process.Start(new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    Arguments = "/q",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                });
                foreach (var cmd in cmds)
                {
                    process.StandardInput.WriteLine(cmd);
                }
                process.StandardInput.WriteLine("exit");
                var response = process.StandardOutput.ReadToEnd();
                process.Close();
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
