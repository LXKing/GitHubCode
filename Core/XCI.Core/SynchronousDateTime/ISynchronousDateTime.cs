namespace XCI.Component
{
    /// <summary>
    /// 时间同步组件
    /// </summary>
    [XCIComponentDescription("时间同步组件", "系统组件")]
    public interface ISynchronousDateTime : IManager
    {
        /// <summary>
        /// 是否同步时间
        /// </summary>
        bool IsSynchronous { get; set; }

        /// <summary>
        /// 时间同步
        /// </summary>
        bool SynchronousDateTime();
    }
}