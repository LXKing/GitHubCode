using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BaseClass:ICloneable
    {
        /// <summary>
        /// 克隆一个对象(实现的ICloneable接口)
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            try
            {
                return CloneByDotnet();
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("异常",ex);
                throw ex;
            }
        }
        /// <summary>
        /// 使用二进制克隆
        /// </summary>
        /// <returns></returns>
        private object CloneByDotnet()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                var obj = formatter.Deserialize(stream) as BaseClass;
                return obj;
            }
            catch (Exception ex)
            {
                COMMON.Logs.Log.WriteException("异常", ex);
                throw ex;
            }
            
        }
    }
}
