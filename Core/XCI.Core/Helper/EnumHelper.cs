using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace XCI.Helper
{
    /// <summary>
    /// ö�ٲ���������
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// �ַ���תΪö��
        /// </summary>
        /// <typeparam name="T">ö������</typeparam>
        /// <param name="str">Ҫת�����ַ���</param>
        /// <returns>����ַ���Ϊ�� ����ö��Ĭ��ֵ</returns>
        public static T ToEnum<T>(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return default(T);
            }
            return (T)Enum.Parse(typeof(T), str);
        }

    }

}
