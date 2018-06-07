using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
namespace SocketTool.Core
{
	public class CommUdpServer : IServer
	{
		private Thread m_UDPThread;
		private string m_CurrentIP;
		private int m_CurrentPort;
		private IPEndPoint m_LocalEndPoint;
		private Socket m_Socket;
		private int m_iState;
		private Hashtable ConnectionMap = new Hashtable();
		private Hashtable NewConnMap = new Hashtable();
		private List<IConnection> ConnectionList = new List<IConnection>();
        public event ReceivedHandler OnDataReceived;
        public event SocketErrorHandler OnSocketError;
		public void Init(string serverIp, int port)
		{
			this.m_CurrentIP = serverIp;
			this.m_CurrentPort = port;
		}
		public int Listen()
		{
			try
			{
				this.m_UDPThread = new Thread(new ThreadStart(this.ReceiveLoop));
				this.m_UDPThread.Name = "RecieveThread";
				this.m_UDPThread.Start();
			}
			catch (Exception)
			{
			}
			return 1;
		}
		public List<IConnection> GetConnectionList()
		{
			return this.ConnectionList;
		}
		private void ReceiveLoop()
		{
			if (string.IsNullOrEmpty(this.m_CurrentIP))
			{
				this.m_LocalEndPoint = new IPEndPoint(IPAddress.Any, this.m_CurrentPort);
			}
			else
			{
				this.m_LocalEndPoint = new IPEndPoint(IPAddress.Parse(this.m_CurrentIP), this.m_CurrentPort);
			}
			try
			{
				this.m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				this.m_Socket.Bind(this.m_LocalEndPoint);
				this.m_iState = Constants.UDP_STATE_LISTENING;

                Action<int> action = (i) => {            
                        byte[] array = new byte[Constants.MAX_COMMAND_LEN];
						EndPoint endPoint = new IPEndPoint(this.m_LocalEndPoint.Address, this.m_LocalEndPoint.Port);
						int num = this.m_Socket.ReceiveFrom(array, ref endPoint);
						IPEndPoint clientIP = (IPEndPoint)endPoint;
						UdpConnection udpConnection = this.NewConnection(clientIP);
						byte[] array2 = new byte[num];
						Array.Copy(array, array2, num);
						if (this.OnDataReceived != null)
						{
							this.OnDataReceived(udpConnection.ID, new ReceivedEventArgs((IPEndPoint)endPoint, array2));
						}
                };
				try
				{
					while (true)
					{
                        action(0);
					}
				}
				catch (SocketException ex)
				{
					if (this.OnSocketError != null)
					{
						this.OnSocketError(0, new SocketEventArgs(ex.ErrorCode, ex.Message));
					}
                    action(0);
				}
				catch (Exception)
				{
                    action(0);
				}
			}
			catch
			{
			}
		}
		private UdpConnection NewConnection(IPEndPoint clientIP)
		{
			string text = clientIP.ToString();
			UdpConnection udpConnection = (UdpConnection)this.ConnectionMap[text];
			if (udpConnection != null)
			{
				udpConnection.OnlineDate = DateTime.Now;
				return udpConnection;
			}
			UdpConnection udpConnection2 = new UdpConnection();
			udpConnection2.ClientIP = clientIP;
			this.ConnectionMap[text] = udpConnection2;
			udpConnection2.ID = text;
			this.ConnectionList.Add(udpConnection2);
			return udpConnection2;
		}
		public void Send(string ID, byte[] data, int length)
		{
			IConnection connection = (IConnection)this.ConnectionMap[ID];
			if (connection != null)
			{
				try
				{
					this.m_Socket.SendTo(data, 0, length, SocketFlags.None, connection.ClientIP);
				}
				catch (SocketException ex)
				{
					if (this.OnSocketError != null)
					{
						this.OnSocketError(0, new SocketEventArgs(ex.ErrorCode, ex.Message));
					}
				}
			}
		}
		public int SendData(string IP, int Port, string Data)
		{
			if (this.m_iState != Constants.UDP_STATE_LISTENING)
			{
				return Constants.UDP_FAIL;
			}
			try
			{
				byte[] buffer = Util.StringToBytes(Data);
				EndPoint remoteEP = new IPEndPoint(IPAddress.Parse(IP), Port);
				this.m_Socket.SendTo(buffer, remoteEP);
			}
			catch (SocketException)
			{
				int uDP_FAIL = Constants.UDP_FAIL;
				return uDP_FAIL;
			}
			catch (Exception)
			{
				int uDP_FAIL = Constants.UDP_FAIL;
				return uDP_FAIL;
			}
			return Constants.UDP_OK;
		}
		public void Close()
		{
			if (this.m_iState != Constants.UDP_STATE_LISTENING)
			{
				return;
			}
			this.m_iState = Constants.UDP_STATE_CLOSING;
			this.m_Socket.Shutdown(SocketShutdown.Both);
			this.m_Socket.Close();
			this.m_UDPThread = null;
		}
	}
}
