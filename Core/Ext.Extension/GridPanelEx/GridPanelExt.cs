using Ext.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Ext.Extension.GridPanelEx
{
    [Meta, System.ComponentModel.Description("Grids are an excellent way of showing large amounts of tabular data on the client side."), System.ComponentModel.Designer(typeof(EmptyDesigner)), ToolboxBitmap(typeof(GridPanel), "Build.ToolboxIcons.GridPanel.bmp"), System.Web.UI.ToolboxData("<{0}:GridPanelExt runat=\"server\" Title=\"Title\" Height=\"300\"></{0}:GridPanelExt>")]
    public class GridPanelExt : GridPanel
    {
        [ConfigOption, Meta, DefaultValue("00000000-0000-0000-0000-000000000000"), Description("")]
        public virtual string GridPanel_GUID
        {
            get
            {
                return base.State.Get<string>("GridPanel_Guid", "00000000-0000-0000-0000-000000000000");
            }
            set
            {
                base.State.Set("GridPanel_Guid", value);
            }
        }

        public virtual bool SaveConfig(IList<ColumnBase> columnCollection, IList<ModelField> modelFieldCollection)
        {
            return false;
        }

        public virtual bool InitConfig(IList<ColumnBase> columnCollection, IList<ModelField> modelFieldCollection)
        {
            return false;
        }
    }
}
