using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using XCI.Extension;
using XCI.Helper;

namespace XCI.WinUtility
{
    public class FormHelper
    {
        /// <summary>
        /// 获取文字的大小
        /// </summary>
        /// <param name="graphics">绘图对象</param>
        /// <param name="text">文本</param>
        /// <param name="font">字体</param>
        /// <param name="size">字体大小</param>
        /// <returns></returns>
        public static Size GetTextSize(Graphics graphics, string text, Font font, Size size)
        {
            if (text.Length == 0)
            {
                return Size.Empty;
            }
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.FitBlackBox;
            RectangleF layoutRect = new RectangleF(0f, 0f, size.Width, size.Height);
            CharacterRange[] ranges = new [] { new CharacterRange(0, text.Length) };
            //Region[] regionArray = new Region[1];
            stringFormat.SetMeasurableCharacterRanges(ranges);
            Rectangle rectangle = Rectangle.Round(graphics.MeasureCharacterRanges(text, font, layoutRect, stringFormat)[0].GetBounds(graphics));
            return new Size(rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// 添加枚举下拉
        /// </summary>
        /// <param name="box">控件</param>
        /// <param name="enumType">枚举类型</param>
        /// <param name="ItemDescriptionDic">项翻译字典</param>
        public static void AddEnum(ImageComboBoxEdit box, Type enumType, Dictionary<string, string> ItemDescriptionDic)
        {
            box.Properties.BeginUpdate();
            try
            {
                Array values = System.Enum.GetValues(enumType);
                foreach (object obj in values)
                {
                    string name = obj.ToString();
                    string description = name;
                    if (ItemDescriptionDic.ContainsKey(name))
                    {
                        description = ItemDescriptionDic[name];
                    }
                    box.Properties.Items.Add(new ImageComboBoxItem(description, obj, -1));
                }
            }
            finally
            {
                box.Properties.EndUpdate();
            }
        }

        /// <summary>
        /// 获取操作系统环境变量
        /// </summary>
        /// <param name="name">变量名</param>
        public static string GetEnvironmentVariable(string name)
        {
            string env = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
            if (String.IsNullOrEmpty(env))
            {
                env = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User);
                if (String.IsNullOrEmpty(env))
                {
                    env = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine);
                }
            }
            return env;
        }

        /// <summary>
        /// 递归查找控件
        /// </summary>
        /// <param name="controls">控件容器</param>
        /// <param name="name">查找的控件名称</param>
        public static Control FindControl(Control controls, string name)
        {
            Control control = null;
            if (controls.Controls.Count > 0)
            {
                foreach (Control item in controls.Controls)
                {
                    if (item.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return item;
                    }
                    if (item.Controls.Count > 0)
                    {
                        control = FindControl(item, name);
                        if (control != null)
                        {
                            return control;
                        }
                    }
                }
            }
            return control;
        }


        /// <summary>
        /// 焦点导航到下一个控件或者上一个控件
        /// </summary>
        /// <param name="form">操作的表单</param>
        /// <param name="control">激活控件</param>
        /// <param name="isNext">方向 true 下一个 false 上一个控件</param>
        public static void SelectNextControl(Form form, Control control, bool isNext = true)
        {
            form.SelectNextControl(control, isNext, true, true, true);
        }


        /// <summary>
        /// 焦点导航到当前激活控件的下一个控件或者上一个控件
        /// </summary>
        /// <param name="form">操作的表单</param>
        /// <param name="isNext">方向 true 下一个 false 上一个控件</param>
        public static void SelectNextControl(Form form, bool isNext = true)
        {
            SelectNextControl(form, form.ActiveControl, isNext);
        }


        /// <summary>
        /// 把控件居中显示
        /// </summary>
        /// <param name="form">操作的表单</param>
        /// <param name="centerControl">要居中的控件</param>
        /// <param name="parentControl">控件的父控件</param>
        /// <param name="isCenterWidth">是否居中宽度</param>
        /// <param name="isCenterHeight">是否居中高度</param>
        public static void CenterControl(Form form, Control centerControl, Control parentControl, bool isCenterWidth = true, bool isCenterHeight = true)
        {
            if (centerControl == null)
            {
                return;
            }
            if (parentControl == null)
            {
                parentControl = form;
            }
            int x = centerControl.Location.X;
            int y = centerControl.Location.Y;

            if (isCenterWidth)
            {
                x = parentControl.Width / 2 - centerControl.Width / 2;
            }
            if (isCenterHeight)
            {
                y = parentControl.Height / 2 - centerControl.Height / 2;
            }
            centerControl.Location = new Point(x, y);
        }


        /// <summary>
        /// 设置窗口圆角
        /// </summary>
        /// <param name="width">窗口宽度</param>
        /// <param name="height">窗口高度</param>
        /// <param name="radius">半径</param>
        public static Region SetFormRegionRound(int width, int height, int radius = 30)
        {
            Rectangle rect = new Rectangle(0, 22, width, height - 22);
            GraphicsPath path = GetRoundedRectPath(rect, 30);
            return new Region(path);
        }


        /// <summary>
        /// 获取圆角路径
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="radius">半径</param>
        private static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //   左上角   

            path.AddArc(arcRect, 180, 90);
            //   右上角   

            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            //   右下角   

            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            //   左下角   

            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 设置字体信息控件框和字体对话框的通讯
        /// </summary>
        /// <param name="control">字体信息控件框</param>
        /// <param name="dialog">字体对话框</param>
        public static void SetControlFont(Control control, FontDialog dialog)
        {
            if (control.Text.Length > 0)
            {
                dialog.Font = (Font)ObjectHelper.GetObjectFromString(control.Text, typeof(Font));
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                control.Text = ObjectHelper.GetObjectString(dialog.Font);
            }
        }

        /// <summary>
        /// 迭代指定容器中的控件 并执行回调
        /// </summary>
        /// <param name="container">容器</param>
        /// <param name="exec">回调</param>
        public static void EachControl(Control container, Action<BaseEdit> exec)
        {
            foreach (Control control in container.Controls)
            {
                if (control is BaseEdit)
                {
                    BaseEdit edit = (BaseEdit)control;
                    if (exec != null)
                    {
                        exec(edit);
                    }
                }
                else if (control is PanelControl)
                {
                    EachControl(control, exec);
                }
                else if (control is XtraScrollableControl)
                {
                    EachControl(control, exec);
                }
                else if (control is XtraTabControl)
                {
                    XtraTabControl tabControl = (XtraTabControl)control;
                    foreach (XtraTabPage tabPage in tabControl.TabPages)
                    {
                        EachControl(tabPage, exec);
                    }
                }
            }
        }

        /// <summary>
        /// 将面板中的控件设为只读
        /// </summary>
        /// <param name="container">容器面板</param>
        /// <param name="exceptControl">排除的控件</param>
        public static void ReadonlyControl(Control container, params Control[] exceptControl)
        {
            EachControl(container, p =>
            {
                if (!HasControl(p, exceptControl))
                {
                    p.Properties.ReadOnly = true;
                }
            });
        }

        /// <summary>
        /// 设置控件状态 启用或者禁用
        /// </summary>
        /// <param name="container">容器面板</param>
        /// <param name="isEnable">是否启用</param>
        /// <param name="exceptControl">排除的控件</param>
        public static void EnableControl(Control container, bool isEnable, params Control[] exceptControl)
        {
            EachControl(container, p =>
            {
                if (!HasControl(p, exceptControl))
                {
                    p.Enabled = isEnable;
                }
            });
        }

        /// <summary>
        /// 检测控件是否包括在排除的控件中
        /// </summary>
        /// <param name="control">检测的控件</param>
        /// <param name="exceptControl">排除的控件</param>
        /// <returns>如果包含返回True</returns>
        private static bool HasControl(Control control, Control[] exceptControl)
        {
            if (exceptControl != null && exceptControl.Length > 0)
            {
                return exceptControl.Any(item => item.Name.Equals(control.Name));
            }
            return false;
        }

        /// <summary>
        /// 将这个容器中的全部控件 用指定的实体绑定
        /// </summary>
        /// <param name="container">容器面板</param>
        /// <param name="entity">实体对象</param>
        public static void BindControlValue(Control container, object entity)
        {
            EachControl(container, p => BindControlValue(p, entity));
        }

        /// <summary>
        /// 把实体属性绑定到控件 属性名称读取控件的Tag属性获取 绑定的控件属性是 EditValue
        /// </summary>
        /// <param name="edit">控件对象</param>
        /// <param name="entity">实体对象</param>
        /// <param name="editProperName">绑定的控件属性</param>
        public static void BindControlValue(BaseEdit edit, object entity, string editProperName = "EditValue")
        {
            if (edit != null && edit.Tag != null && !string.IsNullOrEmpty(edit.Tag.ToString()))
            {

                //if (control is XCIPopupGridControlEdit
                //    || control is ChinaComboBoxEdit)
                //{
                //    properName = "Value";
                //}
                //if (current is DateEdit)
                //{
                //    properName = "DateTime";
                //}

                edit.DataBindings.Clear();
                if (entity != null)
                {
                    if (edit is XCIPopupGrid)
                    {
                        Binding bind;
                        var valueMember = ((XCIPopupGrid) edit).BindValueMember;
                        var displayMember = ((XCIPopupGrid) edit).BindDisplayMember;

                        if (!string.IsNullOrEmpty(valueMember))
                        {
                            bind = new Binding("SelectedValue", entity, valueMember, true);
                            edit.DataBindings.Add(bind);
                        }

                        if (!string.IsNullOrEmpty(displayMember))
                        {
                            bind = new Binding("EditText", entity, displayMember, true);
                            edit.DataBindings.Add(bind);
                        }
                    }
                    else if (edit is XCIPopupGridEdit)
                    {
                        Binding bind;
                        var valueMember = ((XCIPopupGridEdit)edit).BindValueMember;
                        var displayMember = ((XCIPopupGridEdit)edit).BindDisplayMember;

                        if (!string.IsNullOrEmpty(valueMember))
                        {
                            bind = new Binding("SelectedValue", entity, valueMember, true);
                            edit.DataBindings.Add(bind);
                        }

                        if (!string.IsNullOrEmpty(displayMember))
                        {
                            bind = new Binding("Text", entity, displayMember, true);
                            edit.DataBindings.Add(bind);
                        }
                    }
                    else if (edit is XCIPopupTreeGrid)
                    {
                        Binding bind;
                        var valueMember = ((XCIPopupTreeGrid)edit).BindValueMember;
                        var displayMember = ((XCIPopupTreeGrid)edit).BindDisplayMember;

                        if (!string.IsNullOrEmpty(valueMember))
                        {
                            bind = new Binding("SelectedValue", entity, valueMember, true);
                            edit.DataBindings.Add(bind);
                        }

                        if (!string.IsNullOrEmpty(displayMember))
                        {
                            bind = new Binding("EditText", entity, displayMember, true);
                            edit.DataBindings.Add(bind);
                        }
                    }
                    else
                    {
                        Binding bind = new Binding(editProperName, entity, edit.Tag.ToString(), true);
                        edit.DataBindings.Add(bind);
                        if (edit.DataBindings.Count > 0 && edit.Tag !=null)
                        {
                            edit.EditValueChanged += (a, b) =>
                            {
                                ObjectHelper.SetObjectProperty(edit.DataBindings[0].DataSource, edit.Tag.ToString(), edit.EditValue);
                            };
                        }
                    }
                }
            }
        }

        static void edit_EditValueChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 绑定容器中控件的回车 值变化事件
        /// </summary>
        /// <param name="container">容器面板</param>
        /// <param name="getActiveControlFunc">获取激活控件容器回调</param>
        /// <param name="firstControl">第一个控件</param>
        /// <param name="lastControl">最后一个控件</param>
        /// <param name="lastControlAction">最后一个控件回车时的回调</param>
        /// <param name="editValueChangedNotifyAction">值发生变化时回调</param>
        /// <param name="validateFunc">回车到下一个控件时的验证函数</param>
        /// <param name="exceptControl">排除的控件</param>
        public static void BindControlEnterEvent(Control container, Func<Control> getActiveControlFunc, BaseEdit firstControl, BaseEdit lastControl, Action lastControlAction, Action editValueChangedNotifyAction, Func<BaseEdit, bool> validateFunc, params Control[] exceptControl)
        {
            if (getActiveControlFunc == null)
            {
                throw new ArgumentException("获取集合控件函数不能为空");
            }
            EachControl(container, p =>
            {
                if (p.TabStop && !HasControl(p, exceptControl))
                {
                    BindControlEnterEvent(container, p, getActiveControlFunc, firstControl, lastControl, lastControlAction, editValueChangedNotifyAction, validateFunc);
                }
            });
        }

        /// <summary>
        /// 绑定控件的回车 值变化事件
        /// </summary>
        /// <param name="container">容器面板</param>
        /// <param name="edit">绑定的控件</param>
        /// <param name="getActiveControlFunc">获取激活控件容器回调</param>
        /// <param name="firstControl">第一个控件</param>
        /// <param name="lastControl">最后一个控件</param>
        /// <param name="lastControlAction">最后一个控件回车时的回调</param>
        /// <param name="editValueChangedNotifyAction">值发生变化时回调</param>
        /// <param name="validateFunc">回车到下一个控件时的验证函数</param>
        public static void BindControlEnterEvent(Control container, BaseEdit edit, Func<Control> getActiveControlFunc, BaseEdit firstControl, BaseEdit lastControl, Action lastControlAction, Action editValueChangedNotifyAction, Func<BaseEdit, bool> validateFunc)
        {
            edit.Enter += (s, e) => edit.SelectAll();

            edit.EditValueChanged += (s, e) =>
            {
                if (editValueChangedNotifyAction != null) editValueChangedNotifyAction();
            };

            

            edit.KeyPress += (s, e) =>
            {
                if (e.KeyChar == 13)
                {
                    if (edit is MemoEdit)
                    {
                        return;
                    }
                    e.Handled = true;
                    if (edit.Name.Equals(lastControl.Name))
                    {
                        lastControlAction();
                        return;
                    }
                    if (validateFunc == null)
                    {
                        container.SelectNextControl(getActiveControlFunc(), true, true, true, false);
                    }
                    else if (validateFunc(edit))
                    {
                        container.SelectNextControl(getActiveControlFunc(), true, true, true, false);
                    }
                }
                else if (e.KeyChar == 27)
                {
                    e.Handled = true;
                    if (edit.Name.Equals(firstControl.Name))
                    {
                        return;
                    }
                    container.SelectNextControl(getActiveControlFunc(), false, true, true, false);
                }
            };
        }
        

        /// <summary>
        /// 把指定的值赋值给控件的EditValue 并按照EditValue的类型进行转换
        /// </summary>
        /// <param name="control">编辑控件</param>
        /// <param name="controlValue">指定的控件值</param>
        public static void ConvertToControlEditValue(BaseEdit control, object controlValue)
        {
            if (controlValue is Font || controlValue is Color)
            {
                controlValue = ObjectHelper.GetObjectString(controlValue);
            }
            if (control.EditValue != null)
            {
                string controlValueTypeName = control.EditValue.GetType().Name;
                if (controlValueTypeName.Equals(typeof(int).Name))
                {
                    control.EditValue = controlValue.ToInt();
                }
                else if (controlValueTypeName.Equals(typeof(bool).Name))
                {
                    control.EditValue = controlValue.ToBool();
                }
                else
                {
                    control.EditValue = controlValue;
                }
            }
            else
            {
                control.EditValue = controlValue;
            }
        }

        /// <summary>
        /// 绑定控件和对象的双向通讯(把对象的值自动赋值给控件 控件值发生变化时自动改变对象的属性值)
        /// </summary>
        /// <param name="container">控件容器</param>
        /// <param name="obj">对象</param>
        public static void BindCustomPropertyControlValue(Control container, object obj)
        {
            EachControl(container, p =>
            {
                if (p != null && p.Tag != null)
                {
                    string propertyName = p.Tag.ToString();
                    var pValue = ObjectHelper.GetObjectPropertys(obj, propertyName);
                    p.Invoke(new Action(() => ConvertToControlEditValue(p, pValue)));

                    p.EditValueChanged += (s, e) =>
                    {
                        string propertyValue = ObjectHelper.GetObjectString(p.EditValue);
                        ObjectHelper.SetObjectPropertys(obj, propertyName, propertyValue);
                    };
                }
            });
        }
    }
}