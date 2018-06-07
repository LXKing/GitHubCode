using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V8.Net
{
    public static class Program
    {
        static void  Main(string[] argements)
        {
            try
            {
                var js = @"function f1(){ var i= 1+10; return i;}  f1();";
                var handle = new V8EngineEx().Execute(js);
                if(handle.IsInt32)
                {
                    Console.WriteLine(handle.As<Int32>());
                }
                Console.Read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
