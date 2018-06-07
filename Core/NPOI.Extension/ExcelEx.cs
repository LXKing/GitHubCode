using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Data.NPOI
{
    /// <summary>
    /// Excel文件操作类
    /// </summary>
    public static class ExcelEx
    {
        /// <summary>
        /// 从Excel文件读取内容到DataTable
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="sheetName">sheet名</param>
        /// <param name="isFirstRowColumn">第一行是否为列名</param>
        /// <param name="columnNameWithCaption">列名与标题键值对</param>
        /// <param name="columnNameWithDataType">列名与列数据类型的键值对</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn, Dictionary<string, Type> columnNameWithDataType = null, Dictionary<string, string> columnNameWithCaption = null)
        {
            IWorkbook workbook = null;
            ISheet sheet = null;
            FileStream fs = null;
            DataTable data = new DataTable();
            try
            {
                #region 初始化
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.ToLower().Contains(".xlsx"))
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else
                {
                    workbook = new HSSFWorkbook(fs);
                }

                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                    throw new Exception("未能找到对应的Sheet");
                #endregion

                int beginRow = 0;
                IRow firstRow = sheet.GetRow(0);
                int cellCount = firstRow.LastCellNum;
                if (isFirstRowColumn)
                {
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (cellValue != null)
                            {
                                DataColumn column = new DataColumn(cellValue);
                                if ((columnNameWithCaption != null) && columnNameWithCaption.ContainsKey(cellValue))
                                {
                                    column.Caption = columnNameWithCaption[cellValue];
                                }
                                if ((columnNameWithDataType != null) && columnNameWithDataType.ContainsKey(cellValue))
                                {
                                    column.DataType = columnNameWithDataType[cellValue];
                                }
                                data.Columns.Add(column);
                            }
                        }
                    }
                    beginRow = sheet.FirstRowNum + 1;
                }

                int rowCount = sheet.LastRowNum;

                for (int i = beginRow; i <= rowCount; ++i)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue; //没有数据的行默认是null　　　　　　　

                    DataRow dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        var cell = row.GetCell(j);
                        if (cell != null)
                            dataRow[j] = ToObject(data.Columns[j].DataType, cell);
                        //if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                        //dataRow[j] = row.GetCell(j).ToString();
                    }
                    data.Rows.Add(dataRow);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }
        }
        /// <summary>
        /// 从Excel读取到泛型集合中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">excel文件路径</param>
        /// <param name="sheetName">要读取的sheetname</param>
        /// <param name="startCloIndex">起始列索引(默认为0)</param>
        /// <returns></returns>
        public static IEnumerable<T> ExcelToIEnumerable<T>(string fileName, string sheetName, int startCloIndex = 0) where T : new()
        {
            IWorkbook workbook = null;
            ISheet sheet = null;
            FileStream fs = null;
            List<T> dataList = new List<T>();
            try
            {
                #region 初始化
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.ToLower().Contains(".xlsx"))
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else
                {
                    workbook = new HSSFWorkbook(fs);
                }

                sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                    throw new Exception("未能找到名为" + sheetName + "对应的Sheet");
                #endregion

                int beginRowNum = 1;
                int lastRowNum = sheet.LastRowNum;

                IRow headerRow = sheet.GetRow(0);
                if (headerRow == null)
                {
                    throw new Exception("没有找到匹配的单元格列头!");
                }
                int cellCount = headerRow.LastCellNum;

                for (int i = beginRowNum; i <= lastRowNum; ++i)
                {
                    IRow currentRow = sheet.GetRow(i);
                    if (currentRow == null)
                        continue;//没有数据的行默认是null

                    var obj = new T();
                    for (int j = startCloIndex; j < cellCount; ++j)
                    {
                        var p = obj.GetType().GetProperty(ToObject(typeof(string), headerRow.GetCell(j)).ToString());
                        if (p != null)
                            p.SetValue(obj, ToObject(p.PropertyType, currentRow.GetCell(j)), null);
                    }
                    dataList.Add(obj);
                }
                return dataList.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }
        }

        public static ICell ReadCell(string fileName, string sheetName, int rowIndex, int coloumnIndex)
        {
            ICell cell = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            FileStream fs = null;
            #region 初始化
            fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            if (fileName.ToLower().Contains(".xlsx"))
            {
                workbook = new XSSFWorkbook(fs);
            }
            else
            {
                workbook = new HSSFWorkbook(fs);
            }

            sheet = workbook.GetSheet(sheetName);
            if (sheet == null)
                throw new Exception("未能找到名为" + sheetName + "对应的Sheet");
            #endregion
            IRow row = sheet.GetRow(rowIndex);
            if (row != null)
            {
                cell = row.GetCell(coloumnIndex);
            }
            return cell;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        internal static object ToObject(Type type, ICell cell)
        {
            object result = null;
            switch (type.FullName)
            {
                case "System.String"://字符串类型
                    if (cell.CellType == CellType.String)
                        result = cell.StringCellValue;
                    else
                        result = string.Empty;
                    break;
                case "System.DateTime"://日期类型
                    if (cell.CellType == CellType.String)
                    {
                        DateTime dateV;
                        var success = DateTime.TryParse(cell.StringCellValue, out dateV);
                        if (success)
                            result = dateV;
                        else
                            dateV = cell.DateCellValue;
                    }
                    else if (cell.CellType == CellType.Numeric)
                    {
                        result = cell.DateCellValue;
                    }

                    break;
                case "System.Boolean"://布尔型
                    //bool boolV = false;
                    //bool.TryParse(drValue.ToString(), out boolV);
                    result = GetCellValue(cell);
                    break;
                case "System.Int16"://整型
                    Int16 intV16 = 0;
                    var success16 = Int16.TryParse(GetCellValue(cell).ToString(), out intV16);
                    if (success16)
                        result = intV16;
                    break;
                case "System.Int32":
                    Int32 intV32 = 0;
                    var success32 = Int32.TryParse(GetCellValue(cell).ToString(), out intV32);
                    if (success32)
                        result = intV32;
                    break;
                case "System.Int64":
                    Int64 intV64 = 0;
                    var success64 = Int64.TryParse(GetCellValue(cell).ToString(), out intV64);
                    if (success64)
                        result = intV64;
                    break;
                case "System.Decimal":
                    decimal decimalV = 0;
                    var successDec = decimal.TryParse(GetCellValue(cell).ToString(), out decimalV);
                    if (successDec)
                        result = decimalV;
                    break;
                case "System.Double":
                    double doubleV = 0;
                    var successDob = double.TryParse(GetCellValue(cell).ToString(), out doubleV);
                    if (successDob)
                        result = doubleV;
                    break;
                case "System.Byte":
                    byte byteV = 0;
                    var successByte = byte.TryParse(GetCellValue(cell).ToString(), out byteV);
                    if (successByte)
                        result = byteV;
                    break;
                case "System.SByte"://
                    sbyte sbyteV = 0;
                    var successSByte = sbyte.TryParse(GetCellValue(cell).ToString(), out sbyteV);
                    if (successSByte)
                        result = sbyteV;
                    break;
                case "System.DBNull"://空值处理
                    result = "NULL";
                    break;
                default:
                    result = string.Empty;
                    break;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static object GetCellValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Formula:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Unknown:
                    return null;
                default:
                    return null;
            }
        }
    }
}
