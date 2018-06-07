using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCI.Core;
using System.Configuration;
using XCI.Component;

namespace XCI
{
    public partial class ExceptionForm : Form
    {
        public ExceptionForm()
        {
            InitializeComponent();
        }

        public Exception ExceptionObject { get; set; }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {
            txtCompany.Text = ParamFactory.Current.Get("AppStationNames");//"西安城南客运站";
            txtDateTime.Text = DateTime.Now.ToString();
            txtMessage.Text = ExceptionObject.Message
                              + Environment.NewLine + ExceptionObject.Source
                              + Environment.NewLine + ExceptionObject.StackTrace;
            txtSoftName.Text = "站务管理软件"+DateTime.Now.Year.ToString();
            txtUserName.Text = ProjectUser.UserName;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtMessage.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
