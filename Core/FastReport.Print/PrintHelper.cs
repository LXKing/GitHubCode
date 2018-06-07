using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastReport;
using System.Windows.Forms;
using System.Management;

namespace FastReport.Print
{
    public static class PrintHelper
    {
        /// <summary>
        /// 设置报表Label的文本
        /// </summary>
        /// <param name="report"></param>
        /// <param name="key"></param>
        /// <param name="vlaue"></param>
        public static void SetMemoText(TfrxReportClass report, string key, object vlaue)
        {
            IfrxComponent memo = report.FindObject(key);
            if (memo!=null)
            {
                string text = "";
                if (vlaue!=null)
                {
                    text = vlaue.ToString();
                }
                IfrxCustomMemoView control = memo as IfrxCustomMemoView;
                if (control != null)
                {
                    control.Text = text;
                }
                else
                {
                    IfrxBarCodeView controlbar = memo as IfrxBarCodeView;
                    if (controlbar != null)
                    {
                        controlbar.Text = text;
                    }
                }
                
            }            
        }
        public static void Print(TfrxReportClass report, string pages)
        {
            report.ShowProgress = false;
            report.PrintOptions.ShowDialog = false;
            report.PrintOptions.PageNumbers = pages;            
            report.PrepareReport(true);
            report.PrintReport();
            report.ClearDatasets();
        }
        public static void Print(TfrxReportClass report)
        {
            report.ShowProgress = false;
            report.PrintOptions.ShowDialog = false;
            report.PrintOptions.PageNumbers = "";
            report.PrepareReport(true);
            report.PrintReport();
            report.ClearDatasets();
        }
        public static void PrintByPrinterName(TfrxReportClass report, string printerName)
        {
            report.ShowProgress = false;
            report.PrintOptions.ShowDialog = false;
            report.PrintOptions.PageNumbers = "";
            report.PrintOptions.Printer = printerName;
            report.PrepareReport(true);
            report.PrintReport();
            report.ClearDatasets();
            report.ClearReport();
        }
        public static void Preview(TfrxReportClass report)
        {
            report.PrepareReport(true);
            report.ShowReport();
        }
        
        public static void Design(TfrxReportClass report)
        {
            report.PrepareReport(true);
            report.DesignReport();
        }
    }
}
