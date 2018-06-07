using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 序列生成器
    /// </summary>
    public class SequenceFactory: BaseFactory<ISequence>
    {
        private static readonly SequenceFactory _instance = new SequenceFactory();
        
        /// <summary>
        /// 获取默认实现对象
        /// </summary>
        public override ISequence GetDefaultProvider()
        {
            return null;
        }

        /// <summary>
        /// 单例构造 只允许自己创建
        /// </summary>
        internal SequenceFactory()
        {
        }

        /// <summary>
        /// 实例管理对象
        /// </summary>
        public static SequenceFactory Factory
        {
            get { return _instance; }
        }

        /// <summary>
        /// 默认实现对象 当前实现对象
        /// </summary>
        public static ISequence Current
        {
            get { return _instance.Default; }
        }
    }
}