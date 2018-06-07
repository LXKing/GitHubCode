using System;
using System.Drawing;
using System.Xml.Serialization;
using XCI.Core;
using XCI.Helper;

namespace XCI.Component
{
    /// <summary>
    /// 配置对象基类
    /// </summary>
    [Serializable]
    public class ConfigEntity
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 提供程序 格式:{命名空间.类名称,程序集名称}
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// 是否是默认配置
        /// </summary>
        public bool IsDefault { get; set; }

        [NonSerialized]
        private Type _providerType;
        public Type ProviderType
        {
            get
            {
                if (_providerType == null)
                {
                    _providerType = Type.GetType(Provider);
                }
                return _providerType;
            }
        }

        [NonSerialized]
        private XCIComponentAttribute _componentAttribute;
        public XCIComponentAttribute ComponentAttribute
        {
            get
            {
                if (_componentAttribute == null)
                {
                    _componentAttribute = AssemblyHelper.GetCustomAttributes<XCIComponentAttribute>(ProviderType);
                }
                return _componentAttribute;
            }
        }

        [NonSerialized]
        private Image _logo;
        public Image Logo
        {
            get
            {
                if (_logo == null && ComponentAttribute != null)
                {
                    _logo = new Bitmap(ResourceImageHelper.CreateBitmapFromResources(ComponentAttribute.Logo, ProviderType.Assembly), 48, 48);
                }
                return _logo;
            }
        }

        private XCIList<ComponentParamEntity> _paramCollection;
        /// <summary>
        /// 参数集合
        /// </summary>
        public XCIList<ComponentParamEntity> ParamCollection
        {
            get
            {
                if (_paramCollection == null)
                {
                    _paramCollection = new XCIList<ComponentParamEntity>();
                }
                return _paramCollection;
            }
            set { _paramCollection = value; }
        }

        public override bool Equals(object obj)
        {
            return this.Name.Equals(((ConfigEntity)obj).Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}