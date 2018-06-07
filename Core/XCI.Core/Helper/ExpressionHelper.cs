using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace XCI.Helper
{
    /// <summary>
    /// Lambda表达式操作帮助类
    /// </summary>
    public class ExpressionHelper
    {
        /// <summary>
        /// 获取表达式对象属性名称 例如:GetPropertyName(Person)(p => p.FirstName);
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="exp">表达式</param>
        public static string GetPropertyName<T>(Expression<Func<T, object>> exp)
        {
            return GetPropertyName<T, object>(exp);
        }

        /// <summary>
        ///  获取表达式对象属性值 例如:GetPropertyName(Person)(p => p.FirstName);
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="exp">表达式</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue<T>(T obj, Expression<Func<T, object>> exp)
        {
            var fun = Expression.Lambda<Func<T, object>>(exp.Body, exp.Parameters).Compile();
            return fun.Invoke(obj);
        }


        /// <summary>
        /// 获取表达式对象属性名称
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <typeparam name="Y">返回值类型</typeparam>
        /// <param name="exp">表达式</param>
        public static string GetPropertyName<T, Y>(Expression<Func<T, Y>> exp)
        {
            MemberExpression memberExpression = null;

            // Get memberexpression.
            if (exp.Body is MemberExpression)
            {
                memberExpression = exp.Body as MemberExpression;
            }

            if (exp.Body is UnaryExpression)
            {
                var unaryExpression = exp.Body as UnaryExpression;
                if (unaryExpression != null && unaryExpression.Operand is MemberExpression)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
            }

            if (memberExpression == null)
            {
                throw new InvalidOperationException("没有成员访问表达式");
            }

            var info = memberExpression.Member as PropertyInfo;
            if (info != null)
            {
                return info.Name;
            }
            return string.Empty;
        }


        /// <summary>
        /// 获取表达式对象属性名称 例如:GetPropertyName(Person)(p => p.FirstName);
        /// </summary>
        /// <param name="exp">表达式</param>
        public static string GetPropertyName(Expression<Func<object>> exp)
        {
            var memberExpression = exp.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new InvalidOperationException("没有成员访问表达式");
            }

            var info = memberExpression.Member as PropertyInfo;
            if (info != null)
            {
                return info.Name;
            }
            return string.Empty;
        }


        /// <summary>
        /// 获取表达式对象属性值和属性名称
        /// </summary>
        /// <param name="exp">表达式</param>
        /// <param name="propName">属性名称</param>
        /// <returns>属性值</returns>
        public static object GetPropertyNameAndValue(Expression<Func<object>> exp, ref string propName)
        {
            //var memberExpression = exp.Body as MemberExpression;
            PropertyInfo propInfo = null;
            if (exp.Body is MemberExpression)
            {
                propInfo = ((MemberExpression)exp.Body).Member as PropertyInfo;
            }
            else if (exp.Body is UnaryExpression)
            {
                Expression op = ((UnaryExpression)exp.Body).Operand;
                propInfo = ((MemberExpression)op).Member as PropertyInfo;
            }

            object val = exp.Compile().DynamicInvoke();
            if (propInfo != null)
            {
                propName = propInfo.Name;
            }
            return val;
        }

        /// <summary>
        /// 根据表达式数组 获取列名数组
        /// </summary>
        /// <param name="exps">表达式数组</param>
        public static string[] GetColumnArray<T>(params Expression<Func<T, object>>[] exps)
        {
            if (exps == null || exps.Length == 0)
                return new string[0];
            string[] cols = new string[exps.Length];
            for (int ndx = 0; ndx < exps.Length; ndx++)
            {
                cols[ndx] = ExpressionHelper.GetPropertyName(exps[ndx]);
            }
            return cols;
        }
    }
}
