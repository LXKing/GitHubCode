// ===============================================================================
// Copyright (c) 2007-2013 西安交通信息投资营运有限公司。 
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// DataTable操作类
    /// </summary>
    public static class DataTableHelper
    {
        /// <summary>
        /// 复制数据行到新的DataTable实例
        /// </summary>
        /// <param name="copyRows">复制的行</param>
        /// <returns>返回新的DataTable实例</returns>
        public static DataTable CopyDataRow(DataRow[] copyRows)
        {
            if (copyRows.Length == 0)
            {
                return null;
            }
            DataTable sourceTable = copyRows[0].Table;

            //DataTable的数据结构拷贝到新的DataTable实例中
            DataTable tempDt = sourceTable.Clone();

            //把上面得到的那个符合条件的行的数组全部复制到新到DataTable的实例中
            foreach (DataRow t in copyRows)
            {
                //把数据复制到新的DataTable的实例中
                tempDt.ImportRow(t);
            }
            return tempDt;
        }

        /// <summary>
        /// 过滤表数据
        /// </summary>
        /// <param name="table">源表</param>
        /// <param name="filter">过滤表达式</param>
        public static DataTable FilterDataTable(DataTable table, string filter)
        {
            var rows = table.Select(filter);
            return CopyDataRow(rows);
        }

        /// <summary>
        /// 更新源数据行
        /// </summary>
        /// <param name="sourceRow">源数据行</param>
        /// <param name="newRow">新数据行</param>
        public static void UpdateRow(DataRow sourceRow, DataRow newRow)
        {
            foreach (DataColumn col in sourceRow.Table.Columns)
            {
                sourceRow[col.ColumnName] = newRow[col.ColumnName];
            }
        }

        /// <summary>
        /// 数据行中是否有指定属性名称
        /// </summary>
        /// <param name="dataRow">数据行</param>
        /// <param name="columnName">属性名称</param>
        public static bool HasColumnName(DataRow dataRow, string columnName)
        {
            DataColumnCollection collection = dataRow.Table.Columns;
            if (collection.Count > 0)
            {
                foreach (DataColumn item in collection)
                {
                    if (columnName.Equals(item.ColumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 根据实体属性创建表(没有数据)
        /// </summary>
        /// <returns>返回新的DataTable对象</returns>
        public static DataTable CreateTable(object instance)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            ds.Tables.Add(table);
            Type entityType = instance.GetType();
            ds.DataSetName = string.Concat(entityType.Name, "s");
            table.TableName = entityType.Name;
            foreach (PropertyInfo info in entityType.GetProperties())
            {
                DataColumn dc = new DataColumn();
                dc.DataType = info.PropertyType;
                dc.ColumnName = info.Name;
                table.Columns.Add(dc);
            }
            return table;
        }

        /// <summary>
        /// 根据实体属性创建表(没有数据)
        /// </summary>
        /// <returns>返回新的DataTable对象</returns>
        public static DataTable CreateTable(Type type)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            ds.Tables.Add(table);
            ds.DataSetName = string.Concat(type.Name, "s");
            table.TableName = type.Name;
            foreach (PropertyInfo info in type.GetProperties())
            {
                DataColumn dc = new DataColumn();
                dc.DataType = info.PropertyType;
                dc.ColumnName = info.Name;
                table.Columns.Add(dc);
            }
            return table;
        }

        /// <summary>
        /// 复制一个新的数据行对象
        /// </summary>
        /// <param name="sourceRow">源数据行</param>
        /// <returns>返回新的数据行对象</returns>
        public static DataRow CopyDataRow(DataRow sourceRow)
        {
            DataTable table = sourceRow.Table;
            var row = table.NewRow();
            foreach (DataColumn column in table.Columns)
            {
                row[column] = sourceRow[column];
            }
            return row;
        }

        /// <summary>
        /// 把实体对象转为数据行对象
        /// </summary>
        /// <param name="table">源表</param>
        /// <param name="entity">实体对象</param>
        /// <returns>返回新的数据行对象。</returns>
        public static DataRow ConvertToRow(DataTable table, object entity)
        {
            if (entity == null) return null;
            Type entityType = entity.GetType();
            DataRow row = table.NewRow();
            foreach (DataColumn column in table.Columns)
            {
                string colName = column.ColumnName;
                var pro = entityType.GetProperty(colName);
                if (pro == null) continue;
                row[colName] = pro.GetValue(entity, null);
            }
            return row;
        }

        /// <summary>
        /// 把数据行对象中的列值赋值到实现对象的属性中。
        /// 如果实现对象中没有找到对应的属性，则忽略。
        /// </summary>
        /// <param name="row">数据行对象</param>
        /// <param name="entity">实体对象</param>
        public static void ConvertToEntity(DataRow row, object entity)
        {
            Type entityType = entity.GetType();
            foreach (DataColumn column in row.Table.Columns)
            {
                string colName = column.ColumnName;
                var pro = entityType.GetProperty(colName);
                if (pro == null) continue;
                object value = row[colName];
                if (value == DBNull.Value) continue;
                if (!ObjectHelper.IsNullableType(pro.PropertyType) && value == null) continue;
                pro.SetValue(entity, ObjectHelper.ConvertObjectValue(value, pro.PropertyType), null);
            }
        }

        /// <summary>
        /// 把数据行转成格式为"列名(描述)=列值"的字符串
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns>数据行字符串</returns>
        public static string DataRowToString(DataRow row)
        {
            DataTable dt = row.Table;
            StringBuilder sb = new StringBuilder();
            foreach (DataColumn item in dt.Columns)
            {
                sb.AppendFormat("{0}({1})={2}  ", item.ColumnName, item.Caption, row[item]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 把数据列表转为DataTable
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="list">数据列表</param>
        /// <returns>新创建的DataTable</returns>
        public static DataTable ConvertToDataTable<T>(IList<T> list) where T : class,new()
        {
            var table = CreateTable(typeof(T));
            foreach (T entity in list)
            {
                var row = ConvertToRow(table, entity);
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// 比较DataRow
        /// </summary>
        /// <param name="sourceRow">原来的Row</param>
        /// <param name="newRow">新的Row</param>
        /// <returns>如果Row值发生变化返回true</returns>
        public static bool CompareDataRow(DataRow sourceRow, DataRow newRow)
        {
            var cols = sourceRow.Table.Columns;
            foreach (DataColumn col in cols)
            {
                var oldValue = sourceRow[col];
                var newValue = newRow[col];
                if (oldValue != newValue)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// DataTable分页,起始页码为1
        /// </summary>
        /// <param name="sourceTable">源表</param>
        /// <param name="pageIndex">页码索引,起始页为1</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns>返回分页后的DataTable</returns>
        public static DataTable GetPagedTable(DataTable sourceTable, int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
            {
                pageIndex = 1;
            }
            if (pageIndex == 0)
            {
                return sourceTable;
            }
            if (sourceTable.Rows.Count == 0)
            {
                return sourceTable;
            }

            DataTable newdt = sourceTable.Clone();
            int rowbegin = (pageIndex - 1) * pageSize;
            int rowend = pageIndex * pageSize;

            if (rowbegin >= sourceTable.Rows.Count)
                return sourceTable;

            if (rowend > sourceTable.Rows.Count)
                rowend = sourceTable.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow dr = sourceTable.Rows[i];
                newdt.ImportRow(dr);
            }
            return newdt;
        }

        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <param name="sourceTable">源表</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns>返回总页数</returns>
        public static int GetPageCount(DataTable sourceTable, int pageSize)
        {
            int sumCount = sourceTable.Rows.Count;
            int page = sumCount / pageSize;
            if (sumCount % pageSize > 0)
            {
                page = page + 1;
            }
            return page;
        }

        /// <summary>
        /// 复选框字段名称
        /// </summary>
        public const string CheckEditFieldName = "Checked";

        /// <summary>
        /// 添加复选框列
        /// </summary>
        /// <param name="table">数据表</param>
        public static void AddCheckColumn(DataTable table)
        {
            DataColumn dc = new DataColumn();
            dc.DefaultValue = false;
            dc.ColumnName = CheckEditFieldName;
            dc.DataType = typeof(bool);
            table.Columns.Add(dc);
        }
    }
}