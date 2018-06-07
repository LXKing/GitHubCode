using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;
using XCI.Extension;

namespace XCI.Helper
{
    /// <summary>
    /// ListView操作帮助类
    /// </summary>
    public static class ListViewHelper
    {
        private static readonly Dictionary<string, TextOverlay> OverlayDic = new Dictionary<string, TextOverlay>();

        /// <summary>
        /// 初始化表格属性
        /// </summary>
        /// <param name="view">表格对象</param>
        public static void Init(ObjectListView view)
        {
            view.FullRowSelect = true;
            view.GridLines = true;
            view.HideSelection = false;
            view.MultiSelect = false;
            view.OwnerDraw = true;
            view.UseExplorerTheme = true;
            view.UseFiltering = true;
            view.View = System.Windows.Forms.View.Details;
            view.RowHeight = 30;
            view.ShowGroups = false;
            view.SelectColumnsOnRightClick = false;
            view.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
            view.ShowFilterMenuOnRightClick = false;
            view.UnfocusedHighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            view.UnfocusedHighlightForegroundColor = System.Drawing.SystemColors.HighlightText;
            view.UseCompatibleStateImageBehavior = false;
            view.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(207)))));
        }


        /// <summary>
        /// 选中第一行数据
        /// </summary>
        /// <param name="view">表格对象</param>
        public static void SelectFirst(ObjectListView view)
        {
            if (view.Items.Count > 0)
            {
                view.Items[0].Selected = true;
            }
        }
        public static void Filter(ObjectListView view, object sender)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                Filter(view, (textBox).Text.Trim());
            }
        }

        /// <summary>
        /// 过滤表格
        /// </summary>
        /// <param name="view">表格对象</param>
        /// <param name="key">查询关键字</param>
        public static void Filter(ObjectListView view, string key)
        {
            TextMatchFilter filter = null;
            if (key.Length > 0)
            {
                filter = TextMatchFilter.Contains(view, key);
            }
            if (filter == null)
                view.DefaultRenderer = null;
            else
            {
                view.DefaultRenderer = new HighlightTextRenderer(filter);
            }
            HighlightTextRenderer highlightingRenderer = view.GetColumn(0).Renderer as HighlightTextRenderer;
            if (highlightingRenderer != null)
                highlightingRenderer.Filter = filter;

            view.ModelFilter = filter;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="view">表格对象</param>
        /// <param name="data">数据</param>
        public static void BindData(ObjectListView view, dynamic data)
        {
            int count = 0;
            if (data != null)
            {
                count = data.Count;
            }
            view.SetObjects(data);
            if (OverlayDic.ContainsKey(view.Name))
            {
                view.RemoveOverlay(OverlayDic[view.Name]);
            }
            TextOverlay nagOverlay = new TextOverlay();
            nagOverlay.Alignment = ContentAlignment.BottomRight;
            nagOverlay.Text = "共{0}条".FS(count);
            nagOverlay.BackColor = Color.White;
            nagOverlay.BorderWidth = 2.0f;
            nagOverlay.BorderColor = Color.Green;
            nagOverlay.TextColor = Color.Red;
            nagOverlay.Font = new Font("微软雅黑", 18);
            view.OverlayTransparency = 255;
            view.AddOverlay(nagOverlay);
            OverlayDic.AddOrUpdate(view.Name, nagOverlay);
        }


        /// <summary>
        /// 自动生成列
        /// </summary>
        /// <param name="view">表格对象</param>
        /// <param name="entityType">实体类型</param>
        public static void GenerateColumns(ObjectListView view, Type entityType)
        {
            Generator.GenerateColumns(view, entityType);
        }

        /// <summary>
        /// 列宽自适应
        /// </summary>
        /// <param name="view">表格对象</param>
        public static void AutoResizeColumns(ObjectListView view)
        {
            view.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

    }

    public class NamedDescriptionDecoration : BrightIdeasSoftware.AbstractDecoration
    {
        public ImageList ImageList;
        public string ImageName;
        public string Title;
        public string Description;

        public Font TitleFont = new Font("微软雅黑", 12, FontStyle.Bold);
        public Color TitleColor = Color.Green;//Color.FromArgb(255, 32, 32, 32);
        public Font DescripionFont = new Font("微软雅黑", 11);
        public Color DescriptionColor = Color.FromArgb(255, 96, 96, 96);
        public Size CellPadding = new Size(2, 2);

        public override void Draw(BrightIdeasSoftware.ObjectListView olv, Graphics g, Rectangle r)
        {
            Rectangle cellBounds = this.CellBounds;
            cellBounds.Inflate(-this.CellPadding.Width, -this.CellPadding.Height);
            Rectangle textBounds = cellBounds;

            if (this.ImageList != null && !String.IsNullOrEmpty(this.ImageName))
            {
                var img = this.ImageList.Images[this.ImageName];
                if (img != null)
                {
                    g.DrawImage(img, cellBounds.Location);
                    textBounds.X += this.ImageList.ImageSize.Width;
                    textBounds.Width -= this.ImageList.ImageSize.Width;
                }
            }

            //g.DrawRectangle(Pens.Red, textBounds);

            // Draw the title
            using (StringFormat fmt = new StringFormat(StringFormatFlags.NoWrap))
            {
                fmt.Trimming = StringTrimming.EllipsisCharacter;
                fmt.Alignment = StringAlignment.Near;
                fmt.LineAlignment = StringAlignment.Near;
                using (SolidBrush b = new SolidBrush(this.TitleColor))
                {
                    g.DrawString(this.Title, this.TitleFont, b, textBounds, fmt);
                }
                // Draw the description
                SizeF size = g.MeasureString(this.Title, this.TitleFont, (int)textBounds.Width, fmt);
                textBounds.Y += (int)size.Height;
                textBounds.Height -= (int)size.Height;
            }

            // Draw the description
            using (StringFormat fmt2 = new StringFormat())
            {
                fmt2.Trimming = StringTrimming.EllipsisCharacter;
                using (SolidBrush b = new SolidBrush(this.DescriptionColor))
                {
                    g.DrawString(this.Description, this.DescripionFont, b, textBounds, fmt2);
                }
            }
        }
    }
}