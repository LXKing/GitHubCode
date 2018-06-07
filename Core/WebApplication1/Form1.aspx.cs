using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Form1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void WebReport1_StartReport(object sender, EventArgs e)
        {
            FastReport.Report report = new FastReport.Report();
            var path = FastReport.Utils.Config.ApplicationFolder + "report1.frx";//report.Load()
            report.Load(path);
            WebReport1.Report = report;
            var js = string.Format("<script>window.open('/FastReport.Export.axd?object={0}&print_browser=1&s=2944');</script>", WebReport1.ReportGuid);
            ClientScript.RegisterStartupScript(typeof(string), "autorun", js);
            //report.Show();
        }
    }
}