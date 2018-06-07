using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IDCardRead.ChinaVison
{
    /// <summary>
    /// 华视电子读卡器操作类(CVR100U)
    /// </summary>
    public class IDCardRead_CVR100U:IReadIDCard
    {
        int iRetUSB = 0, iRetCOM = 0;
        int iPort;
        public IDCardRead_CVR100U(int IPort)
        {
            iPort = IPort;
        }
        /// <summary>
        /// 初始化端口
        /// </summary>
        /// <param name="IPort"></param>
        private void InitComm(int IPort)
        {
            try
            {
                if (iPort >= 1001)
                {
                    iRetUSB = CVRSDK.CVR_InitComm(IPort);
                }
                else
                {
                    if (iPort > 0)
                    {
                        iRetCOM = CVRSDK.CVR_InitComm(IPort);
                    }
                    else
                    {
                        throw new Exception("无效的端口号:" + IPort);
                    }
                }
                if ((iRetCOM == 1) || (iRetUSB == 1))
                {
                    iPort = IPort;
                }
                else
                {
                    throw new Exception("初始化失败!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PersonInfo ReadPersonInfo()
        {
            var p = new PersonInfo();
            try
            {
                InitComm(iPort);
                
                int authenticate = CVRSDK.CVR_Authenticate();
                if (authenticate == 1)
                {
                    int readContent = CVRSDK.CVR_Read_Content(4);
                    if (readContent == 1)
                    {
                        p = FillData();
                        Close();
                    }
                    else
                    {
                        throw new Exception("读卡操作失败");
                    }
                }
                else
                {
                    throw new Exception("未放卡或者请拿开卡重新放置!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("华视电子读卡器异常\r" + ex.Message, ex);
            }
            return p;
        }
        private PersonInfo FillData()
        {
            PersonInfo p = new PersonInfo();
            try
            {
                //pictureBox1.ImageLocation = Application.StartupPath + "\\zp.bmp";
                byte[] name = new byte[30];
                int length = 30;
                CVRSDK.GetPeopleName(ref name[0], ref length);
                //MessageBox.Show();
                byte[] number = new byte[30];
                length = 36;
                CVRSDK.GetPeopleIDCode(ref number[0], ref length);
                byte[] people = new byte[30];
                length = 3;
                CVRSDK.GetPeopleNation(ref people[0], ref length);
                byte[] validtermOfStart = new byte[30];
                length = 16;
                CVRSDK.GetStartDate(ref validtermOfStart[0], ref length);
                byte[] birthday = new byte[30];
                length = 16;
                CVRSDK.GetPeopleBirthday(ref birthday[0], ref length);
                byte[] address = new byte[30];
                length = 70;
                CVRSDK.GetPeopleAddress(ref address[0], ref length);
                byte[] validtermOfEnd = new byte[30];
                length = 16;
                CVRSDK.GetEndDate(ref validtermOfEnd[0], ref length);
                byte[] signdate = new byte[30];
                length = 30;
                CVRSDK.GetDepartment(ref signdate[0], ref length);
                byte[] sex = new byte[30];
                length = 3;
                CVRSDK.GetPeopleSex(ref sex[0], ref length);

                byte[] samid = new byte[32];
                CVRSDK.CVR_GetSAMID(ref samid[0]);

                p.Address = System.Text.Encoding.GetEncoding("GB2312").GetString(address).Replace("\0", "").Trim();
                p.Sex = System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
                p.Birthday = System.Text.Encoding.GetEncoding("GB2312").GetString(birthday).Replace("\0", "").Trim();
                p.CardNO = System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
                p.Name = System.Text.Encoding.GetEncoding("GB2312").GetString(name).Replace("\0", "").Trim();
                p.GrantDept = System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();
                p.Nation = System.Text.Encoding.GetEncoding("GB2312").GetString(people).Replace("\0", "").Trim();
                p.IDCardBeginDate = System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim();
                p.IDCardEndDate = System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();

                var index = this.GetType().Assembly.Location.LastIndexOf('\\');
                var photoPath = this.GetType().Assembly.Location.Substring(0, index) + @"\DLL\ChinaVison\zp.bmp";
                p.Photo = Image.FromFile(photoPath);
                //lblAddress.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(address).Replace("\0", "").Trim();
                //lblSex.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
                //lblBirthday.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(birthday).Replace("\0", "").Trim();
                //lblDept.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();
                //lblIdCard.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
                //lblName.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(name).Replace("\0", "").Trim();
                //lblNation.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(people).Replace("\0", "").Trim();
                //label11.Text = "安全模块号：" + System.Text.Encoding.GetEncoding("GB2312").GetString(samid).Replace("\0", "").Trim();
                //lblValidDate.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim() + "-" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return p;
        }

        bool isClose = false;
        private void Close()
        {
            isClose = true;
            try
            {
                int isSuccess = CVRSDK.CVR_CloseComm();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Size = 16, CharSet = CharSet.Ansi)]
    public struct IDCARD_ALL
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        public char name;     //姓名
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public char sex;      //性别
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public char people;    //民族，护照识别时此项为空
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public char birthday;   //出生日期
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 70)]
        public char address;  //地址，在识别护照时导出的是国籍简码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
        public char number;  //地址，在识别护照时导出的是国籍简码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        public char signdate;   //签发日期，在识别护照时导出的是有效期至 
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public char validtermOfStart;  //有效起始日期，在识别护照时为空
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public char validtermOfEnd;  //有效截止日期，在识别护照时为空
    }
}
