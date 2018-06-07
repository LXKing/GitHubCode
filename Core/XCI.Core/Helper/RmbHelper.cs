using System;

namespace XCI.Helper
{
    /// <summary> 
    /// ����ҵĴ�Сд���� 
    /// </summary> 
    public static class RmbHelper
    {
        /// <summary> 
        /// ת�������Ϊ��д��ʽ
        /// </summary> 
        /// <param name="num">���</param> 
        /// <returns>���ش�д��ʽ</returns> 
        public static string ConvertRmbToUpper(decimal num)
        {
            const string str1 = "��Ҽ��������½��ƾ�";
            string str2 = "��Ǫ��ʰ��Ǫ��ʰ��Ǫ��ʰԪ�Ƿ�"; //����λ����Ӧ�ĺ��� 
            //��ԭnumֵ��ȡ����ֵ 
            //���ֵ��ַ�����ʽ 
            string str5 = "";  //����Ҵ�д�����ʽ 
            //ѭ������ 
            string ch2 = "";    //����λ�ĺ��ֶ��� 
            int nzero = 0;  //����������������ֵ�Ǽ��� 
            //int temp;            //��ԭnumֵ��ȡ����ֵ 

            num = Math.Round(Math.Abs(num), 2);    //��numȡ����ֵ����������ȡ2λС�� 
            string str4= ((long)(num * 100)).ToString();        //��num��100��ת�����ַ�����ʽ 
            int j = str4.Length;
            if (j > 15) { return "���"; }
            str2 = str2.Substring(15 - j);   //ȡ����Ӧλ����str2��ֵ���磺200.55,jΪ5����str2=��ʰԪ�Ƿ� 

            //ѭ��ȡ��ÿһλ��Ҫת����ֵ 
            int i;
            for (i = 0; i < j; i++)
            {
                string str3 = str4.Substring(i, 1);          //ȡ����ת����ĳһλ��ֵ 
                int temp = Convert.ToInt32(str3);      //ת��Ϊ���� 
                string ch1;    //���ֵĺ������ 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //����ȡλ����ΪԪ�����ڡ������ϵ�����ʱ 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "��" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //��λ�����ڣ��ڣ���Ԫλ�ȹؼ�λ 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "��" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //�����λ����λ��Ԫλ�������д�� 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 2 && str3 == "0")
                {
                    //���2λ���Ƿ֣�Ϊ0ʱ�����ϡ����� 
                    str5 = str5 + '��';
                }
            }
            if (num == 0)
            {
                str5 = "��Ԫ��";
            }
            return str5;
        }

        /// <summary> 
        /// ת�������Ϊ��д��ʽ
        /// </summary> 
        /// <param name="numstring">���</param> 
        /// <returns>���ش�д��ʽ</returns> 
        public static string ConvertRmbToUpper(string numstring)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstring);
                return ConvertRmbToUpper(num);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    } 

}
