using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCI.Component;
using XCI.Core;
using XCI.Extension;

namespace XCI.WinUtility.ComponentManager.UI
{
    public partial class frmComponentEdit : Form
    {
        public frmComponentEdit()
        {
            InitializeComponent();
        }

        public string InterfaceName { get; set; }
        public ConfigEntity Entity { get; set; }

        private XCIList<ClassEntity> ClassEntityList { get; set; }

        public ConfigEntity GetEntity()
        {
            if (Entity == null)
            {
                Entity = new ConfigEntity();
            }
            Entity.Name = txtName.Text.Trim();
            Entity.Comment = txtComment.Text.Trim();
            Entity.Provider = txtProvider.SelectedValue.ToString();
            Entity.IsDefault = txtIsDefault.Checked;
            return Entity;
        }

        public void SetEntity(ConfigEntity entity)
        {
            if (entity != null)
            {
                this.txtName.Text = entity.Name;
                this.txtComment.Text = entity.Comment;
                this.txtProvider.SelectedValue = entity.Provider;
                txtIsDefault.Checked = entity.IsDefault;
            }
        }

        public void BindClassEntityList()
        {
            if (InterfaceName.IsNotEmpty())
            {
                var entity = TypeManager.Data.First(p => p.InterfaceType == Type.GetType(InterfaceName));
                ClassEntityList = entity.ClassEntityList.Copy();
                ClassEntityList.ForEach(p => p.Title = p.Title + " (" + p.Provider + ")");
                txtProvider.DisplayMember = "Title";
                txtProvider.ValueMember = "Provider";
                txtProvider.DataSource = ClassEntityList;
                txtProvider.SelectedIndex = -1;
            }
        }

        private void txtProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtProvider.SelectedIndex > -1)
            {
                ClassEntity entity = ClassEntityList[txtProvider.SelectedIndex];
                if (entity.ComponentAttribute != null)
                {
                    txtComment.Text = entity.ComponentAttribute.Description;
                }
                else
                {
                    txtComment.Text = entity.Title;
                }
            }
            else
            {
                txtComment.Text = string.Empty;
            }
        }

        private void frmComponentEdit_Load(object sender, EventArgs e)
        {
            BindClassEntityList();
            SetEntity(Entity);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GetEntity();
            if (Entity.Name.IsEmpty())
            {
                MessageBox.Show("请输入名称");
                txtName.Select();
                return;
            }
            if (txtProvider.SelectedItem == null)
            {
                MessageBox.Show("请输入实现");
                txtName.Select();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
