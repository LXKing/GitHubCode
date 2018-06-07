using System.Collections.Generic;

namespace System.Data
{
    public static  class DataSetEx
    {
        #region 扩展方法 
        /// <summary>
        /// 填充对象列表：用DataSet的第一个表填充实体类
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <returns></returns>
        public static List<T> FillModel<T>(this DataSet ds)where T: new()
        {
            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0].FillModel<T>();
            }
        }
        /// <summary>  
        /// 填充对象列表：用DataSet的第index个表填充实体类
        /// </summary>  
        public static List<T> FillModel<T>(this DataSet ds, int index=0)where T: new()
        {
            if (ds == null || ds.Tables.Count <= index || ds.Tables[index].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[index].ToList<T>();
            }
        }
        /// <summary>
        /// 转List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataSet ds, int index = 0) where T : new()
        {
            if (ds == null || ds.Tables.Count <= index || ds.Tables[index].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[index].ToList<T>();
            }
        }
        #endregion
    }
}
