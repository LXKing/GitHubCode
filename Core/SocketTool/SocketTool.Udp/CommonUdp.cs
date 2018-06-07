using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
namespace SocketTool.Udp
{
	internal class CommonUdp : IGenesisUDP
	{
		private Thread m_UDPThread;
		private string m_CurrentIP;
		private int m_CurrentPort;
		private IPEndPoint m_LocalEndPoint;
		private Socket m_Socket;
		private int m_iState;
		private bool m_bEncrypt;
		private ConnectionList m_Clients;
		private ConnectionList m_Servers;
		private DateTime m_NextClearOldConnections;
		private DateTime m_NextPingConnections;
		private DateTime m_NextReliableResend;
		private Timer m_TickTimer;
		private ConnTimeout m_ConnTimeouts;
		private DateTime m_NextTimeoutCheck;
		private string m_Name;
        public event DebugHandler OnDebugMessage;
        public event ReceivedHandler OnDataReceived;
        public event ListenHandler OnListenStateChanged;
        public event SocketErrorHandler OnSocketError;
        public event IncomingCommandHandler OnConnectionlessCommand;
        public event ConnectionAuthHandler OnConnectionAuth;
        public event SendLoginHandler OnLoginRequested;
        public event IncomingCommandHandler OnCommandReceived;
        public event ConnectionStateChangeHandler OnConnectionStateChanged;
        public event AuthenticatedHandler OnAuthFeedback;
        public event RequestTimedOutHandler OnConnectionRequestTimedOut;
		public int State
		{
			get
			{
				return this.m_iState;
			}
		}
		public bool Encrypt
		{
			get
			{
				return this.m_bEncrypt;
			}
			set
			{
				this.m_bEncrypt = value;
			}
		}
		public ConnectionList Clients
		{
			get
			{
				return this.m_Clients;
			}
		}
		public ConnectionList Servers
		{
			get
			{
				return this.m_Servers;
			}
		}
		public CommonUdp(string Name)
		{
			this.m_iState = UdpConsts.UDP_STATE_IDLE;
			this.m_Clients = new ConnectionList(this);
			this.m_Servers = new ConnectionList(this);
			this.m_TickTimer = new Timer(new TimerCallback(this.TimerTick), null, 500, 500);
			this.m_NextClearOldConnections = DateTime.Now;
			this.m_NextPingConnections = DateTime.Now;
			this.m_NextReliableResend = DateTime.Now;
			this.m_ConnTimeouts = new ConnTimeout(this);
			this.m_NextTimeoutCheck = DateTime.Now;
			this.m_Name = Name;
		}
		public int StartListen(string IP, int Port)
		{
			this.m_CurrentIP = IP;
			this.m_CurrentPort = Port;
			try
			{
				this.m_UDPThread = new Thread(new ThreadStart(this.ReceiveLoop));
				this.m_UDPThread.Name = "RecieveThread";
				this.m_UDPThread.Start();
			}
			catch (Exception)
			{
				return UdpConsts.UDP_FAIL;
			}
			return UdpConsts.UDP_OK;
		}
		public int StopListen()
		{
			if (this.m_iState != UdpConsts.UDP_STATE_LISTENING)
			{
				return UdpConsts.UDP_FAIL;
			}
			if (this.OnListenStateChanged != null)
			{
				this.OnListenStateChanged(null, new ListenEventArgs(false));
			}
			this.m_Clients.RemoveAllConnections("Server shutting down.");
			this.m_Servers.RemoveAllConnections("Client shutting down.");
			this.m_ConnTimeouts.RemoveAllEntries();
			this.m_iState = UdpConsts.UDP_STATE_CLOSING;
			this.m_Socket.Shutdown(SocketShutdown.Both);
			this.m_Socket.Close();
			this.m_UDPThread = null;
			return UdpConsts.UDP_OK;
		}
		private void ReceiveLoop()
		{
			int num = 0;
			if (this.m_CurrentIP == "")
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
				this.m_iState = UdpConsts.UDP_STATE_LISTENING;
				if (this.OnListenStateChanged != null)
				{
					this.OnListenStateChanged(null, new ListenEventArgs(true));
				}
				do
				{
					try
					{
						byte[] array = new byte[UdpConsts.MAX_COMMAND_LEN];
						EndPoint endPoint = new IPEndPoint(this.m_LocalEndPoint.Address, this.m_LocalEndPoint.Port);
						int length = this.m_Socket.ReceiveFrom(array, ref endPoint);
						IPEndPoint iPEndPoint = (IPEndPoint)endPoint;
						string text = Util.BytesToString(array);
						text = text.Substring(0, length);
						if (this.OnDataReceived != null)
						{
							this.OnDataReceived(null, new ReceivedEventArgs((IPEndPoint)endPoint, text));
						}
						try
						{
							int num2 = 0;
							Command command = new Command();
							command.OPCode = text.Substring(num2, 2);
							num2 += 2;
							command.SequenceNum = Util.BytesToUint(text.Substring(num2, 4));
							num2 += 4;
							command.Flags = (byte)text[num2];
							num2++;
							if ((command.Flags & UdpConsts.FLAGS_CONNECTIONLESS) > 0)
							{
								command.NumFields = Util.BytesToShort(text.Substring(num2, 2));
								num2 += 2;
								command.FieldSizes = new short[(int)command.NumFields];
								for (short num3 = 0; num3 < command.NumFields; num3 += 1)
								{
									command.FieldSizes[(int)num3] = Util.BytesToShort(text.Substring(num2, 2));
									num2 += 2;
								}
								num2 += 2;
								command.AllFields = text.Substring(num2);
								command.Initialize();
								this.ProcessConnectionlessComand(iPEndPoint, command);
							}
							else
							{
								Connection connection;
								this.m_Clients.ConnectionByRemoteEndpoint(iPEndPoint, out connection);
								if (connection == null)
								{
									this.m_Servers.ConnectionByRemoteEndpoint(iPEndPoint, out connection);
								}
								if (connection != null)
								{
									connection.ProcessCommandPacket(text);
								}
							}
						}
						catch
						{
						}
						num = 0;
					}
					catch (SocketException ex)
					{
						if (ex.ErrorCode != 10061 && ex.ErrorCode != 10054)
						{
							if (ex.ErrorCode != 10004 && this.OnSocketError != null)
							{
								this.OnSocketError(null, new SocketEventArgs(ex.ErrorCode, ex.Message));
							}
							if (this.m_iState != UdpConsts.UDP_STATE_LISTENING)
							{
								break;
							}
							num++;
						}
					}
					catch (Exception)
					{
						if (this.m_iState != UdpConsts.UDP_STATE_LISTENING)
						{
							break;
						}
						num++;
					}
				}
				while (num != UdpConsts.MAX_EXCEPTIONS);
			}
			catch (SocketException ex2)
			{
				if (this.OnSocketError != null)
				{
					this.OnSocketError(null, new SocketEventArgs(ex2.ErrorCode, ex2.Message));
				}
			}
			catch (Exception)
			{
			}
			if (this.m_iState != UdpConsts.UDP_STATE_IDLE && this.m_iState != UdpConsts.UDP_STATE_CLOSING)
			{
				this.StopListen();
			}
			this.m_iState = UdpConsts.UDP_STATE_IDLE;
		}
		public int SendData(string IP, int Port, string Data)
		{
			if (this.m_iState != UdpConsts.UDP_STATE_LISTENING)
			{
				return UdpConsts.UDP_FAIL;
			}
			try
			{
				byte[] buffer = Util.StringToBytes(Data);
				EndPoint remoteEP = new IPEndPoint(IPAddress.Parse(IP), Port);
				this.m_Socket.SendTo(buffer, remoteEP);
			}
			catch (SocketException)
			{
				int uDP_FAIL = UdpConsts.UDP_FAIL;
				return uDP_FAIL;
			}
			catch (Exception)
			{
				int uDP_FAIL = UdpConsts.UDP_FAIL;
				return uDP_FAIL;
			}
			return UdpConsts.UDP_OK;
		}
		public int SendConnectionlessCommand(string IP, int Port, string opcode, string[] fields)
		{
			return this.SendCommand(IP, Port, "", 0u, UdpConsts.FLAGS_CONNECTIONLESS, opcode, fields);
		}
		public int SendUnreliableCommandToAll(BroadcastFilter filter, byte flags, string opcode, string[] fields)
		{
			if (filter == BroadcastFilter.None)
			{
				return UdpConsts.UDP_OK;
			}
			if ((filter & BroadcastFilter.Servers) > BroadcastFilter.None)
			{
				Connection[] array;
				this.GetConnections(true, out array);
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if ((filter & BroadcastFilter.AuthedOnly) <= BroadcastFilter.None || array[i].Authed)
						{
							array[i].SendUnreliableCommand(flags, opcode, fields);
						}
					}
				}
			}
			if ((filter & BroadcastFilter.Clients) > BroadcastFilter.None)
			{
				Connection[] array;
				this.GetConnections(false, out array);
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if ((filter & BroadcastFilter.AuthedOnly) <= BroadcastFilter.None || array[i].Authed)
						{
							array[i].SendUnreliableCommand(flags, opcode, fields);
						}
					}
				}
			}
			return UdpConsts.UDP_OK;
		}
		public int SendReliableCommandToAll(BroadcastFilter filter, byte flags, string opcode, string[] fields)
		{
			if (filter == BroadcastFilter.None)
			{
				return UdpConsts.UDP_OK;
			}
			if ((filter & BroadcastFilter.Servers) > BroadcastFilter.None)
			{
				Connection[] array;
				this.GetConnections(true, out array);
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if ((filter & BroadcastFilter.AuthedOnly) <= BroadcastFilter.None || array[i].Authed)
						{
							array[i].SendReliableCommand(flags, opcode, fields);
						}
					}
				}
			}
			if ((filter & BroadcastFilter.Clients) > BroadcastFilter.None)
			{
				Connection[] array;
				this.GetConnections(false, out array);
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if ((filter & BroadcastFilter.AuthedOnly) <= BroadcastFilter.None || array[i].Authed)
						{
							array[i].SendReliableCommand(flags, opcode, fields);
						}
					}
				}
			}
			return UdpConsts.UDP_OK;
		}
		public int SendCommand(string IP, int Port, string key, uint seq_num, byte flags, string opcode, string[] fields)
		{
			int result;
			try
			{
				ArrayList arrayList = new ArrayList();
				bool flag = true;
				Connection connection = null;
				this.ConnectionByIPPort(IP, Port, out connection);
				if (connection == null && (flags & UdpConsts.FLAGS_RELIABLE) > 0)
				{
					result = UdpConsts.UDP_RELIABLENODESTINATION;
				}
				else
				{
					if (fields == null)
					{
						fields = new string[0];
					}
					else
					{
						fields = (string[])fields.Clone();
					}
					if (key != "")
					{
						flags |= UdpConsts.FLAGS_ENCRYPTED;
						for (int i = 0; i < fields.Length; i++)
						{
							fields[i] = Util.XORCrypt(fields[i], key);
						}
					}
					for (int j = 0; j < fields.Length; j++)
					{
						if (fields[j].Length > UdpConsts.MAX_FIELD_LEN)
						{
							fields[j] = fields[j].Substring(0, UdpConsts.MAX_FIELD_LEN);
						}
					}
					if (fields.Length > UdpConsts.MAX_FIELDS)
					{
						string[] array = new string[UdpConsts.MAX_FIELDS];
						for (int k = 0; k < UdpConsts.MAX_FIELDS; k++)
						{
							array[k] = fields[k];
						}
						fields = null;
						fields = array;
					}
					string text = Util.CreatePacketHeader(seq_num, flags, opcode, key, fields);
					for (int l = 0; l < fields.Length; l++)
					{
						text += fields[l];
					}
					if (text.Length > UdpConsts.MAX_COMMAND_LEN)
					{
						if ((flags & UdpConsts.FLAGS_RELIABLE) == 0)
						{
							result = UdpConsts.UDP_UNRELIABLETOOLONG;
							return result;
						}
						flags |= UdpConsts.FLAGS_COMPOUNDPIECE;
						text = Util.CreatePacketHeader(seq_num, flags, opcode, key, fields);
						for (int m = 0; m < fields.Length; m++)
						{
							text += fields[m];
						}
						arrayList.Add(text.Substring(0, UdpConsts.MAX_COMMAND_LEN));
						uint num = 1u;
						string text2 = Util.CreatePacketHeader(seq_num + num, flags, UdpConsts.OPCODE_COMPOUNDPIECE, key, null);
						string text3 = text.Substring(UdpConsts.MAX_COMMAND_LEN, text.Length - UdpConsts.MAX_COMMAND_LEN);
						while (text3.Length + text2.Length > UdpConsts.MAX_COMMAND_LEN)
						{
							arrayList.Add(text2 + text3.Substring(0, UdpConsts.MAX_COMMAND_LEN - text2.Length));
							num += 1u;
							text2 = Util.CreatePacketHeader(seq_num + num, flags, UdpConsts.OPCODE_COMPOUNDPIECE, key, null);
							text3 = text3.Substring(UdpConsts.MAX_COMMAND_LEN - text2.Length, text3.Length - (UdpConsts.MAX_COMMAND_LEN - text2.Length));
						}
						flags &= UdpConsts.FLAGS_COMPOUNDPIECE;
						flags |= UdpConsts.FLAGS_COMPOUNDEND;
						arrayList.Add(Util.CreatePacketHeader(seq_num + num, flags, UdpConsts.OPCODE_COMPOUNDPIECE, key, null) + text3);
						for (int n = 0; n < arrayList.Count; n++)
						{
							if (connection != null)
							{
								if ((flags & UdpConsts.FLAGS_RELIABLE) > 0)
								{
									flag = !connection.RQueue.CommandsWaiting();
									int num2 = connection.CacheReliablePacket((string)arrayList[n]);
									if (num2 != UdpConsts.UDP_OK)
									{
										result = num2;
										return result;
									}
									connection.LastSentPacketR += 1u;
								}
								else
								{
									connection.LastSentPacket += 1u;
								}
							}
							if (flag)
							{
								this.SendData(IP, Port, (string)arrayList[n]);
							}
						}
					}
					else
					{
						if (connection != null)
						{
							if ((flags & UdpConsts.FLAGS_RELIABLE) > 0)
							{
								flag = !connection.RQueue.CommandsWaiting();
								int num2 = connection.CacheReliablePacket(text);
								if (num2 != UdpConsts.UDP_OK)
								{
									result = num2;
									return result;
								}
								connection.LastSentPacketR += 1u;
							}
							else
							{
								connection.LastSentPacket += 1u;
							}
						}
						if (flag)
						{
							this.SendData(IP, Port, text);
						}
					}
					arrayList.Clear();
					result = UdpConsts.UDP_OK;
				}
			}
			catch (Exception)
			{
				result = UdpConsts.UDP_FAIL;
			}
			return result;
		}
		public int RequestConnect(ref string RemoteIP, int RemotePort, out string request_id)
		{
			request_id = "";
			try
			{
				IPAddress.Parse(RemoteIP);
			}
			catch
			{
				IPAddress[] addressList;
				try
				{
					addressList = Dns.GetHostEntry(RemoteIP).AddressList;
				}
				catch
				{
					int uDP_UNABLETORESOLVE = UdpConsts.UDP_UNABLETORESOLVE;
					return uDP_UNABLETORESOLVE;
				}
				RemoteIP = addressList[0].ToString();
			}
			Connection connection;
			this.m_Servers.ConnectionByIPPort(RemoteIP, RemotePort, out connection);
			if (connection != null)
			{
				return UdpConsts.UDP_ALREADYCONNECTED;
			}
			this.m_Clients.ConnectionByIPPort(RemoteIP, RemotePort, out connection);
			if (connection != null)
			{
				return UdpConsts.UDP_ALREADYCONNECTED;
			}
			string newRequestID = this.m_ConnTimeouts.GetNewRequestID();
			request_id = newRequestID;
			this.SendConnectionlessCommand(RemoteIP, RemotePort, UdpConsts.OPCODE_CONNECTIONREQUEST, new string[]
			{
				newRequestID
			});
			this.m_ConnTimeouts.AddConnectionEntry(RemoteIP, RemotePort, UdpConsts.CONNECTION_TIMEOUT_TIME, newRequestID);
			return UdpConsts.UDP_OK;
		}
		public int CancelConnect(string RemoteIP, int RemotePort)
		{
			this.m_ConnTimeouts.RemoveConnectionEntry(RemoteIP, RemotePort);
			return UdpConsts.UDP_OK;
		}
		public int CancelConnect(string RequestID)
		{
			this.m_ConnTimeouts.RemoveConnectionEntry(RequestID);
			return UdpConsts.UDP_OK;
		}
		private int ProcessConnectionlessComand(IPEndPoint remote_ep, Command c)
		{
			int num = UdpConsts.UDP_OK;
			try
			{
				if (c.OPCode == UdpConsts.OPCODE_CONNECTIONREQUEST)
				{
					bool flag = false;
					Connection connection;
					this.m_Servers.ConnectionByIPPort(remote_ep.Address.ToString(), remote_ep.Port, out connection);
					if (connection != null)
					{
						num = UdpConsts.UDP_ALREADYCONNECTED;
						flag = true;
					}
					this.m_Clients.ConnectionByIPPort(remote_ep.Address.ToString(), remote_ep.Port, out connection);
					if (connection != null)
					{
						num = UdpConsts.UDP_ALREADYCONNECTED;
						flag = true;
					}
					int result;
					if (flag)
					{
						this.SendConnectionlessCommand(remote_ep.Address.ToString(), remote_ep.Port, UdpConsts.OPCODE_CONNECTIONACK, new string[]
						{
							c.Fields[0],
							"FAIL",
							"Connection from this client already exists."
						});
						result = num;
						return result;
					}
					string text = "";
					if (this.m_bEncrypt)
					{
						text = Util.GenerateEncryptionKey();
					}
					Connection connection2 = new Connection(this);
					connection2.EncryptionKey = text;
					connection2.RemoteEP = remote_ep;
					this.SendConnectionlessCommand(remote_ep.Address.ToString(), remote_ep.Port, UdpConsts.OPCODE_CONNECTIONACK, new string[]
					{
						c.Fields[0],
						"OK",
						text
					});
					this.m_Clients.NewConnection(connection2);
					result = UdpConsts.UDP_OK;
					return result;
				}
				else
				{
					if (c.OPCode == UdpConsts.OPCODE_CONNECTIONACK)
					{
						if (this.m_ConnTimeouts.EntryExists(c.Fields[0]))
						{
							if (c.Fields[1] == "OK")
							{
								Connection connection3 = new Connection(this);
								connection3.EncryptionKey = c.Fields[2];
								connection3.RemoteEP = remote_ep;
								connection3.Server = true;
								connection3.RequestID = c.Fields[0];
								this.m_Servers.NewConnection(connection3);
								this.m_ConnTimeouts.RemoveConnectionEntry(c.Fields[0]);
								if (this.OnLoginRequested != null)
								{
									this.OnLoginRequested(null, new LoginSendEventArgs(connection3, true, ""));
								}
								else
								{
									this.SendCommand(remote_ep.Address.ToString(), remote_ep.Port, connection3.EncryptionKey, 0u, 0, UdpConsts.OPCODE_LOGINDETAILS, null);
								}
							}
							else
							{
								Connection connection4 = new Connection(this);
								connection4.EncryptionKey = c.Fields[1];
								connection4.RemoteEP = remote_ep;
								connection4.Server = true;
								if (this.OnLoginRequested != null)
								{
									this.OnLoginRequested(null, new LoginSendEventArgs(connection4, false, c.Fields[1]));
								}
							}
						}
						int result = UdpConsts.UDP_OK;
						return result;
					}
				}
			}
			catch (Exception)
			{
				int result = UdpConsts.UDP_FAIL;
				return result;
			}
			if (this.OnConnectionlessCommand != null)
			{
				this.OnConnectionlessCommand(null, new CommandEventArgs(c, null, remote_ep));
			}
			return UdpConsts.UDP_OK;
		}
		internal int CommandReceived(Connection cn, Command cmd)
		{
			if (this.OnCommandReceived != null)
			{
				this.OnCommandReceived(null, new CommandEventArgs(cmd, cn, null));
			}
			return UdpConsts.UDP_OK;
		}
		internal int ConnectionAuthing(Connection cn, Command cmd)
		{
			if (cn.Authed)
			{
				return UdpConsts.UDP_OK;
			}
			ConnectionAuthEventArgs connectionAuthEventArgs = new ConnectionAuthEventArgs(cn, cmd);
			if (this.OnConnectionAuth != null)
			{
				this.OnConnectionAuth(null, connectionAuthEventArgs);
			}
			if (connectionAuthEventArgs.DisallowReason.Length > 200)
			{
				connectionAuthEventArgs.DisallowReason = connectionAuthEventArgs.DisallowReason.Substring(0, 200);
			}
			if (!connectionAuthEventArgs.AllowConnection)
			{
				cn.SendUnreliableCommand(0, UdpConsts.OPCODE_LOGINACK, new string[]
				{
					"FAIL",
					connectionAuthEventArgs.DisallowReason
				});
				this.m_Clients.RemoveConnection(cn, true, connectionAuthEventArgs.DisallowReason);
			}
			else
			{
				cn.SendUnreliableCommand(0, UdpConsts.OPCODE_LOGINACK, new string[]
				{
					"OK"
				});
				cn.Authed = true;
			}
			return UdpConsts.UDP_OK;
		}
		internal int ConnectionStateChanged(Connection cn, bool active, string reason)
		{
			if (this.OnConnectionStateChanged != null)
			{
				this.OnConnectionStateChanged(null, new ConnectionStateChangeEventArgs(cn, active, reason));
			}
			return UdpConsts.UDP_OK;
		}
		internal int AuthenticatedWithConnection(Connection cn, bool accepted, string reason)
		{
			if (this.OnAuthFeedback != null)
			{
				this.OnAuthFeedback(null, new AuthenticatedEventArgs(cn, accepted, reason));
			}
			return UdpConsts.UDP_OK;
		}
		internal int ConnectionRequestTimedOut(string IP, int Port, string ReqID)
		{
			if (this.OnConnectionRequestTimedOut != null)
			{
				this.OnConnectionRequestTimedOut(null, new RequestTimedOutEventArgs(IP, Port, ReqID));
			}
			return UdpConsts.UDP_OK;
		}
		private void TimerTick(object o)
		{
			if (this.m_iState == UdpConsts.UDP_STATE_IDLE)
			{
				return;
			}
			if (this.m_NextClearOldConnections < DateTime.Now)
			{
				this.m_Clients.RemoveOldConnections();
				this.m_Servers.RemoveOldConnections();
				this.m_NextClearOldConnections = DateTime.Now.AddSeconds((double)UdpConsts.CONNECTION_CLEAN_TIME);
			}
			if (this.m_NextPingConnections < DateTime.Now)
			{
				this.m_Servers.PingConnections();
				this.m_NextPingConnections = DateTime.Now.AddSeconds((double)UdpConsts.CONNECTION_PING_DELAY);
			}
			if (this.m_NextReliableResend < DateTime.Now)
			{
				this.m_Servers.ReliableRetry();
				this.m_Clients.ReliableRetry();
				this.m_NextReliableResend = DateTime.Now.AddSeconds((double)UdpConsts.CONNECTION_RELIABLE_RETRY);
			}
			if (this.m_NextTimeoutCheck < DateTime.Now)
			{
				this.m_ConnTimeouts.CheckTimeouts();
				this.m_NextTimeoutCheck = DateTime.Now.AddSeconds((double)UdpConsts.CONNECTION_TIMEOUT_CHECK);
			}
		}
		private int ConnectionByIPPort(string IP, int Port, out Connection conn)
		{
			int result = UdpConsts.UDP_OK;
			conn = null;
			if (this.m_Clients.ConnectionByIPPort(IP, Port, out conn) != UdpConsts.UDP_OK && this.m_Servers.ConnectionByIPPort(IP, Port, out conn) != UdpConsts.UDP_OK)
			{
				result = UdpConsts.UDP_NOTFOUND;
			}
			return result;
		}
		public int ConnectionByIPPort(string IP, int Port, out IConnection conn)
		{
			return this.ConnectionByIPPort(IP, Port, out conn);
		}
		private int RemoveConnection(Connection conn, bool send_disconnect_packet, string reason)
		{
			int num = UdpConsts.UDP_OK;
			num = this.m_Servers.RemoveConnection(conn, send_disconnect_packet, reason);
			if (num != UdpConsts.UDP_OK)
			{
				num = this.m_Clients.RemoveConnection(conn, send_disconnect_packet, reason);
			}
			return num;
		}
		public int RemoveConnection(IConnection conn, bool send_disconnect_packet, string reason)
		{
			return this.RemoveConnection((Connection)conn, send_disconnect_packet, reason);
		}
		private int GetConnections(bool servers, out Connection[] found)
		{
			if (!servers)
			{
				return this.m_Clients.GetConnections(out found);
			}
			return this.m_Servers.GetConnections(out found);
		}
		public int GetConnections(bool servers, out IConnection[] found)
		{
			Connection[] array;
			int connections = this.GetConnections(servers, out array);
			found = (IConnection[])array;
			return connections;
		}
		public string[] GetLocalAddresses()
		{
			return Util.GetLocalAddresses();
		}
		[Conditional("DEBUG")]
		public void DebugDump(string Message)
		{
			Message = string.Concat(new string[]
			{
				this.m_Name,
				": [",
				Util.GetMethod(2),
				"]: ",
				Message,
				"\r\n"
			});
			if (this.OnDebugMessage != null)
			{
				this.OnDebugMessage(null, new DebugEventArgs(Message));
			}
		}
	}
}
