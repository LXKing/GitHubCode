using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.IO
{
   public static class StreamEx
    {
       public static byte[] ToBytes(this Stream stream)
       {
           byte[] bytes = new byte[stream.Length];
           stream.Read(bytes, 0, bytes.Length);
           // 设置当前流的位置为流的开始
           stream.Seek(0, SeekOrigin.Begin);
           return bytes;
       }
    }
}
