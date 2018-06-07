using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ext.Net;
using Ext.Net.Utilities;
namespace Ext.Extension.Message
{
    public class MessageBoxExt
    {
        #region 弹出对话框
        /// <summary>
        /// 弹出对话框(提示)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="okFunction">直接写js,不需要脚本标签和function()</param>
        public static void ShowPrompt(string msg, string okFunction = "")
        {
            ShowMsg(MsgType.Prompt, msg, okFunction);
        }
        public static void ShowPrompt(IEnumerable<string> msgCollection, string okFunction = "")
        {
            var msg= string.Join("<br>", msgCollection.Select(x => x.ToString()).ToList()); 
            ShowMsg(MsgType.Prompt, msg, okFunction);
        }
        /// <summary>
        /// 弹出对话框(警告)
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="okFunction"></param>
        public static void ShowWarning(string msg, string okFunction = "")
        {
            ShowMsg(MsgType.Warning, msg, okFunction);
        }
        public static void ShowWarning(IEnumerable<string> msgCollection, string okFunction = "")
        {
            var msg = string.Join("<br>", msgCollection.Select(x => x.ToString()).ToList());
            ShowMsg(MsgType.Warning, msg, okFunction);
        }
        /// <summary>
        /// 弹出对话框(错误异常)
        /// </summary>
        /// <param name="error"></param>
        /// <param name="okFunction"></param>
        public static void ShowError(string error, string okFunction = "")
        {
            ShowMsg(MsgType.Error, error, okFunction);
        }
        public static void ShowError(IEnumerable<string> msgCollection, string okFunction = "")
        {
            var msg = string.Join("<br>", msgCollection.Select(x => x.ToString()).ToList());
            ShowMsg(MsgType.Error, msg, okFunction);
        }
        private static void ShowMsg(MsgType type, string msg, string okJsFunction = "")
        {
            MessageBoxConfig messageBoxConfig = new MessageBoxConfig();
            MessageBox.Icon icon = MessageBox.Icon.NONE;
            switch (type)
            {
                case MsgType.Prompt:
                    messageBoxConfig.Title = "提示";
                    icon = MessageBox.Icon.INFO;
                    break;
                case MsgType.Warning:
                    messageBoxConfig.Title = "警告";
                    icon = MessageBox.Icon.WARNING;
                    break;
                case MsgType.Error:
                    messageBoxConfig.Title = "错误";
                    icon = MessageBox.Icon.ERROR;
                    break;
                default:
                    break;
            }
            messageBoxConfig.Multiline = false;
            messageBoxConfig.Buttons = MessageBox.Button.OK;
            messageBoxConfig.Icon = icon;
            messageBoxConfig.Message = msg;
            messageBoxConfig.Closable = true;

            MessageBoxButtonsConfig buttonConfig = new MessageBoxButtonsConfig();
            buttonConfig.Ok = new MessageBoxButtonConfig();
            buttonConfig.Ok.Text = "确定";
            if (!string.IsNullOrEmpty(okJsFunction))
            {
                string okHandler = okJsFunction;//"<script type='text/javascript'>{0}</script>";
                buttonConfig.Ok.Handler = okJsFunction;//string.Format(okHandler, okJsFunction);
                messageBoxConfig.Handler = okJsFunction;
            }
            messageBoxConfig.MessageBoxButtonsConfig = buttonConfig;
            
            X.Msg.Show(messageBoxConfig);
        } 
        #endregion

        #region 弹出通知窗
        /// <summary>
        /// 弹出提示框
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <param name="autoHide"></param>
        /// <param name="hideDelay"></param>
        /// <param name="draggable"></param>
        public static void NotifyMsg(ResourceManager res,MsgType type, string content, bool autoHide = false, int hideDelay = 5000, bool draggable = true)
        {
            res.RegisterIcon(Icon.Information);

            NotificationConfig config = new NotificationConfig();
            config.ID = Guid.NewGuid().ToString();
            config.Closable = true;
            config.BringToFront = true;
            config.CloseVisible = true;

            config.Html = content;

            config.AutoHide = autoHide;
            config.HideDelay = hideDelay;
            config.Draggable = draggable;
            config.Closable = true;
            switch (type)
            {
                case MsgType.Prompt:
                    config.Icon = Icon.Information;
                    config.Title = "提示";
                    break;
                case MsgType.Warning:
                    config.Icon = Icon.ApplicationError;
                    config.Title = "警告";
                    break;
                case MsgType.Error:
                    config.Icon = Icon.Cancel;
                    config.Title = "错误";
                    break;
            }
            config.Modal = false;
            config.Resizable = true;
            config.Shadow = true;


            NotificationAlignConfig aliginConfig = new NotificationAlignConfig();
            aliginConfig.ElementAnchor = AnchorPoint.BottomRight;
            aliginConfig.TargetAnchor = AnchorPoint.BottomRight;
            aliginConfig.OffsetX = -20;
            aliginConfig.OffsetY = -20;

            config.AlignCfg = aliginConfig;

            Ext.Net.Notification.Show(config);
        }

        /// <summary>
        /// 弹出提示通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="autoHide"></param>
        /// <param name="hideDelay"></param>
        /// <param name="draggable"></param>
        public static void NotifyPrompt(ResourceManager res,string msg, bool autoHide = false, int hideDelay = 3000, bool draggable = true)
        {
            NotifyMsg(res,MsgType.Prompt, msg, autoHide, hideDelay, draggable);
        }
        /// <summary>
        /// 弹出提示警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="autoHide"></param>
        /// <param name="hideDelay"></param>
        /// <param name="draggable"></param>
        public static void NotifyWarning(ResourceManager res,string msg, bool autoHide = false, int hideDelay = 3000, bool draggable = true)
        {
            NotifyMsg(res,MsgType.Warning, msg, autoHide, hideDelay, draggable);
        }
        /// <summary>
        /// 弹出提示错误异常
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="autoHide"></param>
        /// <param name="hideDelay"></param>
        /// <param name="draggable"></param>
        public static void NotifyError(ResourceManager res,string msg, bool autoHide = false, int hideDelay = 3000, bool draggable = true)
        {
            NotifyMsg(res,MsgType.Error, msg, autoHide, hideDelay, draggable);
        } 
        #endregion
    }
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 提示
        /// </summary>
        Prompt = 0,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 1,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 2
    }
}
