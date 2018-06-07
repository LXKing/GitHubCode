using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace XCI.Helper
{
    /// <summary>  
    /// 输入法切换帮助类
    /// </summary> 
    public static class ImeHelper
    {
        [DllImport("user32")]
        private static extern uint ActivateKeyboardLayout(uint hkl, uint Flags);
        //[DllImport("user32")]
        //private static extern uint LoadKeyboardLayout(string pwszKLID, uint Flags);
        //[DllImport("user32")]
        //private static extern uint GetKeyboardLayoutList(uint nBuff, uint[] List);

        private static Hashtable SystemImeTable;
        private const uint KLF_ACTIVATE = 1;

        /// <summary>
        /// 初始化输入法
        /// </summary>
        static ImeHelper()
        {
            if (SystemImeTable==null)
            {
                SystemImeTable = GetAllImes();
            }
        }

        /// <summary>
        /// 切换输入法 例如 ImeHelper.SwtichIme("中文 (简体) - 拼音加加")
        /// </summary>
        /// <param name="ImeName">输入法名称</param>
        public static void SwtichIme(string ImeName)
        {
            if (string.IsNullOrEmpty(ImeName))
            {
                return;
            }
            if (SystemImeTable.ContainsKey(ImeName))
            {
                uint id = Convert.ToUInt32(SystemImeTable[ImeName]);
                SetIme(id);
            }
        }


        /// <summary>
        /// 切换输入法
        /// </summary>
        /// <param name="ImeId">输入法ID</param>
        private static void SetIme(uint ImeId)
        {
            if (ImeId > 0)
            {
                ActivateKeyboardLayout(ImeId, KLF_ACTIVATE);
            }
        }
        

        /// <summary>
        /// 获取全部输入法名称
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetAllImes()
        {
            Hashtable table = new Hashtable();
            uint[] KbList = new uint[64];
            for (int j = 0; j < InputLanguage.InstalledInputLanguages.Count; j++)
            {
                InputLanguage lang = InputLanguage.InstalledInputLanguages[j];
                var imeName = lang.LayoutName;
                if (!string.IsNullOrEmpty(imeName))
                {
                    table.Add(imeName, KbList[j]);
                }
            }
            return table;
        }
    }

}
