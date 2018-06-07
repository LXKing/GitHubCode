using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCI.Component;
using XCI.Extension;
using XCI.Helper;

namespace XCI.WinUtility.ComponentManager.UI
{
    public partial class frmParamEdit : Form
    {
        public frmParamEdit()
        {
            InitializeComponent();
        }

        public string ClassProvider { get; set; }

        public ComponentParamEntity Entity { get; set; }

        public ComponentParamEntity GetEntity()
        {
            if (Entity==null)
            {
                Entity = new ComponentParamEntity();
            }
            Entity.Name = txtParamName.Text.Trim();
            Entity.Comment = txtParamComment.Text.Trim();
            Entity.Value = txtParamValue.Text.Trim();
            return Entity;
        }

        public void SetEntity(ComponentParamEntity entity)
        {
            if (entity != null)
            {
                this.txtParamName.Text = entity.Name;
                this.txtParamComment.Text = entity.Comment;
                this.txtParamValue.Text = entity.Value;
            }
        }

        public void BindPropertyList()
        {
            var list = ObjectHelper.GetObjectBasicPropertyList(ObjectHelper.CreateInstance(ClassProvider));
            if (list != null)
            {
                foreach (var item in list)
                {
                    txtParamName.Items.Add(item);
                }
            }
        }

        private void frmParamEdit_Load(object sender, EventArgs e)
        {
            BindPropertyList();
            SetEntity(Entity);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GetEntity();
            if (Entity.Name.IsEmpty())
            {
                MessageBox.Show("请输入参数名称");
                txtParamName.Select();
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
