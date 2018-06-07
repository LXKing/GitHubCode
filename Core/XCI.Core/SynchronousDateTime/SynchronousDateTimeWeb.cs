using System;
using XCI.Helper;

namespace XCI.Component
{
    [XCIComponent(
        "Web方式同步时间实现模块",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "1.0.0.2",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "Web方式同步时间实现模块",
        "")]
    public class SynchronousDateTimeWeb : ISynchronousDateTime
    {
        /// <summary>
        /// 时间同步Url
        /// </summary>
        public string SynchronousDateTimeUrl { get; set; }

        /// <summary>
        /// 是否同步时间
        /// </summary>
        public bool IsSynchronous { get; set; }

        /// <summary>
        /// 时间同步
        /// </summary>
        public bool SynchronousDateTime()
        {
            if (!IsSynchronous) return false;
            try
            {
                if (!string.IsNullOrEmpty(SynchronousDateTimeUrl))
                {
                    WebClientHelper helper = new WebClientHelper();
                    string dateTimeString = helper.DownloadString(SynchronousDateTimeUrl);
                    DateTime dt = DateTime.Now;
                    if (DateTime.TryParse(dateTimeString, out dt))
                    {
                        Win32Helper.SetLocalDateTime(dt);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError(ex.Message);
            }
            return false;
        }
    }
}