// ===============================================================================
// Copyright (c) 2007-2013 西安交通信息投资营运有限公司。 
// ===============================================================================

using System.ComponentModel;
using System.ComponentModel.Design;
using DevExpress.Utils.Design;

namespace XCI.Controls.Design
{
    /// <summary>
    /// 控件设计时行为自定义
    /// </summary>
    public class ComboBoxEditXDesignerActionList : DesignerActionList
    {
        private readonly ComboBoxEditXDesigner controldesigner;
        private DesignerActionItemCollection items;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="designer"></param>
        public ComboBoxEditXDesignerActionList(ComboBoxEditXDesigner designer)
            : base(designer.Component)
        {
            this.controldesigner = designer;
        }

        private ComboBoxEditX Editor { get { return controldesigner.Component as ComboBoxEditX; } }

        private void SetPropertyValue(string name, object value)
        {
            PropertyDescriptor propDesc = TypeDescriptor.GetProperties(controldesigner.Component)[name];
            propDesc.SetValue(controldesigner.Component, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return Editor.Name; }
            set { SetPropertyValue("Name", value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayMember
        {
            get { return Editor.DisplayMember; }
            set { SetPropertyValue("DisplayMember", value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ValueMember
        {
            get { return Editor.ValueMember; }
            set { SetPropertyValue("ValueMember", value); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            if (items == null)
            {
                items = new DesignerActionItemCollection();

                items.Add(new DesignerActionPropertyItem("Name", "Name"));
                items.Add(new DesignerActionHeaderItem("数据绑定"));
                items.Add(new DesignerActionPropertyItem("ValueMember", "值成员", "数据绑定"));
                items.Add(new DesignerActionPropertyItem("DisplayMember", "显示成员", "数据绑定"));

                items.Add(new DesignerActionMethodItem(this, "EditItems", "编辑项...", "编辑"));
                items.Add(new DesignerActionMethodItem(this, "EditButtons", "编辑按钮...", "编辑"));
                if (Editor != null)
                {
                    items.Add(new DesignerActionMethodItem(this, "EditMask", Editor.Properties.Mask.MaskType == DevExpress.XtraEditors.Mask.MaskType.None ? "Edit mask" : "Change mask", "Mask"));
                }
            }
            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        public void EditItems()
        {
            EditorContextHelper.EditValue(controldesigner, Editor.Properties, "Items");
        }

        /// <summary>
        /// 
        /// </summary>
        public void EditButtons()
        {
            EditorContextHelper.EditValue(controldesigner, Editor.Properties, "Buttons");
        }

        /// <summary>
        /// 
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        public void EditMask()
        {
            EditorContextHelper.EditValue(controldesigner, Editor.Properties, "Mask");
            EditorContextHelperEx.RefreshSmartPanel(Component);
        }
    }
}