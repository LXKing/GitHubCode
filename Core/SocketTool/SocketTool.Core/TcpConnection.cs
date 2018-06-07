using log4net;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
namespace SocketTool.Core
{
	public class TcpConnection : IConnection
	{
		private static ILog logger = LogManager.GetLogger(typeof(TcpConnection));
		private Thread SendOutgoingThread;
        public event SocketErrorHandler OnSocketError;
		public string ID
		{
			get;
			set;
		}
		public DateTime CreateDate
		{
			get;
			set;
		}
		public DateTime OnlineDate
		{
			get;
			set;
		}
		public IPEndPoint ClientIP
		{
			get;
			set;
		}
		public Queue DataQueue
		{
			get;
			set;
		}
		public string IP
		{
			get;
			set;
		}
		public TcpClient tcpClient
		{
			get;
			set;
		}
		public bool IsConnected
		{
			get;
			set;
		}
		public int ThreadSleepInterval
		{
			get;
			set;
		}
		public TcpConnection()
		{
			this.ThreadSleepInterval = 100;
		}
		public TcpConnection(TcpClient client)
		{
			this.tcpClient = client;
			this.ClientIP = (IPEndPoint)client.Client.RemoteEndPoint;
			this.tcpClient.Client.Blocking = true;
			this.tcpClient.Client.SendBufferSize = 65536;
			this.IP = this.tcpClient.Client.RemoteEndPoint.ToString();
			this.DataQueue = new Queue();
			this.SendOutgoingThread = new Thread(new ThreadStart(this.RecvRequestFromClient));
			this.SendOutgoingThread.Start();
			this.IsConnected = true;
			this.CreateDate = DateTime.Now;
			this.OnlineDate = DateTime.Now;
		}
		public byte[] RecvData()
		{
			if (this.DataQueue.Count > 0)
			{
				return (byte[])this.DataQueue.Dequeue();
			}
			return null;
		}
		public void Close()
		{
			try
			{
				if (this.SendOutgoingThread != null)
				{
					this.SendOutgoingThread.Abort();
				}
			}
			catch (Exception)
			{
			}
			if (this.tcpClient == null)
			{
				return;
			}
			if (this.tcpClient.Client != null)
			{
				try
				{
					this.tcpClient.Client.Shutdown(SocketShutdown.Both);
				}
				catch
				{
				}
				try
				{
					this.tcpClient.Client.Close();
				}
				catch
				{
				}
			}
			try
			{
				this.tcpClient.Close();
			}
			catch
			{
			}
			this.tcpClient = null;
		}
		public void Send(byte[] data, int length)
		{
			SocketError socketError;
			this.tcpClient.Client.Send(data, 0, length, SocketFlags.None, out socketError);
		}
		private void RecvRequestFromClient()
		{
			int num = 0;
			while (this.IsConnected)
			{
				if (this.tcpClient == null || this.tcpClient.Client == null || !this.tcpClient.Connected)
				{
					if (this.OnSocketError != null)
					{
						this.OnSocketError(this.ID, new SocketEventArgs(10058, ""));
					}
					return;
				}
				try
				{
					num = this.tcpClient.Available;
				}
				catch (Exception)
				{
					if (this.OnSocketError != null)
					{
						this.OnSocketError(this.ID, new SocketEventArgs(10101, ""));
					}
					break;
				}
				if (num > 0)
				{
					this.OnlineDate = DateTime.Now;
					byte[] array = new byte[num];
					try
					{
						int size = num;
						SocketError code;
						int num2 = this.tcpClient.Client.Receive(array, 0, size, SocketFlags.None, out code);
						if (num2 > 0)
						{
							byte[] array2 = new byte[num2];
							Array.Copy(array, array2, num2);
							this.DataQueue.Enqueue(array2);
						}
						else
						{
							if (this.OnSocketError != null)
							{
								this.OnSocketError(this.ID, new SocketEventArgs((int)code, ""));
							}
						}
					}
					catch (SocketException ex)
					{
						if (this.OnSocketError != null)
						{
							this.OnSocketError(this.ID, new SocketEventArgs(ex.ErrorCode, ex.Message));
						}
					}
					catch (Exception ex2)
					{
						TcpConnection.logger.Error(ex2.Message);
						TcpConnection.logger.Error(ex2.StackTrace);
					}
				}
				Thread.Sleep(this.ThreadSleepInterval);
			}
		}
	}
}
