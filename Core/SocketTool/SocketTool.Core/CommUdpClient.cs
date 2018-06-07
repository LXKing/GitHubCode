using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
namespace SocketTool.Core
{
	public class CommUdpClient : IClient
	{
		private UdpClient udpClient = new UdpClient();
		private int LocalPort = 4545;
		private Thread receiver;
        public event ReceivedHandler OnDataReceived;
        public event SocketErrorHandler OnSocketError;
		public string ServerIP
		{
			get;
			set;
		}
		public int Port
		{
			get;
			set;
		}
		public bool IsConnected
		{
			get;
			set;
		}
		public void Init(string serverIp, int port)
		{
			this.ServerIP = serverIp;
			this.Port = port;
			try
			{
				this.udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, this.LocalPort));
				this.IsConnected = true;
				this.receiver = new Thread(new ThreadStart(this.ReceiveWork));
				this.receiver.Start();
			}
			catch (SocketException ex)
			{
				if (this.OnSocketError != null)
				{
					this.OnSocketError(0, new SocketEventArgs(ex.ErrorCode, ex.Message));
				}
			}
		}
		public void Send(byte[] data)
		{
			try
			{
				this.udpClient.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(this.ServerIP), this.Port));
			}
			catch (SocketException ex)
			{
				this.Close();
				if (this.OnSocketError != null)
				{
					this.OnSocketError(0, new SocketEventArgs(ex.ErrorCode, ex.Message));
				}
			}
		}
		private void Send(UdpClient sender, string s)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(s);
			Console.WriteLine(string.Concat(new string[]
			{
				"Sending '",
				s,
				"' (",
				bytes.Length.ToString(),
				" bytes)"
			}));
			sender.Send(bytes, bytes.Length);
		}
		private void ReceiveWork()
		{
			try
			{
				while (this.IsConnected)
				{
					this.Receive();
					Thread.Sleep(1000);
				}
			}
			catch (ThreadAbortException)
			{
				Thread.ResetAbort();
			}
		}
		private void Receive()
		{
			try
			{
				IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
				byte[] receivedData = this.udpClient.Receive(ref remote);
				if (this.OnDataReceived != null)
				{
					this.OnDataReceived(null, new ReceivedEventArgs(remote, receivedData));
				}
			}
			catch (SocketException ex)
			{
				this.IsConnected = false;
				if (this.OnSocketError != null)
				{
					this.OnSocketError(0, new SocketEventArgs(ex.ErrorCode, ex.Message));
				}
			}
			catch (Exception)
			{
				this.IsConnected = false;
			}
		}
		public void Close()
		{
			this.udpClient.Close();
			try
			{
				this.receiver.Abort();
			}
			catch (Exception)
			{
			}
		}
	}
}
