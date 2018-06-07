using FastReport;
using FastReport.Data;
using FastReport.MSChart;
using FastReport.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

            #region MyRegion
            //report1.Load(Application.StartupPath + @"\送货单.frx");
            //var tb = (TableObject)report1.FindObject("Table1");
            //var ds = new DataSet();
            //ds.Tables.Add(dt);
            //
            //report1.GetDataSource("App_AppConfig").Enabled = true;
            
            //tb.BaseAssign(basedata);
            //tb.RowCount = basedata.RowCount+1;
            //report1.RegisterData(dt,ds);

            //var titleObject = report1.FindObject("Text4") as TextObject;
            //if (titleObject != null)
            //{
            //    titleObject.Text = "***公司年度财务报表标题";
            //}
            //(4).ForEach<int>(x =>
            //{
            //    var mone = report1.FindObject("Text" + x.ToString());
            //    if (mone != null)
            //    {
            //        var textObj = mone as TextObject;
            //        textObj.Text = x.ToString();
            //    }
            //});
            //var chart = report1.FindObject("MSChart1") as MSChartObject;
            //chart.Series[0].AddValue(10, 5);
            //chart.Series[0].AddValue(20, 6);
            //chart.Series[0].AddValue(30, 7);
            //chart.Series[0].AddValue(40, 8);
            //report1.Show(this); 
            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] bt = new byte[2] {125,15 };
            var hex = bt.ToHexString();
            //Task task = new Task(() => {
            //    StringBuilder build = MakeString(15);
            //    richTextBox1.Invoke(new Action(() => {
            //        richTextBox1.AppendText(build.ToString() + "\r");
            //    }));
            //});
            //task.Start();
        }

        private static StringBuilder MakeString(int count)
        {
            StringBuilder build = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                List<int> data = MakeData();
                var rdm = new Random();
                Thread.Sleep(rdm.Next(0, 10) * 10);
                var index = rdm.Next(0, 10);
                build.Append(data[index]);
            }
            return build;
        }

        private static List<int> MakeData()
        {
            List<int> data = new List<int>();
            while (data.Count < 10)
            {
                var rdm = new Random();
                var d = rdm.Next(0, 10);
                if (!data.Contains(d))
                {
                    data.Add(d);
                }
            }
            return data;
        }

    }
}
