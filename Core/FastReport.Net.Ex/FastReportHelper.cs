using FastReport.Barcode;
using FastReport.MSChart;
using FastReport.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FastReport.Net
{
    public static  class FastReportHelper
    {
        public static void  Test()
        {
            var pathRoot = AppDomain.CurrentDomain.BaseDirectory;
            //var path = AppDomain.CurrentDomain.BaseDirectory + "Example.frx";

            //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //xml直接嵌入程序集 读取的方法
            System.IO.Stream sm = Assembly.GetExecutingAssembly().GetManifestResourceStream("FastReport.Net.Example.frx");
            //doc.Load(sm); //直接将流转成xml

            byte[] bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);
            sm.Close();
            UTF8Encoding con = new UTF8Encoding();
            string str = con.GetString(bs);//将流转成字符串


            Report report = new Report();
            report.LoadFromString(str);

            report.SetTextObject_Text("TextObject_Title", "报表标题");
            report.SetTextObject_Text("TextObject_YeMei", "页眉部分");

            System.IO.Stream sm1 = Assembly.GetExecutingAssembly().GetManifestResourceStream("FastReport.Net.Example.jpg");
            var image = Image.FromStream(sm1);

            report.SetPictureObject_Text("Picture1", image);

            image.Save(pathRoot + "Example.jpg");
            report.SetPictureObject_Text("Picture1", pathRoot + "Example.jpg");

            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(Guid)));
            dt.Columns.Add(new DataColumn("产品名", typeof(string)));
            dt.Columns.Add(new DataColumn("价钱", typeof(decimal)));
            dt.Rows.Add(new object[3] { Guid.NewGuid(), "鞋子", 100.00 });
            dt.Rows.Add(new object[3] { Guid.NewGuid(), "衣服", 150.00 });
            dt.Rows.Add(new object[3] { Guid.NewGuid(), "胸罩", 100.00 });
            dt.Rows.Add(new object[3] { Guid.NewGuid(), "秋衣裤", 150.00 });
            dt.Rows.Add(new object[3] { Guid.NewGuid(), "长腿库", 150.00 });
            report.SetTableObject_Data("Table1", dt, true);

            report.SetBarcodeObject_Text("Barcode1", "0123456789");
            report.SetBarcodeObject_Text("Barcode2", "0123456789", new Barcode.BarcodeQR() { Encoding = QRCodeEncoding.UTF8 });

            List<KeyValuePair<string, decimal>> data = new List<KeyValuePair<string, decimal>>(){
                new KeyValuePair<string,decimal>("2016-01-01",1000),
                new KeyValuePair<string,decimal>("2016-02-01",1100),
                new KeyValuePair<string,decimal>("2016-03-01",1100),
                new KeyValuePair<string,decimal>("2016-04-01",1500),
                new KeyValuePair<string,decimal>("2016-05-01",1200),
                new KeyValuePair<string,decimal>("2016-06-01",1700),
                new KeyValuePair<string,decimal>("2016-07-01",1600),
                new KeyValuePair<string,decimal>("2016-08-01",1200),
                new KeyValuePair<string,decimal>("2016-09-01",1800),
                new KeyValuePair<string,decimal>("2016-10-01",1300)
            };
            var s = report.SetMSChartObject_Serie<string, decimal>("MSChart1", data, null);
            var chart = report.FindObject("MSChart1") as MSChartObject;
            chart.Series.Cast<MSChartSeries>().ToList().ForEach(x=> {
                var se = x;
            });
            

            report.SetZipCodeObject_Text("ZipCode1", "713300");
            report.Show();
        }

        public static void Test1()
        {
            var pathRoot = AppDomain.CurrentDomain.BaseDirectory;
            //var path = AppDomain.CurrentDomain.BaseDirectory + "Example.frx";

            //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //xml直接嵌入程序集 读取的方法
            System.IO.Stream sm = Assembly.GetExecutingAssembly().GetManifestResourceStream("FastReport.Net.Example1.frx");
            //doc.Load(sm); //直接将流转成xml

            byte[] bs = new byte[sm.Length];
            sm.Read(bs, 0, (int)sm.Length);
            sm.Close();
            UTF8Encoding con = new UTF8Encoding();
            string str = con.GetString(bs);//将流转成字符串


            Report report = new Report();
            report.LoadFromString(str);

            var data = new List<dynamic>();
            data.Add(new { Score1=30,Score2=45 });
            data.Add(new { Score1 = 30, Score2 = 45 });
            data.Add(new { Score1 = 30, Score2 = 45 });
            report.RegisterData(data, "object");

            

            report.Show();

        }
        /// <summary>
        /// WXY扩展-设置文本对象
        /// </summary>
        /// <param name="report"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetTextObject_Text(this Report report, string key, object value)
        {
            var obj = report.FindObject(key);
            if(obj!=null)
            {
                if(obj is TextObject)
                {
                    var textObj = obj as TextObject;
                    textObj.Text = value.ToString();
                }
            }
        }
        /// <summary>
        /// WXY扩展-设置图片对象-图片对象
        /// </summary>
        /// <param name="report"></param>
        /// <param name="key"></param>
        /// <param name="image">图片对象</param>
        public static void SetPictureObject_Text(this Report report, string key, Image image)
        {
            var obj = report.FindObject(key);
            if (obj != null)
            {
                if (obj is PictureObject)
                {
                    var picObj = obj as PictureObject;
                    picObj.Image = image;
                }
            }
        }
        /// <summary>
        /// WXY扩展-设置图片对象-图片本地路径
        /// </summary>
        /// <param name="report"></param>
        /// <param name="key"></param>
        /// <param name="path">图片路径</param>
        public static void SetPictureObject_Text(this Report report, string key, string path,bool isUrl=false)
        {
            var obj = report.FindObject(key);
            if (obj != null)
            {
                if (obj is PictureObject)
                {
                    var picObj = obj as PictureObject;
                    if(isUrl)
                    {

                    }
                    else
                        picObj.ImageLocation = path;
                }
            }
        }
        /// <summary>
        /// WXY扩展-设置邮编对象-邮编号
        /// </summary>
        /// <param name="report"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetZipCodeObject_Text(this Report report, string key, string value)
        {
            var obj = report.FindObject(key);
            if (obj != null)
            {
                if (obj is ZipCodeObject)
                {
                    var zipCodeObj = obj as ZipCodeObject;
                    zipCodeObj.Text = value;
                }
            }
        }

        /// <summary>
        /// WXY扩展-设置表格对象-数据
        /// </summary>
        /// <param name="report"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="Caption"></param>
        public static void SetTableObject_Data(this Report report, string key, DataTable data,bool Caption=true)
        {
            
            var obj = report.FindObject(key);
            if (obj != null)
            {
                if (obj is TableObject)
                {
                    var tbObj = obj as TableObject;
                    tbObj.Border.Lines = BorderLines.All;
                    if(Caption)
                    {
                        var rowCaption = new TableRow();
                        data.Columns.Cast<DataColumn>().ToList().ForEach(col =>
                        {
                            rowCaption.AddChild(new TableCell { Text = col.Caption, Border = new Border() { Lines=BorderLines.All }, HorzAlign=HorzAlign.Center,VertAlign=VertAlign.Center });
                        });
                        tbObj.Rows.Add(rowCaption);
                    }
                    
                    var rows = data.Rows.Cast<DataRow>().ToList();
                    rows.ForEach(row =>
                    {
                        var rowData = new TableRow();
                        foreach( var col in data.Columns)
                        {
                            var column = col as DataColumn;
                            rowData.AddChild(new TableCell { Text = row[column] == null ? "" : row[column].ToString(), Border = new Border() { Lines = BorderLines.All } });
                        }
                        tbObj.Rows.Add(rowData);
                    });
                }
            }
        }

        /// <summary>
        /// WXY扩展-设置二维码对象-默认条码类型
        /// </summary>
        /// <param name="report"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetBarcodeObject_Text(this Report report, string key, string value)
        {
            var obj = report.FindObject(key);
            if (obj != null)
            {
                if (obj is BarcodeObject)
                {
                    var picObj = obj as BarcodeObject;
                    picObj.Text = value;
                }
            }
        }
        /// <summary>
        /// WXY扩展-设置二维码对象-指定条码类型
        /// </summary>
        /// <param name="report"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="borcodetype"></param>
        public static BarcodeObject SetBarcodeObject_Text(this Report report, string key, string value, BarcodeBase borcodetype)
        {
            BarcodeObject barcode = null;
            var obj = report.FindObject(key);
            if (obj != null)
            {
                if (obj is BarcodeObject)
                {
                    barcode = obj as BarcodeObject;
                    barcode.Barcode = borcodetype;
                    barcode.Text = value;
                }
            }
            return barcode;
        }
        /// <summary>
        /// WXY扩展-图标系列-数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="report"></param>
        /// <param name="msChartKey"></param>
        /// <param name="data"></param>
        /// <param name="msChartSeriesKey"></param>
        /// <returns></returns>
        public static MSChartSeries  SetMSChartObject_Serie<TKey, TValue>(this Report report, string msChartKey, IEnumerable<KeyValuePair<TKey, TValue>> data, string  msChartSeriesKey)
        {
            MSChartSeries serie = null;
            var obj = report.FindObject(msChartKey);
            if (obj != null)
            {
                if (obj is MSChartObject)
                {
                    var chartObj = obj as MSChartObject;
                    
                    if (chartObj.Series!=null)
                    {
                        if (chartObj.Series.Count==1)
                        {
                            serie = chartObj.Series[0];
                        }
                        else
                        {
                            if(msChartSeriesKey!=null)
                            {
                                var serObj = chartObj.FindObject(msChartSeriesKey);
                                if (serObj != null)
                                {
                                    if (serObj is MSChartSeries)
                                    {
                                        serie = (MSChartSeries)serObj;
                                    }
                                }
                            }
                        }
                    }

                    if(serie!=null)
                    {
                        data.ToList().ForEach(x =>
                        {
                            chartObj.Series[0].AddValue(x.Key, x.Value);
                        });
                    }
                    
                }
            }
            return serie;
        }
    }
}
