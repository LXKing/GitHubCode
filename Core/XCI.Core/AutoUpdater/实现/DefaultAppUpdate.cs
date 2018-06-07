using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using XCI.Core;
using XCI.Helper;

namespace XCI.Component
{
    [XCIComponent(
        "自动更新模块",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "3.5.0.1",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "",
        "XCI.AutoUpdater.AutoUpdaterLogo.png")]
    public class DefaultAppUpdate : IAppUpdate
    {
        public DefaultAppUpdate()
        {
            DownloadResetEvent = new ManualResetEvent(false);
            Client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Client_DownloadFileCompleted);
            Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
        }

        void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Message("更新下载完成...");

            UpdateCompleted();

            if (RemoteAppUpdate.IsRestart)
            {
                Message("正在重启应用程序...");
                StartupHelper.RestartApp();
                StartupHelper.ExitApp();
            }
            else
            {
                DownloadResetEvent.Set();
            }
        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Message(string.Format("正在下载更新 {0}%", e.ProgressPercentage));
        }

        public DefaultAppUpdate(string app)
            : this()
        {
            this.App = app;
        }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string App { get; set; }

        /// <summary>
        /// 是否启用更新
        /// </summary>
        public bool IsUpdate { get; set; }

        public string UpdateUrl { get; set; }

        private string _updatePath;

        /// <summary>
        /// 配置文件路径
        /// </summary>
        protected string UpdateConfigFile
        {
            get
            {
                if (_updatePath == null)
                {
                    _updatePath = Path.Combine(ConfigFolder, "UpdateConfig.xml");
                    FileHelper.CreateDirectoryByPath(_updatePath);
                }
                return _updatePath;
            }
            set { _updatePath = value; }
        }

        private string _downTempFile;
        protected string DownTempFile
        {
            get
            {
                if (_downTempFile == null)
                {
                    _downTempFile = Path.Combine(TempFolder, StringHelper.GetGuid() + ".zip");
                }
                return _downTempFile;
            }
        }

        private string _configFolder;
        public string ConfigFolder
        {
            get
            {
                if (_configFolder == null)
                {
                    _configFolder = XmlHelper.GetAppConfig("ConfigDirectory", "Config");
                    FileHelper.CreateDirectory(_configFolder);
                }
                return _configFolder;
            }
        }

        private string _tempFolder;
        public string TempFolder
        {
            get
            {
                if (_tempFolder == null)
                {
                    _tempFolder = XmlHelper.GetAppConfig("TempDirectory", "Temp");
                    FileHelper.CreateDirectory(_tempFolder);
                }
                return _tempFolder;
            }
        }

        private string _backupFolder;
        public string BackupFolder
        {
            get
            {
                if (_backupFolder == null)
                {
                    _backupFolder = XmlHelper.GetAppConfig("BackupDirectory", "Backup");
                    FileHelper.CreateDirectory(_backupFolder);
                }
                return _backupFolder;
            }
        }

        protected ManualResetEvent DownloadResetEvent { get; set; }

        private XCIWebClient _client;
        public XCIWebClient Client
        {
            get
            {
                if (_client==null)
                {
                    _client = new XCIWebClient();
                    _client.Timeout = 3;
                }
                return _client ;
            }
        }

        public event EventHandler<AppUpdateEventArgs> ShowMessage;

        protected void OnShowMessage(AppUpdateEventArgs e)
        {
            if (ShowMessage != null)
            {
                ShowMessage(this, e);
            }
        }

        public void Message(string message)
        {
            AppUpdateEventArgs e = new AppUpdateEventArgs();
            e.Message = message;
            OnShowMessage(e);
        }


        private AppUpdateEntity _localAppUpdate;
        protected AppUpdateEntity LocalAppUpdate
        {
            get
            {
                if (_localAppUpdate == null)
                {
                    var entity = XmlHelper.XmlDeserializePath<AppUpdateEntity>(UpdateConfigFile);
                    if (entity == null)
                    {
                        entity = new AppUpdateEntity();
                        entity.Version = 0;
                    }
                    _localAppUpdate = entity;
                }
                return _localAppUpdate;
            }
        }

        private AppUpdateEntity _remoteAppUpdate;
        protected AppUpdateEntity RemoteAppUpdate
        {
            get
            {
                if (_remoteAppUpdate == null)
                {
                    NameValueCollection data = new NameValueCollection();
                    data.Add("App", App);
                    data.Add("Action", "Check");

                    AppUpdateEntity entity = null;
                    var bytes = Client.UploadValues(UpdateUrl, "POST", data);
                    if (bytes.Length > 0)
                    {
                        string txt = Encoding.UTF8.GetString(bytes);
                        string tempFile = PathHelper.CreateTempFile();
                        File.WriteAllText(tempFile, txt);
                        entity = XmlHelper.XmlDeserializePath<AppUpdateEntity>(tempFile);

                        FileHelper.DeleteFile(tempFile);
                        return entity;
                    }
                    entity = new AppUpdateEntity();
                    entity.Version = 0;
                    _remoteAppUpdate = entity;
                }
                return _remoteAppUpdate;
            }
        }

        /// <summary>
        /// 检测是否有更新
        /// </summary>
        /// <returns></returns>
        public bool CheckUpdate()
        {
            try
            {
                Message("正在查找可用更新...");
                if (RemoteAppUpdate.Version > LocalAppUpdate.Version)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Message(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 更新程序
        /// </summary>
        public void Update()
        {
            if (!IsUpdate) return;

            DownloadResetEvent.Reset();
            if (CheckUpdate())
            {
                StartUpdate();
            }
        }


        public BoolMessage Add(AppUpdateEntity entity)
        {
            NameValueCollection data = new NameValueCollection();
            data.Add("App", App);
            data.Add("Action", "Add");

            data.Add("FileName", entity.FileName);
            data.Add("FileSize", entity.FileSize.ToString());
            data.Add("Content", entity.Content);
            data.Add("IsRestart", entity.IsRestart.ToString());

            var bytes = Client.UploadValues(UpdateUrl, "POST", data);
            string result = Encoding.UTF8.GetString(bytes);
            if (result.Equals("True")) return new BoolMessage(true);
            return new BoolMessage(false, result);
        }

        public BoolMessage Delete(int version)
        {
            NameValueCollection data = new NameValueCollection();
            data.Add("App", App);
            data.Add("Action", "Delete");
            data.Add("Version", version.ToString());

            var bytes = Client.UploadValues(UpdateUrl, "POST", data);
            string result = Encoding.UTF8.GetString(bytes);
            if (result.Equals("True")) return new BoolMessage(true);
            return new BoolMessage(false, result);
        }

        public string GetViewUpdateUrl(string appTitle)
        {
            string queryString = string.Format("?App={0}&Action=View&AppTitle={1}", App, HttpUtility.UrlEncode(appTitle));
            return UpdateUrl + queryString;
        }

        public XCIList<AppUpdateEntity> GetUpdateList()
        {
            NameValueCollection data = new NameValueCollection();
            data.Add("App", App);
            data.Add("Action", "GetList");

            var bytes = Client.UploadValues(UpdateUrl, "POST", data);
            if (bytes.Length > 0)
            {
                string txt = Encoding.UTF8.GetString(bytes);
                string tempFile = PathHelper.CreateTempFile();
                File.WriteAllText(tempFile, txt);
                var list = XmlHelper.XmlDeserializePath<XCIList<AppUpdateEntity>>(tempFile);

                FileHelper.DeleteFile(tempFile);
                return list;
            }
            return null;
        }

        public BoolMessage UploadFile(string filePath)
        {
            string queryString = string.Format("?App={0}&Action=UploadFile", App);
            string url = UpdateUrl + queryString;
            WebClient client = new WebClient();
            var bytes = client.UploadFile(url, "POST", filePath);
            string result = Encoding.UTF8.GetString(bytes);
            if (result.Equals("True")) return new BoolMessage(true);
            return new BoolMessage(false, result);
        }

        protected void StartUpdate()
        {
            Client.DownloadFileAsync(new Uri(RemoteAppUpdate.FileName), DownTempFile);
            DownloadResetEvent.WaitOne();
        }

        protected void UpdateCompleted()
        {
            Message("正在解压更新文件...");
            string ExtractDir = Path.Combine(TempFolder, Path.GetFileNameWithoutExtension(DownTempFile));

            ZipHelper.ExtractFolder(DownTempFile, ExtractDir);

            Message("正在应用更新...");
            string versionBackupDir = Path.Combine(BackupFolder, "Update" + RemoteAppUpdate.Version);
            if (Directory.Exists(versionBackupDir))
            {
                Directory.Delete(versionBackupDir, true);
            }
            ReplaceFile(versionBackupDir, ExtractDir, Application.StartupPath);

            Message("正在删除临时文件...");
            File.Delete(DownTempFile);
            Directory.Delete(ExtractDir, true);

            Message("正在更新配置文件...");
            XmlHelper.XmlSerializePath(UpdateConfigFile, RemoteAppUpdate);
        }

        /// <summary>
        /// 替换文件
        /// </summary>
        /// <param name="backupDir">备份目录</param>
        /// <param name="targetDir">目标目录</param>
        public static void ReplaceFile(string backupDir, string targetDir, string currentDir)
        {
            if (!Directory.Exists(backupDir))
            {
                Directory.CreateDirectory(backupDir);
            }

            foreach (string file in Directory.GetFiles(targetDir))
            {
                string fileName = Path.GetFileName(file);
                if (!Directory.Exists(currentDir))
                {
                    Directory.CreateDirectory(currentDir);
                }
                string sourceFilePath = Path.Combine(currentDir, fileName);
                if (File.Exists(sourceFilePath))
                {
                    File.Move(sourceFilePath, Path.Combine(backupDir, fileName));
                }
                File.Copy(file, sourceFilePath);
            }

            foreach (string dir in Directory.GetDirectories(targetDir))
            {
                string dirName = Path.GetFileName(dir);
                string subDir = Path.Combine(backupDir, dirName);
                ReplaceFile(subDir, dir, Path.Combine(currentDir, dirName));
            }
        }
    }

    public class AppUpdateEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}