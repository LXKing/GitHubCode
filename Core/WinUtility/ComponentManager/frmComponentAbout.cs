using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCI.Component;
using XCI.Extension;
using XCI.Helper;

namespace XCI.WinUtility.ComponentManager.UI
{
    public partial class frmComponentAbout : Form
    {
        public frmComponentAbout()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 组件类型
        /// </summary>
        public virtual Type ComponentType { get; set; }
        public virtual XCIComponentAttribute ComponentAttribute { get; set; }

        public virtual Image Logo { get; set; }


        private void frmConsoleAbout_Load(object sender, EventArgs e)
        {
            if (ComponentAttribute == null && ComponentType != null)
            {
                ComponentAttribute = AssemblyHelper.GetCustomAttributes<XCIComponentAttribute>(ComponentType);
            }
            if (ComponentAttribute != null)
            {
                labName.Text = ComponentAttribute.Name;
                labAuthor.Text = ComponentAttribute.Author;
                labContact.Text = ComponentAttribute.Contact;
                labVersion.Text = ComponentAttribute.Version;
                labCopyRight.Text = ComponentAttribute.CopyRight;
                txtDescription.Text = ComponentAttribute.Description;

                this.Text = "关于 " + ComponentAttribute.Name;

                string path = ComponentAttribute.Logo;
                if (path.IsNotEmpty())
                {
                    this.pictureBox1.Image = new Bitmap(ResourceImageHelper.CreateBitmapFromResources(path, ComponentType.Assembly), 64, 64);
                }
                else
                {
                    Type type = typeof(IManager);
                    var img = ResourceImageHelper.CreateBitmapFromResources("XCI.XCIComponent.ComponentLogo.png",
                                                                            type.Assembly);

                    this.pictureBox1.Image = new Bitmap(img, 64, 64);

                }
            }
            if (Logo != null)
            {
                this.pictureBox1.Image = Logo;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labContact_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(labContact.Text);
        }
    }
}
