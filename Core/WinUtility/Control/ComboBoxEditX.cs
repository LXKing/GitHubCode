// ===============================================================================
// Copyright (c) 2007-2013 西安交通信息投资营运有限公司。 
// ===============================================================================

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using XCI.Controls.Design;
using XCI.Helper;
using XCI.WinUtility;

namespace XCI.Controls
{
    /// <summary>
    /// 文本下拉控件
    /// </summary>
    [System.ComponentModel.DesignerCategoryAttribute("Code")]
    [Designer(typeof(ComboBoxEditXDesigner))]
    public class ComboBoxEditX : ComboBoxEdit
    {
        private object dataSource;
        private CurrencyManager dataManager;

        #region ComboBox
        
        /// <summary>
        /// 指示要为此控件中的项显示的属性
        /// </summary>
        [Category("XCI数据"), Description("指示要为此控件中的项显示的属性")]
        public string DisplayMember { get; set; }

        /// <summary>
        /// 指示用作控件中项的实际值的属性
        /// </summary>
        [Category("XCI数据"), Description("指示用作控件中项的实际值的属性")]
        public string ValueMember { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataSource
        {
            get { return dataSource; }
            set
            {
                if (value != null && !(value is IList || value is IListSource))
                    throw new ArgumentException("无效数据源");
                if (dataSource == value) return;

                dataSource = value;
                if (((dataSource != null) && (BindingContext != null)) && (dataSource != Convert.DBNull))
                {
                    dataManager = (CurrencyManager)this.BindingContext[dataSource];
                }

                this.Properties.Items.BeginUpdate();
                this.Properties.Items.Clear();
                foreach (object item in dataManager.List)
                {
                    object d = CurrencyManagerHelper.GetValue(dataManager, item, DisplayMember);
                    this.Properties.Items.Add(d);
                }
                this.Properties.Items.EndUpdate();

            }
        }

        /// <summary>
        /// 获取或设置当前选定的项
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new object SelectedItem
        {
            get { return CurrencyManagerHelper.GetItem(dataManager, SelectedIndex); }
            set
            {
                if (dataSource != null)
                {
                    this.EditValue = CurrencyManagerHelper.GetValue(dataManager, value, ValueMember);
                }
            }
        }

        /// <summary>
        /// 文本值
        /// </summary>
        [Bindable(false), Browsable(false)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        /// <summary>
        /// 获取或设置指定当前选定值。
        /// </summary>
        [Category("Data"), Description("获取或设置指定当前选定值。")]
        public new object EditValue
        {
            get { return SelectedValue; }
            set { SelectedValue = value; }
        }

        /// <summary>
        /// 获取或设置选中值
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue
        {
            get
            {
                if (DataSource==null)
                {
                    return base.EditValue;
                }
                object currentItem = CurrencyManagerHelper.GetItem(dataManager, SelectedIndex);
                return CurrencyManagerHelper.GetValue(dataManager, currentItem, ValueMember);
            }
            set
            {
                if (DataSource==null)
                {
                    base.EditValue = value;
                    return;
                }
                if (value == null) this.SelectedIndex = -1;

                if (dataManager == null) return;
                string propertyName = ValueMember;
                if (string.IsNullOrEmpty(propertyName))
                    throw new InvalidOperationException("请设置ValueMember");
                this.SelectedIndex = CurrencyManagerHelper.GetIndex(dataManager, propertyName, value);
            }
        }

        #endregion

        #region Implementation of IComboEditor

        /// <summary>
        /// 绑定的值数据属性名称
        /// </summary>
        [Category("Data"), Description("绑定的值数据属性名称")]
        public string ValueField { get; set; }

        /// <summary>
        /// 绑定的显示数据属性名称
        /// </summary>
        [Category("Data"), Description("绑定的显示数据属性名称")]
        public string TextField { get; set; }

        /// <summary>
        /// 获取控件显示值
        /// </summary>
        /// <returns>返回控件显示值</returns>
        public object GetText()
        {
            return Text;
        }

        /// <summary>
        /// 获取控件值
        /// </summary>
        /// <returns>控件值</returns>
        public object GetValue()
        {
            return SelectedValue??Text;
        }

        /// <summary>
        /// 设置控件值
        /// </summary>
        /// <param name="value">控件值</param>
        public void SetValue(object value)
        {
            SelectedValue = value;
        }

        #endregion
        
    }
}