using System.Collections.Generic;
using XCI.Core;
using XCI.WinUtility;

namespace XCI.Component
{
    /// <summary>
    /// 表格配置模板组件
    /// </summary>
    [XCIComponentDescription("表格配置模板组件", "UI组件")]
    public interface IGridConfigTemplate : IManager
    {
        /// <summary>
        /// 加载表格配置模板
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="name">模板名称</param>
        void LoadConfig(XCIGrid grid, string name);

        /// <summary>
        /// 加载表格配置模板
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="name">模板名称</param>
        void LoadConfig(XCITreeGrid grid, string name);

        /// <summary>
        /// 保存表格配置模板
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="name">模板名称</param>
        void SaveConfig(XCIGrid grid, string name);

        /// <summary>
        /// 保存表格配置模板
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="name">模板名称</param>
        void SaveConfig(XCITreeGrid grid, string name);
        
        /// <summary>
        /// 获取模板名称列表
        /// </summary>
        /// <param name="gridID">表格ID</param>
        XCIList<GridConfigTemplateEntity> GetTemplateList(string gridID);

        /// <summary>
        /// 删除表格配置模板
        /// </summary>
        /// <param name="templateID">模板ID</param>
        void DeleteConfig(string templateID);

        /// <summary>
        /// 修改配置名称
        /// </summary>
        /// <param name="templateID">模板ID</param>
        /// <param name="newName">新模板名称</param>
        void RenameConfig(string templateID, string newName);

        /// <summary>
        /// 复制表格配置模板
        /// </summary>
        /// <param name="templateID">模板ID</param>
        /// <param name="newName">新模板名称</param>
        void CopyConfig(string templateID,string newName);
    }
}