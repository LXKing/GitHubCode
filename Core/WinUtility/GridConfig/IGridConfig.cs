using DevExpress.XtraGrid;
using System;
using XCI.WinUtility;

namespace XCI.Component
{
    /// <summary>
    /// 表格配置存取组件
    /// </summary>
    [XCIComponentDescription("表格配置组件", "UI组件")]
    public interface IGridConfig : IManager
    {
        /// <summary>
        /// 加载表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="userID">用户ID</param>
        void LoadConfig(XCIGrid grid, Guid userID);

        /// <summary>
        /// 加载表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="userID">用户ID</param>
        void LoadConfig(XCITreeGrid grid, Guid userID);

        /// <summary>
        /// 保存表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="userID">用户ID</param>
        void SaveConfig(XCIGrid grid, Guid userID);

        /// <summary>
        /// 保存表格配置
        /// </summary>
        /// <param name="grid">表格对象</param>
        /// <param name="userID">用户ID</param>
        void SaveConfig(XCITreeGrid grid, Guid userID);

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="gridID">表格ID</param>
        /// <param name="userID">用户ID</param>
        void DeleteConfig(string gridID, Guid userID);

        /// <summary>
        /// 删除全部控件配置
        /// </summary>
        void DeleteAllControlConfig();

        /// <summary>
        /// 删除全部控件全部用户配置
        /// </summary>
        void DeleteAllControlAllUserConfig();
        
        /// <summary>
        /// 删除当前控件的全部配置
        /// </summary>
        /// <param name="gridID">表格ID</param>
        void DeleteCurrentControlConfig(string gridID);

        /// <summary>
        /// 删除当前控件的全部用户配置
        /// </summary>
        /// <param name="gridID">表格ID</param>
        void DeleteCurrentControlAllUserConfig(string gridID);

    }
}