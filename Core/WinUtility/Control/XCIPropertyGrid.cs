using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.Utils.Controls;
using DevExpress.XtraEditors;
using XCI.WinUtility.Properties;

namespace XCI.WinUtility
{
    public partial class XCIPropertyGrid : DevExpress.XtraEditors.XtraUserControl
    {
        #region 属性
        /// <summary>
        /// 显示描述
        /// </summary>
        [DefaultValue(true), Description("显示描述")]
        public bool ShowDescription
        {
            get { return biDescription.Down; }
            set
            {
                biDescription.Down = value;
            }
        }
        /// <summary>
        /// 显示分类
        /// </summary>
        [DefaultValue(true), Description("显示分类")]
        public bool ShowCategories
        {
            get { return bciCategories.Checked; }
            set
            {
                if (value)
                    bciCategories.Checked = true;
                else bciAlphabetical.Checked = true;
            }
        }
        /// <summary>
        /// 显示按钮
        /// </summary>
        [DefaultValue(true), Description("显示按钮")]
        public bool ShowButtons
        {
            get { return bMain.Visible; }
            set
            {
                bMain.Visible = pnlTop.Visible = value;
            }
        }

        /// <summary>
        /// 属性表格
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DevExpress.XtraVerticalGrid.PropertyGridControl PropertyGrid
        {
            get { return propertyGridControl1; }
        }

        #endregion

        /// <summary>
        /// 本地化字典
        /// </summary>
        public static Dictionary<string, LocalResource> LocalDic = new Dictionary<string, LocalResource>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public XCIPropertyGrid()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                biDescription.Down = true;
                bciCategories.Checked = true;
                barManager1.Images = XCI.Helper.ResourceImageHelper.CreateImageListFromResources(Resources.XCIPropertyGridHead, new Size(16, 16));
                InitLocalDic();
            }
        }

        /// <summary>
        /// 初始化本地字典
        /// </summary>
        private void InitLocalDic()
        {
            if (LocalDic.Count == 0)
            {
                LocalDic.Add("BackColor", new LocalResource("BackColor", "背景颜色", "背景颜色"));
                LocalDic.Add("BackColor2", new LocalResource("BackColor2", "背景渐变颜色", "背景渐变颜色"));
                LocalDic.Add("BorderColor", new LocalResource("BorderColor", "边框颜色", "边框颜色"));
                LocalDic.Add("Font", new LocalResource("Font", "字体", "字体"));
                LocalDic.Add("Bold", new LocalResource("Bold", "加粗", "加粗"));
                LocalDic.Add("GdiCharSet", new LocalResource("GdiCharSet", "字符集", "字符集"));
                LocalDic.Add("GdiVerticalFont", new LocalResource("GdiVerticalFont", "垂直字体", "垂直字体"));
                LocalDic.Add("Italic", new LocalResource("Italic", "斜体", "斜体"));
                LocalDic.Add("Name", new LocalResource("Name", "名称", "名称"));
                LocalDic.Add("Size", new LocalResource("Size", "大小", "大小"));
                LocalDic.Add("Strikeout", new LocalResource("Strikeout", "删除线", "删除线"));
                LocalDic.Add("Underline", new LocalResource("Underline", "下划线", "下划线"));
                LocalDic.Add("Unit", new LocalResource("Unit", "单位", "单位"));
                LocalDic.Add("ForeColor", new LocalResource("ForeColor", "字体颜色", "字体颜色"));
                LocalDic.Add("GradientMode", new LocalResource("GradientMode", "背景渐变模式", "背景渐变模式"));
                LocalDic.Add("Image", new LocalResource("Image", "背景图片", "背景图片"));
                LocalDic.Add("Options", new LocalResource("Options", "界面选项", "界面选项"));
                LocalDic.Add("UseBackColor", new LocalResource("UseBackColor", "使用背景色", "使用背景色"));
                LocalDic.Add("UseBorderColor", new LocalResource("UseBorderColor", "使用边框色", "使用边框色"));
                LocalDic.Add("UseFont", new LocalResource("UseFont", "使用字体", "使用字体"));
                LocalDic.Add("UseForeColor", new LocalResource("UseForeColor", "使用字体颜色", "使用字体颜色"));
                LocalDic.Add("UseImage", new LocalResource("UseImage", "使用背景图片", ""));
                LocalDic.Add("UseTextOptions", new LocalResource("UseTextOptions", "使用文本选项", "使用文本选项"));
                LocalDic.Add("TextOptions", new LocalResource("TextOptions", "文本选项", "文本选项"));
                LocalDic.Add("HAlignment", new LocalResource("HAlignment", "水平对齐方式", "水平对齐方式"));
                LocalDic.Add("HotkeyPrefix", new LocalResource("HotkeyPrefix", "热键前缀", "热键前缀"));
                LocalDic.Add("Trimming", new LocalResource("Trimming", "整理方式", "整理方式"));
                LocalDic.Add("VAlignment", new LocalResource("VAlignment", "垂直对齐方式", "垂直对齐方式"));
                LocalDic.Add("WordWrap", new LocalResource("WordWrap", "是否换行", "是否换行"));
            }
        }

        /// <summary>
        /// 选中行事件
        /// </summary>
        private void propertyGridControl1_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            PropertyDescriptor descriptor = null;
            if (e.Row != null) descriptor = propertyGridControl1.GetPropertyDescriptor(e.Row);
            if (descriptor != null)
            {
                lbCaption.Text = descriptor.DisplayName;
                pnlHint.Text = descriptor.Description;
            }
            else if (e.Row != null)
            {
                lbCaption.Text = e.Row.Properties.Caption;
                pnlHint.Text = string.Empty;
            }
            else
            {
                lbCaption.Text = pnlHint.Text = string.Empty;
            }
            SetPanelHeight();
        }

        /// <summary>
        /// 属性表格大小变化时
        /// </summary>
        private void XtraPropertyGrid_Resize(object sender, System.EventArgs e)
        {
            SetPanelHeight();
        }

        private void bci_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            propertyGridControl1.OptionsView.ShowRootCategories = bciCategories.Checked;
        }

        private void biDescription_DownChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pncDescription.Visible = pnlBottom.Visible = biDescription.Down;
        }

        /// <summary>
        /// 设置属性对象
        /// </summary>
        /// <param name="obj">属性对象</param>
        public void SetObject(object obj)
        {
            PropertyGrid.SelectedObject = obj;
        }

        /// <summary>
        /// 设置描述面板高度
        /// </summary>
        private void SetPanelHeight()
        {
            pncDescription.Height = lbCaption.Height + pnlHint.Height + 4;
        }

        /// <summary>
        /// 自定义属性描述
        /// </summary>
        private void propertyGridControl1_CustomPropertyDescriptors(object sender, DevExpress.XtraVerticalGrid.Events.CustomPropertyDescriptorsEventArgs e)
        {
            // 最外层
            if (e.Context.PropertyDescriptor == null)
            {
                e.Properties = SetProperty(e.Properties);
            }

            //内层
            if (e.Context.PropertyDescriptor != null)
            {
                e.Properties = SetProperty(e.Properties);
            }

        }

        /// <summary>
        /// 翻译属性
        /// </summary>
        /// <param name="sourceCollection"></param>
        /// <returns></returns>
        PropertyDescriptorCollection SetProperty(PropertyDescriptorCollection sourceCollection)
        {
            PropertyDescriptorCollection filteredCollection = new PropertyDescriptorCollection(null);
            if (sourceCollection==null)
            {
                return filteredCollection;
            }
            foreach (PropertyDescriptor item in sourceCollection)
            {
                CustomPropertyDescriptor des = new CustomPropertyDescriptor(item);
                string name = item.Name;
                if (LocalDic.ContainsKey(name))
                {
                    var local = LocalDic[name];
                    des.SetDisplayName(local.Name);
                    des.SetDescription(local.Description);
                }
                filteredCollection.Add(des);
            }
            return filteredCollection.Sort(new[] { "Font", "ForeColor", "BackColor", "BorderColor", "BackColor2", "GradientMode", "Image" });
        }

    }

    /// <summary>
    /// 本地资源
    /// </summary>
    public class LocalResource
    {
        /// <summary>
        /// 初始化本地资源
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="name">中文名称</param>
        /// <param name="description">描述</param>
        public LocalResource(string key, string name, string description)
        {
            Key = key;
            Name = name;
            Description = description;
        }
        /// <summary>
        /// 键名
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 自定义属性描述
    /// </summary>
    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        #region 字段
        private readonly PropertyDescriptor _proDescriptor;
        private string mCategory;
        private string mDisplayName;
        private string mDescription;
        #endregion

        #region 构造
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="proDescriptor">属性描述对象</param>
        public CustomPropertyDescriptor(PropertyDescriptor proDescriptor)
            : base(proDescriptor)
        {
            this.mCategory = proDescriptor.Category;
            this.mDisplayName = proDescriptor.DisplayName;
            this.mDescription = proDescriptor.Description;
            this._proDescriptor = proDescriptor;
        }
        #endregion

        #region 属性

        public override string Category
        {
            get { return mCategory; }
        }

        public override string DisplayName
        {
            get { return mDisplayName; }
        }

        public override string Description
        {
            get { return mDescription; }
        }

        public override bool CanResetValue(object component)
        {
            return _proDescriptor.CanResetValue(component);
        }

        public override Type ComponentType
        {
            get { return _proDescriptor.ComponentType; }
        }

        public override object GetValue(object component)
        {
            return _proDescriptor.GetValue(component);
        }

        public override bool IsReadOnly
        {
            get { return _proDescriptor.IsReadOnly; }
        }

        public override Type PropertyType
        {
            get { return _proDescriptor.PropertyType; }
        }

        public override void ResetValue(object component) { _proDescriptor.ResetValue(component); }

        public override void SetValue(object component, object value) { _proDescriptor.SetValue(component, value); }

        #endregion

        #region 方法

        public override bool ShouldSerializeValue(object component)
        {
            return _proDescriptor.ShouldSerializeValue(component);
        }
        /// <summary>
        /// 设置属性显示名称
        /// </summary>
        /// <param name="pDispalyName"></param>
        public void SetDisplayName(string pDispalyName)
        {
            mDisplayName = pDispalyName;
        }
        /// <summary>
        /// 设置属性分组
        /// </summary>
        /// <param name="pCategory"></param>
        public void SetCategory(string pCategory)
        {
            mCategory = pCategory;
        }
        /// <summary>
        /// 设置属性描述
        /// </summary>
        /// <param name="pDescription"></param>
        public void SetDescription(string pDescription)
        {
            mDescription = pDescription;
        }

        #endregion

    }
}
