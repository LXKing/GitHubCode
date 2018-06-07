using System.IO;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using XCI.Core;
using XCI.Helper;
using XCI.WinUtility;
using System;

namespace XCI.Component
{
    [XCIComponent(
        "Xml存储表格配置实现模块",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.0.0.1",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "",
        "")]
    public class XmlGridConfig : IGridConfig
    {
        private string _configDir;
        public string ConfigDir
        {
            get
            {
                if (string.IsNullOrEmpty(_configDir))
                {
                    _configDir = PathHelper.AddStartupPath("GridConfig");
                }
                return _configDir;
            }
        }

        /// <summary>
        /// 获取并创建文件路径
        /// </summary>
        /// <param name="gridID">表格ID</param>
        /// <param name="userID">用户ID</param>
        protected string GetConfigPath(string gridID, Guid userID)
        {
            string path = Path.Combine(ConfigDir, userID.ToString(), gridID + ".xml");
            FileHelper.CreateDirectoryByPath(path);
            return path;
        }

        /// <summary>
        /// 加载表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="userID">用户ID</param>
        public void LoadConfig(XCIGrid grid, Guid userID)
        {
            string path = GetConfigPath(grid.GridID, userID);
            if (File.Exists(path))
            {
                grid.MainView.RestoreLayoutFromXml(path, OptionsLayoutBase.FullLayout);
            }
            else
            {
                path = GetConfigPath(grid.GridID, new Guid());
                if (File.Exists(path))
                {
                    grid.MainView.RestoreLayoutFromXml(path, OptionsLayoutBase.FullLayout);
                }
            }
        }

        /// <summary>
        /// 加载表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="userID">用户ID</param>
        public void LoadConfig(XCITreeGrid grid, Guid userID)
        {
            string path = GetConfigPath(grid.GridID, userID);
            if (File.Exists(path))
            {
                grid.RestoreLayoutFromXml(path, OptionsLayoutBase.FullLayout);
            }
            else
            {
                path = GetConfigPath(grid.GridID, new Guid());
                if (File.Exists(path))
                {
                    grid.RestoreLayoutFromXml(path, OptionsLayoutBase.FullLayout);
                }
            }
        }

        /// <summary>
        /// 保存表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="userID">用户ID</param>
        public void SaveConfig(XCIGrid grid, Guid userID)
        {
            string path = GetConfigPath(grid.GridID, userID);
            grid.MainView.SaveLayoutToXml(path, OptionsLayoutBase.FullLayout);
        }

        /// <summary>
        /// 保存表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="userID">用户ID</param>
        public void SaveConfig(XCITreeGrid grid, Guid userID)
        {
            string path = GetConfigPath(grid.GridID, userID);
            grid.SaveLayoutToXml(path, OptionsLayoutBase.FullLayout);
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="gridID">表格ID</param>
        /// <param name="userID">用户ID</param>
        public void DeleteConfig(string gridID, Guid userID)
        {
            if (userID == new Guid()) return;
            string path = GetConfigPath(gridID, userID);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// 删除全部控件配置
        /// </summary>
        public void DeleteAllControlConfig()
        {
            if (!Directory.Exists(ConfigDir)) return;
            Directory.Delete(ConfigDir, true);
        }

        /// <summary>
        /// 删除全部控件全部用户配置
        /// </summary>
        public void DeleteAllControlAllUserConfig()
        {
            if (!Directory.Exists(ConfigDir)) return;
            foreach (string item in Directory.GetDirectories(ConfigDir))
            {
                DirectoryInfo info = new DirectoryInfo(item);
                if (info.Name.Equals(new Guid().ToString()))
                {
                    continue;
                }
                info.Delete(true);
            }
        }

        /// <summary>
        /// 删除当前控件的全部配置
        /// </summary>
        /// <param name="gridID">表格ID</param>
        public void DeleteCurrentControlConfig(string gridID)
        {
            if (!Directory.Exists(ConfigDir)) return;
            var fileList = FileHelper.GetAllFiles(ConfigDir);
            foreach (string item in fileList)
            {
                if (FileHelper.GetFileName(item).Equals(gridID))
                {
                    File.Delete(item);
                }
            }
        }

        /// <summary>
        /// 删除当前控件的全部用户配置
        /// </summary>
        /// <param name="gridID">表格ID</param>
        public void DeleteCurrentControlAllUserConfig(string gridID)
        {
            if (!Directory.Exists(ConfigDir)) return;
            var fileList = FileHelper.GetAllFiles(ConfigDir);
            foreach (string item in fileList)
            {
                FileInfo info = new FileInfo(item);
                if (info.Directory == null) continue;
                if (FileHelper.GetFileName(item).Equals(gridID)
                    && !info.Directory.Name.Equals(new Guid().ToString()))
                {
                    File.Delete(item);
                }
            }
        }
    }
}