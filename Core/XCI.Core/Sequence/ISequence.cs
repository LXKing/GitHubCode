namespace XCI.Component
{
    /// <summary>
    /// 序列生成组件
    /// </summary>
    [XCIComponentDescription("序列生成组件", "系统组件")]
    public interface ISequence : IManager
    {
        /// <summary>
        /// 获取大序号
        /// </summary>
        /// <param name="name">序列名称</param>
        int GetSequence(string name);


        /// <summary>
        /// 获取小序号
        /// </summary>
        /// <param name="name">序列名称</param>
        int GetReduction(string name);
    }
}