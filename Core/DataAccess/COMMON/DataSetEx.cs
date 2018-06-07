using System.Collections.Generic;
using System.Text;

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
        public static List<T> FillModel_DA<T>(this DataSet ds) where T : new()
        {
            if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0].FillModel_DA<T>();
            }
        }
        /// <summary>  
        /// 填充对象列表：用DataSet的第index个表填充实体类
        /// </summary>  
        public static List<T> FillModel_DA<T>(this DataSet ds, int index = 0) where T : new()
        {
            if (ds == null || ds.Tables.Count <= index || ds.Tables[index].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[index].ToList_DA<T>();
            }
        }
        /// <summary>
        /// 转List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<T> ToList_DA<T>(this DataSet ds, int index = 0) where T : new()
        {
            if (ds == null || ds.Tables.Count <= index || ds.Tables[index].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[index].ToList_DA<T>();
            }
        }

        #region DataSet转换成Json格式
        /// <summary>  
        /// DataSet转换成Json格式  
        /// </summary>  
        /// <param name="ds">DataSet</param> 
        /// <returns></returns>  
        public static string Dataset2Json(this DataSet ds)
        {
            StringBuilder json = new StringBuilder();

            foreach (DataTable dt in ds.Tables)
            {
                json.Append("{\"");
                json.Append(dt.TableName);
                json.Append("\":");
                json.Append(dt.DataTableToJson());
                json.Append("}");
            }
            return json.ToString();
        }
        #endregion
        #endregion
    }
}
