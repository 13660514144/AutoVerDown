using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoVerDown
{
    static class Program
    {
        public static string Url = string.Empty;
        public static string RunFile = string.Empty;
        public static string CurrentVer = string.Empty;
        public static string UpVer = string.Empty;
        public static string DeviceType = string.Empty;
        public static string Ip = string.Empty;
        public static string Port = string.Empty;
        public static string ApiUri = string.Empty;
        public static string ApiPort = string.Empty;
        public static long ISize = 0;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            bool createdNew;//返回是否赋予了使用线程的互斥体初始所属权
            System.Threading.Mutex instance = new System.Threading.Mutex(true, "AutoVerDown", out createdNew); //同步基元变量
            try
            {
                if (createdNew)
                {
                    Url = args[0].Trim();//用一个字符串来接收FormA中传过来的数据
                    RunFile = args[1].Trim();
                    CurrentVer = args[2].Trim();
                    UpVer = args[3].Trim();
                    DeviceType = args[4].Trim();
                    Ip = args[5].Trim();
                    Port = args[6].Trim();
                    ApiUri= args[7].Trim();
                    ApiPort= args[8].Trim();
                    ISize = Convert.ToInt64(args[9].Trim());
                    Application.Run(new Form1());
                    instance.ReleaseMutex();
                }
                else
                {
                    //MessageBox.Show("已经有一个程序已经在运行，请不要同时运行多个程序！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Restart();
                }
            }
            catch (Exception ex)
            {
                //File.Copy("Ver-bak.json", "Ver.json",true);
                Restart();
            }
        }
        private static void Restart()
        {
            Process process = new Process();
            process.StartInfo.FileName = $"{RunFile}";
            process.StartInfo.UseShellExecute = true;
            process.Start();
            Thread.Sleep(100);
            System.Environment.Exit(0);
        }
    }
}
