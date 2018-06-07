using System;
using System.Text;

namespace XCI.Component
{
    /// <summary>
    /// 组件信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [Serializable]
    public class XCIComponentAttribute : System.Attribute
    {
        /// <summary>
        /// 构造组件信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="author">作者</param>
        /// <param name="contact">联系方式</param>
        /// <param name="version">版本</param>
        /// <param name="copyRight">版权</param>
        /// <param name="logo">logo图片</param>
        /// <param name="description">功能描述</param>
        public XCIComponentAttribute(string name,string author,
            string contact, string version, string copyRight, 
            string description, string logo)
        {
            this.Name = name;
            this.Author = author;
            this.Contact = contact;
            this.Version = version;
            this.CopyRight = copyRight;
            this.Logo = logo;
            this.Description = description;
        }
        
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }


        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }


        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 版权标记
        /// </summary>
        public string CopyRight { get; set; }

        /// <summary>
        /// 功能描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// logo图片
        /// </summary>
        public string Logo { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Name, Version, Author);
        }
    }

    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    [Serializable]
    public class XCIComponentDescriptionAttribute : System.Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        public XCIComponentDescriptionAttribute(string name,string group)
        {
            this.Name = name;
            this.Group = group;
        }

        public override string ToString()
        {
            return Name;
        }
    }



}