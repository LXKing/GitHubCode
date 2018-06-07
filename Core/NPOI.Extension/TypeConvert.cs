using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPOI.Extension
{
    internal static  class TypeConvert
    {
        /// <summary>
        /// 类型转换对应的字符串
        /// </summary>
        /// <param name="type"></param>
        /// <param name="drValue"></param>
        /// <returns></returns>
        internal static string ToString(Type type, object drValue)
        {
            string result = string.Empty;
            switch (type.FullName)
            {
                case "System.String"://字符串类型
                    result = drValue.ToString();
                    break;
                case "System.DateTime"://日期类型
                    DateTime dateV;
                    DateTime.TryParse(drValue.ToString(), out dateV);
                    result = dateV.ToString("yyyy-MM-dd HH:mm:dd");
                    break;
                case "System.Boolean"://布尔型
                    bool boolV = false;
                    bool.TryParse(drValue.ToString(), out boolV);
                    result = boolV.ToString();
                    break;
                case "System.Int16"://整型
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    int intV = 0;
                    int.TryParse(drValue.ToString(), out intV);
                    result = intV.ToString();
                    break;
                case "System.Decimal"://浮点型
                case "System.Double":
                    double doubV = 0;
                    double.TryParse(drValue.ToString(), out doubV);
                    result = doubV.ToString();
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
    }
}
