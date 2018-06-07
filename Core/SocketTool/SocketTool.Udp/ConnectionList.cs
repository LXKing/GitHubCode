using System;
using System.Collections;
using System.Net;
using System.Threading;
namespace SocketTool.Udp
{
	internal class ConnectionList : CollectionBase
	{
		private CommonUdp m_Parent;
		public int NumConnections
		{
			get
			{
				object syncRoot;
				Monitor.Enter(syncRoot = base.List.SyncRoot);
				int count;
				try
				{
					count = base.List.Count;
				}
				finally
				{
					Monitor.Exit(syncRoot);
				}
				return count;
			}
		}
		public ConnectionList(CommonUdp udp)
		{
			this.m_Parent = udp;
		}
		public int NewConnection(Connection conn)
		{
			if (conn == null)
			{
				return UdpConsts.UDP_FAIL;
			}
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				base.List.Add(conn);
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			this.m_Parent.ConnectionStateChanged(conn, true, "");
			return UdpConsts.UDP_OK;
		}
		public int RemoveConnection(Connection conn, bool send_disconnect_packet, string reason)
		{
			if (conn == null)
			{
				return UdpConsts.UDP_FAIL;
			}
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				if (!base.List.Contains(conn))
				{
					return UdpConsts.UDP_FAIL;
				}
				if (reason == "")
				{
					reason = "Disconnected by remote host.";
				}
				if (send_disconnect_packet)
				{
					if (reason.Length > 200)
					{
						reason = reason.Substring(0, 200);
					}
					conn.SendUnreliableCommand(0, UdpConsts.OPCODE_DISCONNECT, new string[]
					{
						reason
					});
				}
				base.List.Remove(conn);
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			this.m_Parent.ConnectionStateChanged(conn, false, reason);
			return UdpConsts.UDP_OK;
		}
		public int RemoveAllConnections(string Reason)
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				if (base.List.Count == 0)
				{
					return UdpConsts.UDP_OK;
				}
				while (base.List.Count > 0)
				{
					this.RemoveConnection((Connection)base.List[0], true, Reason);
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return UdpConsts.UDP_OK;
		}
		public int RemoveOldConnections()
		{
			ArrayList arrayList = new ArrayList();
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				if (base.List.Count == 0)
				{
					return UdpConsts.UDP_OK;
				}
				for (int i = 0; i < base.List.Count; i++)
				{
					Connection connection = base.List[i] as Connection;
					if (connection != null && connection.TimeoutTime <= DateTime.Now)
					{
						arrayList.Add(connection);
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			if (arrayList.Count > 0)
			{
				for (int j = 0; j < arrayList.Count; j++)
				{
					this.RemoveConnection((Connection)arrayList[j], false, "Timed out.");
				}
			}
			arrayList.Clear();
			return UdpConsts.UDP_OK;
		}
		public void PingConnections()
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					Connection connection = base.List[i] as Connection;
					if (connection != null && connection.Authed)
					{
						connection.SendUnreliableCommand(0, UdpConsts.OPCODE_PING, null);
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
		public void ReliableRetry()
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					Connection connection = base.List[i] as Connection;
					if (connection != null)
					{
						connection.ResendReliablePacket();
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
		public int ConnectionByIPPort(string IP, int Port, out Connection found)
		{
			found = null;
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					Connection connection = base.List[i] as Connection;
					if (connection != null && connection.RemoteEP.Address.ToString() == IP && connection.RemoteEP.Port == Port)
					{
						found = connection;
						return UdpConsts.UDP_OK;
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return UdpConsts.UDP_NOTFOUND;
		}
		public int ConnectionByRequestID(string request_id, out Connection found)
		{
			found = null;
			if (request_id == "")
			{
				return UdpConsts.UDP_NOTFOUND;
			}
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					Connection connection = base.List[i] as Connection;
					if (connection != null && connection.RequestID == request_id)
					{
						found = connection;
						return UdpConsts.UDP_OK;
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return UdpConsts.UDP_NOTFOUND;
		}
		public int GetConnections(out Connection[] found)
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				found = new Connection[base.List.Count];
				for (int i = 0; i < base.List.Count; i++)
				{
					Connection connection = base.List[i] as Connection;
					if (connection != null)
					{
						found[i] = connection;
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return UdpConsts.UDP_OK;
		}
		public int ConnectionByRemoteEndpoint(IPEndPoint remoteEP, out Connection found)
		{
			return this.ConnectionByIPPort(remoteEP.Address.ToString(), remoteEP.Port, out found);
		}
	}
}
