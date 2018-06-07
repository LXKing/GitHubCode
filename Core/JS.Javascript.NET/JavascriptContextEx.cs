using Noesis.Javascript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JS.Javascript.NET
{
    public class JavascriptContextEx : JavascriptContext
    {
        public object Run(string iSourceCode,bool autoDispose=false)
        {
            object result;
            try
            {
                result = base.Run(iSourceCode);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(autoDispose)
                {
                    this.Dispose();
                }
            }
        }

        public T RunAsT<T>(string iSourceCode, bool autoDispose = false) where T: struct
        {
            T result=default(T);
            try
            {
                result = (T)this.Run(iSourceCode,autoDispose);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        public string RunAsString(string iSourceCode, bool autoDispose = false)
        {
            string result = default(string);
            try
            {
                result = this.Run(iSourceCode, autoDispose).ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
    }
}
