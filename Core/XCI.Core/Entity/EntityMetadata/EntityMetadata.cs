using System;
using System.Collections.Generic;
using XCI.Core;
using XCI.Extension;
using XCI.Helper;

namespace XCI.Component
{
    /// <summary>
    /// 实体元数据
    /// </summary>
    [Serializable]
    public class EntityMetadata
    {
        /// <summary>
        /// 实体名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        public string TableComment { get; set; }

        /// <summary>
        /// 主键字段
        /// </summary>
        public string PrimaryKeyFieldName { get; set; }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsAutoIncrement { get; set; }

        /// <summary>
        /// 是否启用操作记录
        /// </summary>
        public bool IsEnableAuditable { get; set; }

        /// <summary>
        /// 创建人ID字段
        /// </summary>
        public string CreateUserIdFieldName { get; set; }

        /// <summary>
        /// 创建人姓名字段
        /// </summary>
        public string CreateUserNameFieldName { get; set; }

        /// <summary>
        /// 创建日期字段
        /// </summary>
        public string CreateDateTimeFieldName { get; set; }

        /// <summary>
        /// 更新人ID字段
        /// </summary>
        public string UpdateUserIdFieldName { get; set; }

        /// <summary>
        /// 更新人名称字段
        /// </summary>
        public string UpdateUserNameFieldName { get; set; }

        /// <summary>
        /// 更新日期字段
        /// </summary>
        public string UpdateDateTimeFieldName { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public string ParentFieldName { get; set; }

        /// <summary>
        /// 版本字段名
        /// </summary>
        public string VersionFieldName { get; set; }

        /// <summary>
        /// 排序码字段
        /// </summary>
        public string SortCodeFieldName { get; set; }

        /// <summary>
        /// 删除字段
        /// </summary>
        public string DeleteFieldName { get; set; }

        /// <summary>
        /// 禁用字段
        /// </summary>
        public string ValidFieldName { get; set; }

        /// <summary>
        /// 是否允许编辑字段
        /// </summary>
        public string AllowEditFieldName { get; set; }

        /// <summary>
        /// 是否允许删除字段
        /// </summary>
        public string AllowDeleteFieldName { get; set; }

        private XCIList<string> _encryptFields = new XCIList<string>();
        /// <summary>
        /// 加密字段列表
        /// </summary>
        public XCIList<string> EncryptFields
        {
            get { return _encryptFields; }
            set { _encryptFields = value; }
        }

        private XCIList<EntityField> _entityFields = new XCIList<EntityField>();
        /// <summary>
        /// 实体字段列表
        /// </summary>
        public XCIList<EntityField> EntityFields
        {
            get { return _entityFields; }
            set { _entityFields = value; }
        }

        private XCIList<SpellField> _spellList = new XCIList<SpellField>();
        /// <summary>
        /// 拼音字段列表
        /// </summary>
        public XCIList<SpellField> SpellFields
        {
            get { return _spellList; }
            set { _spellList = value; }
        }

        private static readonly Dictionary<string, FastPropertyInfo> PropertyCache = new Dictionary<string, FastPropertyInfo>();

        /// <summary>
        /// 获取属性包装对象
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="propertyName">属性名称</param>
        public static FastPropertyInfo GetFastProperty(Type type, string propertyName)
        {
            //string key = type.FullName + propertyName;
            //FastPropertyInfo result = null;
            //if (!PropertyCache.TryGetValue(key, out result))
            //{
            //    result = new FastPropertyInfo(type.GetProperty(propertyName));
            //    PropertyCache.Add(key, result);
            //}
            FastPropertyInfo result = new FastPropertyInfo(type.GetProperty(propertyName));
            return result;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="propertyName">属性名称</param>
        public static object GetPropertyValue(object entity, string propertyName)
        {
            FastPropertyInfo fastProerty = GetFastProperty(entity.GetType(), propertyName);
            if (fastProerty != null && fastProerty.Property != null)
            {
                return fastProerty.Get(entity);
            }
            return null;
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        public static void SetPropertyValue(object entity, string propertyName, object propertyValue)
        {
            FastPropertyInfo fastProerty = GetFastProperty(entity.GetType(), propertyName);
            if (fastProerty != null && fastProerty.Property != null)
            {
                fastProerty.Set(entity,
                                ObjectHelper.ConvertObjectValue(propertyValue, fastProerty.Property.PropertyType));
            }
        }

        /// <summary>
        /// 获取主键值
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int GetPrimaryKeyValue(object entity)
        {
            Guard.IsNotNull(entity, "没有指定主机字段");
            return GetPropertyValue(entity, PrimaryKeyFieldName).ToInt();
        }

        /// <summary>
        /// 设置主键值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="PKValue">新值</param>
        public void SetPrimaryKeyValue(object entity, int PKValue)
        {
            Guard.IsNotNull(entity, "没有指定主机字段");
            SetPropertyValue(entity, PrimaryKeyFieldName, PKValue);
        }


        /// <summary>
        /// 解密属性
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void GetEncryptFields(object entity)
        {
            if (EncryptFields.Count == 0)
            {
                return;
            }
            var entityType = entity.GetType();
            foreach (string item in EncryptFields)
            {
                var fastPro = GetFastProperty(entityType, item);
                if (fastPro != null && fastPro.Property != null)
                {
                    var proValue = fastPro.Property.GetValue(entity, null);
                    if (proValue.IsNotEmpty())
                    {
                        fastPro.Property.SetValue(entity, EncryptFactory.Current.Decrypt(proValue.ToString()), null);
                    }
                }
            }
        }

        ///// <summary>
        ///// 加密属性
        ///// </summary>
        ///// <param name="entity">实体对象</param>
        //public void SetEncryptFields(object entity)
        //{
        //    if (EncryptFields.Count == 0)
        //    {
        //        return;
        //    }
        //    var entityType = entity.GetType();
        //    foreach (string item in EncryptFields)
        //    {
        //        var fastPro = GetFastProperty(entityType, item);
        //        if (fastPro != null)
        //        {
        //            var proValue = fastPro.Property.GetValue(entity, null);
        //            if (proValue.IsNotEmpty())
        //            {
        //                fastPro.Property.SetValue(entity, EncryptFactory.Current.Encrypt(proValue.ToString()), null);
        //            }
        //        }
        //    }
        //}

        public object GetFieldValue(object entity, string fieldName)
        {
            if (EncryptFields.IsTrueForAny(p => p.Equals(fieldName, StringComparison.CurrentCultureIgnoreCase)))
            {
                var entityType = entity.GetType();
                var fastPro = GetFastProperty(entityType, fieldName);
                if (fastPro != null && fastPro.Property != null)
                {
                    var proValue = fastPro.Property.GetValue(entity, null);
                    if (proValue != null)
                    {
                        return EncryptFactory.Current.Encrypt(proValue.ToString());
                    }
                }
            }
            return EntityMetadata.GetPropertyValue(entity, fieldName);
        }

        /// <summary>
        /// 设置拼音属性
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void SetSpellFields(object entity)
        {
            if (SpellFields.Count == 0)
            {
                return;
            }
            var entityType = entity.GetType();
            foreach (SpellField item in SpellFields)
            {
                var sourceFastPro = GetFastProperty(entityType, item.SourceFieldName);
                if (sourceFastPro != null && sourceFastPro.Property != null)
                {
                    var proValue = sourceFastPro.Property.GetValue(entity, null);
                    if (proValue.IsNotEmpty())
                    {
                        var targetFastPro = GetFastProperty(entityType, item.TargetFieldName);
                        if (targetFastPro != null && targetFastPro.Property != null)
                        {
                            targetFastPro.Property.SetValue(entity, SpellHelper.GetStringSpell(proValue.ToString()), null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 初始化属性值
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void InitEntityFields(object entity)
        {
            if (EntityFields.Count == 0)
            {
                return;
            }
            var entityType = entity.GetType();
            foreach (var item in EntityFields)
            {
                var fastPro = GetFastProperty(entityType, item.FieldName);
                if (fastPro != null && fastPro.Property != null)
                {
                    fastPro.Property.SetValue(entity, ObjectHelper
                                                 .ConvertObjectValue(item.DefaultValue,
                                                 fastPro.Property.PropertyType), null);
                }
            }
        }

        /// <summary>
        /// 设置操作人信息
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isUpdate">是否是更新</param>
        public void SetAuditable(object entity, bool isUpdate)
        {
            if (!IsEnableAuditable)
            {
                return;
            }
            FastPropertyInfo fastPro = null;
            var userId = ProjectUser.UserID;
            var userName = ProjectUser.UserName;
            var dateTime = DateTime.Now;
            var entityType = entity.GetType();

            if (!isUpdate)
            {
                fastPro = GetFastProperty(entityType, CreateUserIdFieldName);
                if (fastPro != null && fastPro.Property != null)
                {
                    fastPro.Property.SetValue(entity, userId, null);
                }
                fastPro = GetFastProperty(entityType, CreateUserNameFieldName);
                if (fastPro != null && fastPro.Property != null)
                {
                    fastPro.Property.SetValue(entity, userName, null);
                }
                fastPro = GetFastProperty(entityType, CreateDateTimeFieldName);
                if (fastPro != null && fastPro.Property != null)
                {
                    fastPro.Property.SetValue(entity, dateTime, null);
                }
            }
            else
            {
                fastPro = GetFastProperty(entityType, UpdateUserIdFieldName);
                if (fastPro != null && fastPro.Property != null)
                {
                    fastPro.Property.SetValue(entity, userId, null);
                }
                fastPro = GetFastProperty(entityType, UpdateUserNameFieldName);
                if (fastPro != null && fastPro.Property != null)
                {
                    fastPro.Property.SetValue(entity, userName, null);
                }
                fastPro = GetFastProperty(entityType, UpdateDateTimeFieldName);
                if (fastPro != null && fastPro.Property != null)
                {
                    fastPro.Property.SetValue(entity, dateTime, null);
                }
            }
        }

        /// <summary>
        /// 设置版本值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="newVersion">新值</param>
        public void SetVersionValue(object entity, int newVersion)
        {
            if (!string.IsNullOrEmpty(VersionFieldName))
            {
                SetPropertyValue(entity, VersionFieldName, newVersion);
            }
        }

        /// <summary>
        /// 获取排序码值
        /// </summary>
        /// <param name="entity">实体对象</param>
        public int GetSortCodeValue(object entity)
        {
            if (!string.IsNullOrEmpty(SortCodeFieldName))
            {
                return GetPropertyValue(entity, SortCodeFieldName).ToInt();
            }
            return 0;
        }

        /// <summary>
        /// 设置排序码值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="newSortCode">新值</param>
        public void SetSortCodeValue(object entity, int newSortCode)
        {
            SetPropertyValue(entity, SortCodeFieldName, newSortCode);
        }

        /// <summary>
        /// 获取是否允许编辑值
        /// </summary>
        /// <param name="entity">实体对象</param>
        public bool GetAllowEditValue(object entity)
        {
            if (!string.IsNullOrEmpty(AllowEditFieldName))
            {
                return GetPropertyValue(entity, AllowEditFieldName).ToBool();
            }
            return true;
        }

        /// <summary>
        /// 设置是否允许编辑值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="newStatus">新值</param>
        public void SetAllowEditValue(object entity, bool newStatus)
        {
            if (!string.IsNullOrEmpty(AllowEditFieldName))
            {
                SetPropertyValue(entity, AllowEditFieldName, newStatus);
            }
        }

        /// <summary>
        /// 获取是否允许删除值
        /// </summary>
        /// <param name="entity">实体对象</param>
        public bool GetAllowDeleteValue(object entity)
        {
            if (!string.IsNullOrEmpty(AllowDeleteFieldName))
            {
                return GetPropertyValue(entity, AllowDeleteFieldName).ToBool();
            }
            return true;
        }

        /// <summary>
        /// 设置是否允许删除值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="newStatus">新值</param>
        public void SetAllowDeleteValue(object entity, bool newStatus)
        {
            if (!string.IsNullOrEmpty(AllowDeleteFieldName))
            {
                SetPropertyValue(entity, AllowDeleteFieldName, newStatus);
            }
        }
    }

    /// <summary>
    /// 实体字段
    /// </summary>
    [Serializable]
    public class EntityField
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段备注
        /// </summary>
        public string FieldComment { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }

    /// <summary>
    /// 拼音字段
    /// </summary>
    [Serializable]
    public class SpellField
    {
        /// <summary>
        /// 源字段 汉字字段名称
        /// </summary>
        public string SourceFieldName { get; set; }

        /// <summary>
        /// 目标字段 拼音字段名称
        /// </summary>
        public string TargetFieldName { get; set; }
    }
}