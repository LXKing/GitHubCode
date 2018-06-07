using System;
using System.Collections;
using System.Threading;
namespace SocketTool.Udp
{
	internal class ConnTimeout : CollectionBase
	{
		private CommonUdp m_Parent;
		public ConnTimeout(CommonUdp parent)
		{
			this.m_Parent = parent;
		}
		public string GetNewRequestID()
		{
			Random random = new Random(DateTime.Now.Millisecond);
			string text = random.Next().ToString();
			Connection connection;
			while (this.EntryExists(text) || this.m_Parent.Servers.ConnectionByRequestID(text, out connection) != UdpConsts.UDP_NOTFOUND)
			{
				text = random.Next().ToString();
			}
			return text;
		}
		public void AddConnectionEntry(string IP, int Port, int TimeoutSecs, string RequestID)
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				if (this.EntryExists(IP, Port))
				{
					this.RemoveConnectionEntry(IP, Port);
				}
				TimeoutEntry timeoutEntry = new TimeoutEntry();
				timeoutEntry.ServerIP = IP;
				timeoutEntry.ServerPort = Port;
				timeoutEntry.TimeoutTime = DateTime.Now.AddSeconds((double)TimeoutSecs);
				timeoutEntry.ConnectionRequestID = RequestID;
				base.List.Add(timeoutEntry);
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
		public bool EntryExists(string RequestID)
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					TimeoutEntry timeoutEntry = base.List[i] as TimeoutEntry;
					if (timeoutEntry != null && timeoutEntry.ConnectionRequestID == RequestID)
					{
						return true;
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return false;
		}
		public bool EntryExists(string IP, int Port)
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					TimeoutEntry timeoutEntry = base.List[i] as TimeoutEntry;
					if (timeoutEntry != null && timeoutEntry.ServerIP == IP && timeoutEntry.ServerPort == Port)
					{
						return true;
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return false;
		}
		public void RemoveConnectionEntry(string IP, int Port)
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					TimeoutEntry timeoutEntry = base.List[i] as TimeoutEntry;
					if (timeoutEntry != null && timeoutEntry.ServerIP == IP && timeoutEntry.ServerPort == Port)
					{
						base.List.Remove(timeoutEntry);
						break;
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
		public void RemoveConnectionEntry(string request_id)
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				for (int i = 0; i < base.List.Count; i++)
				{
					TimeoutEntry timeoutEntry = base.List[i] as TimeoutEntry;
					if (timeoutEntry != null && timeoutEntry.ConnectionRequestID == request_id)
					{
						base.List.Remove(timeoutEntry);
						break;
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
		public void RemoveAllEntries()
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				base.List.Clear();
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
		public void CheckTimeouts()
		{
			object syncRoot;
			Monitor.Enter(syncRoot = base.List.SyncRoot);
			try
			{
				if (base.List.Count != 0)
				{
					for (int i = 0; i < base.List.Count; i++)
					{
						TimeoutEntry timeoutEntry = base.List[i] as TimeoutEntry;
						if (timeoutEntry != null && timeoutEntry.TimeoutTime < DateTime.Now)
						{
							this.m_Parent.ConnectionRequestTimedOut(timeoutEntry.ServerIP, timeoutEntry.ServerPort, timeoutEntry.ConnectionRequestID);
							base.List.Remove(timeoutEntry);
							break;
						}
					}
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
		}
	}
}
