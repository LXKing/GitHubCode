using System;
using System.Collections;
using System.Threading;
namespace SocketTool.Udp
{
	internal class ReliableQueue : Queue
	{
		public ReliableQueue()
		{
			this.Clear();
		}
		public int AddReliableCommand(ReliableEntry cmd)
		{
			if (this.Count >= UdpConsts.MAX_RELIABLE_QUEUED)
			{
				return UdpConsts.UDP_RELIABLEQUEUEFULL;
			}
			object syncRoot;
			Monitor.Enter(syncRoot = this.SyncRoot);
			try
			{
				if (this.Count > 0)
				{
					ReliableEntry reliableEntry = this.Peek() as ReliableEntry;
					if (reliableEntry != null && reliableEntry.SequenceNum == cmd.SequenceNum)
					{
						return UdpConsts.UDP_ALREADYINQUEUE;
					}
				}
				this.Enqueue(cmd);
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return UdpConsts.UDP_OK;
		}
		public int GetCurrentReliableCommand(out ReliableEntry cmd_out)
		{
			cmd_out = null;
			object syncRoot;
			Monitor.Enter(syncRoot = this.SyncRoot);
			try
			{
				if (this.Count == 0)
				{
					return UdpConsts.UDP_NOTFOUND;
				}
				cmd_out = (this.Peek() as ReliableEntry);
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return UdpConsts.UDP_OK;
		}
		public int NextReliableCommand()
		{
			object syncRoot;
			Monitor.Enter(syncRoot = this.SyncRoot);
			try
			{
				if (this.Count == 0)
				{
					return UdpConsts.UDP_NOTFOUND;
				}
				this.Dequeue();
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return UdpConsts.UDP_OK;
		}
		public int ClearReliableQueue()
		{
			object syncRoot;
			Monitor.Enter(syncRoot = this.SyncRoot);
			try
			{
				this.Clear();
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return UdpConsts.UDP_OK;
		}
		public bool CommandsWaiting()
		{
			object syncRoot;
			Monitor.Enter(syncRoot = this.SyncRoot);
			bool result;
			try
			{
				if (this.Count > 0)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				Monitor.Exit(syncRoot);
			}
			return result;
		}
	}
}
