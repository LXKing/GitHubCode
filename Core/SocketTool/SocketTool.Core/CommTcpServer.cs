using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
namespace SocketTool.Core
{
	public class CommTcpServer : IServer
	{
		private Hashtable ClientConnections = new Hashtable();
		public List<IConnection> ConnectionList = new List<IConnection>();
		private readonly int MaxSimultaneousIncomingConnections = 10;
		private Thread AcceptIncomingThreads;
		private Thread ProcessIncomingThreads;
		private Thread CheckDisconnectThreads;
		private static int connectId = 0;
		private static ILog logger = LogManager.GetLogger(typeof(CommTcpServer));
        public event ReceivedHandler OnDataReceived;
        public event SocketErrorHandler OnSocketError;
		public IPAddress ListenOnLocalIP
		{
			get;
			set;
		}
		public int ListenPort
		{
			get;
			set;
		}
		private TcpListener TcpListenerObject
		{
			get;
			set;
		}
		public bool WantExit
		{
			get;
			set;
		}
		public Queue DataQueue
		{
			get;
			set;
		}
		public void Init(string serverIp, int port)
		{
			if (string.IsNullOrEmpty(serverIp))
			{
				this.ListenOnLocalIP = IPAddress.Any;
			}
			else
			{
				this.ListenOnLocalIP = IPAddress.Parse(serverIp);
			}
			this.ListenPort = port;
		}
		public List<IConnection> GetConnectionList()
		{
			if (this.ClientConnections.Count == 0)
			{
				return new List<IConnection>();
			}
			this.ConnectionList.Clear();
			foreach (IConnection item in this.ClientConnections.Values)
			{
				this.ConnectionList.Add(item);
			}
			return this.ConnectionList;
		}
		public int Listen()
		{
			this.ClientConnections = new Hashtable();
			this.WantExit = false;
			this.TcpListenerObject = new TcpListener(this.ListenOnLocalIP, this.ListenPort);
			this.TcpListenerObject.Start(this.MaxSimultaneousIncomingConnections);
			this.AcceptIncomingThreads = new Thread(new ThreadStart(this.AcceptIncoming));
			this.AcceptIncomingThreads.Start();
			this.ProcessIncomingThreads = new Thread(new ThreadStart(this.ProcessIncomingConnectionWorker));
			this.ProcessIncomingThreads.Start();
			return 1;
		}
		private void AcceptIncoming()
		{
			while (!this.WantExit)
			{
				try
				{
					if (this.TcpListenerObject.Pending())
					{
						TcpClient tcpClient = this.TcpListenerObject.AcceptTcpClient();
						TcpConnection tcpConnection = new TcpConnection(tcpClient);
						((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
						if (CommTcpServer.connectId < 100000)
						{
							Interlocked.Increment(ref CommTcpServer.connectId);
						}
						else
						{
							CommTcpServer.connectId = 1;
						}
						tcpConnection.ID = string.Concat(CommTcpServer.connectId);
						if (this.OnSocketError != null)
						{
							tcpConnection.OnSocketError += this.OnSocketError;
						}
						try
						{
							Monitor.Enter(this.ClientConnections);
							this.ClientConnections[tcpConnection.ID] = tcpConnection;
						}
						catch (Exception ex)
						{
							CommTcpServer.logger.Error(ex.Message);
							CommTcpServer.logger.Error(ex.StackTrace);
						}
					}
				}
				catch (Exception ex2)
				{
					CommTcpServer.logger.Error(ex2.Message);
					CommTcpServer.logger.Error(ex2.StackTrace);
				}
				Thread.Sleep(500);
			}
		}
		public void Send(string Id, byte[] data, int length)
		{
			TcpConnection tcpConnection = (TcpConnection)this.ClientConnections[Id];
			if (tcpConnection == null || !tcpConnection.IsConnected)
			{
				throw new Exception("没有连接，无法发送数据！");
			}
			tcpConnection.Send(data, length);
		}
		private void ProcessVc(TcpConnection vc)
		{
			if (vc != null)
			{
				byte[] array = vc.RecvData();
				if (array != null && this.OnDataReceived != null)
				{
					this.OnDataReceived(vc.ID, new ReceivedEventArgs(vc.ClientIP, array));
				}
			}
		}
		private void ProcessIncomingConnectionWorker()
		{
			try
			{
				while (!this.WantExit)
				{
					try
					{
						ArrayList arrayList = new ArrayList(this.ClientConnections.Values);
						foreach (TcpConnection vc in arrayList)
						{
							try
							{
								this.ProcessVc(vc);
							}
							catch (Exception)
							{
							}
						}
					}
					catch (Exception)
					{
					}
					Thread.Sleep(200);
				}
			}
			catch (Exception)
			{
			}
		}
		public void Close()
		{
			this.WantExit = true;
			try
			{
				if (this.TcpListenerObject != null && this.TcpListenerObject.Server != null)
				{
					this.TcpListenerObject.Server.Close();
				}
			}
			catch (Exception)
			{
			}
			try
			{
				ArrayList arrayList = new ArrayList(this.ClientConnections.Values);
				foreach (TcpConnection tcpConnection in arrayList)
				{
					try
					{
						tcpConnection.Close();
					}
					catch (Exception)
					{
					}
				}
				this.ClientConnections.Clear();
			}
			catch (Exception)
			{
			}
			try
			{
				if (this.AcceptIncomingThreads != null)
				{
					this.AcceptIncomingThreads.Abort();
				}
			}
			catch (Exception)
			{
			}
			try
			{
				if (this.ProcessIncomingThreads != null)
				{
					this.ProcessIncomingThreads.Abort();
				}
			}
			catch (Exception)
			{
			}
			try
			{
				if (this.CheckDisconnectThreads != null)
				{
					this.CheckDisconnectThreads.Abort();
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
