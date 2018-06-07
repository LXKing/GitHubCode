using System;
using System.ComponentModel;
using System.Net;
using System.Text;
using XCI.Component;

namespace XCI.Helper
{
    /// <summary>
    /// Web上传下载
    /// </summary>
    public class WebClientHelper
    {
        /// <summary>
        /// 是否上传完成
        /// </summary>
        public bool IsUploadFinished { get; private set; }

        /// <summary>
        /// 是否下载完成
        /// </summary>
        public bool IsDownloadFinished { get; private set; }

        private XCIWebClient client;
        public XCIWebClient Client { get { return client; } }

        #region 上传文件进度变化事件

        /// <summary>
        /// 上传文件进度变化事件
        /// </summary>
        public event EventHandler<UploadProgressChangedEventArgs> UploadProgressChanged;

        /// <summary>
        /// 上传文件进度变化事件
        /// </summary>
        /// <param name="e">上传文件进度变化事件参数</param>
        protected void OnUploadProgressChanged(UploadProgressChangedEventArgs e)
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(this, e);
            }
        }

        #endregion

        #region 下载文件进度变化事件

        /// <summary>
        /// 下载文件进度变化事件
        /// </summary>
        public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;

        /// <summary>
        /// 下载文件进度变化事件
        /// </summary>
        /// <param name="e">下载文件进度变化事件参数</param>
        protected void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e)
        {
            if (DownloadProgressChanged != null)
            {
                DownloadProgressChanged(this, e);
            }
        }

        #endregion

        #region 上传文件完成事件

        /// <summary>
        /// 上传文件完成事件
        /// </summary>
        public event EventHandler<UploadFileCompletedEventArgs> UploadCompleted;

        /// <summary>
        /// 上传文件完成事件
        /// </summary>
        /// <param name="e">上传文件完成事件参数</param>
        protected void OnUploadCompleted(UploadFileCompletedEventArgs e)
        {
            if (UploadCompleted != null)
            {
                UploadCompleted(this, e);
            }
        }

        #endregion

        #region 下载文件完成事件

        /// <summary>
        /// 下载文件完成事件
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> DownloadCompleted;

        /// <summary>
        /// 下载文件完成事件
        /// </summary>
        /// <param name="e">下载文件完成事件参数</param>
        protected void OnDownloadCompleted(AsyncCompletedEventArgs e)
        {
            if (DownloadCompleted != null)
            {
                DownloadCompleted(this, e);
            }
        }

        #endregion

        #region 下载字符串完成事件

        /// <summary>
        /// 下载字符串完成事件
        /// </summary>
        public event EventHandler<DownloadStringCompletedEventArgs> DownloadStringCompleted;

        /// <summary>
        /// 下载字符串完成事件
        /// </summary>
        /// <param name="e">下载字符串完成事件参数</param>
        protected void OnDownloadStringCompleted(DownloadStringCompletedEventArgs e)
        {
            if (DownloadStringCompleted != null)
            {
                DownloadStringCompleted(this, e);
            }
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public WebClientHelper()
        {
            client = new XCIWebClient();
            client.Timeout = 3;
            client.Encoding = Encoding.UTF8;

            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            client.UploadProgressChanged += new UploadProgressChangedEventHandler(client_UploadProgressChanged);
            client.UploadFileCompleted += new UploadFileCompletedEventHandler(client_UploadFileCompleted);

            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            OnDownloadStringCompleted(e);
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            OnDownloadCompleted(e);
            IsDownloadFinished = true;
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            OnDownloadProgressChanged(e);
        }

        void client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            OnUploadCompleted(e);
            IsUploadFinished = true;
        }

        void client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            OnUploadProgressChanged(e);
        }

        /// <summary>
        /// 异步上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <param name="userToken">用户定义对象</param>
        public void UploadFileAsync(string url, string file,object userToken)
        {
            IsUploadFinished = false;
            client.UploadFileAsync(new Uri(url), "POST", file, userToken);
        }

        /// <summary>
        /// 异步下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <param name="userToken">用户定义对象</param>
        public void DownloadFileAsync(string url, string file,object userToken)
        {
            IsDownloadFinished = false;
            client.DownloadFileAsync(new Uri(url), file, userToken);
        }

        /// <summary>
        /// 异步下载字符串
        /// </summary>
        /// <param name="url"></param>
        public void DownloadStringAsync(string url)
        {
            client.DownloadStringAsync(new Uri(url));
        }

        /// <summary>
        /// 同步下载字符串
        /// </summary>
        /// <param name="url"></param>
        public string DownloadString(string url)
        {
            return client.DownloadString(new Uri(url));
        }

        /// <summary>
        /// 取消异步操作
        /// </summary>
        public void CancelAsync()
        {
            IsDownloadFinished = true;
            client.CancelAsync();
        }

    }

}