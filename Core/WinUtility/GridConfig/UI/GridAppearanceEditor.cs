using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DevExpress.Utils;
using XCI.Component;
using XCI.Core;
using XCI.Extension;
using XCI.Helper;

namespace XCI.WinUtility.GridConfig
{
    public partial class GridAppearanceEditor : UserControlBase
    {
        /// <summary>
        /// 项目汉化字典
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected static Dictionary<string, string> ItemDescriptionDic { get; set; }

        /// <summary>
        /// 外观对象类型
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected object AppearanceObject { get; set; }

        protected Type AppearanceType { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GridAppearanceEditor()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appearanceObject">外观对象</param>
        public void Initialize(object appearanceObject)
        {
            this.AppearanceObject = appearanceObject;
            this.AppearanceType = appearanceObject.GetType();
            InitItemDescriptionDic();

            xciGridControl1.DataSource = GetAppearanceData();
        }

        /// <summary>
        /// 初始化本地字典
        /// </summary>
        void InitItemDescriptionDic()
        {
            if (ItemDescriptionDic == null)
            {
                ItemDescriptionDic = new Dictionary<string, string>();
                ItemDescriptionDic
                    .AddEx("HeaderPanel", "页眉面板").AddEx("GroupPanel", "分组面板").AddEx("FooterPanel", "页脚面板")
                    .AddEx("Row", "数据行").AddEx("TopNewRow", "新建行").AddEx("RowSeparator", "行分割")
                    .AddEx("GroupRow", "分组行").AddEx("EvenRow", "偶数行").AddEx("OddRow", "奇数行")
                    .AddEx("HorzLine", "水平线").AddEx("VertLine", "垂直线").AddEx("Preview", "预览")
                    .AddEx("FocusedRow", "焦点行").AddEx("FocusedCell", "焦点列").AddEx("GroupButton", "分组按钮")
                    .AddEx("DetailTip", "详细提示").AddEx("GroupFooter", "分组页脚面板").AddEx("Empty", "空白区")
                    .AddEx("SelectedRow", "选中行").AddEx("HideSelectionRow", "失去焦点选中行").AddEx("ColumnFilterButton", "列过滤按钮")
                    .AddEx("ColumnFilterButtonActive", "列过滤按钮激活").AddEx("FixedLine", "固定线")
                    .AddEx("CustomizationFormHint", "自定义窗口提示").AddEx("FilterCloseButton", "查询关闭按钮")
                    .AddEx("FilterPanel", "查询面板").AddEx("TreeLine", "树线条").AddEx("ViewCaption", "视图标题");
            }
        }
        
        /// <summary>
        /// 获取外观类型数据
        /// </summary>
        protected virtual XCIList<XCIAppearanceObject> GetAppearanceData()
        {
            XCIList<XCIAppearanceObject> list = new XCIList<XCIAppearanceObject>();
            PropertyInfo[] pi = AppearanceType.GetProperties();
            string appearanceType = typeof(AppearanceObject).Name;
            foreach (PropertyInfo info in pi)
            {
                if (info.PropertyType.Name.Equals(appearanceType))
                {
                    XCIAppearanceObject obj = new XCIAppearanceObject();
                    string name = info.Name;
                    string des = name;
                    if (ItemDescriptionDic.ContainsKey(name))
                    {
                        des = ItemDescriptionDic[name];
                    }
                    obj.Name = name;
                    obj.Description = des;
                    obj.Spell = SpellHelper.GetStringSpell(des);
                    list.Add(obj);
                }
            }
            return list;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            XCIAppearanceObject obj = xciGridControl1.Get<XCIAppearanceObject>(e.FocusedRowHandle);
            if (obj != null)
            {
                object appearanceObject = AppearanceType.GetProperty(obj.Name).GetValue(AppearanceObject, null);
                xciPropertyGrid1.SetObject(appearanceObject);
                xciPropertyGrid1.Enabled = true;
            }
            else
            {
                xciPropertyGrid1.SetObject(null);
                xciPropertyGrid1.Enabled = false;
            }
        }

        private void editSearchBox_EditValueChanged(object sender, EventArgs e)
        {
            xciGridControl1.View.ApplyFindFilter(((XCIButtonEdit) sender).Text.Trim());
        }
    }

    /// <summary>
    /// 外观类型
    /// </summary>
    public class XCIAppearanceObject : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        public string Spell { get; set; }
    }
}
