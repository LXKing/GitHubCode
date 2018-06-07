using System;

namespace XCI.Component
{
    /// <summary>
    /// 参数实体
    /// </summary>
    [Serializable]
    public class ComponentParamEntity
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Comment { get; set; }

        public override bool Equals(object obj)
        {
            return this.Name.Equals(((ComponentParamEntity) obj).Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}