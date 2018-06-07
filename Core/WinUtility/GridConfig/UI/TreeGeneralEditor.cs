using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraTreeList;
using XCI.Extension;

namespace XCI.WinUtility.GridConfig
{
    public partial class TreeGeneralEditor : UserControlBase
    {
        /// <summary>
        /// 目标表格
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public XCITreeGrid TargetGrid { get; set; }

        /// <summary>
        /// 项目汉化字典
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected static Dictionary<string, string> ItemDescriptionDic { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeGeneralEditor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="grid">目标表格</param>
        /// <param name="targetObject">目标对象</param>
        public void Initialize(XCITreeGrid grid, object targetObject)
        {
            this.TargetGrid = grid;
            FormHelper.BindCustomPropertyControlValue(this, targetObject);
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        private void GridGeneralEditor_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitItemDescriptionDic();
                FormHelper.AddEnum(boxTreeLineStyle, typeof(LineStyle), ItemDescriptionDic);
                FormHelper.AddEnum(boxShowButtonMode, typeof(ShowButtonModeEnum), ItemDescriptionDic);
                FormHelper.AddEnum(boxHorzScrollVisibility, typeof(ScrollVisibility), ItemDescriptionDic);
                FormHelper.AddEnum(boxVertScrollVisibility, typeof(ScrollVisibility), ItemDescriptionDic);
            }
        }

        /// <summary>
        /// 初始化本地字典
        /// </summary>
        public void InitItemDescriptionDic()
        {
            if (ItemDescriptionDic == null)
            {
                ItemDescriptionDic = new Dictionary<string, string>();
                ItemDescriptionDic.AddEx("None", "无").AddEx("RowFocus", "行焦点").AddEx("CellFocus", "单元格焦点");
                ItemDescriptionDic.AddEx("Default", "默认").AddEx("ShowAlways", "一直显示").AddEx("ShowForFocusedRow", "行焦点显示").AddEx("ShowForFocusedCell", "单元格焦点显示").AddEx("ShowOnlyInEditor", "仅激活编辑器显示");
                ItemDescriptionDic.AddEx("Never", "无").AddEx("Always", "一直显示").AddEx("Auto", "自动");
                ItemDescriptionDic.AddEx("MouseDown", "鼠标按下").AddEx("MouseUp", "鼠标起来").AddEx("Click", "单击").AddEx("MouseDownFocused", "鼠标按下并焦点");
            }
        }
    }
}
