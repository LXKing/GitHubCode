using IDCardRead;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IDCardRead.Synjones
{
    public class IDCardRead_Syn : IReadIDCard
    {
        #region 端口类API
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_GetCOMBaud", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetCOMBaud(int iComID, ref uint puiBaud);
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_SetCOMBaud", CharSet = CharSet.Ansi)]
        public static extern int Syn_SetCOMBaud(int iComID, uint uiCurrBaud, uint uiSetBaud);
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_OpenPort", CharSet = CharSet.Ansi)]
        public static extern int Syn_OpenPort(int iPortID);
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_ClosePort", CharSet = CharSet.Ansi)]
        public static extern int Syn_ClosePort(int iPortID);
        #endregion

        #region SAM类API
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_GetSAMStatus", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetSAMStatus(int iPortID, int iIfOpen);
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_ResetSAM", CharSet = CharSet.Ansi)]
        public static extern int Syn_ResetSAM(int iPortID, int iIfOpen);
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_GetSAMID", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetSAMID(int iPortID, ref byte pucSAMID, int iIfOpen);
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_GetSAMIDToStr", CharSet = CharSet.Ansi)]
        public static extern int Syn_GetSAMIDToStr(int iPortID, ref byte pcSAMID, int iIfOpen);
        #endregion

        #region 身份证卡类API
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_StartFindIDCard", CharSet = CharSet.Ansi)]
        public static extern int Syn_StartFindIDCard(int iPortID, ref byte pucManaInfo, int iIfOpen);
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_SelectIDCard", CharSet = CharSet.Ansi)]
        public static extern int Syn_SelectIDCard(int iPortID, ref byte pucManaMsg, int iIfOpen);
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_ReadMsg", CharSet = CharSet.Ansi)]
        public static extern int Syn_ReadMsg(int iPortID, int iIfOpen, ref IDCardData pIDCardData);
        /********************附加类API *****************************/
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_SendSound", CharSet = CharSet.Ansi)]
        public static extern int Syn_SendSound(int iCmdNo);
        [DllImport(@"IDCardRead\Synjones\Syn_IDCardRead.dll", EntryPoint = "Syn_DelPhotoFile", CharSet = CharSet.Ansi)]
        public static extern void Syn_DelPhotoFile();
        #endregion

        int iPort;
        public IDCardRead_Syn(int iport)
        {
            iPort = iport;
            //var r = InitIDCardRead();
            //if(!r.Success)
            //    throw new Exception("新中新电子读卡器:\n"+r.Message);
        }
        public PersonInfo ReadPersonInfo()
        {
            PersonInfo p = new PersonInfo();
            try
            {
                //String sText;
                byte[] pucIIN = new byte[4];
                byte[] pucSN = new byte[8];
                IDCardData CardMsg = new IDCardData();
                var nRet = ConvertResultToMessage(Syn_OpenPort(iPort));
                if (nRet.Success)
                {
                    //判断状态
                    nRet = ConvertResultToMessage(Syn_GetSAMStatus(iPort, 0));
                    if (!nRet.Success)
                    {
                        throw new Exception(nRet.Message);
                    }

                    //寻找卡
                    nRet = ConvertResultToMessage(Syn_StartFindIDCard(iPort, ref pucIIN[0], 0));
                    if (!nRet.Success)
                    {
                        throw new Exception(nRet.Message);
                    }

                    //选择卡
                    nRet = ConvertResultToMessage(Syn_SelectIDCard(iPort, ref pucSN[0], 0));
                    if (!nRet.Success)
                    {
                        throw new Exception(nRet.Message);
                    }

                    nRet = ConvertResultToMessage(Syn_ReadMsg(iPort, 0, ref CardMsg));

                    if (nRet.Success)
                    {
                        p.Name = CardMsg.Name.Trim();
                        p.Sex = CardMsg.Sex;
                        p.CardNO = CardMsg.IDCardNo;
                        p.Birthday = CardMsg.Born;
                        p.Address = CardMsg.Address;
                        p.Nation = CardMsg.Nation;
                        p.GrantDept = CardMsg.GrantDept;
                        p.IDCardBeginDate = CardMsg.UserLifeBegin;
                        p.IDCardEndDate = CardMsg.UserLifeEnd;
                        p.Photo = Image.FromFile(CardMsg.PhotoFileName); 

                        #region Old
                        //listBox1.Items.Add("姓名：" + CardMsg.Name);
                        //listBox1.Items.Add("性别：" + CardMsg.Sex);
                        //listBox1.Items.Add("民族：" + CardMsg.Nation);
                        //listBox1.Items.Add("出生日期：" + CardMsg.Born);
                        //listBox1.Items.Add("住址：" + CardMsg.Address);
                        //listBox1.Items.Add("身份证号：" + CardMsg.IDCardNo);
                        //listBox1.Items.Add("发证机关：" + CardMsg.GrantDept);
                        //listBox1.Items.Add("有效期：" + CardMsg.UserLifeBegin + "-" + CardMsg.UserLifeEnd);
                        //listBox1.Items.Add("照片文件名：" + CardMsg.PhotoFileName);
                        //pictureBox1.Image = Image.FromFile(CardMsg.PhotoFileName); 
                        #endregion
                    }
                    else
                    {
                        throw new Exception(nRet.Message);
                    }
                }
                else
                {
                    throw new Exception(nRet.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("新中新电子读卡器:\n" + ex.Message, ex);
            }
            finally
            {
                var nRet = ConvertResultToMessage(Syn_ClosePort(iPort));
                if (!nRet.Success)
                {
                    throw new Exception(nRet.Message);
                }
            }
            return p;
        }
        internal ResultInfo<string> ConvertResultToMessage(int result)
        {
            var r = new ResultInfo<string>() { Success = true, Message = "", Data = "", Code = result };
            switch (result)
            {

                case 0:
                    r.Success = true;
                    r.Data = r.Message = "作成功或相片解码解码正确!";
                    break;
                case -1:
                    r.Success = false;
                    r.Data = r.Message = "端口打开失败/端口尚未打开/端口号不合法!";
                    break;
                case -2:
                    r.Success = false;
                    r.Data = r.Message = "证/卡中此项无内容!";
                    break;
                case -3:
                    r.Success = false;
                    r.Data = r.Message = "PC接收超时，在规定的时间内未接收到指定长度的数据!";
                    break;
                case -4:
                    r.Success = false;
                    r.Data = r.Message = "数据传输错误!";
                    break;
                case -5:
                    r.Success = false;
                    r.Data = r.Message = "该SAM_V串口不可用,只在SDT_GetComBaud时才有可能返回!";
                    break;
                case -6:
                    r.Success = false;
                    r.Data = r.Message = "接收业务终端数据的校验和错!";
                    break;
                case -7:
                    r.Success = false;
                    r.Data = r.Message = "接收业务终端数据的长度错!";
                    break;
                case -8:
                    r.Success = false;
                    r.Data = r.Message = "收业务终端的命令错误，包括命令中的各种数值或逻辑搭配错误!";
                    break;
                case -9:
                    r.Success = false;
                    r.Data = r.Message = "越权操作!";
                    break;
                case -10:
                    r.Success = false;
                    r.Data = r.Message = "无法是别的错误!";
                    break;
                case -11:
                    r.Success = false;
                    r.Data = r.Message = "寻找证/卡失败!";
                    break;
                case -12:
                    r.Success = false;
                    r.Data = r.Message = "选取证/卡失败!";
                    break;
                case -13:
                    r.Success = false;
                    r.Data = r.Message = "调用sdtapi.dll错误!";
                    break;
                case -14:
                    r.Success = false;
                    r.Data = r.Message = "相片解码错误!";
                    break;
                case -15:
                    r.Success = false;
                    r.Data = r.Message = "授权文件不存在!";
                    break;
                case -16:
                    r.Success = false;
                    r.Data = r.Message = "设备连接错误!";
                    break;
                default:
                    break;
            }
            return r;
        }
        
        public CheckResult InitIDCardRead()
        {
            CheckResult result = new CheckResult() { Success = true, Message = "" };
            iPort = 0;
            for (iPort = 1001; iPort < 1017; iPort++)
            {
                if (Syn_OpenPort(iPort) == 0)
                {
                    if (Syn_GetSAMStatus(iPort, 0) == 0)
                    {
                        Syn_ClosePort(iPort);
                        result.Message = "读卡器连接在" + iPort + "USB端口上";
                        return result;
                    }
                }
                Syn_ClosePort(iPort);
            }
            for (iPort = 1; iPort < 17; iPort++)
            {
                if (Syn_OpenPort(iPort) == 0)
                {
                    if (Syn_GetSAMStatus(iPort, 0) == 0)
                    {
                        Syn_ClosePort(iPort);
                        result.Message = "读卡器连接在串口" + iPort + "上";
                        return result;
                    }
                }
                Syn_ClosePort(iPort);
            }
            result.Success = false;
            result.Message = "没有连接读卡器";
            return result;
        }

    }
    public class CheckResult
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
    }
}