using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace IDCardRead.Potevio
{
    /// <summary>
    /// 中国普天读卡器读卡操作类(CP IDMR02系列)
    /// </summary>
    public class IDCradRead_Potevio : IReadIDCard
    {
        const int maxErrorTextLen = 32;
        PERSONINFOW person;
        int iPort = 0;
        public IDCradRead_Potevio(int IPort)
        {
            iPort = IPort;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="com">1表示串口1，2表示串口2，以此类推；1001表示USB；0表示自动选择</param>
        /// <returns></returns>
        public PersonInfo ReadPersonInfo()
        { 
            var p = new PersonInfo();
            try
            {
                person = new PERSONINFOW();
           
                //p.Birthday = "1989-07-23";
                //p.CardNO = "610424198907232671";
                //p.Address = "显示莲湖区北院门178号";
                //p.Name = "张三";

                #region 打开设备
                Int32 result;
                StringBuilder errorText = new StringBuilder();
                /*参数1为端口号。1表示串口1，2表示串口2，依次类推。1001表示USB。0表示自动选择。
                  参数2为标志位。0x02表示启用重复读卡。0x04表示读卡后接着读取新地址。
                  各个数值可以用“按位或”运算符组合起来。
                  参数3为波特率。使用串口阅读器的程序应正确设置此参数。出厂机器的波特率一般为115200。
                */
                #region 检测
                result = OpenCardReader(iPort, 2, 115200);
                var msg = Convert.ToString(result);
                GetErrorTextW(errorText, maxErrorTextLen);
                if (result > 0)
                {
                    throw new Exception(errorText.ToString());
                } 
                #endregion
                #endregion
                //textDescription.Text = errorText.ToString();

                #region 读取信息
                String imagePath;
                imagePath = Path.GetTempPath() + "image.bmp";
                result = GetPersonMsgW(ref person, imagePath);
                if(result>0)
                {
                    switch(result)
                    {
                        case 1:
                            throw new Exception("端口打开失败!");
                        case 2:
                            throw new Exception("数据传输超时!");
                        case 10:
                            throw new Exception("没有找到卡!");
                        case 11:
                            throw new Exception("读卡操作失败!");
                        case 20:
                            throw new Exception("自检失败!");
                        case 30:
                            throw new Exception("其他错误!");
                    }
                }
                else
                {
                    p.Name = person.name;
                    p.CardNO = person.cardId;
                    p.Address = person.address;
                    p.Sex = person.sex;
                    p.Birthday = person.birthday;
                    p.GrantDept = person.police;
                    p.IDCardBeginDate = person.validStart;
                    p.IDCardEndDate = person.validEnd;
                    p.Nation = person.nation;
                    p.Photo = Image.FromFile(imagePath);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception("中国普天读卡器异常:\r"+ex.Message,ex);
            }
            finally
            {
                #region 关闭设备
                CloseCardReader();  
                #endregion
            }
            return p;
            //throw new NotImplementedException();
        }
        string ConvertDate(string str, int mode)
        {
            string year;
            string month;
            string day;
            if (1 == mode)
            {
                if (str.Length >= 8)
                {
                    year = str.Substring(0, 4);
                    month = str.Substring(4, 2);
                    day = str.Substring(6, 2);
                    return string.Format("{0}年{1}月{2}日", year, month, day);
                }
            }
            else if (2 == mode)
            {
                if (str.Equals("长期"))
                {
                    return "长期";
                }
                else
                {
                    if (str.Length >= 8)
                    {
                        year = str.Substring(0, 4);
                        month = str.Substring(4, 2);
                        day = str.Substring(6, 2);
                        return string.Format("{0}.{1}.{2}", year, month, day);
                    }
                }
            }
            return "";
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 8)]
        public struct PERSONINFOW
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string name;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
            public string sex;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string nation;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string birthday;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
            public string address;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string cardId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string police;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string validStart;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
            public string validEnd;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
            public string sexCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string nationCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
            public string appendMsg;
        }
        /// <summary>
        /// 打开读卡器
        /// </summary>
        /// <param name="lPort"></param>
        /// <param name="ulFlag"></param>
        /// <param name="ulBaudRate"></param>
        /// <returns></returns>
        [DllImport(@"DLL\Potevio\cardapi3.dll", EntryPoint = "OpenCardReader",
            CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern Int32 OpenCardReader(Int32 lPort, UInt32 ulFlag, UInt32 ulBaudRate);
        [DllImport(@"DLL\Potevio\cardapi3.dll", EntryPoint = "GetPersonMsgW",
            CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern Int32 GetPersonMsgW(ref PERSONINFOW pInfo, string pszImageFile);
        [DllImport(@"DLL\Potevio\cardapi3.dll", EntryPoint = "CloseCardReader",
            CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern Int32 CloseCardReader();
        [DllImport(@"DLL\Potevio\cardapi3.dll", EntryPoint = "GetErrorTextW",
            CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern void GetErrorTextW(StringBuilder pszBuffer, UInt32 dwBufLen);
    }
}
