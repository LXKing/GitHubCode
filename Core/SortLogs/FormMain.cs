using SortLogs.Properties;
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

namespace SortLogs
{
    public partial class FormMain : CCWin.CCSkinMain
    {
        public FormMain()
        {
            InitializeComponent();
        }
        Task task;
        private void btnSort1_Click(object sender, EventArgs e)
        {
            if (task!=null && !task.IsCompleted)
                return;
            var sort = new COMMON.Logs.LogSort(txtPath1.Text);
            sort.NewDirection_Create += sort_NewDirection_Create;
            sort.BeforeSort += sort_BeforeSort;
            sort.AfterSort += sort_AfterSort;
            sort.AfterSortOne_Update += sort_AfterSortOne_Update;
            sort.AfterSortOne_Exception += sort_AfterSortOne_Exception;
            var pattern=System.Configuration.ConfigurationManager.AppSettings["pattern"].FormatWith(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            var dt=dateTimePicker1.Value;
            task = new Task(new Action(() => {
                sort.SortByOneDate(dt, pattern);
            }));
            task.Start();
        }

        void sort_AfterSortOne_Exception(int index,string filename,Exception ex)
        {
            richTextBox1_AppendLine("异常:日志{0}归档发生异常({1})".FormatWith(filename, ex.Message), richTextBox1);
        }

        

        void sort_NewDirection_Create(string dir)
        {
            richTextBox1_AppendLine("创建目录{0}".FormatWith(dir), richTextBox1);
        }
        

        void sort_BeforeSort(int fileCount)
        {
            skinProgressBar1.Invoke(new Action(() =>
            {
                skinProgressBar1.Maximum = fileCount;
                skinProgressBar1.Value = 0;
            }));
            richTextBox1_AppendLine("开始日志归档...", richTextBox1);
        }

        void sort_AfterSort()
        {
            richTextBox1_AppendLine("日志归档完成!", richTextBox1);
            skinProgressBar1.Invoke(new Action(() =>
            {
                skinProgressBar1.Value = skinProgressBar1.Maximum;
            }));
        }

        void sort_AfterSortOne_Update(int arg1, string arg2)
        {
            richTextBox1_AppendLine("正在归档日志文件:{0}...".FormatWith(arg2),richTextBox1);
            
            skinProgressBar1.Invoke(new Action(() =>
            {
                skinProgressBar1.Value = arg1;
            }));
            Thread.Sleep(1000);
        }
        protected void richTextBox1_AppendLine(string msg,RichTextBox richtex)
        {
            richtex.Invoke(new Action(() =>
            {
                richtex.AppendText(msg + "\n");
                richtex.SelectionStart = richtex.Text.Length;
                richtex.ScrollToCaret();
            }));
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Application.StartupPath;
            var result =  folderBrowserDialog1.ShowDialog();
            if(result==DialogResult.OK)
            {
                txtPath1.Text = folderBrowserDialog1.SelectedPath;
                SortLogs.Properties.Settings.Default.Path = txtPath1.Text;
                SortLogs.Properties.Settings.Default.Save();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            #region 日志归档1
            dateTimePicker1.MaxDate = DateTime.Now.AddDays(-1);
            dateTimePicker1.Value = DateTime.Now.AddDays(-1); 
            #endregion
            
            #region 日志归档2
            dtBegin.Value = DateTime.Now.AddDays(-1);

            dtEnd.MaxDate = DateTime.Now.AddDays(-1);
            dtEnd.Value = DateTime.Now.AddDays(-1);
            #endregion
        }

        private void btnSort2_Click(object sender, EventArgs e)
        {
            if (task!=null && !task.IsCompleted)
                return;
            var sort = new COMMON.Logs.LogSort(txtPath1.Text);
            sort.NewDirection_Create += sort_NewDirection_Create;
            sort.BeforeSort += sort_BeforeSort;
            sort.AfterSort += sort_AfterSort;
            sort.AfterSortOne_Update += sort_AfterSortOne_Update;
            sort.AfterSortOne_Exception += sort_AfterSortOne_Exception;
            var dt0 = dtBegin.Value;
            var dt1 = dtEnd.Value;
            if (dtBegin.Value > dtEnd.Value)
            {
                dt0 = dtEnd.Value;
                dt1 = dtBegin.Value;
            }
            var dtList = new List<DateTime>();
            dtList.Add(dt0);
            var days = dt1.Subtract(dt0).Days;
            days.ForEach<int>(x => {
                dtList.Add(dtList.LastOrDefault().AddDays(1));
            });
            task = new Task(new Action(() =>
            {
                dtList.ForEach(x => {
                    var pattern = System.Configuration.ConfigurationManager.AppSettings["pattern"].FormatWith(x.ToString("yyyy-MM-dd"));
                    sort.SortByOneDate(x, pattern);
                });
                
            }));
            task.Start();
        }
    }
}
