using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace XCI.WinUtility
{
    /// <summary>
    /// 绑定数据源操作类
    /// </summary>
    public static class CurrencyManagerHelper
    {
        /// <summary>
        /// 获取数据项
        /// </summary>
        /// <param name="dataManager">数据管理对象</param>
        /// <param name="index">索引</param>
        /// <returns>返回指定位置的数据项</returns>
        public static object GetItem(CurrencyManager dataManager, int index)
        {
            if (index > -1 && dataManager != null && index <= dataManager.List.Count)
            {
                return dataManager.List[index];
            }
            return null;
        }

        /// <summary>
        /// 获取数据项的属性值
        /// </summary>
        /// <param name="dataManager">数据管理对象</param>
        /// <param name="item">数据项</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>返回指定属性的值</returns>
        public static object GetValue(CurrencyManager dataManager, object item, string propertyName)
        {
            if (item == null || propertyName.Length == 0) return null;
            try
            {
                PropertyDescriptor descriptor;
                if (dataManager != null)
                    descriptor = dataManager.GetItemProperties().Find(propertyName, true);
                else
                    descriptor = TypeDescriptor.GetProperties(item).Find(propertyName, true);
                if (descriptor != null)
                {
                    item = descriptor.GetValue(item);
                }
            }
            catch
            {
            }
            return item;
        }

        /// <summary>
        /// 获取数据项索引
        /// </summary>
        /// <param name="dataManager">数据管理对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns>返回指定属性名称的项的索引</returns>
        public static int GetIndex(CurrencyManager dataManager, string propertyName, object propertyValue)
        {
            if (propertyValue == null) throw new ArgumentNullException("propertyValue");
            PropertyDescriptorCollection props = dataManager.GetItemProperties();
            PropertyDescriptor property = props.Find(propertyName, true);
            if (property == null) throw new ArgumentNullException("propertyName");
            var list = dataManager.List;
            if ((list is IBindingList) && ((IBindingList)list).SupportsSearching)
            {
                return ((IBindingList)list).Find(property, propertyValue);
            }
            for (int i = 0; i < list.Count; i++)
            {
                object obj2 = property.GetValue(list[i]);
                if (propertyValue.Equals(obj2))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}