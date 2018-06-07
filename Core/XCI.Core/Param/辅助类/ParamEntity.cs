using System;

namespace XCI.Component
{
    /// <summary>
    /// 参数对象
    /// </summary>
    [Serializable]
    public class ParamEntity
    {
        /// <summary>
        /// 参数键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Comment { get; set; }

        public override bool Equals(object obj)
        {
            return this.Key.Equals(((ParamEntity)obj).Key);
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode();
        }
    }

    /// <summary>
    /// 用户参数对象
    /// </summary>
    [Serializable]
    public class UserParamEntity : ParamEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
    }
}