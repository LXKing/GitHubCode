using NPOI.Extension;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data.NPOI;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Collections.Generic.NPOI
{
    /// <summary>
    /// 泛型集合
    /// </summary>
    public static class IEnumerableEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dtSource"></param>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="writeColumnName"></param>
        /// <param name="excelType"></param>
        public static void WriteInToExcel<T>(this IEnumerable<T> dtSource, string fileName, string sheetName, bool writeColumnName = true, ExcelType excelType = ExcelType.XLS)
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
                    typeof(T).GetProperties().AsEnumerable().ToList().ForEach(x =>
                    {
                        row.CreateCell(columnIndex).SetCellValue(x.Name != null ? x.Name : string.Empty);
                        columnIndex++;
                    });
                }
                #endregion

                #region 写内容
                int i = writeColumnName ? 1 : 0;
                dtSource.ToList().ForEach(x =>
                {
                    IRow row = sheet.CreateRow(i);
                    int m = 0;
                    typeof(T).GetProperties().AsEnumerable().ToList().ForEach(p =>
                    {
                        var value = x.GetType().GetProperty(p.Name).GetValue(x, null);
                        row.CreateCell(m).SetCellValue(TypeConvert.ToString(p.PropertyType, value != null ? value : string.Empty));
                        m++;
                    });
                    i++;
                });
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
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">必须是有构造函数的实体类</typeparam>
        /// <param name="dtSource"></param>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="writeColumnName"></param>
        /// <param name="excelType"></param>
        public static void WriteStringsInToExcel(this IEnumerable<string> dtSource, string fileName, string sheetName, bool writeColumnName = true, string columnName = "Null Column Title", ExcelType excelType = ExcelType.XLS)
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

                var list = dtSource.ToList();

                #region 写列名称和内容
                if (writeColumnName)
                {
                    list.Insert(0,columnName);
                }
                var rowBeginIndex = 0;
                list.ForEach(x => {
                    IRow row = sheet.CreateRow(rowBeginIndex);
                    var cell =row.CreateCell(0,CellType.String);
                    cell.CellStyle.WrapText = true;
                    if (rowBeginIndex == 0 && writeColumnName)
                    {
                        ICellStyle style = workbook.CreateCellStyle();
                        //设置单元格的样式：水平对齐居中，有边框
                        style.Alignment = HorizontalAlignment.Center;
                        style.VerticalAlignment = VerticalAlignment.Center;

                        style.FillForegroundColor = HSSFColor.White.Index;
                        style.FillBackgroundColor = HSSFColor.Red.Index; //指定背景颜色
                        //将新的样式赋给单元格
                        cell.CellStyle = style;
                    }
                    cell.SetCellValue(x);
                    rowBeginIndex++;
                });
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
}
