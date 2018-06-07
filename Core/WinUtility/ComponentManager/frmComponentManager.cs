using System;
using System.Windows.Forms;
using BrightIdeasSoftware;
using XCI.Component;
using XCI.Core;
using XCI.Helper;

namespace XCI.WinUtility.ComponentManager.UI
{
    public partial class frmComponentManager : Form
    {
        public frmComponentManager()
        {
            InitializeComponent();
        }


        private void frmManager_Load(object sender, EventArgs e)
        {
            ListViewHelper.Init(objectListView1);
            InitListView1();
            ListViewHelper.BindData(objectListView1, TypeManager.Data);


            ListViewHelper.Init(objectListView2);
            InitListView2();
        }

        private void InitListView1()
        {
            objectListView1.ShowGroups = true;
            var col = objectListView1.AllColumns[0];
            col.UseInitialLetterForGroup = true;
            col.GroupKeyGetter = p =>
                                 {
                                     InterfaceEntity entity = (InterfaceEntity)p;
                                     return entity.Group;
                                 };
        }

        private void InitListView2()
        {
            objectListView2.ShowGroups = false;
            objectListView2.UseCellFormatEvents = true;
            objectListView2.RowHeight = 68;
            var col = objectListView2.AllColumns[0];
            col.AspectToStringConverter = delegate
                                          {
                                              return "";
                                          };
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
                XCIList<ClassEntity> classData = null;
                var entity = (InterfaceEntity) objectListView1.SelectedObject;
                if (entity != null)
                {
                    classData = entity.ClassEntityList;
                }
                else
                {
                    classData = null;
                }
                ListViewHelper.BindData(objectListView2, classData);
            //}
            //catch(ObjectDisposedException)
            //{
                
            //}
        }

        private void txtSearch1_TextChanged(object sender, EventArgs e)
        {
            ListViewHelper.Filter(objectListView1, sender);
        }

        private void txtSearch2_TextChanged(object sender, EventArgs e)
        {
            ListViewHelper.Filter(objectListView2, sender);
        }

        private void objectListView2_FormatCell(object sender, FormatCellEventArgs e)
        {
            if (e.Column.Index == 0)
            {
                ClassEntity entity = (ClassEntity)e.Model;
                NamedDescriptionDecoration decoration = new NamedDescriptionDecoration();
                decoration.ImageList = TypeManager.LarImageList;
                decoration.Title = entity.Title;
                decoration.ImageName = entity.Icon;
                if (entity.ComponentAttribute != null)
                {
                    decoration.Description = entity.ComponentAttribute.Author + " " + entity.ComponentAttribute.Version;
                }
                e.SubItem.Decoration = decoration;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            var entity = (ClassEntity)objectListView2.SelectedObject;
            if (entity != null)
            {
                frmComponentAbout about = new frmComponentAbout();
                about.ComponentType = entity.ClassType;
                about.ShowDialog();
                about.Dispose();
            }

        }


        private void objectListView2_DoubleClick(object sender, EventArgs e)
        {
            btnAbout.PerformClick();
        }

        private void objectListView1_DoubleClick(object sender, EventArgs e)
        {
            btnSetting.PerformClick();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            var entity = (InterfaceEntity)objectListView1.SelectedObject;
            if (entity != null)
            {
                string provider = entity.Provider;
                frmComponentSetting seting = new frmComponentSetting();
                seting.InterfaceName = provider;
                if (entity.InterfaceType == typeof(IConfigProvider))
                {
                    seting.GetConfigData = ConfigFactory.GetList;
                    seting.SaveConfigData = () =>
                    {
                        var data = ConfigFactory.Current.GetData().Copy();
                        ConfigFactory.Save();
                        ConfigFactory.ResetDefault();
                        ConfigFactory.Current.SetData(data);
                        ConfigFactory.SaveComponentConfig();
                    };
                    seting.BindInterfaceInfo("组件配置容器", provider);
                }
                else
                {
                    seting.GetConfigData = () => ConfigFactory.Current.GetConfig(provider);
                    seting.SaveConfigData = ConfigFactory.SaveComponentConfig;
                    seting.BindInterfaceInfo(entity.Title, provider);
                }

                seting.ShowDialog();
                seting.Dispose();
            }
        }

        private void btnAutoCreate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您要清空现有的配置数据吗?", "提示", MessageBoxButtons.YesNo)
               == System.Windows.Forms.DialogResult.Yes)
            {
                ConfigFactory.Current.CleanConfig();
            }
            var data = TypeManager.Data;
            string name = "default";
            for (int index = 0; index < data.Count; index++)
            {
                var item = data[index];
                if (item.InterfaceType==typeof(IConfigProvider))
                {
                    continue;
                }
                for (int i = 0; i < item.ClassEntityList.Count; i++)
                {
                    var classItem = item.ClassEntityList[i];
                    ConfigEntity entity = new ConfigEntity();
                    if (i>0)
                    {
                        entity.Name = name + i;
                    }
                    else
                    {
                        entity.Name = name;
                        entity.IsDefault = true;
                    }
                    entity.Provider = classItem.Provider;
                    entity.Comment = classItem.Title;
                    ConfigFactory.Current.AddOrUpdateConfig(item.Provider, entity);
                }
            }
            ConfigFactory.SaveComponentConfig();
            MessageBox.Show("生成完成");
        }
    }

}
