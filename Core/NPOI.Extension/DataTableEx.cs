using System;
using System.Data;
using System.Linq;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Collections.Generic;
using NPOI.Extension;

namespace System.Data.NPOI
{
    /// <summary>
    /// DataTable扩展方法
    /// </summary>
    public static class DataTableEx
    {
        /// <summary>
        /// 将DataTable写入到Excel
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="writeColumnName"></param>
        /// <param name="excelType"></param>
        public static void WriteInToExcel(this DataTable dtSource, string fileName,string sheetName,bool writeColumnName=true, ExcelType excelType = ExcelType.XLS) 
        {
            FileStream fs = null;
            IWorkbook workbook = null;
            ISheet sheet = null;

            try
            {
                #region 初始化
                if (excelType == ExcelType.XLS)
                {
                    workbook = new HSSFWorkbook();
                    if (fileName.Substring(fileName.Length - 4, 4).ToLower() != ".xls")
                    {
                        fileName = fileName + ".xls";
                    }
                }
                else
                {
                    workbook = new XSSFWorkbook();
                    if (fileName.Substring(fileName.Length - 4, 4).ToLower() != ".xls")
                    {
                        fileName = fileName + ".xlsx";
                    }
                }

                if (workbook != null)
                {
                    if (sheet == null)
                        sheet = workbook.CreateSheet(sheetName);
                }
                fs = new FileStream(fileName + ".", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                #endregion

                #region 写列名称
                if (writeColumnName)
                {
                    IRow row = sheet.CreateRow(0);
                    int columnIndex = 0;
                    dtSource.Columns.Cast<DataColumn>().ToList().ForEach(x =>
                    {
                        row.CreateCell(columnIndex).SetCellValue(x.ColumnName != null ? x.ColumnName : string.Empty);
                        columnIndex++;
                    });
                }
                #endregion

                #region 写内容
                int i = writeColumnName ? 1 : 0;
                for (int n = 0; n < dtSource.Rows.Count; ++n)
                {
                    IRow row = sheet.CreateRow(i);
                    int m = 0;
                    dtSource.Columns.Cast<DataColumn>().ToList().ForEach(x =>
                    {
                        row.CreateCell(m).SetCellValue(TypeConvert.ToString(x.DataType, dtSource.Rows[n][m]));
                        m++;
                    });
                    i++;
                }
                #endregion

                workbook.Write(fs); //写入到excel
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs.Dispose();
                fs.Close();
            }
        }
    }
    /// <summary>
    /// Excel文件后缀类型
    /// </summary>
    public enum ExcelType
    {
        /// <summary>
        /// 2003版本
        /// </summary>
        XLS,
        /// <summary>
        /// 2007以后版本
        /// </summary>
        XLSX
    }
}
