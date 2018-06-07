using System;
using System.Drawing;
using System.Windows.Forms;

namespace XCI.Helper
{
    public static class DialogHelper
    {
        /// <summary>
        /// 显示字体对话框
        /// </summary>
        /// <param name="exec">执行的动作</param>
        /// <param name="font">字体</param>
        /// <param name="color">颜色</param>
        public static FontDialog ShowFontDialog(Action exec,Font font,Color color)
        {
            var dialog = new FontDialog();
            dialog.Font = font;
            dialog.Color = color;
            if (dialog.ShowDialog() == DialogResult.Yes)
            {
                exec();
            }
            return dialog;
        }

        /// <summary>
        /// 显示颜色对话框
        /// </summary>
        /// <param name="exec">执行的动作</param>
        /// <param name="color">颜色</param>
        public static ColorDialog ShowColorDialog(Action exec,Color color)
        {
            var dialog = new ColorDialog();
            dialog.Color = color;
            if (dialog.ShowDialog() == DialogResult.Yes)
            {
                exec();
            }
            return dialog;
        }


        /// <summary>
        /// 显示打开文件对话框
        /// </summary>
        /// <param name="exec">执行的动作</param>
        /// <param name="title">标题</param>
        /// <param name="initialDirectory">初始化目录</param>
        /// <param name="fileName">文件名</param>
        /// <param name="defaultExt">默认扩展名</param>
        public static OpenFileDialog ShowOpenFileDialog(Action exec, string title, string initialDirectory, string fileName, string defaultExt)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = title;
            dialog.InitialDirectory = initialDirectory;
            dialog.FileName = fileName;
            dialog.DefaultExt = defaultExt;
            if (dialog.ShowDialog() == DialogResult.Yes)
            {
                exec();
            }
            return dialog;
        }

        /// <summary>
        /// 显示保存文件对话框
        /// </summary>
        /// <param name="exec">执行的动作</param>
        /// <param name="title">标题</param>
        /// <param name="initialDirectory">初始化目录</param>
        /// <param name="fileName">文件名</param>
        /// <param name="defaultExt">默认扩展名</param>
        public static SaveFileDialog ShowSaveFileDialog(Action exec,
            string title,string initialDirectory,string fileName,string defaultExt)
        {
            var dialog = new SaveFileDialog();
            dialog.Title = title;
            dialog.InitialDirectory = initialDirectory;
            dialog.FileName = fileName;
            dialog.DefaultExt = defaultExt;
            if (dialog.ShowDialog() == DialogResult.Yes)
            {
                exec();
            }
            return dialog;
        }

        /// <summary>
        /// 显示文件夹浏览对话框
        /// </summary>
        /// <param name="exec">执行的动作</param>
        /// <param name="isShowNewFolderButton">是否显示新建按钮</param>
        /// <param name="selectedPath">选中的路径</param>
        /// <param name="description">对话框描述</param>
        /// <param name="folder">根目录</param>
        public static FolderBrowserDialog ShowFolderBrowserDialog(Action<FolderBrowserDialog> exec
            , bool isShowNewFolderButton = true, string selectedPath = "",
            string description = "", Environment.SpecialFolder folder = Environment.SpecialFolder.Desktop)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = isShowNewFolderButton;
            dialog.SelectedPath = selectedPath;
            dialog.Description = description;
            dialog.RootFolder = folder;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                exec(dialog);
                dialog.Dispose();
            }
            return dialog;
        }
    }
}