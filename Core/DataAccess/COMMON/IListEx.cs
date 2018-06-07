using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// 对实现IList接口的类扩展方法
    /// </summary>
    public static class IListEx
    {
        /// <summary>
        /// 对一个集合深克隆(T必须实现ICloneable接口)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IList<T> Clone_DA<T>(this IList<T> source) where T:ICloneable
        {

            IList<T> newList = new List<T>(source.Count);
            foreach (var item in source)
            {
                var obj=item.Clone();
                newList.Add((T)obj);
            }
            return newList;
        }

        /// <summary>
        /// 实体类转换成DataSet
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public static DataSet FillDataSet_DA<T>(this IList<T> modelList) where T : new()
        {
            if (modelList == null || modelList.Count == 0)
            {
                return null;
            }
            else
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(FillDataTable_DA(modelList));
                return ds;
            }
        }

        /// <summary>
        /// 实体类转换成DataTable
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public static DataTable FillDataTable_DA<T>(this IList<T> modelList) where T : new()
        {
            T t1 = new T();
            DataTable dt = t1.CreateData();//DataTable dt = modelList[0].CreateData();
            if (modelList == null || modelList.Count == 0)
            {
                return dt;
            }
            

            foreach (T model in modelList)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    var value=propertyInfo.GetValue(model, null);
                    dataRow[propertyInfo.Name] = value==null?DBNull.Value:value;
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }
    }
}
