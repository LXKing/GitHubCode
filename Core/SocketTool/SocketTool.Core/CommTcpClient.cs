using log4net;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
namespace SocketTool.Core
{
	public class CommTcpClient : IClient
	{
		private static ILog logger = LogManager.GetLogger(typeof(CommTcpClient));
		private TcpClient tcpClient;
		private Thread recvThread;
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
		public int ThreadSleepInterval
		{
			get;
			set;
		}
		public bool IsConnected
		{
			get;
			set;
		}
		public CommTcpClient()
		{
			this.ThreadSleepInterval = 100;
		}
		public void Init(string serverIp, int port)
		{
			this.ServerIP = serverIp;
			this.Port = port;
		}
		public bool Connect()
		{
			try
			{
				if (this.recvThread != null)
				{
					this.recvThread.Abort();
				}
			}
			catch (Exception)
			{
			}
			this.tcpClient = new TcpClient();
			try
			{
				this.tcpClient.Connect(this.ServerIP, this.Port);
				this.tcpClient.Client.Blocking = true;
				this.tcpClient.Client.LingerState = new LingerOption(true, 0);
				this.recvThread = new Thread(new ThreadStart(this.RecvRequestFromClient));
				this.recvThread.Start();
				this.IsConnected = true;
				return true;
			}
			catch (SocketException ex)
			{
				if (this.OnSocketError != null)
				{
					this.OnSocketError(0, new SocketEventArgs(ex.ErrorCode, ex.Message));
				}
			}
			return false;
		}
		public void Send(byte[] data)
		{
			if (this.tcpClient == null || !this.tcpClient.Connected)
			{
				this.Connect();
			}
			this.tcpClient.Client.Send(data);
		}
		public void Reconnect()
		{
			this.Close();
			this.Connect();
		}
		public void Close()
		{
			this.IsConnected = false;
			try
			{
				if (this.tcpClient != null)
				{
					this.tcpClient.Client.Close();
				}
			}
			catch (Exception ex)
			{
				CommTcpClient.logger.Error(ex.Message);
				CommTcpClient.logger.Error(ex.StackTrace);
			}
		}
		public void RecvRequestFromClient()
		{
			int num = 0;
			while (this.IsConnected)
			{
				if (this.tcpClient == null || this.tcpClient.Client == null || !this.tcpClient.Connected)
				{
					return;
				}
				try
				{
					num = this.tcpClient.Available;
				}
				catch (Exception)
				{
					break;
				}
				if (num > 0)
				{
					byte[] array = new byte[num];
					try
					{
						int size = num;
						SocketError socketError;
						this.tcpClient.Client.Receive(array, 0, size, SocketFlags.None, out socketError);
						if (this.OnDataReceived != null)
						{
							this.OnDataReceived(0, new ReceivedEventArgs((IPEndPoint)this.tcpClient.Client.RemoteEndPoint, array));
						}
					}
					catch (SocketException ex)
					{
						if (this.OnSocketError != null)
						{
							this.OnSocketError(0, new SocketEventArgs(ex.ErrorCode, ex.Message));
						}
						break;
					}
					catch (Exception ex2)
					{
						CommTcpClient.logger.Error(ex2.Message);
						CommTcpClient.logger.Error(ex2.StackTrace);
					}
				}
				Thread.Sleep(this.ThreadSleepInterval);
			}
		}
	}
}
