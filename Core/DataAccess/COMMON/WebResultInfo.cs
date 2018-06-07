using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web
{
    public class WebResultInfo
    {
        public bool Success
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public int TotalCount
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }
    }
}
