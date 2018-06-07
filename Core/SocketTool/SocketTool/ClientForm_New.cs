using log4net;
using SocketTool.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SocketTool
{
    public partial class ClientForm_New : Form, ISocketInfo
    {
        public static ILog logger = LogManager.GetLogger(typeof(ClientForm_New));
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
        public ClientForm_New()
        {
            InitializeComponent();

            this.SocketInfo = new SocketInfo();
        }
        private void ClientForm_Load(object sender, EventArgs e)
        {
            this.txtIP.Text = this.SocketInfo.ServerIp;
            this.rbTcp.Checked = (this.SocketInfo.Protocol == "Tcp");
            this.rbUdp.Checked = (this.SocketInfo.Protocol != "Tcp");


            this.rbUTF8.Checked = (this.SocketInfo.Format == "UTF-8");
            this.rbAscII.Checked = (this.SocketInfo.Format == "AscII");
            this.rbHex.Checked = (this.SocketInfo.Format == "Hex");

            this.txtPort.Text = string.Concat(this.SocketInfo.Port);
            this.rtSendData.Text = this.SocketInfo.Data;
            this.cbAutoSend.Checked = this.SocketInfo.IsAuto;
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
				}else
                if(this.rbUTF8.Checked)
                {
                    data = System.Text.Encoding.UTF8.GetBytes(this.sendContent);
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
					logger.Error(ex.Message);
					logger.Error(ex.StackTrace);
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
					logger.Error(ex.Message);
					logger.Error(ex.StackTrace);
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
            if (this.rbUTF8.Checked)
            {
                text2 = System.Text.Encoding.UTF8.GetString(e.Data);//ParseUtil.ToHexString(e.Data, num);
            }
			listViewItem.SubItems.Add(text2);
			listViewItem.SubItems.Add(string.Concat(num));
			if (this.cbLog.Checked)
			{
				logger.Info(e.RemoteHost.ToString() + " " + text2);
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
            if(this.rbUTF8.Checked)
            {
                this.SocketInfo.Format = "UTF-8";
            }
            if (this.rbAscII.Checked)
            {
                this.SocketInfo.Format = "AscII";
            }
            if (this.rbHex.Checked)
            {
                this.SocketInfo.Format = "Hex";
            }
            //this.SocketInfo.Format = (this.rbUTF8.Checked ? "UTF-8" : "AscII");
            //this.SocketInfo.Format = (this.rbAscII.Checked ? "AscII" : "Hex");
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
		
		private void btnOpenLog_Click(object sender, EventArgs e)
		{
			Process.Start("notepad.exe", "client.log");
		}
    }
}
