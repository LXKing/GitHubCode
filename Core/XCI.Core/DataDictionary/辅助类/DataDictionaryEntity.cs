using System;
using XCI.Helper;

namespace XCI.Component
{
    /// <summary>
    /// 数据字典对象
    /// </summary>
    [Serializable]
    public class DataDictionaryEntity : EntityBase
    {
        /// <summary>
        /// 字典编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 项名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 项名称简拼
        /// </summary>
        public string ItemNameSpell { get { return SpellHelper.GetStringSpell(ItemName); } }

        /// <summary>
        /// 项值
        /// </summary>
        public string ItemValue { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Comment { get; set; }
        
        public override bool Equals(object obj)
        {
            return this.Code.Equals(((DataDictionaryEntity)obj).Code);
        }

        public override int GetHashCode()
        {
            return this.Code.GetHashCode();
        }
    }
}