using System;
using System.Collections.Generic;

namespace XCI.Core
{
    public class ServiceFactory<I> : BaseFactory<I> where I : class
    {
        
    }

    public static class ServiceManager
    {
        private readonly static Dictionary<string, object> InstanceDic = new Dictionary<string, object>();

        public static BaseFactory<I> GetManager<I>() where I : class
        {
            string key = typeof(I).FullName;
            if (!string.IsNullOrEmpty(key))
            {
                if (!InstanceDic.ContainsKey(key))
                {
                    var instance = new BaseFactory<I>();
                    InstanceDic[key] = instance;
                }
                return (BaseFactory<I>)InstanceDic[key];
            }
            return default(BaseFactory<I>);
        }
    }
}