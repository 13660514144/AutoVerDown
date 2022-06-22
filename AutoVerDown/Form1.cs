using HelperTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace AutoVerDown
{
    public partial class Form1 : Form
    {
        private string _saveDir;
        private string _UnZipFile;
        public bool _UpFlg = false;
        public FileMessage _FileMessage;
        public UpVerMsg _UpVerMsg;
        public static string dirCenter = AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            _saveDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "download");
            _UnZipFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UnZip");
            _FileMessage = new FileMessage();
            _UpVerMsg = new UpVerMsg();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(_saveDir))
                {
                    Directory.CreateDirectory(_saveDir);
                }
                if (!Directory.Exists(_UnZipFile))
                {
                    Directory.CreateDirectory(_UnZipFile);
                }
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.ControlBox = false;
                this.button1.Visible = false;
                _UpVerMsg.CurrentVer = Program.CurrentVer;
                _UpVerMsg.UpVer = Program.UpVer;
                _UpVerMsg.DeviceType = Program.DeviceType;
                _UpVerMsg.Ip = Program.Ip;
                _UpVerMsg.Port = Program.Port;
                DelUnZipFile();
                DownVerFile();
            }
            catch (Exception ex)
            {
                Restart();
            }
        }
        private void DownVerFile()
        {
            string imageUrl = Program.Url;// "http://192.168.31.106:24260/dw/MyDrivers.zip";

            string fileExt = Path.GetExtension(imageUrl);
            string fileNewName = Guid.NewGuid() + fileExt;
            bool isDownLoad = false;
            string filePath = Path.Combine(_saveDir, fileNewName);
            if (File.Exists(filePath))
            {
                isDownLoad = true;
            }
            //var file = new FileMessage
            //{
            _FileMessage.FileName = fileNewName;
            _FileMessage.RelativeUrl = string.Empty;
            _FileMessage.Url = imageUrl;
            _FileMessage.IsDownLoad = isDownLoad;
            _FileMessage.SavePath = filePath;
            //};

            if (!_FileMessage.IsDownLoad)
            {

                string fileDirPath = Path.GetDirectoryName(_FileMessage.SavePath);
                if (!Directory.Exists(fileDirPath))
                {
                    Directory.CreateDirectory(fileDirPath);
                }
                try
                {
                    WebClient client = new WebClient();
                    client.DownloadFileCompleted += client_DownloadFileCompleted;
                    client.DownloadProgressChanged += client_DownloadProgressChanged;
                    client.DownloadFileAsync(new Uri(_FileMessage.Url), _FileMessage.SavePath, _FileMessage.FileName);
                }
                catch
                {
                    Restart();
                }
            }
        }
        /// <summary>
        /// 递归拷贝所有子目录。
        /// </summary>
        /// <param >源目录</param>
        /// <param >目的目录</param>
        private void copyDirectory(string sPath, string dPath)
        {
            string[] directories = System.IO.Directory.GetDirectories(sPath);
            if (!System.IO.Directory.Exists(dPath))
            {
                System.IO.Directory.CreateDirectory(dPath);
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sPath);
            System.IO.DirectoryInfo[] dirs = dir.GetDirectories();
            CopyFile(dir, dPath);
            if (dirs.Length > 0)
            {
                foreach (System.IO.DirectoryInfo temDirectoryInfo in dirs)
                {
                    string sourceDirectoryFullName = temDirectoryInfo.FullName;
                    string destDirectoryFullName = sourceDirectoryFullName.Replace(sPath, dPath);
                    if (!System.IO.Directory.Exists(destDirectoryFullName))
                    {
                        System.IO.Directory.CreateDirectory(destDirectoryFullName);
                    }
                    CopyFile(temDirectoryInfo, destDirectoryFullName);
                    copyDirectory(sourceDirectoryFullName, destDirectoryFullName);
                }
            }

        }
        /// <summary>
        /// 拷贝目录下的所有文件到目的目录。
        /// </summary>
        /// <param >源路径</param>
        /// <param >目的路径</param>
        private void CopyFile(System.IO.DirectoryInfo path, string desPath)
        {
            string sourcePath = path.FullName;
            System.IO.FileInfo[] files = path.GetFiles();
            foreach (System.IO.FileInfo file in files)
            {
                string sourceFileFullName = file.FullName;
                string destFileFullName = sourceFileFullName.Replace(sourcePath, desPath);
                file.CopyTo(destFileFullName, true);
            }
        }
        private void DelUnZipFile()
        {
            String file = dirCenter + @"\\UnZip";
            DirectoryInfo dir = new DirectoryInfo(file);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)            //判断是否文件夹
                {
                    DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                    subdir.Delete(true);          //删除子目录和文件
                }
                else
                {
                    File.Delete(i.FullName);      //删除指定文件
                }
            }
        }
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="zipFilePath">压缩文件所在目录</param>
        ///  <param name="unzippath">解压文件所在目录</param>
        ///  string zipFilePath, string unzippath
        private bool UnZipFile()
        {
            string zipfile = $@"{_saveDir}\{_FileMessage.FileName}";
            try
            {
                if (!File.Exists(zipfile))
                {
                    return false;
                }
                long FileSize = 0;
                FileSize = new FileInfo(zipfile).Length;
                if (FileSize !=Program.ISize)
                {
                    return false;
                }
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                System.IO.Compression.ZipFile.ExtractToDirectory(zipfile, _UnZipFile, Encoding.GetEncoding("GB2312")); //解压                
                return true;
            }
            catch (Exception ex)
            {               
                return false;
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        private void Restart()
        {
            if (_UpFlg)
            {
                _UpVerMsg.UpFlg = "版本更新成功";
            }
            else
            {
                _UpVerMsg.UpFlg = "版本更新失败";
                //File.Copy("Ver-bak.json", "Ver.json",true);
            }
            /*上传版本更新信息*/
            Dispatch _Dispatch = new Dispatch();
            _Dispatch.ApiGetData("Api/PassRecoadGuest/UpVerMessage",
                JsonConvert.SerializeObject(_UpVerMsg),
                Program.ApiUri,Program.ApiPort);
            /*上传版本更新信息*/
            Process process = new Process();
            process.StartInfo.FileName = $"{Program.RunFile}";
            process.StartInfo.UseShellExecute = true;
            process.Start();
            Thread.Sleep(100);
            System.Environment.Exit(0);
        }
        /// <summary>
        /// 下载进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {

                if (e.UserState != null)
                {
                    //this.lblMessage.Text = e.UserState.ToString() + ",下载完成";
                    //MessageBox.Show(e.UserState.ToString() + ",下载完成");
                    _UpFlg = UnZipFile();
                    if (_UpFlg)
                    {
                        copyDirectory(_UnZipFile, dirCenter);
                        System.IO.File.Delete($@"{_saveDir}\{_FileMessage.FileName}");
                        DelUnZipFile();
                    }                
                    Restart();
                }
            }
            catch (Exception ex)
            {
                Restart();
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                this.proBarDownLoad.Minimum = 0;
                this.proBarDownLoad.Maximum = (int)e.TotalBytesToReceive;
                this.proBarDownLoad.Value = (int)e.BytesReceived;
                this.Invoke(
                    new Action(() =>
                    {
                        this.lblPercent.Text = e.ProgressPercentage + "%";
                    }
                ));
            }
            catch (Exception ex)
            {
                Restart();
            }
            
        }
        public class FileMessage
        {
            public string FileName { get; set; }
            public string RelativeUrl { get; set; }
            public string Url { get; set; }
            public bool IsDownLoad { get; set; }
            public string SavePath { get; set; }
        }
        public class UpVerMsg
        {
            public string CurrentVer { get; set; }
            public string UpVer { get; set; }
            public string DeviceType { get; set; }
            public string Ip { get; set; }
            public string Port { get; set; }
            public string UpFlg { get; set; }
        }
    }
}
