using log4net;
using SocketTool.Core;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace SocketTool
{
	public class ClientForm : Form, ISocketInfo
	{
		private IContainer components;
		private Panel panel1;
		private RichTextBox rtSendData;
		private Label label4;
		private TextBox txtInterval;
		private Label label3;
		private CheckBox cbAutoSend;
		private TextBox txtPort;
		private Label label2;
		private TextBox txtIP;
		private Label label1;
		private ListView PacketView;
		private Button btnSend;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private Label label6;
		private ColumnHeader columnHeader4;
		private Button btnDisconnect;
		private GroupBox groupBox2;
		private RadioButton rbAscII;
		private RadioButton rbHex;
		private GroupBox groupBox1;
		private RadioButton rbUdp;
		private RadioButton rbTcp;
		private Button btnClearLog;
		private Label label8;
		private CheckBox cbLog;
		private Button btnOpenLog;
		private static ILog logger = LogManager.GetLogger(typeof(ClientForm));
		private IClient socketClient = new CommTcpClient();
		private Thread SendOutgoingThread;
		private int sendInterval;
		private bool IsAutoSend;
		private bool continueSend;
		private string sendContent;
		private string errorMsg = "";
		public SocketInfo SocketInfo
		{
			get;
			set;
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
			this.cbLog = new CheckBox();
			this.btnOpenLog = new Button();
			this.btnClearLog = new Button();
			this.label8 = new Label();
			this.groupBox2 = new GroupBox();
			this.rbAscII = new RadioButton();
			this.rbHex = new RadioButton();
			this.groupBox1 = new GroupBox();
			this.rbUdp = new RadioButton();
			this.rbTcp = new RadioButton();
			this.btnDisconnect = new Button();
			this.label6 = new Label();
			this.btnSend = new Button();
			this.rtSendData = new RichTextBox();
			this.label4 = new Label();
			this.txtInterval = new TextBox();
			this.label3 = new Label();
			this.cbAutoSend = new CheckBox();
			this.txtPort = new TextBox();
			this.label2 = new Label();
			this.txtIP = new TextBox();
			this.label1 = new Label();
			this.PacketView = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.cbLog);
			this.panel1.Controls.Add(this.btnOpenLog);
			this.panel1.Controls.Add(this.btnClearLog);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.btnDisconnect);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.btnSend);
			this.panel1.Controls.Add(this.rtSendData);
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
			this.panel1.Size = new Size(676, 449);
			this.panel1.TabIndex = 1;
			this.cbLog.AutoSize = true;
			this.cbLog.Checked = true;
			this.cbLog.CheckState = CheckState.Checked;
			this.cbLog.ForeColor = Color.Maroon;
			this.cbLog.Location = new Point(170, 179);
			this.cbLog.Name = "cbLog";
			this.cbLog.Size = new Size(156, 16);
			this.cbLog.TabIndex = 29;
			this.cbLog.Text = "保存数据到日志文件当中";
			this.cbLog.UseVisualStyleBackColor = true;
			this.btnOpenLog.Location = new Point(340, 174);
			this.btnOpenLog.Name = "btnOpenLog";
			this.btnOpenLog.Size = new Size(96, 25);
			this.btnOpenLog.TabIndex = 28;
			this.btnOpenLog.Text = "打开日志目录";
			this.btnOpenLog.UseVisualStyleBackColor = true;
			this.btnOpenLog.Click += new EventHandler(this.btnOpenLog_Click);
			this.btnClearLog.Location = new Point(80, 174);
			this.btnClearLog.Name = "btnClearLog";
			this.btnClearLog.Size = new Size(75, 25);
			this.btnClearLog.TabIndex = 27;
			this.btnClearLog.Text = "清空日志";
			this.btnClearLog.UseVisualStyleBackColor = true;
			this.btnClearLog.Click += new EventHandler(this.btnClearLog_Click);
			this.label8.AutoSize = true;
			this.label8.Location = new Point(9, 180);
			this.label8.Name = "label8";
			this.label8.Size = new Size(65, 12);
			this.label8.TabIndex = 26;
			this.label8.Text = "接收数据：";
			this.groupBox2.Controls.Add(this.rbAscII);
			this.groupBox2.Controls.Add(this.rbHex);
			this.groupBox2.Location = new Point(26, 48);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(200, 29);
			this.groupBox2.TabIndex = 25;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "数据格式";
			this.rbAscII.AutoSize = true;
			this.rbAscII.Checked = true;
			this.rbAscII.Location = new Point(57, 10);
			this.rbAscII.Name = "rbAscII";
			this.rbAscII.Size = new Size(65, 16);
			this.rbAscII.TabIndex = 4;
			this.rbAscII.TabStop = true;
			this.rbAscII.Text = "ASCII码";
			this.rbAscII.UseVisualStyleBackColor = true;
			this.rbHex.AutoSize = true;
			this.rbHex.Location = new Point(128, 10);
			this.rbHex.Name = "rbHex";
			this.rbHex.Size = new Size(59, 16);
			this.rbHex.TabIndex = 5;
			this.rbHex.Text = "16进制";
			this.rbHex.UseVisualStyleBackColor = true;
			this.groupBox1.Controls.Add(this.rbUdp);
			this.groupBox1.Controls.Add(this.rbTcp);
			this.groupBox1.Location = new Point(268, 48);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(172, 28);
			this.groupBox1.TabIndex = 24;
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
			this.btnDisconnect.Enabled = false;
			this.btnDisconnect.Location = new Point(407, 87);
			this.btnDisconnect.Name = "btnDisconnect";
			this.btnDisconnect.Size = new Size(75, 25);
			this.btnDisconnect.TabIndex = 17;
			this.btnDisconnect.Text = "断开连接";
			this.btnDisconnect.UseVisualStyleBackColor = true;
			this.btnDisconnect.Click += new EventHandler(this.btnDisconnect_Click);
			this.label6.AutoSize = true;
			this.label6.Location = new Point(8, 138);
			this.label6.Name = "label6";
			this.label6.Size = new Size(53, 12);
			this.label6.TabIndex = 13;
			this.label6.Text = "数据包：";
			this.btnSend.Location = new Point(320, 87);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new Size(75, 25);
			this.btnSend.TabIndex = 11;
			this.btnSend.Text = "发送数据";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new EventHandler(this.btnSend_Click);
			this.rtSendData.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.rtSendData.Location = new Point(61, 118);
			this.rtSendData.Name = "rtSendData";
			this.rtSendData.Size = new Size(612, 50);
			this.rtSendData.TabIndex = 10;
			this.rtSendData.Text = "曾经为还";
			this.label4.AutoSize = true;
			this.label4.Location = new Point(221, 93);
			this.label4.Name = "label4";
			this.label4.Size = new Size(77, 12);
			this.label4.TabIndex = 9;
			this.label4.Text = "秒钟发送一次";
			this.txtInterval.Enabled = false;
			this.txtInterval.Location = new Point(176, 87);
			this.txtInterval.Name = "txtInterval";
			this.txtInterval.Size = new Size(39, 21);
			this.txtInterval.TabIndex = 8;
			this.txtInterval.Text = "1";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(141, 93);
			this.label3.Name = "label3";
			this.label3.Size = new Size(29, 12);
			this.label3.TabIndex = 7;
			this.label3.Text = "每隔";
			this.cbAutoSend.AutoSize = true;
			this.cbAutoSend.Location = new Point(61, 92);
			this.cbAutoSend.Name = "cbAutoSend";
			this.cbAutoSend.Size = new Size(72, 16);
			this.cbAutoSend.TabIndex = 6;
			this.cbAutoSend.Text = "自动发送";
			this.cbAutoSend.UseVisualStyleBackColor = true;
			this.cbAutoSend.CheckedChanged += new EventHandler(this.cbAutoSend_CheckedChanged);
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
			this.PacketView.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.PacketView.BackColor = SystemColors.Menu;
			this.PacketView.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader3,
				this.columnHeader4
			});
			this.PacketView.Font = new Font("宋体", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 134);
			this.PacketView.FullRowSelect = true;
			this.PacketView.GridLines = true;
			this.PacketView.LabelEdit = true;
			this.PacketView.Location = new Point(2, 201);
			this.PacketView.Name = "PacketView";
			this.PacketView.Size = new Size(671, 236);
			this.PacketView.TabIndex = 2;
			this.PacketView.UseCompatibleStateImageBehavior = false;
			this.PacketView.View = View.Details;
			this.columnHeader1.Text = "序号";
			this.columnHeader1.Width = 44;
			this.columnHeader2.Text = "时间";
			this.columnHeader2.Width = 71;
			this.columnHeader3.Text = "数据报文";
			this.columnHeader3.Width = 426;
			this.columnHeader4.Text = "字节数";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(676, 449);
			base.Controls.Add(this.PacketView);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ClientForm";
			this.Text = "客户端";
			base.Load += new EventHandler(this.ClientForm_Load);
			base.FormClosing += new FormClosingEventHandler(this.ClientForm_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}
		public ClientForm()
		{
			this.InitializeComponent();
			this.SocketInfo = new SocketInfo();
		}
		private void btnSend_Click(object sender, EventArgs e)
		{
			if (this.rbUdp.Checked)
			{
				this.socketClient = new CommUdpClient();
			}
			this.socketClient.OnDataReceived += new ReceivedHandler(this.ListenMessage);
			this.socketClient.OnSocketError += new SocketErrorHandler(this.ListenErrorMessage);
			string text = this.txtIP.Text;
			this.errorMsg = "";
			if (string.IsNullOrEmpty(text))
			{
				this.errorMsg += "请输入合法的IP地址";
			}
			try
			{
				int port = int.Parse(this.txtPort.Text);
				this.socketClient.Init(text, port);
			}
			catch (Exception)
			{
				this.errorMsg += "请输入合法的端口";
			}
			this.sendContent = this.rtSendData.Text;
			if (string.IsNullOrEmpty(this.sendContent))
			{
				this.errorMsg += "请输入要发送的内容";
			}
			if (this.cbAutoSend.Checked)
			{
				try
				{
					this.sendInterval = int.Parse(this.txtInterval.Text) * 1000;
				}
				catch (Exception)
				{
					this.errorMsg += "请输入整数的发送时间间隔";
				}
				this.IsAutoSend = true;
			}
			if (!string.IsNullOrEmpty(this.errorMsg))
			{
				MessageBox.Show(this.errorMsg);
				return;
			}
			this.continueSend = true;
			this.btnDisconnect.Enabled = true;
			this.btnSend.Enabled = !this.IsAutoSend;
			this.SendOutgoingThread = new Thread(new ThreadStart(this.SendThreadFunc));
			this.SendOutgoingThread.Start();
		}
		private void SendThreadFunc()
		{
			while (this.continueSend)
			{
				byte[] data = Encoding.Default.GetBytes(this.sendContent);
				if (this.rbHex.Checked)
				{
					data = ParseUtil.ToByesByHex(this.sendContent);
				}
				try
				{
					this.socketClient.Send(data);
				}
				catch (Exception ex)
				{
					this.ListenMessage(0, "", ex.Message);
					break;
				}
				if (!this.IsAutoSend)
				{
					return;
				}
				Thread.Sleep(this.sendInterval);
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
			this.ListenMessage((int)o, "Socket错误", msg);
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
						0,
						type,
						msg
					});
					return;
				}
				catch (Exception ex)
				{
					ClientForm.logger.Error(ex.Message);
					ClientForm.logger.Error(ex.StackTrace);
					return;
				}
			}
			if (type == "Socket错误")
			{
				this.continueSend = false;
				this.btnDisconnect.Enabled = false;
				this.btnSend.Enabled = true;
			}
			if (this.PacketView.Items.Count > 200)
			{
				this.PacketView.Items.Clear();
			}
			ListViewItem listViewItem = this.PacketView.Items.Insert(0, string.Concat(this.PacketView.Items.Count));
			string text = DateTime.Now.ToString("HH:mm:ss");
			listViewItem.SubItems.Add(text);
			listViewItem.SubItems.Add(msg);
		}
		public void ListenMessage(object o, ReceivedEventArgs e)
		{
			if (this.PacketView.InvokeRequired)
			{
				try
				{
					ReceivedHandler method = new ReceivedHandler(this.ListenMessage);
					base.Invoke(method, new object[]
					{
						o,
						e
					});
					return;
				}
				catch (Exception ex)
				{
					ClientForm.logger.Error(ex.Message);
					ClientForm.logger.Error(ex.StackTrace);
					return;
				}
			}
			if (this.PacketView.Items.Count > 200)
			{
				this.PacketView.Items.Clear();
			}
			ListViewItem listViewItem = this.PacketView.Items.Insert(0, string.Concat(this.PacketView.Items.Count));
			int num = e.Data.Length;
			string text = DateTime.Now.ToString("HH:mm:ss");
			listViewItem.SubItems.Add(text);
			string text2 = ParseUtil.ParseString(e.Data, num);
			if (this.rbHex.Checked)
			{
				text2 = ParseUtil.ToHexString(e.Data, num);
			}
			listViewItem.SubItems.Add(text2);
			listViewItem.SubItems.Add(string.Concat(num));
			if (this.cbLog.Checked)
			{
				ClientForm.logger.Info(e.RemoteHost.ToString() + " " + text2);
			}
		}
		private void cbAutoSend_CheckedChanged(object sender, EventArgs e)
		{
			this.txtInterval.Enabled = this.cbAutoSend.Checked;
		}
		private void btnDisconnect_Click(object sender, EventArgs e)
		{
			this.continueSend = false;
			try
			{
				if (this.socketClient != null)
				{
					this.socketClient.Close();
				}
				this.SendOutgoingThread.Abort();
			}
			catch (Exception)
			{
			}
			this.btnSend.Enabled = true;
		}
		private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
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
			this.SocketInfo.Format = (this.rbAscII.Checked ? "AscII" : "Hex");
			this.SocketInfo.Types = "Client";
			this.SocketInfo.ServerIp = this.txtIP.Text;
			this.SocketInfo.Data = this.rtSendData.Text;
			this.SocketInfo.IsAuto = this.cbAutoSend.Checked;
			try
			{
				this.SocketInfo.Port = int.Parse(this.txtPort.Text);
			}
			catch (Exception)
			{
			}
		}
		private void btnClearLog_Click(object sender, EventArgs e)
		{
			this.PacketView.Clear();
		}
		private void ClientForm_Load(object sender, EventArgs e)
		{
			this.txtIP.Text = this.SocketInfo.ServerIp;
			this.rbTcp.Checked = (this.SocketInfo.Protocol == "Tcp");
			this.rbUdp.Checked = (this.SocketInfo.Protocol != "Tcp");
			this.rbAscII.Checked = (this.SocketInfo.Format == "AscII");
			this.rbHex.Checked = (this.SocketInfo.Format != "AscII");
			this.txtPort.Text = string.Concat(this.SocketInfo.Port);
			this.rtSendData.Text = this.SocketInfo.Data;
			this.cbAutoSend.Checked = this.SocketInfo.IsAuto;
		}
		private void btnOpenLog_Click(object sender, EventArgs e)
		{
			Process.Start("notepad.exe", "client.log");
		}
	}
}
