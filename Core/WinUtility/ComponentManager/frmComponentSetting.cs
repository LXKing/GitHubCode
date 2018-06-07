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
using XCI.Helper;

namespace XCI.WinUtility.ComponentManager.UI
{
    public partial class frmComponentSetting : Form
    {
        public frmComponentSetting()
        {
            InitializeComponent();
        }

        private ConfigEntity CurrentConfigEntity { get; set; }

        private ComponentParamEntity CurrentComponentParamEntity { get; set; }

        public string InterfaceName { get; set; }
        
        public Func<XCIList<ConfigEntity>> GetConfigData { get; set; }
        public Action SaveConfigData { get; set; }

        private XCIList<ConfigEntity> ConfigData { get; set; }

        private void frmComponentSetting_Load(object sender, EventArgs e)
        {
            ListViewHelper.Init(objectListView1);
            ListViewHelper.Init(objectListView2);

            try
            {
                BindConfigEntity();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BindInterfaceInfo(string title, string provider)
        {
            this.txtTitle.Text = title;
            this.txtInterfaceName.Text = provider;
        }

        public void BindConfigEntity()
        {
            ConfigData = GetConfigData();
            ListViewHelper.BindData(objectListView1, ConfigData);

            var index = ConfigData.IndexOf(p => p.IsDefault);
            if (index > -1)
            {
                objectListView1.Items[index].BackColor = Color.FromArgb(255, 255, 192);
            }
            btnParamNew.Enabled = btnEdit.Enabled = btnDelete.Enabled
                = btnTest.Enabled = btnSetDefault.Enabled
                = objectListView1.SelectedIndex > -1 && objectListView1.Items.Count > 0;
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindParam();
            btnParamNew.Enabled = btnEdit.Enabled = btnDelete.Enabled
                = btnTest.Enabled = btnSetDefault.Enabled
                = objectListView1.SelectedIndex > -1 && objectListView1.Items.Count > 0;
            objectListView2_SelectedIndexChanged(null, null);
        }

        public void BindParam()
        {
            ConfigEntity entity = (ConfigEntity)objectListView1.SelectedObject;
            if (entity != null)
            {
                CurrentConfigEntity = entity;
                ListViewHelper.BindData(objectListView2, entity.ParamCollection);
            }
            else
            {
                CurrentConfigEntity = null;
                ListViewHelper.BindData(objectListView2, null);
            }
            btnParamEdit.Enabled = btnParamDelete.Enabled = objectListView2.SelectedIndex > -1;
        }

        private void txtSearch1_TextChanged(object sender, EventArgs e)
        {
            ListViewHelper.Filter(objectListView1, sender);
        }

        private void txtSearch2_TextChanged(object sender, EventArgs e)
        {
            ListViewHelper.Filter(objectListView2, sender);
        }

        private void objectListView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComponentParamEntity entity = (ComponentParamEntity)objectListView2.SelectedObject;
            if (entity != null)
            {
                CurrentComponentParamEntity = entity;
            }
            else
            {
                CurrentComponentParamEntity = null;
            }
            btnParamEdit.Enabled = btnParamDelete.Enabled = objectListView2.SelectedIndex > -1;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmComponentEdit frm = new frmComponentEdit();
            frm.InterfaceName = InterfaceName;
            frm.Entity = null;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var entity = frm.Entity;
                if (entity.IsDefault)
                {
                    ConfigData.ForEach(p => p.IsDefault = false);
                }
                ConfigData.AddOrUpdate(p => p.Name.Equals(entity.Name), entity);
                BindConfigEntity();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmComponentEdit frm = new frmComponentEdit();
            frm.InterfaceName = InterfaceName;
            frm.Entity = CurrentConfigEntity;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var entity = frm.Entity;
                if (entity.IsDefault)
                {
                    ConfigData.ForEach(p => p.IsDefault = false);
                    entity.IsDefault = true;
                }
                ConfigData.AddOrUpdate(p => p.Name.Equals(entity.Name), entity);
                BindConfigEntity();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CurrentConfigEntity != null)
            {
                if (MessageBox.Show("确定要删除 {0} 吗?".FS(CurrentConfigEntity.Name), "提示", MessageBoxButtons.YesNo)
                == System.Windows.Forms.DialogResult.Yes)
                {
                    ConfigData.Remove(p => p.Name.Equals(CurrentConfigEntity.Name));
                    BindConfigEntity();
                }
            }
        }

        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            if (CurrentConfigEntity != null)
            {
                if (MessageBox.Show("确定要把 {0} 设为默认吗?".FS(CurrentConfigEntity.Name), "提示", MessageBoxButtons.YesNo)
                == System.Windows.Forms.DialogResult.Yes)
                {
                    ConfigData.ForEach(p => p.IsDefault = false);
                    ConfigData.First(p => p.Name.Equals(CurrentConfigEntity.Name)).IsDefault = true;
                    BindConfigEntity();
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var type = Type.GetType(CurrentConfigEntity.Provider);
            if (AssemblyHelper.ValidInterfaceClass(type, false, typeof(IXCIComponentTest)))
            {
                IXCIComponentTest t = (IXCIComponentTest)ConfigFactory.CreateInstance(CurrentConfigEntity);
                t.ShowTestForm(CurrentConfigEntity.Name);
                BindParam();
            }
            else
            {
                MessageBox.Show("不支持");
            }
        }

        private void btnParamNew_Click(object sender, EventArgs e)
        {
            frmParamEdit frm = new frmParamEdit();
            frm.ClassProvider = CurrentConfigEntity.Provider;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (CurrentConfigEntity != null)
                {
                    var entity = frm.Entity;
                    CurrentConfigEntity.ParamCollection.AddOrUpdate(p => p.Name.Equals(entity.Name), entity);
                    BindParam();
                }
            }
        }

        private void btnParamEdit_Click(object sender, EventArgs e)
        {
            frmParamEdit frm = new frmParamEdit();
            frm.ClassProvider = CurrentConfigEntity.Provider;
            frm.Entity = CurrentComponentParamEntity;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (CurrentConfigEntity != null)
                {
                    var entity = frm.Entity;
                    CurrentConfigEntity.ParamCollection.AddOrUpdate(p => p.Name.Equals(entity.Name), entity);
                    BindParam();
                }
            }
        }

        private void btnParamDelete_Click(object sender, EventArgs e)
        {
            if (CurrentConfigEntity != null && CurrentComponentParamEntity != null)
            {
                if (MessageBox.Show("确定要删除 {0} 吗?".FS(CurrentComponentParamEntity.Name), "提示",
                    MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    CurrentConfigEntity.ParamCollection.Remove(p => p.Name.Equals(CurrentComponentParamEntity.Name));
                    BindParam();
                }
            }
        }

        private void frmComponentSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        public void SaveConfig()
        {
            try
            {
                SaveConfigData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void objectListView1_DoubleClick(object sender, EventArgs e)
        {
            btnEdit.PerformClick();
        }

        private void objectListView2_DoubleClick(object sender, EventArgs e)
        {
            btnParamEdit.PerformClick();
        }
    }
}
