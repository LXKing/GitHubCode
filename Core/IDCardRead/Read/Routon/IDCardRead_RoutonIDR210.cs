using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IDCardRead.Routon
{
    /// <summary>
    /// 精伦电子IDR210
    /// </summary>
    public class IDCardRead_RoutonIDR210:IReadIDCard
    {
        //[DllImport(@"DLL\Routon\Sdtapi.dll", EntryPoint = "InitComm",
        //    CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        //public static extern Int32 InitComm(int iPort);
        //[DllImport(@"DLL\Routon\Sdtapi.dll", EntryPoint = "Authenticate",
        //    CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        //public static extern int Authenticate();

        //[DllImport(@"DLL\Routon\Sdtapi.dll", EntryPoint = "ReadBaseMsg",
        //    CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        //public static extern int ReadBaseMsg(out byte[] pMsg, out int len);
        #region 外部方法
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="lPort">串口(1-16)  USB(1001)</param>
        /// <returns>1初始化正确  非1初始化失败</returns>
        [DllImport(@"DLL\Routon\sdtapi.dll", EntryPoint = "InitComm",
           CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern Int32 InitComm(Int32 lPort);

        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns>1关闭正确  非1关闭失败</returns>
        [DllImport(@"DLL\Routon\sdtapi.dll", EntryPoint = "CloseComm",
            CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern Int32 CloseComm();

        /// <summary>
        /// 卡认证接口
        /// </summary>
        /// <returns>1关闭正确  非1关闭失败</returns>
        [DllImport(@"DLL\Routon\sdtapi.dll", EntryPoint = "Authenticate",
            CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern Int32 Authenticate();

        /// <summary>
        /// 读卡信息接口
        /// </summary>
        /// <returns>1读取正确  非1读取失败</returns>
        [DllImport(@"DLL\Routon\sdtapi.dll", EntryPoint = "ReadBaseMsg",
            CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern Int32 ReadBaseMsg(StringBuilder pMsg, ref int len);
        /// <summary>
        /// 读卡信息接口
        /// </summary>
        /// <returns>1读取正确  非1读取失败</returns>
        [DllImport(@"DLL\Routon\sdtapi.dll", EntryPoint = "ReadBaseInfos",
            CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public static extern Int32 ReadBaseInfos(byte[] Name, byte[] Gender, byte[] Folk, byte[] BirthDay,
            byte[] Code, byte[] Address, byte[] Agency, byte[] ExpireStart, byte[] ExpireEnd);
        #endregion

        int iPort = 0;
        public IDCardRead_RoutonIDR210(int IPort)
        {
            iPort = IPort;
        }
        public PersonInfo ReadPersonInfo()
        {
            PersonInfo p = new PersonInfo();
            try
            {
                bool success = InitComm(iPort) != 1;
                if (success)
                {
                    throw new Exception("精伦电子:读卡器初始化失败!");
                }

                success = Authenticate() != 1;
                if (success)
                {
                    throw new Exception("精伦电子:读卡器未检测到身份证或者拿开重新放置.");
                }
                byte[] Name = new byte[31];//姓名
                byte[] Gender = new byte[3];//性别
                byte[] Folk = new byte[10];//民族
                byte[] BirthDay = new byte[9];//生日
                byte[] Code = new byte[19];//代码
                byte[] Address = new byte[71];//地址
                byte[] Agency = new byte[31];//签发机关
                byte[] ExpireStart = new byte[9];//起始日期
                byte[] ExpireEnd = new byte[9];//截止日期
                //int length = 0;
                success = ReadBaseInfos(Name, Gender, Folk, BirthDay, Code, Address, Agency, ExpireStart, ExpireEnd) != 1;
                if (success)
                {
                    throw new Exception("精伦电子:读卡器读取身份证信息失败!");
                }
                else
                {
                    var name = System.Text.Encoding.Default.GetString(Name);
                    p.Name = name.Trim();

                    var gender = System.Text.Encoding.Default.GetString(Gender);
                    p.Sex = gender;

                    var folk = System.Text.Encoding.Default.GetString(Folk);
                    p.Nation = folk;

                    var birthDay = System.Text.Encoding.Default.GetString(BirthDay);
                    p.Birthday = birthDay;

                    var code = System.Text.Encoding.Default.GetString(Code);
                    p.CardNO = code;

                    var address = System.Text.Encoding.Default.GetString(Address);
                    p.Address = address;

                    var agency = System.Text.Encoding.Default.GetString(Agency);
                    p.GrantDept = agency;

                    var expireStart = System.Text.Encoding.Default.GetString(ExpireStart);
                    p.IDCardBeginDate = expireStart;

                    var expireEnd = System.Text.Encoding.Default.GetString(ExpireEnd);
                    p.IDCardEndDate = expireEnd;

                    /*2017-03-08*/
                    var path = Path.GetDirectoryName(this.GetType().Assembly.Location) + @"\DLL\Routon\photo.bmp";
                    p.Photo = Image.FromFile(path);
                }

                success = CloseComm() != 1;
                if (success)
                {
                    throw new Exception("精伦电子:读卡器关闭失败!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return p;
        }
    }
}
