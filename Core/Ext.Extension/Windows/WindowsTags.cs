using Ext.Net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Ext.Extension.Windows
{
    /// <summary>
    /// 
    /// </summary>
    [Meta, System.ComponentModel.Description("标签窗体."), System.ComponentModel.Designer(typeof(EmptyDesigner)), ToolboxBitmap(typeof(Window), "Build.ToolboxIcons.GridPanel.bmp"), System.Web.UI.ToolboxData("<{0}:WindowsTags runat=\"server\" Title=\"Title\" Height=\"300\" Hidden=\"true\"></{0}:WindowsTags>")]
    public class WindowTags:Ext.Net.Window
    {
        public event System.EventHandler<TagSelect_EventArgs> TagSelect;
        protected TagLabel _TagLabel;

        public string TagLabelID
        {
            get
            {
                return _TagLabel.ID;
            }
            set
            {
                _TagLabel.ID = value;
            }
        }

        public string TagSelectBeforeHandler
        {
            get
            {
                return _TagLabel.DirectEvents.Select.Before;
            }
            set
            {
                _TagLabel.DirectEvents.Select.Before = value;
            }
        }

        public string TagSelectCompleteHandler
        {
            get
            {
                return _TagLabel.DirectEvents.Select.Complete;
            }
            set
            {
                _TagLabel.DirectEvents.Select.Complete = value;
            }
        }

        public string TagSelectHandller
        {
            get
            {
                return _TagLabel.Listeners.Select.Handler;
            }
            set
            {
                _TagLabel.Listeners.Select.Handler = value;
            }
        }

        public int TagsMax
        {
            get
            {
                return _TagLabel.TagsMax;
            }
            set
            {
                _TagLabel.TagsMax = value;
            }
        }

        public TagSelectionMode SelectModel
        {
            get
            {
                return _TagLabel.SelectionMode;
            }
            set
            {
                _TagLabel.SelectionMode = value;
            }
        }
        public WindowTags()
        {
            this.MinHeight = 400;
            this.MinWidth = 600;
            this.Layout = "fit";
            _TagLabel = new TagLabel(new TagLabel.Config()
            {
                SelectionMode=Ext.Net.TagSelectionMode.Simple,
                AutoDataBind=true,
            });
            _TagLabel.DirectEvents.Select.Event += Select_Event;
            
            this.Items.Add(_TagLabel);
        }

        public void BindTags(IEnumerable<Ext.Net.Tag> datasource)
        {
            this._TagLabel.Tags.AddRange(datasource.ToArray());
            this._TagLabel.Render();
        }

        protected void Select_Event(object sender, DirectEventArgs e)
        {
            if (TagSelect != null)
                TagSelect.Invoke(sender,new TagSelect_EventArgs(e.ExtraParams));
        }
    }

    public class TagSelect_EventArgs:DirectEventArgs
    {
        public Tags SelectTags
        {
            get;set;
        }
        public TagSelect_EventArgs(ParameterCollection extraParams)
            : base(extraParams)
        {

        }
    }
}
