using System;
using System.Text;
using XCI.Core;

namespace XCI.Component
{
    /// <summary>
    /// 实体基类
    /// </summary>
    [Serializable]
    public class EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int ID { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// 确定指定的 System.Object 是否等于当前的 System.Object
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == System.DBNull.Value||obj == null)
            {
                return false;
            }
            return ID == ((EntityBase)obj).ID;
        }

        /// <summary>
        /// 用作特定类型的哈希函数
        /// </summary>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    /// <summary>
    /// 实体扩展操作
    /// </summary>
    public static class EntityBaseExtension
    {
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>实体属性</returns>
        public static FastPropertyInfo GetFastProperty(this EntityBase entity, string propertyName)
        {
            return EntityMetadata.GetFastProperty(entity.GetType(), propertyName);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        public static void SetPropertyValue(this EntityBase entity, string propertyName, object propertyValue)
        {
            EntityMetadata.SetPropertyValue(entity, propertyName, propertyValue);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue(this EntityBase entity, string propertyName)
        {
            return EntityMetadata.GetPropertyValue(entity, propertyName);
        }


        public static object String(this EntityBase entity)
        {
            var metadata = EntityMetadataFactory.Current.Get(entity.GetType().FullName);
            if (metadata != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in metadata.EntityFields)
                {
                    sb.AppendFormat("{0}({1})={2}  ", item.FieldName, item.FieldComment,
                                    GetPropertyValue(entity, item.FieldName));
                }
                return sb.ToString();
            }
            return string.Empty;
        }

    }
}