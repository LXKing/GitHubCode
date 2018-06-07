using FastReport.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            COMMON.Logs.Log.WriteLog("测试");
            //FastReport.Report report1 = new FastReport.Report();
            //report1.Load(Application.StartupPath + @"\Test.frx");

            //var ds = report1.GetDataSource("Table1") as TableDataSource;
            //var p1 = ds.Parameters[0];
            //p1.Value = "超级管理员";

            
            //report1.Show();
        }
    }
}
