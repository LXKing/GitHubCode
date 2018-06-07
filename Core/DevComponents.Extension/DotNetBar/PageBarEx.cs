using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public partial class PageBarEx : UserControl
    {
        private string PageTemplate = " of {0}";
        public event EventHandler<PageIndex_Change_EventArgs> Page_Change;
        public PageBarEx()
        {
            InitializeComponent();
            this.balloonTip1.DefaultBalloonWidth = 65;
            txtCurrentPageIndex.MaxValue = _PageCount;
        }
        public PageBarEx(int recordCount, int pageSize)
        {
            InitializeComponent();
            this.balloonTip1.DefaultBalloonWidth = 65;
            txtCurrentPageIndex.MaxValue = _PageCount;
        }
        public void InitPageInfo(IPage page)
        {
            btnNextPage.Enabled = btnLastPage.Enabled = page.HasNextPage;
            btnFirstPage.Enabled = btnPreviousPage.Enabled = page.HasPreviousPage;
            RecordCount = page.TotalCount;
            //if(RecordCount>0 && CurrentPageIndex == 0)
            //{
            //    CurrentPageIndex = 1;
            //}
            txtCurrentPageIndex.MaxValue = PageCount = page.TotalPages;
            labPageInfo.Text = PageTemplate.FormatWith(page.TotalPages);
            txtCurrentPageIndex.Value = page.PageIndex;//+1;
            this.Refresh();
            txtCurrentPageIndex.Refresh();
        }

        private int _PageSize=50;
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        private int _CurrentPageIndex = 0;
        public int CurrentPageIndex
        {
            get { return _CurrentPageIndex; }
            set { _CurrentPageIndex = value; }
        }
        private int _PageCount = 0;
        public int PageCount
        {
            get { return _PageCount; }
            set { _PageCount = value; }
        }
        public int RecordCount { set; get; }

        #region ProgressBarX属性
        public int ProgressBarMaxValue
        {
            get
            {
                return progressBarX1.Maximum;
            }
            set
            {
                progressBarX1.Maximum=value;
            }
        }
        public int ProgressBarMinValue
        {
            get
            {
                return progressBarX1.Minimum;
            }
            set
            {
                progressBarX1.Minimum = value;
            }
        }
        #endregion

        

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if(Page_Change!=null)
            {
                var args=new PageIndex_Change_EventArgs(PageSize,txtCurrentPageIndex.Value);
                Page_Change.Invoke(sender, args);
                
            }
        }

        private void txtCurrentPageIndex_ValueChanged(object sender, EventArgs e)
        {
            //btnRefresh_Click( sender,  e);
        }

        private void PageBarEx_Resize(object sender, EventArgs e)
        {
            progressBarX1.Width = this.Width - btnRefresh.Location.X - btnRefresh.Width - 15;
        }

        private void PageBarEx_SizeChanged(object sender, EventArgs e)
        {
            progressBarX1.Width = this.Width - btnRefresh.Location.X - btnRefresh.Width - 15;
        }

        public void RefreshData()
        {
            if (Page_Change != null)
            {
                var args = new PageIndex_Change_EventArgs(PageSize, _CurrentPageIndex);
                Page_Change.Invoke(new object(), args);
                txtCurrentPageIndex.Refresh();
            }
        }

        public void SetProgressBarValue(int value,string msg="")
        {
            progressBarX1.Invoke(new Action(() => {
                progressBarX1.Value = value;
                progressBarX1.Text = msg;
            }));
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = CurrentPageIndex - 1;
            RefreshData();
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = 1;
            RefreshData();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            txtCurrentPageIndex.RemoveControlEvent("ValueObjectChanged");
            CurrentPageIndex = CurrentPageIndex + 1;//txtCurrentPageIndex.Value+1;
            RefreshData();
            //txtCurrentPageIndex.ValueObjectChanged += txtCurrentPageIndex_ValueObjectChanged;
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            txtCurrentPageIndex.RemoveControlEvent("ValueObjectChanged");
            CurrentPageIndex = txtCurrentPageIndex.MaxValue;
            RefreshData();
            //txtCurrentPageIndex.ValueObjectChanged += txtCurrentPageIndex_ValueObjectChanged;
        }

        private void txtCurrentPageIndex_ValueObjectChanged(object sender, EventArgs e)
        {
            //txtCurrentPageIndex.RemoveControlEvent("ValueObjectChanged");
            //CurrentPageIndex = txtCurrentPageIndex.Value;
            //RefreshData();
            //txtCurrentPageIndex.ValueObjectChanged += txtCurrentPageIndex_ValueObjectChanged;
        }

        private void txtCurrentPageIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                RefreshData();
            }
        }

        private void txtCurrentPageIndex_ConvertFreeTextEntry(object sender, Editors.FreeTextEntryConversionEventArgs e)
        {
            txtCurrentPageIndex.Refresh();
        }
    }
    public class PageIndex_Change_EventArgs:EventArgs
    {
        public PageIndex_Change_EventArgs (int pageSize,int currentPageIndex)
	    {
            PageSize=pageSize;
            CurrentPageIndex=currentPageIndex;
	    }
        public int PageSize
        {
            get;set;
        }
        public int CurrentPageIndex
        {
            get;set;
        }
    }
}
