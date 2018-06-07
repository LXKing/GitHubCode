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
        public static IList<T> Clone<T>(this IList<T> source) where T:ICloneable
        {

            IList<T> newList = new List<T>(source.Count);
            foreach (var item in source)
            {
                var obj=item.ToJsonString().JsonToObject<T>();
                newList.Add((T)obj);
            }
            return newList;
        }

        /// <summary>
        /// 实体类转换成DataSet
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public static DataSet FillDataSet<T>(this IList<T> modelList) where T : new()
        {
            DataSet ds = new DataSet();
            if (modelList == null || modelList.Count == 0)
            {
                return ds;
            }
            else
            {
                ds.Tables.Add(FillDataTable(modelList));
                return ds;
            }
        }

        /// <summary>
        /// 实体类转换成DataTable
        /// </summary>
        /// <param name="modelList">实体类列表</param>
        /// <returns></returns>
        public static DataTable FillDataTable<T>(this IList<T> modelList) where T : new()
        {
            DataTable dt = new T().CreateData();//DataTable dt = modelList[0].CreateData();
            if (modelList == null || modelList.Count == 0)
            {
                return dt;
            }
            

            foreach (T model in modelList)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    var v=propertyInfo.GetValue(model, null);
                    dataRow[propertyInfo.Name] = v.IsNull() ? DBNull.Value : v ;
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }
    }
}
