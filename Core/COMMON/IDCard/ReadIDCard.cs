using COMMON.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace IDCardRead
{
    public class ReadIDCard
    {
        IReadIDCard _ReadIDCardInstance;

        public IReadIDCard ReadIDCardInstance
        {
            get { return _ReadIDCardInstance; }
            set { _ReadIDCardInstance = value; }
        }
        int _IPort;
        public int IPort
        {
            get { return _IPort; }
            set { _IPort = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="readTypeName"></param>
        /// <param name="iport">1001-1006为USB端口</param>
        /// <param name="readIDCardRemark"></param>
        public ReadIDCard(string assemblyName,string readTypeName,int iport=1001,string readIDCardRemark="")
        {
            Assembly assembly;
            Type t;
            try
            {
                var p = Application.StartupPath + @"\" + assemblyName;
                if ((!p.Contains(".dll")))
                {
                    p = p + ".dll";
                }
                try
                {
                    assembly = Assembly.LoadFile(p);
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Log.WriteException("读卡器实例化初始异常(" + readIDCardRemark + ")", ex);
                    throw new Exception(@"path 参数为空字符串 ("") 或不存在"+p,ex);
                }
                catch (System.IO.FileLoadException ex)
                {
                    Log.WriteException("读卡器实例化初始异常(" + readIDCardRemark + ")", ex);
                    throw new Exception("发现一个未能加载的文件" + p, ex);
                }
                t = assembly.GetType(readTypeName);
                IPort = iport;
                ReadIDCardInstance = (IReadIDCard)Activator.CreateInstance(t, new object[] { IPort });  
            }
            catch (Exception ex)
            {
                Log.WriteException("读卡器实例化初始异常(" + readIDCardRemark+")", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <returns></returns>
        public PersonInfo Read()
        {
            return _ReadIDCardInstance.ReadPersonInfo();
        }
    }
}
