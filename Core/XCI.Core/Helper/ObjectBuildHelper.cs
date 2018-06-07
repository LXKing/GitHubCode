using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XCI.Helper
{
    /// <summary>
    /// 对象生成帮助类
    /// </summary>
    public static class ObjectBuildHelper
    {
        /// <summary>
        /// 对象类型容器
        /// </summary>
        private static IDictionary<Type, Type> ObjectTypeDic = new Dictionary<Type, Type>(100);


        /// <summary>
        /// 对象实例参数容器
        /// </summary>
        private static IDictionary<Type, IDictionary<string, string>> ObjectParamDic = new Dictionary<Type, IDictionary<string, string>>();


        /// <summary>
        /// 对象实例容器
        /// </summary>
        private static IDictionary<Type, object> ObjectInstanceDic = new Dictionary<Type, object>(100);


        /// <summary>
        /// 注册对象类型
        /// </summary>
        /// <typeparam name="TFrom">接口类型</typeparam>
        /// <typeparam name="TTo">实现类型</typeparam>
        /// <returns>注册成功返回True</returns>
        public static bool Register<TFrom, TTo>() where TTo : TFrom
        {
            return Register(typeof(TFrom), typeof(TTo));
        }


        /// <summary>
        /// 注册对象类型
        /// </summary>
        /// <param name="from">接口类型字符串</param>
        /// <param name="to">实现类型字符串</param>
        /// <returns>注册成功返回True</returns>
        public static bool Register(string from, string to)
        {
            Type f = Type.GetType(from);
            Type t = Type.GetType(to);
            Register(f, t);
            return true;
        }

        /// <summary>
        /// 注册对象类型
        /// </summary>
        /// <param name="from">接口类型</param>
        /// <param name="to">实现类型</param>
        /// <exception cref="System.ArgumentNullException">注册类型不能为空</exception>
        /// <returns>注册成功返回True</returns>
        public static bool Register(Type from, Type to)
        {
            if (from == null || to == null)
            {
                throw new ArgumentNullException("from", "注册类型不能为空");
            }

            if (!ObjectTypeDic.ContainsKey(from)) //不包含接口
            {
                ObjectTypeDic.Add(from, to);
            }
            else
            {
                ObjectTypeDic[from] = to;
            }
            return true;
        }


        /// <summary>
        /// 注册对象实例参数
        /// </summary>
        /// <typeparam name="TFrom">接口类型</typeparam>
        /// <param name="param">参数字典</param>
        /// <returns>注册成功返回True</returns>
        public static bool RegisterParam<TFrom>(IDictionary<string, string> param)
        {
            return RegisterParam(typeof(TFrom), param);
        }


        /// <summary>
        /// 注册对象实例参数
        /// </summary>
        /// <param name="from">接口类型</param>
        /// <param name="param">参数字典</param>
        /// <returns>注册成功返回True</returns>
        public static bool RegisterParam(Type from, IDictionary<string, string> param)
        {
            if (!ObjectParamDic.ContainsKey(from))
            {
                ObjectParamDic.Add(from, param);
            }
            else
            {
                ObjectParamDic[from] = param;
            }
            return true;
        }


        /// <summary>
        /// 是否包含指定类型
        /// </summary>
        /// <typeparam name="TFrom">接口类型</typeparam>
        /// <returns>包含返回True</returns>
        public static bool ContainsReg<TFrom>()
        {
            return ContainsReg(typeof(TFrom));
        }


        /// <summary>
        /// 是否包含指定类型
        /// </summary>
        /// <param name="from">接口类型</param>
        /// <returns>包含返回True</returns>
        public static bool ContainsReg(Type from)
        {
            return ObjectTypeDic.ContainsKey(from);
        }


        /// <summary>
        /// 获取接口实现类
        /// </summary>
        /// <typeparam name="TFrom">接口类型</typeparam>
        /// <returns>接口实现类</returns>
        public static Type GetImplementType<TFrom>()
        {
            return GetImplementType(typeof (TFrom));
        }


        /// <summary>
        /// 获取接口实现类
        /// </summary>
        /// <param name="from">接口类型</param>
        /// <returns>接口实现类</returns>
        public static Type GetImplementType(Type from)
        {
            if (ContainsReg(from))
            {
                return ObjectTypeDic[from];
            }
            return null;
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="TFrom">接口类型</typeparam>
        /// <returns>返回新对象</returns>
        public static TFrom Create<TFrom>()
        {
            return (TFrom)Create(typeof(TFrom), false);
        }


        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="fromType">接口类型</param>
        /// <param name="isSingle">是否单例创建</param>
        /// <returns>如果 isSingle 是True 返回单例 否则创建新对象</returns>
        public static object Create(Type fromType, bool isSingle)
        {
            if (!ObjectTypeDic.ContainsKey(fromType))
            {
                return null;
            }
            Type target = ObjectTypeDic[fromType];
            object instance;
            if (isSingle)
            {
                if (!ObjectInstanceDic.ContainsKey(fromType))
                {
                    instance = CreateInstance(fromType, target);
                    ObjectInstanceDic.Add(fromType, instance);
                }
                else
                {
                    instance = ObjectInstanceDic[fromType];
                }
            }
            else
            {
                instance = CreateInstance(fromType, target);
            }

            return instance;
        }


        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="fromType">接口类型</param>
        /// <param name="target">实现类型</param>
        /// <returns>对象实例</returns>
        private static object CreateInstance(Type fromType, Type target)
        {
            var instance = Activator.CreateInstance(target);
            if (ObjectParamDic.ContainsKey(fromType))
            {
                var paramDic = ObjectParamDic[fromType];
                foreach (KeyValuePair<string, string> pair in paramDic)
                {
                    object value = pair.Value;
                    if (value.Equals("no")||string.IsNullOrEmpty(value.ToString()))
                    {
                        continue;
                    }
                    
                    if (value.Equals("null"))
                    {
                        value = null;
                    }

                    PropertyInfo pro = target.GetProperty(pair.Key);
                    if (pro != null)
                    {
                        pro.SetValue(instance,
                            ObjectHelper.ConvertObjectValue(value, pro.PropertyType), null);
                    }
                    
                }
            }

            return instance;
        }


        /// <summary>
        /// 单例创建 如果已经创建 直接返回
        /// </summary>
        /// <typeparam name="TFrom">接口类型</typeparam>
        /// <returns>返回单例对象</returns>
        public static TFrom SingleCreate<TFrom>()
        {
            return (TFrom)SingleCreate(typeof(TFrom));
        }


        /// <summary>
        /// 单例创建 如果已经创建 直接返回
        /// </summary>
        /// <param name="fromType">接口类型</param>
        /// <returns>返回单例对象</returns>
        public static object SingleCreate(Type fromType)
        {
            return Create(fromType, true);
        }

        
        ///// <summary>
        ///// 获取实现类说明信息
        ///// </summary>
        ///// <typeparam name="TFrom">接口类型</typeparam>
        //public static XCIComponentAttribute GetComponentAttribute<TFrom>()
        //{
        //    return GetComponentAttribute(typeof (TFrom));
        //}


        ///// <summary>
        ///// 获取实现类说明信息
        ///// </summary>
        ///// <param name="from">接口类型</param>
        //public static XCIComponentAttribute GetComponentAttribute(Type from)
        //{
        //    if (!ContainsReg(from))
        //    {
        //        return null;
        //    }
        //    Type implement = GetImplementType(from);
        //    XCIComponentAttribute att = AttributeHelper
        //        .GetClassAttribute<XCIComponentAttribute>(implement);
        //    return att;
        //}

    }
}
