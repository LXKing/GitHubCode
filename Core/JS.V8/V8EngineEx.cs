using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace V8.Net
{
    public class V8EngineEx:V8.Net.V8Engine
    {
        public Handle Execute(string script, string sourceName = "V8.NET", bool throwExceptionOnError = false)
        {
            return base.Execute(script, sourceName, throwExceptionOnError);
        }
    }
}
