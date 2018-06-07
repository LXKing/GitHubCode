using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// 系统类型操作帮助类
    /// </summary>
    public class TypeHelper
    {
        /// <summary>
        /// 数字类型
        /// </summary>
        private static readonly IDictionary<string, bool> _numericTypes;

        /// <summary>
        /// 基础类型
        /// </summary>
        private static readonly IDictionary<string, bool> _basicTypes;


        /// <summary>
        /// 静态构造
        /// </summary>
        static TypeHelper()
        {
            _numericTypes = new Dictionary<string, bool>();
            _numericTypes[typeof(int).Name] = true;
            _numericTypes[typeof(long).Name] = true;
            _numericTypes[typeof(float).Name] = true;
            _numericTypes[typeof(double).Name] = true;
            _numericTypes[typeof(decimal).Name] = true;
            _numericTypes[typeof(sbyte).Name] = true;
            _numericTypes[typeof(Int16).Name] = true;
            _numericTypes[typeof(Int32).Name] = true;
            _numericTypes[typeof(Int64).Name] = true;
            _numericTypes[typeof(Double).Name] = true;
            _numericTypes[typeof(Decimal).Name] = true;

            _basicTypes = new Dictionary<string, bool>();
            _basicTypes[typeof(int).Name] = true;
            _basicTypes[typeof(long).Name] = true;
            _basicTypes[typeof(float).Name] = true;
            _basicTypes[typeof(double).Name] = true;
            _basicTypes[typeof(decimal).Name] = true;
            _basicTypes[typeof(sbyte).Name] = true;
            _basicTypes[typeof(Int16).Name] = true;
            _basicTypes[typeof(Int32).Name] = true;
            _basicTypes[typeof(Int64).Name] = true;
            _basicTypes[typeof(Double).Name] = true;
            _basicTypes[typeof(Decimal).Name] = true;
            _basicTypes[typeof(bool).Name] = true;
            _basicTypes[typeof(DateTime).Name] = true;            
            _basicTypes[typeof(string).Name] = true;            
        }


        /// <summary>
        /// 测试对象是否是数字类型
        /// </summary>
        /// <param name="val">测试对象</param>
        public static bool IsNumeric(object val)
        {
            return _numericTypes.ContainsKey(val.GetType().Name);
        }


        /// <summary>
        /// 测试类型是否是数字类型
        /// </summary>
        /// <param name="type">测试类型</param>
        public static bool IsNumeric(Type type)
        {
            return _numericTypes.ContainsKey(type.Name);
        }

        public static bool IsBoolean(Type type)
        {
            return type == typeof(bool) || type == typeof(bool?);
        }
        public static bool IsDateTime(Type type)
        {
            return type == typeof(DateTime);
        }


        /// <summary>
        /// 测试类型是否是基础类型
        /// </summary>
        /// <param name="type">测试类型</param>
        public static bool IsBasicType(Type type)
        {
            return _basicTypes.ContainsKey(type.Name);
        }

        public static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }
            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null) return ienum;
                }
            }
            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
            {
                return FindIEnumerable(seqType.BaseType);
            }
            return null;
        }

        public static Type GetSequenceType(Type elementType)
        {
            return typeof(IEnumerable<>).MakeGenericType(elementType);
        }

        public static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            if (ienum == null) return seqType;
            return ienum.GetGenericArguments()[0];
        }

        public static bool IsNullableType(Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsNullAssignable(Type type)
        {
            return !type.IsValueType || IsNullableType(type);
        }

        public static Type GetNonNullableType(Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }
            return type;
        }

        public static Type GetNullAssignableType(Type type)
        {
            if (!IsNullAssignable(type))
            {
                return typeof(Nullable<>).MakeGenericType(type);
            }
            return type;
        }

        public static ConstantExpression GetNullConstant(Type type)
        {
            return Expression.Constant(null, GetNullAssignableType(type));
        }

        public static Type GetMemberType(MemberInfo mi)
        {
            FieldInfo fi = mi as FieldInfo;
            if (fi != null) return fi.FieldType;
            PropertyInfo pi = mi as PropertyInfo;
            if (pi != null) return pi.PropertyType;
            EventInfo ei = mi as EventInfo;
            if (ei != null) return ei.EventHandlerType;
            MethodInfo meth = mi as MethodInfo;  // property getters really
            if (meth != null) return meth.ReturnType;
            return null;
        }

        public static object GetDefault(Type type)
        {
            bool isNullable = !type.IsValueType || TypeHelper.IsNullableType(type);
            if (!isNullable)
                return Activator.CreateInstance(type);
            return null;
        }

        public static bool IsReadOnly(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    return (((FieldInfo)member).Attributes & FieldAttributes.InitOnly) != 0;
                case MemberTypes.Property:
                    PropertyInfo pi = (PropertyInfo)member;
                    return !pi.CanWrite || pi.GetSetMethod() == null;
                default:
                    return true;
            }
        }

        public static bool IsInteger(Type type)
        {
            Type nnType = GetNonNullableType(type);
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }
    }
}
