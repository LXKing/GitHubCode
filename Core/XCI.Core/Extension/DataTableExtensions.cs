// ===============================================================================
// Copyright (c) 2007-2013 西安交通信息投资营运有限公司。 
// ===============================================================================

using System;
using System.Data;

namespace XCI.Extensions
{
    /// <summary>
    /// DataTable扩展操作
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// 对 DataTable 的每行执行指定操作
        /// </summary>
        /// <param name="table">DataTable对象</param>
        /// <param name="action">执行的操作</param>
        /// <exception cref="System.ArgumentNullException">参数action 为null</exception>
        public static void ForEach(this DataTable table, Action<DataRow> action)
        {
            if (action == null)
            {
                throw new System.ArgumentNullException("action", "参数action不能为null");
            }
            foreach (DataRow row in table.Rows)
            {
                action(row);
            }
        }
    }
}