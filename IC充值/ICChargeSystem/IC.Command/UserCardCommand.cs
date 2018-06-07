using COMMON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace IC.Command
{
    
    /// <summary>
    /// (QUN_START)用户卡命令类
    /// </summary>
    public class UserCardCommand
    {
        public event Action<object, OperatingChangeEventArgs> OperationChange;
        private string _operatingTip = string.Empty;
        /// <summary>
        /// 操作提示信息
        /// </summary>
        public string OperatingTip
        {
            get { return _operatingTip; }
            set { _operatingTip = value; }
        }

        /// <summary>
        /// 内码(4个字节)
        /// </summary>
        private List<byte> InnerNO=new List<byte>();
        /// <summary>
        /// 读取内码(返回四个字节内码)
        /// </summary>
        /// <returns>返回字节数组</returns>
        private  List<byte> ReadInnerNO()
        {
            List<byte> result = new List<byte>();
            try
            {
                try
                {
                    var res=this.ReadData(0x20, 0x04);
                    if (res.Count == 7)
                        result = res.Skip(3).ToList();
                    else
                        return null;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
            return result;
        }
        /// <summary>
        /// 判断是否有卡,并验证(获取4个字节内码)
        /// </summary>
        /// <returns>是否有卡</returns>
        public  JudgeCardResult JudgeCard()
        {
            try
            {
                InnerNO = ReadInnerNO();

                if(InnerNO==null || InnerNO.Count!=4)
                {
                    return JudgeCardResult.HasBadCard;
                }
                else
                {
                    var key = MakeKey();
                    bool success = VerifySecretKey(key);
                    if (success)
                    {
                        if(InnerNO.ByteToHexString(" ")=="FF FF FF FF")
                        {
                            return JudgeCardResult.HasOKNewCard;
                        }
                        else
                        {
                            return JudgeCardResult.HasOKOldCard;
                        }
                    }
                        
                    else
                        return JudgeCardResult.HasBadCard;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 产生要修改或者验证的发送字节(默认产生验证指令)
        /// </summary>
        /// <param name="Function">0或者1,0代表产生验证指令,1代表产生修改的指令</param>
        /// <returns></returns>
        private List<byte> MakeKey(int Function = 0)
        {
            try
            {
                if (Function > 2 || Function < 0)
                {
                    throw new Exception("只能传参数0或1 !");
                }
                //JudgeCardResult result=JudgeCardResult.HasOKCard;
                //if(InnerNO==null || InnerNO.Count!=4)
                //{
                //    result = JudgeCard();//同时返回四位内码
                //}
                //if (result != JudgeCardResult.HasOKCard)
                //{
                //    throw new Exception(string.Format("Sorry,{0}", result));
                //}
                InnerNO = ReadInnerNO();
                List<byte> btList = new List<byte>();
                string str = InnerNO.ByteToHexString(" ");
                if (str == "FF FF FF FF")
                {
                    var key = new List<byte>();
                    #region MyRegion1
                    if (Function == 0)
                    {
                        key.Add(0XFF);
                        key.Add(0XA3);
                    }
                    if (Function == 1)
                    {
                        key.Add(0XFF);
                        key.Add(0XA4);
                    }

                    key.Add(0XFF);
                    key.Add(0XFF);
                    key.Add(0XFF); 
                    #endregion
                    return key;
                }
                else
                {
                    byte bt0, bt1, bt2, bt3;
                    uint Total, nTem;
                    byte bTmp;
                    bt0 = (byte)(Convert.ToUInt32(InnerNO[0]) + 0XAA);
                    bt1 = (byte)(Convert.ToUInt32(InnerNO[1]) + 0X5A);
                    bt2 = (byte)(Convert.ToUInt32(InnerNO[2]) + 0X55);
                    bt3 = (byte)(Convert.ToUInt32(InnerNO[3]) + 0XA5);

                    Total = (Convert.ToUInt32(bt0) << 24) + (Convert.ToUInt32(bt1) << 16) + (Convert.ToUInt32(bt2) << 8) + (bt3);

                    for (int i = 0; i < 11; i++)
                    {
                        nTem = Total & Convert.ToUInt32(0X80000000);
                        if (nTem == 0x80000000)
                        {
                            Total = (Total << 1) + 0X01;
                        }
                        else
                        {
                            Total = (Total << 1);
                        }
                    }
                    bt0 = (byte)(~(Total >> 24));//
                    bt1 = (byte)((Total >> 16) + 0X24);
                    bt2 = (byte)(~(Total >> 8));
                    bt3 = (byte)(Total + 0X42);

                    bTmp = bt0;
                    bt0 = bt3;
                    bt1 = (byte)((bt1 << 4) + (bt1 >> 4));
                    bt2 = (byte)((bt2 << 4) + (bt2 >> 4));
                    bt3 = bTmp;

                    Total = (Convert.ToUInt32(bt0) << 24) + (Convert.ToUInt32(bt1) << 16) + (Convert.ToUInt32(bt2) << 8) + Convert.ToUInt32(bt3);
                    for (int i = 0; i < 11; i++)
                    {
                        nTem = Total & 1;
                        if (nTem == 1)
                        {
                            Total = (Total >> 1) + Convert.ToUInt32(0X80000000);
                        }
                        else
                        {
                            Total = (Total >> 1);
                        }
                    }

                    bTmp = (byte)(Total >> 24);
                    bt1 = (byte)(Total >> 16);
                    bt2 = (byte)(Total >> 8);

                    bt0 = bt2;
                    bt2 = bTmp;
                    if (Function == 0)
                    {
                        btList.Add(0XFF);
                        btList.Add(0XA3);
                    }
                    if (Function == 1)
                    {
                        btList.Add(0XFF);
                        btList.Add(0XA4);
                    }
                    var tmpList = new List<byte>();
                    tmpList.Add(bt0);
                    tmpList.Add(bt1);
                    tmpList.Add(bt2);
                    btList.AddRange(tmpList);
                    return btList;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //public List<byte> MakeKey(List<byte> innerNO,int Function)
        //{
        //    try
        //    {
        //        if (innerNO == null || innerNO.Count != 4)
        //            throw new Exception("传入生成验证码的内码个数不对!");
        //        if (Function > 2 || Function < 0)
        //        {
        //            throw new Exception("只能传参数0或1 !");
        //        }
        //        List<byte> btList = new List<byte>();
        //        string str = innerNO.ByteToHexString(" ");
        //        if (str == "FF FF FF FF")
        //        {
        //            InnerNO.Clear();
        //            #region MyRegion1
        //            if (Function == 0)
        //            {
        //                InnerNO.Add(0XFF);
        //                InnerNO.Add(0XA3);
        //            }
        //            if (Function == 1)
        //            {
        //                InnerNO.Add(0XFF);
        //                InnerNO.Add(0XA4);
        //            }

        //            InnerNO.Add(0XFF);
        //            InnerNO.Add(0XFF);
        //            InnerNO.Add(0XFF);
        //            #endregion
        //            return InnerNO;
        //        }
        //        else
        //        {
        //            byte bt0, bt1, bt2, bt3;
        //            uint Total, nTem;
        //            byte bTmp;
        //            bt0 = (byte)(Convert.ToUInt32(innerNO[0]) + 0XAA);
        //            bt1 = (byte)(Convert.ToUInt32(innerNO[1]) + 0X5A);
        //            bt2 = (byte)(Convert.ToUInt32(innerNO[2]) + 0X55);
        //            bt3 = (byte)(Convert.ToUInt32(innerNO[3]) + 0XA5);

        //            Total = (Convert.ToUInt32(bt0) << 24) + (Convert.ToUInt32(bt1) << 16) + (Convert.ToUInt32(bt2) << 8) + (bt3);

        //            for (int i = 0; i < 11; i++)
        //            {
        //                nTem = Total & Convert.ToUInt32(0X80000000);
        //                if (nTem == 0x80000000)
        //                {
        //                    Total = (Total << 1) + 0X01;
        //                }
        //                else
        //                {
        //                    Total = (Total << 1);
        //                }
        //            }
        //            bt0 = (byte)(~(Total >> 24));//
        //            bt1 = (byte)((Total >> 16) + 0X24);
        //            bt2 = (byte)(~(Total >> 8));
        //            bt3 = (byte)(Total + 0X42);

        //            bTmp = bt0;
        //            bt0 = bt3;
        //            bt1 = (byte)((bt1 << 4) + (bt1 >> 4));
        //            bt2 = (byte)((bt2 << 4) + (bt2 >> 4));
        //            bt3 = bTmp;

        //            Total = (Convert.ToUInt32(bt0) << 24) + (Convert.ToUInt32(bt1) << 16) + (Convert.ToUInt32(bt2) << 8) + Convert.ToUInt32(bt3);
        //            for (int i = 0; i < 11; i++)
        //            {
        //                nTem = Total & 1;
        //                if (nTem == 1)
        //                {
        //                    Total = (Total >> 1) + Convert.ToUInt32(0X80000000);
        //                }
        //                else
        //                {
        //                    Total = (Total >> 1);
        //                }
        //            }

        //            bTmp = (byte)(Total >> 24);
        //            bt1 = (byte)(Total >> 16);
        //            bt2 = (byte)(Total >> 8);

        //            bt0 = bt2;
        //            bt2 = bTmp;
        //            if (Function == 0)
        //            {
        //                btList.Add(0XFF);
        //                btList.Add(0XA3);
        //            }
        //            if (Function == 1)
        //            {
        //                btList.Add(0XFF);
        //                btList.Add(0XA4);
        //            }
        //            for (int i = 0; i < 3; i++)
        //            {
        //                btList.Add(InnerNO[i]);
        //            }
        //            return btList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #region 修改秘钥
        /// <summary>
        /// 修改密码,参数为空时发送(0XFF,0XA4,0XFF,0XFF,0XFF)
        /// </summary>
        /// <param name="btList">修改密码指令(0XFF,0XA4,0XFF,0XFF,0XFF)</param>
        /// <returns></returns>
        private bool Modify(List<byte> btList=null)
        {
            try
            {
                using(var cmd=new Command1())
                {
                    if (btList == null)
                    {
                        btList = new List<byte>() { 0XFF, 0XA4, 0XFF, 0XFF, 0XFF };
                    }
                    if (btList.Count == 5)
                    {
                        var cmdStr = btList.Take(2).ByteToHexString(" ");
                        if (cmdStr != "FF A4")
                            throw new Exception("不是有效的修改密码指令!");
                        cmd.btWrite = btList;
                        //Thread.Sleep(btList.Count*100);
                        if (cmd.WriteData())
                        {
                            //Thread.Sleep(2 * 100);
                            if (cmd.ReadData(2))
                            {
                                if (cmd.btHexString == "FF A4")
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                                return false;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        throw new Exception("发送的修改指令字节数不符合要求!");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            } 
        }
        #endregion

        #region 验证秘钥
        /// <summary>
        /// 验证秘钥
        /// </summary>
        /// <param name="btList">5个字节验证命令</param>
        /// <returns></returns>
        private bool VerifySecretKey(List<byte> btList)
        {
            try
            {
                if (btList.Count != 5)
                    throw new Exception("验证字节数不正确!");
                if (btList.Take(2).ByteToHexString(" ") != "FF A3")
                    throw new Exception("验证指令不正确!");
                using(var cmd=new Command1())
                {
                    cmd.btWrite = btList;
                    //Thread.Sleep(btList.Count*100);
                    if (cmd.WriteData())
                    {
                        //Thread.Sleep(2 * 100);
                        if (cmd.ReadData(2))
                        {
                            if (cmd.btHexString == "FF A3")
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 读写地址
        /// <summary>
        /// 读数据("FF A1")
        /// </summary>
        /// <param name="beginAddress"></param>
        /// <param name="readByteLength"></param>
        /// <returns></returns>
        public List<byte> ReadData(int beginAddress, int readByteLength)
        {
            var listSend = new List<byte>();
            try
            {
                using (var cmd = new Command1())
                {
                    listSend.Add(0XFF);
                    listSend.Add(0XA1);
                    listSend.Add(beginAddress.ToByte());
                    listSend.Add(readByteLength.ToByte());
                    cmd.btWrite = listSend;
                    if (cmd.WriteData())
                    {
                        if (cmd.ReadData(3 + readByteLength))
                        {
                            return cmd.btRead;
                        }
                        else
                            throw new Exception("读取数据失败!");
                    }
                    else
                        throw new Exception("发送指令失败!");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 写数据("FF A2")
        /// </summary>
        /// <param name="beginAddress">起始地址</param>
        /// <param name="sendDataLength">写入的长度(要写的数据字节数)</param>
        /// <param name="btList">写入的数据</param>
        /// <returns></returns>
        private bool WriteData(int beginAddress,int sendDataLength, List<byte> btList)
        {
            var listSend = new List<byte>();
            try
            {
                if(sendDataLength!=btList.Count)
                {
                    throw new Exception("发送的长度指定长度不一致!");
                }
                listSend.Add(0XFF);
                listSend.Add(0XA2);
                listSend.Add(beginAddress.ToByte());
                listSend.Add(sendDataLength.ToByte());
                listSend.AddRange(btList);
                using (var cmd = new Command1())
                {
                    cmd.btWrite = listSend;
                    if (cmd.WriteData())
                    {
                        if (cmd.ReadData(2))
                        {
                            if (cmd.btHexString == "FF A2")
                                return true;
                            else
                                return false;
                        }
                        else
                            throw new Exception("读取数据失败!");
                    }
                    else
                        throw new Exception("发送指令失败!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断是否为新卡,并验证(true为新卡，否则为老卡)
        /// </summary>
        /// <returns></returns>
        private bool IsNewCard()
        {
            try
            {
                var cardType = JudgeCard();
                if (cardType == JudgeCardResult.HasBadCard)
                {
                    throw new Exception("请检查是否插好卡或者卡已损坏!");
                }
                else
                {
                    if (JudgeCardResult.HasOKOldCard == cardType)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据一个字符串生成新内码4个字节(通过区号和表号产生四个字节的不同内码)
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private List<byte> MakeInerNO(string strValue)
        {
            List<byte> btList = new List<byte>();
            string str = string.Empty;
            string strTemp = Md5.GetMd5Str16(strValue);
            for (int i = 0; i < 16; i += 2)
            {
                btList.Add(Convert.ToByte(strTemp.Substring(i, 2), 16));
            }
            List<byte> bt = new List<byte>();
            for (int i = 0; i < 4; i++)
            {
                bt.Add(btList[i]);
            }
            return bt;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        private bool Modify(string strValue)
        {
            var oldInnerCode = InnerNO;
            if (oldInnerCode.Count != 4)
            {
                return false;
            }
            var newInnerCode = MakeInerNO(strValue);
            if (newInnerCode.Count == 4)
            {
                var success = WriteData(0x20, 0x04, newInnerCode);
                if (success)
                {
                    var newKey = this.MakeKey(1);
                    success = Modify(newKey);
                    return success;
                }
                else
                {
                    success = WriteData(0x20, 0x04, oldInnerCode);
                    return success;
                }
            }
            else
                return false;
        }
        #endregion
        
        private void CauseOperationChange(string text,int percentage)
        {
            if(OperationChange!=null)
            {
                OperationChange.Invoke(new Object(), new OperatingChangeEventArgs(text, percentage));
            }
        }

        #region 电卡操作逻辑
        /// <summary>
        /// 初始化卡
        /// </summary>
        /// <returns></returns>
        public CardOperateResult ResetNewCard()
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                CauseOperationChange("开始重置新卡,请稍等......", 10);
                Thread.Sleep(200);
                CauseOperationChange("开始读取卡信息......", 20);
                var judegResult = this.JudgeCard();
                if (judegResult == JudgeCardResult.HasBadCard)
                {
                    result.Success = false;
                    result.Message = "请检查是否插好卡或者卡已损坏!";
                }
                else
                {
                    CauseOperationChange("验证卡信息成功......", 50);
                    Thread.Sleep(200);
                    #region 开始写入数据一
                    CauseOperationChange("开始写入数据一......", 60);
                    List<byte> btList1 = new List<byte>();
                    for (int i = 0; i < 48; i++)
                    {
                        btList1.Add(0XFF);
                    }
                    if (this.WriteData(0X20, 0X30, btList1))
                    {
                        #region 开始写入数据二
                        CauseOperationChange("开始写入数据二......", 70);
                        List<byte> btList2 = new List<byte>();
                        for (int i = 0; i < 53; i++)
                        {
                            btList2.Add(0XFF);
                        }
                        if (this.WriteData(0X7B, 0X35, btList2))
                        {
                            #region 开始写入数据三
                            CauseOperationChange("开始写入数据三......", 80);
                            List<byte> btList3 = new List<byte>();
                            for (int i = 0; i < 48; i++)
                            {
                                btList3.Add(0XFF);
                            }
                            if (this.WriteData(0XB0, 0X30, btList3))
                            {
                                #region 正在修改加密
                                CauseOperationChange("开始重新加密......", 80);
                                if (Modify())
                                {
                                    CauseOperationChange("加密成功......", 90);
                                    result.Success = true;
                                }
                                else
                                {
                                    result.Success = false;
                                    result.Message = "加密失败!";
                                }
                                #endregion
                            }
                            else
                            {
                                result.Success = false;
                                result.Message = "初始化写入数据三失败!";
                            }
                            #endregion
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "初始化写入数据二失败!";
                        }
                        #endregion
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "初始化写入数据一失败!";
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        
        /// <summary>
        /// 发行新卡
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="cardID"></param>
        /// <param name="systemParmValue"></param>
        /// <param name="checkCodeValue"></param>
        /// <returns></returns>
        public CardOperateResult PublishNewCard(int areaID, int cardID, int systemParmValue,int checkCodeValue)
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                CauseOperationChange("开始验证是否为新卡...",10);
                bool newcard = IsNewCard();
                if(newcard)
                {
                    CauseOperationChange("开始验证卡信息...", 20);
                    var key = MakeKey();
                    var success = VerifySecretKey(key);
                    if(success)
                    {
                        CauseOperationChange("验证卡信息成功...", 30);
                        CauseOperationChange("写入卡号和区号中...", 50);
                        #region 写入卡数据(区号，卡号)
                        List<byte> btList1 = new List<byte>();
                        btList1.AddRange(areaID.ToHexByte(4));
                        btList1.AddRange(cardID.ToHexByte(4));

                        success = this.WriteData(0x24, 8, btList1);
                        if(success)
                        {
                            CauseOperationChange("写入卡号和区号成功...", 60);
                            #region MyRegion
                            List<byte> btList2=new List<byte>();
                            btList2.AddRange(systemParmValue.ToHexByte(4));
                            for (int i = 0; i < 16;i++ )
                            {
                                btList2.Add(0x00);
                            }
                            btList2.AddRange(systemParmValue.ToHexByte(4));
                            btList2.AddRange(checkCodeValue.ToHexByte(4));
                            success = WriteData(0X30, 0X1C, btList2);
                            if(success)
                            {
                                CauseOperationChange("写入系统参数和稽查码成功...", 70);
                                #region 开始写入水表参数
                                CauseOperationChange("正在写入水表参数...", 80);
                                List<byte> btList3 = new List<byte>();
                                btList3.Add(0X03);
                                btList3.AddRange(DateTime.Now.Year.ToHexByte(2));
                                btList3.AddRange(DateTime.Now.Month.ToHexByte(1));
                                btList3.AddRange(DateTime.Now.Day.ToHexByte(1));
                                success = WriteData(0X7B, 0X05, btList3);
                                if(success)
                                {
                                    CauseOperationChange("写入水表参数成功...", 80);
                                    #region MyRegion
                                    CauseOperationChange("正在修改密码...", 90);
                                       success  = Modify(cardID.ToString());
                                    if(success)
                                    {
                                        result.Success = true;
                                        result.Message = "修改密码成功!";
                                        CauseOperationChange("新开卡成功!", 100);
                                    }
                                    else
                                    {
                                        result.Success = false;
                                        result.Message = "修改密码失败!";
                                    }
                                    #endregion
                                    
                                }
                                else
                                {
                                    result.Success = false;
                                    result.Message = "写入水表参数失败!";
                                }
                                #endregion
                            }
                            else
                            {
                                result.Success = false;
                                result.Message = "写入系统参数和稽查码失败!";
                            }
                            #endregion
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "写入卡号和区号失败!";
                        }
                        #endregion
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "验证卡信息失败!";
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "该卡为老卡，请先重置为新卡!";
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            finally
            {

            }
            return result;

        }
        /// <summary>
        /// 购电
        /// </summary>
        /// <param name="counts"></param>
        /// <param name="systemParmValue"></param>
        /// <param name="eleBuy"></param>
        /// <param name="limitPowerValue"></param>
        /// <param name="stopEleValue"></param>
        /// <param name="alarmEleValue"></param>
        /// <param name="checkCodeValue"></param>
        /// <returns></returns>
        public CardOperateResult ChargeEle(int counts, int systemParmValue, int eleBuy, int limitPowerValue, int stopEleValue, int alarmEleValue, int checkCodeValue)
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                CauseOperationChange("开始验证卡信息...", 10);
                var key = MakeKey();

                var success = VerifySecretKey(key);
                if(success)
                {
                    CauseOperationChange("验证成功...", 10);
                    var _btListSend = new List<byte>();

                    _btListSend.AddRange(counts.ToHexByte(4));//购买次数

                    _btListSend.AddRange(systemParmValue.ToHexByte(4));//用户卡参数

                    _btListSend.AddRange(eleBuy.ToHexByte(4));//购买电量

                    for (int i = 0; i < 8; i++)
                    {
                        _btListSend.Add(0X00);
                    }

                    _btListSend.AddRange(limitPowerValue.ToHexByte(1));//可限功率

                    _btListSend.Add(0X00);

                    _btListSend.AddRange(stopEleValue.ToHexByte(1));//断点电量

                    _btListSend.AddRange(alarmEleValue.ToHexByte(1));//报警电量

                    _btListSend.AddRange(systemParmValue.ToHexByte(4));//用户卡参数

                    _btListSend.AddRange(checkCodeValue.ToHexByte(4));//稽查编码
                    CauseOperationChange("开始写入数据...", 10);
                    if (WriteData(0X2C, 0X20, _btListSend))
                    {
                        result.Success = true;
                        result.Message="IC卡购电成功!";
                        CauseOperationChange("购电成功!", 10);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "IC卡购电失败!";
                        CauseOperationChange("购电失败!", 10);
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "IC卡验证失败!";
                }
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            finally
            {

            }
            return result;
        }
        /// <summary>
        /// 撤销费用
        /// </summary>
        /// <param name="areaCode"></param>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public CardOperateResult RevokedEle(int areaCode,int cardCode)
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                CauseOperationChange("正在读取卡信息...", 10);
                #region 判断卡
                var judege = JudgeCard();
                if (judege == JudgeCardResult.HasBadCard)
                {
                    result.Success = false;
                    result.Message = "请检查是否插好卡或者卡已损坏";
                    return result;
                } 
                #endregion

                #region 读取卡信息
                var readCardInfo = ReadEleCardInfo(areaCode);
                if (!readCardInfo.Success)
                {
                    result.Success = false;
                    result.Message = readCardInfo.Message;
                    return result;
                } 
                #endregion

                #region 校验区号
                CauseOperationChange("正在校验区号...", 30);
                var cardInfo = (EleCardInfo)readCardInfo.Data;
                if (cardInfo.CardCode != cardCode)
                {
                    result.Success = false;
                    result.Message = "卡号不对应，不能进行撤销!";
                    return result;
                } 
                #endregion

                #region 判断是否有电
                if (cardInfo.ChargeEleInCard == 0)
                {
                    result.Success = false;
                    result.Message = "卡中无费用，不能进行撤销费用";
                    return result;
                } 
                #endregion

                #region 修改购电次数
                CauseOperationChange("正在撤销费用...", 60);
                var btList = new List<byte>();
                btList.AddRange((cardInfo.ChargeCountsInCard - 1).ToHexByte(2));
                var success0 = WriteData(0x2e, 0x02, btList);

                if (!success0)
                {
                    result.Success = false;
                    result.Message = "撤销费用写入次数数据失败!";
                    return result;
                } 
                #endregion

                #region 修改购电量为0
                CauseOperationChange("正在撤销费用...", 80);
                var btList1 = new List<byte>();
                btList1.AddRange((0).ToHexByte(4));
                var success1 = WriteData(0x34, 0x04, btList1);
                if (!success1)
                {
                    result.Success = false;
                    result.Message = "撤销费用写入次数数据失败!";
                    return result;
                } 
                #endregion

                result.Success = true;
                result.Message = "撤销费用成功!";
                CauseOperationChange("撤销费用成功!", 100);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 补卡
        /// </summary>
        /// <param name="areaCode"></param>
        /// <param name="cardCode"></param>
        /// <param name="counts"></param>
        /// <param name="systemParmValue"></param>
        /// <param name="eleBuy"></param>
        /// <param name="limitPowerValue"></param>
        /// <param name="stopEleValue"></param>
        /// <param name="alarmEleValue"></param>
        /// <param name="checkCodeValue"></param>
        /// <returns></returns>
        public CardOperateResult ReMakeCardsEle(int areaCode,int cardCode,int counts, int systemParmValue, int eleBuy, int limitPowerValue, int stopEleValue, int alarmEleValue, int checkCodeValue)
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                var publicNewCardResult = PublishNewCard(areaCode, cardCode, systemParmValue, checkCodeValue);
                if(!publicNewCardResult.Success)
                {
                    result.Success = false;
                    result.Message = "初始化新卡数据失败!";
                    return result;
                }
                var chargeResult = ChargeEle(counts, systemParmValue, eleBuy, limitPowerValue, stopEleValue, alarmEleValue, checkCodeValue);
                if(!chargeResult.Success)
                {
                    result.Success = false;
                    result.Message = "写入电卡数据失败!";
                    return result;
                }
                result.Success = true;
                result.Message = "写入电卡数据成功!";
                CauseOperationChange("写入电卡数据成功!", 100);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 读取电卡信息
        /// </summary>
        /// <param name="AreaCode"></param>
        /// <returns></returns>
        public CardOperateResult ReadEleCardInfo(int AreaCode)
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                //EleCardInfo result = new EleCardInfo();
                CauseOperationChange("开始读取卡信息...",10);
                
                var innerNO = ReadInnerNO();
                if (innerNO != null && innerNO.Count==4)
                {
                    CauseOperationChange("读取卡信息成功...", 30);
                    Thread.Sleep(200);
                    CauseOperationChange("正在读取电卡信息...", 50);
                    var data = this.ReadData(0x20, 0x30);
                    if(data.Count==0x33)
                    {
                        CauseOperationChange("读取电卡信息成功，正在分析数据...", 80);
                        var innerCodeRead = data.Skip(3).Take(4).ByteToHexString(" ");
                        var areaCodeRead = data.Skip(7).Take(4).ByteToHexString(" ");
                        var areaCodeInput = AreaCode.ToHexByte(4).ByteToHexString(" ");
                        if (innerCodeRead=="FF FF FF FF")
                        {
                            result.Success = false;
                            result.Message = "该卡为新卡不能读取有效的数据!";
                        }
                        else
                        {
                            if (areaCodeRead != areaCodeInput)
                            {
                                result.Success = false;
                                result.Message = "该卡不属于该小区的有效卡!";
                            }
                            else
                            {
                                CauseOperationChange("分析数据完成.", 100);
                                EleCardInfo cardInfo = new EleCardInfo(data.Skip(3).ToList());
                                result.Success = true;
                                result.Data = cardInfo;
                            }
                        }
                        
                    }
                    else
                    {
                        result.Success = false;
                        CauseOperationChange("读取电卡信息失败", 0);
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "请检查是否是否已插卡！";
                    CauseOperationChange("请检查是否是否已插卡", 0);
                }

            }
            catch(Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        
        #region 电管理卡
        /// <summary>
        /// 单项清零卡
        /// </summary>
        /// <returns></returns>
        public CardOperateResult MakeSingleCleanCard()
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                CauseOperationChange("正在验证卡信息", 30);
                var judge = JudgeCard();
                Thread.Sleep(300);
                if(judge==JudgeCardResult.HasBadCard)
                {
                    CauseOperationChange("请检查是否插好卡或者卡已损坏!", 0);
                }
                else
                {
                    var btList=new List<byte>();
                    (12).ForEach<int>((x)=>{
                    btList.Add(0x00);
                    });
                    btList[2] = 0x01;
                    btList.Add(0x11);
                    btList.Add(0x22);
                    btList.Add(0x33);
                    btList.Add(0x44);
                    (4).ForEach<int>((x) =>
                    {
                        btList.Add(0x00);
                    });
                    CauseOperationChange("正在写入电表清零卡数据...", 70);
                    var success = WriteData(0x24,0x14,btList);
                    if(success)
                    {
                         success = Modify(System.DateTime.Now.ToString());
                         if (!success)
                         {
                             CauseOperationChange("电表清零卡发行修改密码失败!", 0);
                             result.Success = false;
                             result.Message = "电表清零卡发行修改密码失败";
                             return result;
                         }
                         CauseOperationChange("发行电表清零卡完成!", 100);
                         result.Success = true;
                         result.Message = "电表清零卡发行成功";
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "电表清零卡发行失败";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        #endregion
        #endregion
        #region 水卡操作
        /// <summary>
        /// 水表清零卡(管理卡)
        /// </summary>
        /// <returns></returns>
        public CardOperateResult MakeWaterCleanCard()
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                CauseOperationChange("正在验证卡信息...", 30);
                var judge = IsNewCard();
                if (!judge)
                {
                    result.Success = false;
                    result.Message = "请初始化为新卡再发行水表清零卡!";
                    return result;
                }
                else
                {
                    CauseOperationChange("正在写入数据，请稍等...", 60);
                    var btListSend0 = new List<byte>();
                    btListSend0.Add(0X01);
                    var success = this.WriteData(0x7B, 0x01, btListSend0);
                    if(success)
                    {
                        CauseOperationChange("正在修改密码，请稍等...", 90);
                        success = Modify(System.DateTime.Now.ToString());
                        if(!success)
                        {
                            result.Success = false;
                            result.Message = "发行水表清零卡密码修改失败!";
                            return result;
                        }
                        result.Success = true;
                        result.Message = "发行水表清零卡成功!";
                        CauseOperationChange("水表清零卡发行成功!", 100);
                    }
                    else
                    {
                        result.Success = true;
                        result.Message = "发行水表清零卡写入数据失败!";
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 水表分类卡(管理卡)
        /// </summary>
        /// <param name="meterNO">水表编号(1-6)</param>
        /// <returns></returns>
        public CardOperateResult MakeWaterClassifiedCard(int meterNO)
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                if (meterNO < 1 || meterNO > 6)
                {
                    result.Success = false;
                    result.Message = "分类卡表号必须为1-6之间的整数!";
                    return result;
                }
                CauseOperationChange("正在验证卡信息...", 30);
                var judge = IsNewCard();
                if (!judge)
                {
                    result.Success = false;
                    result.Message = "请初始化为新卡再发行水表分类卡!";
                    return result;
                }
                else
                {
                    CauseOperationChange("正在写入数据，请稍等...", 60);
                    var btListSend0 = new List<byte>();
                    for (int i = 0; i < 16; i++)
                    {
                        btListSend0.Add(0);
                    }
                    btListSend0[0] = meterNO.ToByte();
                    btListSend0[11] = 5;
                    var success = this.WriteData(0x70, 0x10, btListSend0);
                    if (success)
                    {
                        CauseOperationChange("正在修改密码，请稍等...", 90);
                        success = Modify(System.DateTime.Now.ToString());
                        if (!success)
                        {
                            result.Success = false;
                            result.Message = "发行水表分类卡密码修改失败!";
                            return result;
                        }
                        result.Success = true;
                        result.Message = "发行水表分类卡成功!";
                        CauseOperationChange("水表分类卡发行成功!", 100);
                    }
                    else
                    {
                        result.Success = true;
                        result.Message = "发行水表分类卡写入数据失败!";
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 发行时钟卡(管理卡)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public CardOperateResult MakeWaterClockCard(DateTime date)
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                if(date==null)
                {
                    result.Success = false;
                    result.Message = "发行水表时钟卡，需要一个不为空的时间参数!";
                    return result;
                }
                CauseOperationChange("正在验证卡信息...", 30);
                var judge = IsNewCard();
                if (!judge)
                {
                    result.Success = false;
                    result.Message = "请初始化为新卡再发行水表时钟卡!";
                    return result;
                }
                else
                {
                    CauseOperationChange("正在写入数据，请稍等...", 60);
                    var btListSend0 = new List<byte>();
                    for (int i = 0; i < 16; i++)
                    {
                        btListSend0.Add(0);
                    }
                    btListSend0[0] = (byte)Convert.ToInt32(date.Year.ToString().Substring(2, 2), 16);
                    btListSend0[1] = (byte)Convert.ToInt32(date.Month.ToString(), 16);
                    btListSend0[3] = (byte)Convert.ToInt32(date.Day.ToString(), 16);
                    btListSend0[4] = (byte)Convert.ToInt32(date.Hour.ToString(), 16);
                    btListSend0[5] = (byte)Convert.ToInt32(date.Minute.ToString(), 16);
                    btListSend0[6] = (byte)Convert.ToInt32(date.Second.ToString(), 16);
                    btListSend0[11] = 2;
                    var success = this.WriteData(0x70, 0x10, btListSend0);
                    if (success)
                    {
                        CauseOperationChange("正在修改密码，请稍等...", 90);
                        success = Modify(System.DateTime.Now.ToString());
                        if (!success)
                        {
                            result.Success = false;
                            result.Message = "发行水表时钟卡密码修改失败!";
                            return result;
                        }
                        result.Success = true;
                        result.Message = "发行水表时钟卡成功!";
                        CauseOperationChange("水表时钟卡发行成功!", 100);
                    }
                    else
                    {
                        result.Success = true;
                        result.Message = "发行水表时钟卡写入数据失败!";
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 发行磁解除卡
        /// </summary>
        /// <returns></returns>
        public CardOperateResult MakeWaterMagneticLiftingCard()
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                CauseOperationChange("正在验证卡信息...", 30);
                var judge = IsNewCard();
                if (!judge)
                {
                    result.Success = false;
                    result.Message = "请初始化为新卡再发行水表磁解除卡!";
                    return result;
                }
                else
                {
                    CauseOperationChange("正在写入数据，请稍等...", 60);
                    var btListSend0 = new List<byte>();
                    btListSend0.Add(0X07);
                    var success = this.WriteData(0x7B, 0x01, btListSend0);
                    if (success)
                    {
                        CauseOperationChange("正在修改密码，请稍等...", 90);
                        success = Modify(System.DateTime.Now.ToString());
                        if (!success)
                        {
                            result.Success = false;
                            result.Message = "发行水表磁解除卡密码修改失败!";
                            return result;
                        }
                        result.Success = true;
                        result.Message = "发行水表磁解除卡成功!";
                        CauseOperationChange("水表磁解除卡发行成功!", 100);
                    }
                    else
                    {
                        result.Success = true;
                        result.Message = "发行水表磁解除卡写入数据失败!";
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 读取水卡信息,返回一个对象的Data属性为WaterCardInfo类型的对象
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        public CardOperateResult ReadWaterCardInfo(int areaCode)
        {
            CardOperateResult result = new CardOperateResult();
            try
            {
                var isNewCard = IsNewCard();
                if (isNewCard)
                {
                    result.Success = false;
                    result.Message = "该卡为新卡，没有水表对应的数据!";
                    return result;
                }

                var cardBaseInfo =  ReadData(0x24, 0x08).Skip(3);
                var readAreaCode = cardBaseInfo.Take(4).ByteToHexString(" ");
                var readCardCodeHexString = cardBaseInfo.Skip(4).Take(4).ByteToHexString(" ");
                var cardCode = cardBaseInfo.Skip(4).Take(4).ToList().HexToInt();

                if(areaCode.ToHexByte(4).ByteToHexString(" ")!=readAreaCode)
                {
                    result.Success = false;
                    result.Message = "该卡不属于该区有效卡!";
                    return result;
                }
                //跳过指令和长度3个字节
                var waterData = ReadData(0x80, 0x60).Skip(3).ToList();
                result.Success = true;
                result.Data = new WaterCardInfo(waterData, areaCode, cardCode);
                
            }
            catch (Exception ex)
            {
                result.Success=false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 购水充值,返回成功的集合,Message返回消息
        /// </summary>
        /// <param name="waterCollection"></param>
        /// <returns></returns>
        public CardOperateResult ChargeWater(int areaCode, IEnumerable<WaterMeterInfoEntity> waterCollection)
        {
            CardOperateResult result = new CardOperateResult();
            StringBuilder strMsg = new StringBuilder();
            List<WaterMeterInfoEntity> successWaterEntityList = new List<WaterMeterInfoEntity>();
            try
            {
                if (waterCollection == null || waterCollection.Count() == 0)
                {
                    result.Success = false;
                    result.Message = "至少需要为一个水表购水充值!";
                    return result;
                }
                CauseOperationChange("正在验证卡信息...", 20);
                var isNewCard = IsNewCard();
                if (isNewCard)
                {
                    result.Success = false;
                    result.Message = "该卡为新卡，请先为卡开户,再进行购水充值!";
                    return result;
                }

                var cardBaseInfo = ReadData(0x24, 0x08).Skip(3);
                var readAreaCode = cardBaseInfo.Take(4).ByteToHexString(" ");

                if (areaCode.ToHexByte(4).ByteToHexString(" ") != readAreaCode)
                {
                    result.Success = false;
                    result.Message = "该卡不属于该区有效卡!";
                    return result;
                }


                CauseOperationChange("正在写入数据...", 60);
                List<List<byte>> btListCollection = new List<List<byte>>();
                //waterCollection.ForEach(x =>
                foreach (var x in waterCollection)
                {
                    if (x.MeterNO > 6 || x.MeterNO < 1)
                    {
                        strMsg.AppendLine(string.Format("{0}号表不在充值表号范围内;", x.MeterNO.ToString()));
                        break;
                    }
                    var btList = new List<byte>();
                    btList.AddRange(x.MeterNO.ToHexByte(1));
                    btList.AddRange(x.ChargeCountInCard.ToHexByte1(1));
                    btList.AddRange(((int)(x.ChargeWater * 10)).ToHexByte1(2));
                    for (int i = 0; i < 11; i++)
                    {
                        btList.Add(0X00);//写入十一个0x00
                    }
                    int btLast;
                    btLast = btList[0] ^ btList[1];
                    for (int i = 2; i < btList.Count - 1; i++)
                    {
                        btLast ^= btList[i];
                    }
                    btList.Add(btLast.ToByte());
                    btListCollection.Add(btList);
                }

                //btListCollection.ForEach(x =>
                foreach (var x in btListCollection)
                {
                    var beginAddress = 0x80 + (x[0] - 1) * 0x10;
                    var success = WriteData(beginAddress, x.Count, x);
                    if (success)
                    {
                        strMsg.AppendLine(string.Format("{0}号表充值成功;", x[0].ToInt().ToString()));
                        successWaterEntityList.Add(waterCollection.Where(x1 => x1.MeterNO == x[0]).FirstOrDefault());
                    }
                    else
                    {
                        strMsg.AppendLine(string.Format("{0}号表充值失败;", x[0].ToInt().ToString()));
                    }
                }
                result.Message = strMsg.ToString();
                result.Data = successWaterEntityList;
                CauseOperationChange("购水充值成功!", 100);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        /// <summary>
        /// 撤销费用
        /// </summary>
        /// <param name="areaCode"></param>
        /// <param name="cardCode"></param>
        /// <param name="waterCollection"></param>
        /// <returns></returns>
        public CardOperateResult RevokedWater(int areaCode, int cardCode, IEnumerable<WaterMeterInfoEntity> waterCollection)
        {
            CardOperateResult result = new CardOperateResult();
            StringBuilder strMsg = new StringBuilder();
            List<WaterMeterInfoEntity> successWaterEntityList = new List<WaterMeterInfoEntity>();
            try
            {

                if (waterCollection == null || waterCollection.Count() == 0)
                {
                    result.Success = false;
                    result.Message = "至少需要为一个水表撤销费用!";
                    return result;
                }

                CauseOperationChange("正在读取卡信息...", 10);
                #region 判断卡
                var judege = JudgeCard();
                if (judege == JudgeCardResult.HasBadCard)
                {
                    result.Success = false;
                    result.Message = "请检查是否插好卡或者卡已损坏";
                    return result;
                }
                #endregion

                #region 读取卡信息
                var readCardInfo = ReadEleCardInfo(areaCode);
                if (!readCardInfo.Success)
                {
                    result.Success = false;
                    result.Message = readCardInfo.Message;
                    return result;
                }
                #endregion

                #region 校验区号
                CauseOperationChange("正在校验区号...", 30);
                var cardInfo = (EleCardInfo)readCardInfo.Data;
                if (cardInfo.CardCode != cardCode)
                {
                    result.Success = false;
                    result.Message = "卡号不对应，不能进行撤销!";
                    return result;
                }
                #endregion

                #region 退费
                CauseOperationChange("正在写入数据...", 60);
                List<List<byte>> btListCollection = new List<List<byte>>();
                foreach(var x in waterCollection)
                {
                    if (x.MeterNO > 6 || x.MeterNO < 1)
                    {
                        strMsg.AppendLine(string.Format("{0}号表不在充值表号范围内;", x.MeterNO.ToString()));
                        break;
                    }
                    var btList = new List<byte>();
                    btList.AddRange(x.MeterNO.ToHexByte(1));
                    btList.AddRange(x.ChargeCountInCard.ToHexByte1(1));
                    btList.AddRange(((int)(x.ChargeWater * 10)).ToHexByte1(2));
                    for (int i = 0; i < 11; i++)
                    {
                        btList.Add(0X00);//写入十一个0x00
                    }
                    int btLast;
                    btLast = btList[0] ^ btList[1];
                    for (int i = 2; i < btList.Count - 1; i++)
                    {
                        btLast ^= btList[i];
                    }
                    btList.Add(btLast.ToByte());
                    btListCollection.Add(btList);
                }
                
                foreach(var x in btListCollection)
                {
                    var beginAddress = 0x80 + (x[0] - 1) * 0x10;
                    var success = WriteData(beginAddress, x.Count, x);
                    if (success)
                    {
                        strMsg.AppendLine(string.Format("{0}号表撤销费用成功;", x[0].ToInt().ToString()));
                        successWaterEntityList.Add(waterCollection.Where(x1 => x1.MeterNO == x[0]).FirstOrDefault());
                    }
                    else
                    {
                        strMsg.AppendLine(string.Format("{0}号表撤销失败;", x[0].ToInt().ToString()));
                    }
                }
                result.Message = strMsg.ToString();
                result.Data = successWaterEntityList;
                CauseOperationChange("购水撤销费用成功!", 100);
                #endregion
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.BindAllException(ex);
            }
            return result;
        }
        
        #endregion
    }
    public class CardOperateResult
    {
        private bool _Success=true;
        /// <summary>
        /// 表示执行是否成功(默认true)
        /// </summary>
        public bool Success
        {
            get { return _Success; }
            set { _Success = value; }
        }

        private int _Code;
        /// <summary>
        /// 执行返回代码
        /// </summary>
        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        private string _Message=string.Empty;
        /// <summary>
        /// 执行返回消息
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        private Object _Data=null;
        /// <summary>
        /// 执行返回结果
        /// </summary>
        public Object Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        private bool _HasException = false;
        /// <summary>
        /// 是否包含异常
        /// </summary>
        public bool HasException
        {
            get { return _HasException; }
            //set { _HasException = value; }
        }


        private IEnumerable<Exception> _ExceptionCollection=new List<Exception>();
        /// <summary>
        /// 异常集合
        /// </summary>
        public IEnumerable<Exception> ExceptionCollection
        {
            get { return _ExceptionCollection; }
            //set { _ExceptionCollection = value; }
        }
        /// <summary>
        /// 绑定所有异常
        /// </summary>
        /// <param name="ex">传入顶级异常</param>
        public void BindAllException(Exception ex)
        {
            var exceptionList = new List<Exception>();
            
            bool hasInnerException=false;
            var tmpException = ex;
            if(ex!=null)
            {
                this._Success = false;
                _HasException = true;
                exceptionList.Add(tmpException);
                hasInnerException = tmpException.InnerException!=null;
            }
            else
            {
                _HasException = false;
                return;
            }
            while(hasInnerException)
            {
                tmpException=tmpException.InnerException;
                exceptionList.Add(tmpException);
                hasInnerException = tmpException.InnerException != null;
            }
            _ExceptionCollection = exceptionList;
            if(string.IsNullOrEmpty(_Message))
            {
                _Message = ex.Message;
            }
        }
    }
    public enum JudgeCardResult
    {
        HasOKNewCard,
        HasOKOldCard,
        HasBadCard
    }
    public static class ByetCollectionEx
    {
        /// <summary>
        /// byte集合转成字符串，使用分隔符进行分割
        /// </summary>
        /// <param name="bt"></param>
        /// <param name="splitString">分割字符串</param>
        /// <returns></returns>
        public static string ByteToHexString(this IEnumerable<byte> bt, string splitString)
        {
            return string.Join(splitString, bt.Select(x => x.ToString("X2")));
        }
        /// <summary>
        /// 整形转成固定个数的byte(左补齐)
        /// </summary>
        /// <param name="ListCount"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static List<byte> ToHexByte(this int Value,int ListCount)
        {   
            try
            {
                List<byte> btList = new List<byte>();
                string str1 = string.Empty;
                if (Value < 0)
                {
                    str1 = "0";
                }
                else
                    str1 = Value.ToString();
                int i = ListCount * 2 - str1.Length;
                string str2 = string.Empty;
                for (int j = 0; j < i; j++)
                {
                    str2 += "0";
                }
                str1 = str2 + str1;
                for (int k = 0; k < ListCount; k++)
                {
                    btList.Add((byte)(Convert.ToInt32(str1.Substring(k * 2, 2), 16)));
                }
                return btList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// 整形转成固定个数的byte(左补齐)(20071017转成 0x20 0x07 0x10 0x17)
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="ListCount"></param>
        /// <returns></returns>
        public static List<byte> ToHexByte(this Int64 Value, int ListCount)
        {
            try
            {
                List<byte> btList = new List<byte>();
                string str1 = string.Empty;
                if (Value < 0)
                {
                    str1 = "0";
                }
                else
                    str1 = Value.ToString();
                int i = ListCount * 2 - str1.Length;
                string str2 = string.Empty;
                for (int j = 0; j < i; j++)
                {
                    str2 += "0";
                }
                str1 = str2 + str1;
                for (int k = 0; k < ListCount; k++)
                {
                    btList.Add((byte)(Convert.ToInt32(str1.Substring(k * 2, 2), 16)));
                }
                return btList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 整数转字节
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="ListCount"></param>
        /// <returns></returns>
        public static List<byte> ToHexByte1(this int Value, int ListCount)
        {
            List<byte> btList = new List<byte>();
            string str1 = Value.ToString("X2");

            if (str1.Length % 2 != 0)
            {
                str1 = "0" + str1;
            }
            for (int i = 0; i < str1.Length; i = i + 2)
            {
                btList.Add(Convert.ToByte("0X" + str1.Substring(i, 2), 16));
            }
            for (int i = 0; i < ListCount - btList.Count; i++)
            {
                btList.Insert(0, 0x00);
            }
            return btList;
        }
        /// <summary>
        /// 将一个整数转换为指定个数的List<byte>类型返回
        /// </summary>
        /// <param name="ListCount"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static List<byte> ToHexByte1(this Int64 Value, int ListCount)
        {
            List<byte> btList = new List<byte>();
            string str1 = Value.ToString("X2");

            if (str1.Length % 2 != 0)
            {
                str1 = "0" + str1;
            }
            for (int i = 0; i < str1.Length; i = i + 2)
            {
                btList.Add(Convert.ToByte("0X" + str1.Substring(i, 2), 16));
            }
            for (int i = 0; i < ListCount - btList.Count; i++)
            {
                btList.Insert(0, 0x00);
            }
            return btList;
        }
        /// <summary>
        /// 整形数字转成字符串，左补齐0
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="Count">总位数</param>
        /// <returns></returns>
        public static string IntToStr(this int Value, int Count)
        {
            string str1 = Value.ToString();
            int i = Count - str1.Length;
            string str2 = string.Empty;
            for (int j = 0; j < i; j++)
            {
                str2 += "0";
            }
            return str2 + str1;
        }
        /// <summary>
        /// 十六进制字节转换成十进制整数
        /// </summary>
        /// <param name="btList"></param>
        /// <returns></returns>
        public static int HexToInt(this List<byte> btList)
        {
            int d = 0;
            for (int i = 0; i < btList.Count; i++)
            {
                d += Convert.ToInt32(btList[i]) * Convert.ToInt32(Math.Pow(2.0, (btList.Count - i - 1) * 8.0));
            }
            return d;
        }
        /// <summary>
        /// 十六进制字节转换成十进制浮点
        /// </summary>
        /// <param name="btList"></param>
        /// <returns></returns>
        public static double HexToDouble(this List<byte> btList)
        {
            double d = 0.0;
            for (int i = 0; i < btList.Count; i++)
            {
                d += Convert.ToDouble(btList[i]) * Math.Pow(2.0, (btList.Count - i - 1) * 8.0);
            }
            return d;
        }
    }
    public class OperatingChangeEventArgs:EventArgs
        {
            public OperatingChangeEventArgs(string opeationMsg, int opeatiePercentage)
            {
                _opeationMsg = opeationMsg;
                _opeatiePercentage = opeatiePercentage;
            }

            /// <summary>
            /// 操作显示信息
            /// </summary>
            private string _opeationMsg = string.Empty;
            public string OpeationMsg
            {
                get
                {
                    return _opeationMsg;
                }
            }
            /// <summary>
            /// 操作进度百分比
            /// </summary>
            private int _opeatiePercentage=0;
            public int OpeatiePercentage
            {
                get
                {
                    return _opeatiePercentage;
                }
            }
        }
}
