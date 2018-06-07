// ===============================================================================
// Copyright (c) 2007-2013 西安交通信息投资营运有限公司。 
// ===============================================================================

using System.ComponentModel.Design;
using DevExpress.XtraEditors.Design;

namespace XCI.Controls.Design
{
    /// <summary>
    /// 
    /// </summary>
    public class ComboBoxEditXDesigner : ComboBoxEditDesigner
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        protected override void RegisterActionLists(DesignerActionListCollection list)
        {
            list.Add(new ComboBoxEditXDesignerActionList(this));
            //list.Add(new BaseEditActionList(this));
        }
    }
}