using System;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 应用程序更新接口
    /// </summary>
    [XCIComponentDescription("更新组件", "系统组件")]
    public interface IAppUpdate:IManager
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        string App { get; set; }

        /// <summary>
        /// 是否启用更新
        /// </summary>
        bool IsUpdate { get; set; }

        event EventHandler<AppUpdateEventArgs> ShowMessage;

        /// <summary>
        /// 检测更新
        /// </summary>
        /// <returns></returns>
        bool CheckUpdate();

        /// <summary>
        /// 更新程序
        /// </summary>
        void Update();

        BoolMessage Add(AppUpdateEntity entity);

        BoolMessage Delete(int version);

        BoolMessage UploadFile(string filePath);

        string GetViewUpdateUrl(string appTitle);

        XCIList<AppUpdateEntity> GetUpdateList();
    }
}