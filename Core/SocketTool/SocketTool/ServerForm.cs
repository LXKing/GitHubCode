using log4net;
using SocketTool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace SocketTool
{
	public class ServerForm : Form, ISocketInfo
	{
        private RadioButton rbUTF8;
		private static ILog logger = LogManager.GetLogger(typeof(ServerForm));
		private IServer commServer = new CommTcpServer();
		private Thread refreshThread;
		private bool continueRefresh = true;
		private List<IConnection> conns = new List<IConnection>();
		private string selectedConnectionID;
		private IContainer components;
		private Panel panel1;
		private RichTextBox rtbData;
		private Label label4;
		private TextBox txtInterval;
		private Label label3;
		private CheckBox cbAutoSend;
		private RadioButton rbHex;
		private RadioButton rbAscII;
		private TextBox txtPort;
		private Label label2;
		private TextBox txtIP;
		private Label label1;
		private ListView PacketView;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private Label label6;
		private RadioButton rbUdp;
		private RadioButton rbTcp;
		private ColumnHeader columnHeader4;
		private Button btnStopListen;
		private Button btnListen;
		private Button btnClearLog;
		private ColumnHeader columnHeader5;
		private ColumnHeader columnHeader6;
		private GroupBox groupBox2;
		private GroupBox groupBox1;
		private TabControl tbConnections;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private ListView connectionView;
		private ColumnHeader columnHeader7;
		private ColumnHeader columnHeader8;
		private ColumnHeader columnHeader10;
		private ColumnHeader columnHeader9;
		private ColumnHeader columnHeader11;
		private BackgroundWorker refreshConnectionWorker;
		private RichTextBox richTextBox2;
		private Button btnDisconnect;
		private Button btnSend;
		private TextBox txtConn;
		private Label label5;
		private Button btnOpenLog;
		private CheckBox cbLog;
		public SocketInfo SocketInfo
		{
			get;
			set;
		}
		public ServerForm()
		{
			this.InitializeComponent();
			this.SocketInfo = new SocketInfo();
		}
		private void btnSend_Click(object sender, EventArgs e)
		{
		}
		private void btnListen_Click(object sender, EventArgs e)
		{
			if (this.rbUdp.Checked)
			{
				this.commServer = new CommUdpServer();
			}
			int num = int.Parse(this.txtPort.Text);
			this.commServer.Init(null, num);
			this.commServer.OnDataReceived += new ReceivedHandler(this.ListenMessage);
			this.commServer.OnSocketError += new SocketErrorHandler(this.ListenErrorMessage);
			if (!this.refreshConnectionWorker.IsBusy)
			{
				this.refreshConnectionWorker.RunWorkerAsync();
			}
			try
			{
				this.commServer.Listen();
				this.btnListen.Enabled = false;
				this.btnStopListen.Enabled = true;
				this.ListenMessage(0, "", "启动监听成功,端口:" + num);
			}
			catch (Exception)
			{
				MessageBox.Show("监听失败，端口可能被占用");
			}
		}
		public void ListenErrorMessage(object o, SocketEventArgs e)
		{
			string msg = string.Concat(new object[]
			{
				"[",
				e.ErrorCode,
				"]",
				SocketUtil.DescrError(e.ErrorCode)
			});
			this.ListenMessage(o, "Socket错误", msg);
		}
		private void ListenMessage(object ID, string type, string msg)
		{
			if (this.PacketView.InvokeRequired)
			{
				try
				{
					MsgHandler method = new MsgHandler(this.ListenMessage);
					base.Invoke(method, new object[]
					{
						ID,
						type,
						msg
					});
					return;
				}
				catch (Exception ex)
				{
					ServerForm.logger.Error(ex.Message);
					ServerForm.logger.Error(ex.StackTrace);
					return;
				}
			}
			if (this.PacketView.Items.Count > 200)
			{
				this.PacketView.Items.Clear();
			}
			ListViewItem listViewItem = this.PacketView.Items.Insert(0, string.Concat(this.PacketView.Items.Count));
			listViewItem.SubItems.Add(string.Concat(ID));
			listViewItem.SubItems.Add("");
			string text = DateTime.Now.ToString("HH:mm:ss");
			listViewItem.SubItems.Add(text);
			listViewItem.SubItems.Add(msg);
		}
		public void ListenMessage(object o, ReceivedEventArgs e)
		{
			if (this.PacketView.InvokeRequired)
			{
				ReceivedHandler method = new ReceivedHandler(this.ListenMessage);
				base.Invoke(method, new object[]
				{
					o,
					e
				});
				return;
			}
			byte[] data = e.Data;
			int num = data.Length;
			bool @checked = this.cbAutoSend.Checked;
			string text = string.Concat(o);
			if (@checked)
			{
				this.commServer.Send(text, data, data.Length);
			}
			if (this.PacketView.Items.Count > 200)
			{
				this.PacketView.Items.Clear();
			}
			ListViewItem listViewItem = this.PacketView.Items.Insert(0, string.Concat(this.PacketView.Items.Count));
			listViewItem.SubItems.Add(text ?? "");
			listViewItem.SubItems.Add(e.RemoteHost.ToString() ?? "");
			string text2 = ParseUtil.ParseString(data, num);
			if (this.rbUTF8.Checked)
			{
				text2 = ParseUtil.ParseStringByUTF8(data, num);
			}
            else if (this.rbHex.Checked)
            {
                text2 = ParseUtil.ToHexString(data, num);
            }
			string text3 = DateTime.Now.ToString("HH:mm:ss");
			listViewItem.SubItems.Add(text3);
			listViewItem.SubItems.Add(text2);
			listViewItem.SubItems.Add(string.Concat(num));
			if (this.cbLog.Checked)
			{
				ServerForm.logger.Info(e.RemoteHost.ToString() + " " + text2);
			}
		}
		private void btnStopListen_Click(object sender, EventArgs e)
		{
			this.commServer.Close();
			this.btnListen.Enabled = true;
			this.btnStopListen.Enabled = false;
		}
		private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.SocketInfo.ServerIp = this.txtIP.Text;
			try
			{
				this.SocketInfo.Port = int.Parse(this.txtPort.Text);
			}
			catch (Exception)
			{
			}
			this.SocketInfo.Protocol = (this.rbTcp.Checked ? "Tcp" : "Udp");
            this.SocketInfo.Format = this.rbUTF8.Checked ? "UTF-8" : "AscII";
            this.SocketInfo.Format = this.rbAscII.Checked ? "AscII" : "Hex";
            this.SocketInfo.Format = this.rbHex.Checked ? "Hex" : "Hex";

			this.SocketInfo.Types = "Server";
			this.SocketInfo.Data = this.rtbData.Text;
			this.SocketInfo.IsAuto = this.cbAutoSend.Checked;
			this.refreshConnectionWorker.CancelAsync();
			this.commServer.Close();
		}
		private void btnClearLog_Click(object sender, EventArgs e)
		{
			this.PacketView.Items.Clear();
		}
		private void refreshConnectionWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			int percentProgress = 0;
			while (this.continueRefresh)
			{
				this.conns = this.commServer.GetConnectionList();
				this.refreshConnectionWorker.ReportProgress(percentProgress);
				Thread.Sleep(1000);
			}
		}
		private void refreshConnectionWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			try
			{
				this.connectionView.Items.Clear();
				foreach (IConnection current in this.conns)
				{
					ListViewItem listViewItem = this.connectionView.Items.Insert(0, string.Concat(this.connectionView.Items.Count));
					listViewItem.Tag = current.ID;
					listViewItem.SubItems.Add(current.ID ?? "");
					DateTime.Now.ToString("dd HH:mm:ss");
					listViewItem.SubItems.Add(current.CreateDate.ToString("dd HH:mm:ss"));
					listViewItem.SubItems.Add(current.ClientIP.ToString());
					listViewItem.SubItems.Add(current.OnlineDate.ToString("dd HH:mm:ss"));
				}
			}
			catch (Exception ex)
			{
				ServerForm.logger.Error(ex.Message);
				ServerForm.logger.Error(ex.StackTrace);
			}
		}
		private void connectionView_SelectedIndexChanged(object sender, EventArgs e)
		{
			foreach (ListViewItem listViewItem in this.connectionView.SelectedItems)
			{
				this.selectedConnectionID = string.Concat(listViewItem.Tag);
				this.txtConn.Text = listViewItem.SubItems[3].Text;
			}
		}
		private void PacketView_SelectedIndexChanged(object sender, EventArgs e)
		{
			foreach (ListViewItem listViewItem in this.PacketView.SelectedItems)
			{
				this.selectedConnectionID = string.Concat(listViewItem.Tag);
				this.rtbData.Text = listViewItem.SubItems[4].Text;
			}
		}
		private void ServerForm_Load(object sender, EventArgs e)
		{
			this.txtIP.Text = this.SocketInfo.ServerIp;
			this.rbTcp.Checked = (this.SocketInfo.Protocol == "Tcp");
			this.rbUdp.Checked = (this.SocketInfo.Protocol != "Tcp");
            this.rbUTF8.Checked = (this.SocketInfo.Format == "UTF-8");
			this.rbAscII.Checked = (this.SocketInfo.Format == "AscII");
			this.rbHex.Checked = (this.SocketInfo.Format == "Hex");

			this.txtPort.Text = string.Concat(this.SocketInfo.Port);
			this.cbAutoSend.Checked = this.SocketInfo.IsAuto;
			this.rtbData.Text = this.SocketInfo.Data;
		}
		private void btnOpenLog_Click(object sender, EventArgs e)
		{
			Process.Start("notepad.exe", "server.log");
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.panel1 = new Panel();
			this.tbConnections = new TabControl();
			this.tabPage1 = new TabPage();
			this.cbLog = new CheckBox();
			this.btnOpenLog = new Button();
			this.PacketView = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.btnClearLog = new Button();
			this.tabPage2 = new TabPage();
			this.btnDisconnect = new Button();
			this.btnSend = new Button();
			this.txtConn = new TextBox();
			this.label5 = new Label();
			this.richTextBox2 = new RichTextBox();
			this.connectionView = new ListView();
			this.columnHeader7 = new ColumnHeader();
			this.columnHeader8 = new ColumnHeader();
			this.columnHeader10 = new ColumnHeader();
			this.columnHeader9 = new ColumnHeader();
			this.columnHeader11 = new ColumnHeader();
			this.groupBox2 = new GroupBox();
			this.rbAscII = new RadioButton();
			this.rbHex = new RadioButton();
            this.rbUTF8 = new RadioButton();
			this.groupBox1 = new GroupBox();
			this.rbUdp = new RadioButton();
			this.rbTcp = new RadioButton();
			this.btnStopListen = new Button();
			this.btnListen = new Button();
			this.label6 = new Label();
			this.rtbData = new RichTextBox();
			this.label4 = new Label();
			this.txtInterval = new TextBox();
			this.label3 = new Label();
			this.cbAutoSend = new CheckBox();
			this.txtPort = new TextBox();
			this.label2 = new Label();
			this.txtIP = new TextBox();
			this.label1 = new Label();
			this.refreshConnectionWorker = new BackgroundWorker();
			this.panel1.SuspendLayout();
			this.tbConnections.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.tbConnections);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.btnStopListen);
			this.panel1.Controls.Add(this.btnListen);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.rtbData);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.txtInterval);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.cbAutoSend);
			this.panel1.Controls.Add(this.txtPort);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.txtIP);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(905, 449);
			this.panel1.TabIndex = 1;
			this.tbConnections.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.tbConnections.Controls.Add(this.tabPage1);
			this.tbConnections.Controls.Add(this.tabPage2);
			this.tbConnections.Location = new Point(0, 180);
			this.tbConnections.Name = "tbConnections";
			this.tbConnections.SelectedIndex = 0;
			this.tbConnections.Size = new Size(902, 266);
			this.tbConnections.TabIndex = 24;
			this.tabPage1.Controls.Add(this.cbLog);
			this.tabPage1.Controls.Add(this.btnOpenLog);
			this.tabPage1.Controls.Add(this.PacketView);
			this.tabPage1.Controls.Add(this.btnClearLog);
			this.tabPage1.Location = new Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new Padding(3);
			this.tabPage1.Size = new Size(884, 241);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "接收数据";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.cbLog.AutoSize = true;
			this.cbLog.Checked = true;
			this.cbLog.CheckState = CheckState.Checked;
			this.cbLog.ForeColor = Color.Maroon;
			this.cbLog.Location = new Point(106, 11);
			this.cbLog.Name = "cbLog";
			this.cbLog.Size = new Size(156, 16);
			this.cbLog.TabIndex = 24;
			this.cbLog.Text = "保存数据到日志文件当中";
			this.cbLog.UseVisualStyleBackColor = true;
			this.btnOpenLog.Location = new Point(276, 6);
			this.btnOpenLog.Name = "btnOpenLog";
			this.btnOpenLog.Size = new Size(96, 25);
			this.btnOpenLog.TabIndex = 23;
			this.btnOpenLog.Text = "打开日志目录";
			this.btnOpenLog.UseVisualStyleBackColor = true;
			this.btnOpenLog.Click += new EventHandler(this.btnOpenLog_Click);
			this.PacketView.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.PacketView.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader5,
				this.columnHeader6,
				this.columnHeader2,
				this.columnHeader3,
				this.columnHeader4
			});
			this.PacketView.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.PacketView.FullRowSelect = true;
			this.PacketView.Location = new Point(3, 36);
			this.PacketView.Name = "PacketView";
			this.PacketView.Size = new Size(881, 202);
			this.PacketView.TabIndex = 2;
			this.PacketView.UseCompatibleStateImageBehavior = false;
			this.PacketView.View = View.Details;
			this.PacketView.SelectedIndexChanged += new EventHandler(this.PacketView_SelectedIndexChanged);
			this.columnHeader1.Text = "序号";
			this.columnHeader1.Width = 46;
			this.columnHeader5.Text = "连接ID";
			this.columnHeader6.DisplayIndex = 3;
			this.columnHeader6.Text = "IP地址";
			this.columnHeader6.TextAlign = HorizontalAlignment.Center;
			this.columnHeader6.Width = 118;
			this.columnHeader2.DisplayIndex = 2;
			this.columnHeader2.Text = "时间";
			this.columnHeader2.Width = 72;
			this.columnHeader3.Text = "数据报文";
			this.columnHeader3.Width = 520;
			this.columnHeader4.Text = "字节数";
			this.btnClearLog.Location = new Point(13, 6);
			this.btnClearLog.Name = "btnClearLog";
			this.btnClearLog.Size = new Size(75, 25);
			this.btnClearLog.TabIndex = 21;
			this.btnClearLog.Text = "清空日志";
			this.btnClearLog.UseVisualStyleBackColor = true;
			this.btnClearLog.Click += new EventHandler(this.btnClearLog_Click);
			this.tabPage2.Controls.Add(this.btnDisconnect);
			this.tabPage2.Controls.Add(this.btnSend);
			this.tabPage2.Controls.Add(this.txtConn);
			this.tabPage2.Controls.Add(this.label5);
			this.tabPage2.Controls.Add(this.richTextBox2);
			this.tabPage2.Controls.Add(this.connectionView);
			this.tabPage2.Location = new Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new Padding(3);
			this.tabPage2.Size = new Size(894, 241);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "当前客户端连接";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.btnDisconnect.Location = new Point(557, 10);
			this.btnDisconnect.Name = "btnDisconnect";
			this.btnDisconnect.Size = new Size(75, 25);
			this.btnDisconnect.TabIndex = 28;
			this.btnDisconnect.Text = "断开连接";
			this.btnDisconnect.UseVisualStyleBackColor = true;
			this.btnSend.Location = new Point(470, 10);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new Size(75, 25);
			this.btnSend.TabIndex = 27;
			this.btnSend.Text = "发送数据";
			this.btnSend.UseVisualStyleBackColor = true;
			this.txtConn.Enabled = false;
			this.txtConn.Location = new Point(70, 13);
			this.txtConn.Name = "txtConn";
			this.txtConn.Size = new Size(127, 21);
			this.txtConn.TabIndex = 26;
			this.txtConn.Text = "127.0.0.1";
			this.label5.AutoSize = true;
			this.label5.Location = new Point(4, 17);
			this.label5.Name = "label5";
			this.label5.Size = new Size(65, 12);
			this.label5.TabIndex = 25;
			this.label5.Text = "当前连接：";
			this.richTextBox2.Location = new Point(199, 13);
			this.richTextBox2.Name = "richTextBox2";
			this.richTextBox2.Size = new Size(265, 21);
			this.richTextBox2.TabIndex = 11;
			this.richTextBox2.Text = "请选择某一连接后发送数据";
			this.connectionView.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.connectionView.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader7,
				this.columnHeader8,
				this.columnHeader10,
				this.columnHeader9,
				this.columnHeader11
			});
			this.connectionView.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.connectionView.FullRowSelect = true;
			this.connectionView.Location = new Point(3, 41);
			this.connectionView.Name = "connectionView";
			this.connectionView.Size = new Size(891, 197);
			this.connectionView.TabIndex = 3;
			this.connectionView.UseCompatibleStateImageBehavior = false;
			this.connectionView.View = View.Details;
			this.connectionView.SelectedIndexChanged += new EventHandler(this.connectionView_SelectedIndexChanged);
			this.columnHeader7.Text = "序号";
			this.columnHeader8.Text = "连接ID";
			this.columnHeader8.Width = 83;
			this.columnHeader10.Text = "创建时间";
			this.columnHeader10.Width = 110;
			this.columnHeader9.Text = "IP地址";
			this.columnHeader9.TextAlign = HorizontalAlignment.Center;
			this.columnHeader9.Width = 123;
			this.columnHeader11.Text = "在线时间";
			this.columnHeader11.Width = 333;
            this.groupBox2.Controls.Add(this.rbUTF8);
			this.groupBox2.Controls.Add(this.rbAscII);
			this.groupBox2.Controls.Add(this.rbHex);
			this.groupBox2.Location = new Point(48, 48);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(240, 29);
			this.groupBox2.TabIndex = 23;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "显示格式";

            this.rbUTF8.AutoSize = true;
            this.rbUTF8.Checked = true;
            this.rbUTF8.Location = new Point(27, 10);
            this.rbUTF8.Name = "rbUTF8";
            this.rbUTF8.Size = new Size(65, 30);
            this.rbUTF8.TabIndex = 4;
            this.rbUTF8.TabStop = true;
            this.rbUTF8.Text = "UTF-8码";
            this.rbUTF8.UseVisualStyleBackColor = true;

			this.rbAscII.AutoSize = true;
			this.rbAscII.Location = new Point(107, 10);
			this.rbAscII.Name = "rbAscII";
			this.rbAscII.Size = new Size(65, 16);
			this.rbAscII.TabIndex = 4;
			this.rbAscII.TabStop = true;
			this.rbAscII.Text = "ASCII码";
			this.rbAscII.UseVisualStyleBackColor = true;

			this.rbHex.AutoSize = true;
			this.rbHex.Location = new Point(178, 10);
			this.rbHex.Name = "rbHex";
			this.rbHex.Size = new Size(59, 16);
			this.rbHex.TabIndex = 5;
			this.rbHex.Text = "16进制";
			this.rbHex.UseVisualStyleBackColor = true;

			this.groupBox1.Controls.Add(this.rbUdp);
			this.groupBox1.Controls.Add(this.rbTcp);
			this.groupBox1.Location = new Point(350, 48);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(172, 28);
			this.groupBox1.TabIndex = 22;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "通信协议";
			this.rbUdp.AutoSize = true;
			this.rbUdp.Location = new Point(102, 10);
			this.rbUdp.Name = "rbUdp";
			this.rbUdp.Size = new Size(41, 16);
			this.rbUdp.TabIndex = 15;
			this.rbUdp.Text = "UDP";
			this.rbUdp.UseVisualStyleBackColor = true;
			this.rbTcp.AutoSize = true;
			this.rbTcp.Checked = true;
			this.rbTcp.Location = new Point(55, 10);
			this.rbTcp.Name = "rbTcp";
			this.rbTcp.Size = new Size(41, 16);
			this.rbTcp.TabIndex = 14;
			this.rbTcp.TabStop = true;
			this.rbTcp.Text = "TCP";
			this.rbTcp.UseVisualStyleBackColor = true;
			this.btnStopListen.Location = new Point(387, 17);
			this.btnStopListen.Name = "btnStopListen";
			this.btnStopListen.Size = new Size(75, 25);
			this.btnStopListen.TabIndex = 20;
			this.btnStopListen.Text = "停止监听";
			this.btnStopListen.UseVisualStyleBackColor = true;
			this.btnStopListen.Click += new EventHandler(this.btnStopListen_Click);
			this.btnListen.Location = new Point(290, 17);
			this.btnListen.Name = "btnListen";
			this.btnListen.Size = new Size(75, 25);
			this.btnListen.TabIndex = 19;
			this.btnListen.Text = "开始监听";
			this.btnListen.UseVisualStyleBackColor = true;
			this.btnListen.Click += new EventHandler(this.btnListen_Click);
			this.label6.AutoSize = true;
			this.label6.ForeColor = Color.Maroon;
			this.label6.Location = new Point(304, 93);
			this.label6.Name = "label6";
			this.label6.Size = new Size(287, 12);
			this.label6.TabIndex = 13;
			this.label6.Text = "应答数据包(默认是原文回复，如要定制回复请填写：";
			this.rtbData.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.rtbData.Location = new Point(61, 118);
			this.rtbData.Name = "rtbData";
			this.rtbData.Size = new Size(832, 56);
			this.rtbData.TabIndex = 10;
			this.rtbData.Text = "";
			this.label4.AutoSize = true;
			this.label4.Location = new Point(221, 93);
			this.label4.Name = "label4";
			this.label4.Size = new Size(77, 12);
			this.label4.TabIndex = 9;
			this.label4.Text = "秒钟发送一次";
			this.txtInterval.Location = new Point(176, 87);
			this.txtInterval.Name = "txtInterval";
			this.txtInterval.Size = new Size(39, 21);
			this.txtInterval.TabIndex = 8;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(141, 93);
			this.label3.Name = "label3";
			this.label3.Size = new Size(29, 12);
			this.label3.TabIndex = 7;
			this.label3.Text = "每隔";
			this.cbAutoSend.AutoSize = true;
			this.cbAutoSend.Checked = true;
			this.cbAutoSend.CheckState = CheckState.Checked;
			this.cbAutoSend.Location = new Point(61, 91);
			this.cbAutoSend.Name = "cbAutoSend";
			this.cbAutoSend.Size = new Size(72, 16);
			this.cbAutoSend.TabIndex = 6;
			this.cbAutoSend.Text = "自动应答";
			this.cbAutoSend.UseVisualStyleBackColor = true;
			this.txtPort.Location = new Point(209, 20);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new Size(39, 21);
			this.txtPort.TabIndex = 3;
			this.txtPort.Text = "8899";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(174, 26);
			this.label2.Name = "label2";
			this.label2.Size = new Size(41, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "端口：";
			this.txtIP.Location = new Point(61, 20);
			this.txtIP.Name = "txtIP";
			this.txtIP.Size = new Size(100, 21);
			this.txtIP.TabIndex = 1;
			this.txtIP.Text = "127.0.0.1";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(25, 23);
			this.label1.Name = "label1";
			this.label1.Size = new Size(29, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "IP：";
			this.refreshConnectionWorker.WorkerReportsProgress = true;
			this.refreshConnectionWorker.WorkerSupportsCancellation = true;
			this.refreshConnectionWorker.DoWork += new DoWorkEventHandler(this.refreshConnectionWorker_DoWork);
			this.refreshConnectionWorker.ProgressChanged += new ProgressChangedEventHandler(this.refreshConnectionWorker_ProgressChanged);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(905, 449);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ServerForm";
			this.Text = "客户端";
			base.Load += new EventHandler(this.ServerForm_Load);
			base.FormClosing += new FormClosingEventHandler(this.ServerForm_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tbConnections.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
