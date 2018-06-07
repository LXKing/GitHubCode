using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevExpress.Utils;
using XCI.Core;
using XCI.Helper;
using XCI.WinUtility;

namespace XCI.Component
{
    [XCIComponent(
        "Xml存储表格配置模板实现模块",
        "吕艳阳", "http://lvyanyang.cnblogs.com", "2.0.0.1",
        "©Copyright2011 lvyanyang2012@gmail.com",
        "",
        "")]
    public class XmlGridConfigTemplate : IGridConfigTemplate
    {
        private readonly XCIList<GridConfigTemplateEntity> TemplateList = new XCIList<GridConfigTemplateEntity>();

        private string _templatepath;
        public string TemplatePath
        {
            get
            {
                if (string.IsNullOrEmpty(_templatepath))
                {
                    _templatepath = PathHelper.AddStartupPath(Path.Combine("GridConfigTemplate", "Template.xml"));
                    FileHelper.CreateDirectoryByPath(_templatepath);
                }
                return _templatepath;
            }
        }

        public XmlGridConfigTemplate()
        {
            TemplateList.LoadDataFromXml(TemplatePath);
        }

        /// <summary>
        /// 获取并创建文件路径
        /// </summary>
        /// <param name="configPath">配置路径</param>
        protected string GetPath(string configPath)
        {
            string path = PathHelper.AddStartupPath(Path.Combine("GridConfigTemplate", "TemplateFile", configPath + ".xml"));
            FileHelper.CreateDirectoryByPath(path);
            return path;
        }

        /// <summary>
        /// 加载表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="name">模板名称</param>
        public void LoadConfig(XCIGrid grid, string name)
        {
            string configName = AddTemplateName(grid.GridID, name);
            string path = GetPath(configName);
            if (File.Exists(path))
            {
                grid.MainView.RestoreLayoutFromXml(path, OptionsLayoutBase.FullLayout);
            }
        }

        /// <summary>
        /// 加载表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="name">模板名称</param>
        public void LoadConfig(XCITreeGrid grid, string name)
        {
            string configName = AddTemplateName(grid.GridID, name);
            string path = GetPath(configName);
            if (File.Exists(path))
            {
                grid.RestoreLayoutFromXml(path, OptionsLayoutBase.FullLayout);
            }
        }

        /// <summary>
        /// 保存表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="name">模板名称</param>
        public void SaveConfig(XCIGrid grid, string name)
        {
            string configName = AddTemplateName(grid.GridID, name);
            string path = GetPath(configName);
            grid.MainView.SaveLayoutToXml(path, OptionsLayoutBase.FullLayout);
            SaveData();
        }

        /// <summary>
        /// 保存表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="name">模板名称</param>
        public void SaveConfig(XCITreeGrid grid, string name)
        {
            string configName = AddTemplateName(grid.GridID, name);
            string path = GetPath(configName);
            grid.SaveLayoutToXml(path, OptionsLayoutBase.FullLayout);
            SaveData();
        }


        /// <summary>
        /// 获取模板名称列表
        /// </summary>
        /// <param name="gridID">表格ID</param>
        public XCIList<GridConfigTemplateEntity> GetTemplateList(string gridID)
        {
            var list = new XCIList<GridConfigTemplateEntity>();
            list.AddRange(TemplateList.Where(p => p.GridID.Equals(gridID)));
            return list;
        }

        /// <summary>
        /// 删除表格配置模板
        /// </summary>
        /// <param name="templateID">模板ID</param>
        public void DeleteConfig(string templateID)
        {
            var index = TemplateList.IndexOf(p => p.ID.Equals(templateID));
            if (index > -1)
            {
                var entity = TemplateList[index];
                string fileName = entity.ConfigPath;
                string path = GetPath(fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                TemplateList.RemoveAt(index);
                SaveData();
            }
        }

        /// <summary>
        /// 修改配置名称
        /// </summary>
        /// <param name="templateID">模板ID</param>
        /// <param name="newName">新模板名称</param>
        public void RenameConfig(string templateID, string newName)
        {
            var entity = TemplateList.First(p => p.ID.Equals(templateID));
            if (entity != null)
            {
                entity.Name = newName;
                SaveData();
            }
        }

        /// <summary>
        /// 复制表格配置模板
        /// </summary>
        /// <param name="templateID">模板ID</param>
        /// <param name="newName">新模板名称</param>
        public void CopyConfig(string templateID, string newName)
        {
            var entity = TemplateList.First(p => p.ID.Equals(templateID));
            if (entity != null)
            {
                var gridID = entity.GridID;
                string oldName = entity.Name;
                string oldConfigName = AddTemplateName(gridID, oldName);
                string oldpath = GetPath(oldConfigName);

                string configName = AddTemplateName(gridID, newName);
                string path = GetPath(configName);
                if (File.Exists(oldpath))
                {
                    File.Copy(oldpath, path);
                    SaveData();
                }
            }
        }
        

        protected string AddTemplateName(string gridID, string name)
        {
            var index = TemplateList.IndexOf(p => p.Name.Equals(name) && p.Name.Equals(name));
            if (index > -1)
            {
                return TemplateList[index].ConfigPath;
            }
            string configPath = StringHelper.GetGuidString();
            GridConfigTemplateEntity entity = new GridConfigTemplateEntity();
            entity.ID = StringHelper.GetGuidString();
            entity.Name = name;
            entity.GridID = gridID;
            entity.ConfigPath = configPath;
            TemplateList.Add(entity);
            return configPath;
        }

        protected void SaveData()
        {
            TemplateList.SaveDataAsXml(TemplatePath);
        }
    }
}