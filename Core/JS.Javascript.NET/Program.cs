using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Noesis.Javascript;
namespace JS.Javascript.NET
{
    public static class Program
    {
        static void  Main(string[] argements)
        {
            try
            {
                var context = new JavascriptContextEx();
                var js = @"function f1(a){ var i= 1+a; return i;}  f1(index);";
                context.SetParameter("index", 10);
                var handle = context.RunAsT<Int32>(js,false);
                var handle1 = context.RunAsString(js);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
