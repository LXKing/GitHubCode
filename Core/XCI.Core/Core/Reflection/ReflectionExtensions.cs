using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;

namespace XCI.Core
{
    ///<summary>
    /// 方法调用句柄
    ///</summary>
    ///<param name="target">调用对象</param>
    ///<param name="paramters">方法参数</param>
    public delegate object FastInvokeHandler(object target, params object[] paramters);

    ///<summary>
    /// 对象反射管理
    ///</summary>
    public static class ReflectionExtensions
    {
        ///<summary>
        /// 返回对象的方法信息
        ///</summary>
        ///<param name="type">对象类型</param>
        ///<returns>对象方法数组</returns>
        public static FastMethodInfo[] GetFastMethods(this Type type)
        {
            IList<FastMethodInfo> fastMethods = new List<FastMethodInfo>();
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                fastMethods.Add(new FastMethodInfo(method));
            }
            return fastMethods.ToArray();
        }
        /// <summary>
        /// 获取快速的反射属性列表
        /// </summary>
        /// <param name="type">对象类型</param>
        public static Dictionary<string, FastPropertyInfo> GetFastPropertiesToDic(this Type type)
        {
            Dictionary<string, FastPropertyInfo> fastProperties = new Dictionary<string, FastPropertyInfo>();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (!fastProperties.ContainsKey(property.Name))
                {
                    fastProperties.Add(property.Name, new FastPropertyInfo(property));
                }                
            }
            return fastProperties;
        }
        ///<summary>
        /// 返回对象的属性信息
        ///</summary>
        ///<param name="type">对象类型</param>
        ///<returns>对象属性数组</returns>
        public static FastPropertyInfo[] GetFastProperties(this Type type)
        {
            IList<FastPropertyInfo> fastProperties = new List<FastPropertyInfo>();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                fastProperties.Add(new FastPropertyInfo(property));
            }
            return fastProperties.ToArray();
        }

        /// <summary>
        /// 返回对象的属性信息
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>对象属性</returns>
        public static FastPropertyInfo GetFastPropertyInfo(this Type type,string propertyName)
        {
            PropertyInfo property = type.GetProperty(propertyName);
            return new FastPropertyInfo(property);
        }

        ///<summary>
        /// 创建对象实例
        ///</summary>
        ///<typeparam name="TResult">对象类型</typeparam>
        ///<returns>对象实例</returns>
        public static TResult LambdaCreate<TResult>()
            where TResult : class
        {
            var info = typeof (TResult).GetConstructor(new Type[] {});
            if (info != null)
            {
                NewExpression newExpression = Expression.New(info);
                Expression<Func<TResult>> newLambda = Expression.Lambda<Func<TResult>>(
                    newExpression, null);
                return newLambda.Compile()();
            }
            return null;
        }

        ///<summary>
        /// 创建对象实例
        ///</summary>
        ///<param name="type">对象类型</param>
        ///<returns>对象实例</returns>
        public static object LambdaCreate(this Type type)
        {
            var info = type.GetConstructor(new Type[] {});
            if (info != null)
            {
                NewExpression newExpression = Expression.New(info);
                Expression<Func<object>> newLambda = Expression.Lambda<Func<object>>(newExpression, null);
                return newLambda.Compile()();
            }
            return null;
        }

        ///<summary>
        /// 获取方法调用句柄
        ///</summary>
        ///<param name="methodInfo">方法信息</param>
        ///<returns>方法调用句柄</returns>
        public static FastInvokeHandler GetFastInvoker(this MethodInfo methodInfo)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object), typeof(object[]) }, methodInfo.DeclaringType.Module);
            ILGenerator il = dynamicMethod.GetILGenerator();
            ParameterInfo[] ps = methodInfo.GetParameters();
            Type[] paramTypes = new Type[ps.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                    paramTypes[i] = ps[i].ParameterType.GetElementType();
                else
                    paramTypes[i] = ps[i].ParameterType;
            }
            LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];

            for (int i = 0; i < paramTypes.Length; i++)
            {
                locals[i] = il.DeclareLocal(paramTypes[i], true);
            }
            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                EmitFastInt(il, i);
                il.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(il, paramTypes[i]);
                il.Emit(OpCodes.Stloc, locals[i]);
            }
            if (!methodInfo.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_0);
            }
            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                    il.Emit(OpCodes.Ldloca_S, locals[i]);
                else
                    il.Emit(OpCodes.Ldloc, locals[i]);
            }
            il.EmitCall(methodInfo.IsStatic ? OpCodes.Call : OpCodes.Callvirt, methodInfo, null);
            if (methodInfo.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                EmitBoxIfNeeded(il, methodInfo.ReturnType);

            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                {
                    il.Emit(OpCodes.Ldarg_1);
                    EmitFastInt(il, i);
                    il.Emit(OpCodes.Ldloc, locals[i]);
                    if (locals[i].LocalType.IsValueType)
                        il.Emit(OpCodes.Box, locals[i].LocalType);
                    il.Emit(OpCodes.Stelem_Ref);
                }
            }

            il.Emit(OpCodes.Ret);
            FastInvokeHandler invoder = (FastInvokeHandler)dynamicMethod.CreateDelegate(typeof(FastInvokeHandler));
            return invoder;
        }

        private static void EmitCastToReference(ILGenerator il, Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, type);
            }
            else
            {
                il.Emit(OpCodes.Castclass, type);
            }
        }

        private static void EmitBoxIfNeeded(ILGenerator il, Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Box, type);
            }
        }

        private static void EmitFastInt(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
            {
                il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, value);
            }
        }
    }
}
