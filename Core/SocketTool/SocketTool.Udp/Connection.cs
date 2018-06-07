using System;
using System.Net;
namespace SocketTool.Udp
{
	internal class Connection : IConnection
	{
		private ReliableQueue m_RQueue;
		private string m_EncryptionKey;
		private IPEndPoint m_RemoteEP;
		private uint m_LastSentPacket;
		private uint m_LastReceivedPacket;
		private uint m_LastSentPacketR;
		private uint m_LastReceivedPacketR;
		private uint m_LastReceivedPacketSeq;
		private bool m_Authed;
		private bool m_Server;
		private DateTime m_TimeoutTime;
		private object m_UserObject;
		private string m_RequestID;
		private CommonUdp m_Parent;
		private Command CompoundCommand;
		public ReliableQueue RQueue
		{
			get
			{
				return this.m_RQueue;
			}
			set
			{
				this.m_RQueue = value;
			}
		}
		public string RequestID
		{
			get
			{
				return this.m_RequestID;
			}
			set
			{
				this.m_RequestID = value;
			}
		}
		public string EncryptionKey
		{
			get
			{
				return this.m_EncryptionKey;
			}
			set
			{
				this.m_EncryptionKey = value;
			}
		}
		public IPEndPoint RemoteEP
		{
			get
			{
				return this.m_RemoteEP;
			}
			set
			{
				this.m_RemoteEP = value;
			}
		}
		public uint LastSentPacket
		{
			get
			{
				return this.m_LastSentPacket;
			}
			set
			{
				this.m_LastSentPacket = value;
			}
		}
		public uint LastReceivedPacket
		{
			get
			{
				return this.m_LastReceivedPacket;
			}
			set
			{
				this.m_LastReceivedPacket = value;
			}
		}
		public uint LastSentPacketR
		{
			get
			{
				return this.m_LastSentPacketR;
			}
			set
			{
				this.m_LastSentPacketR = value;
			}
		}
		public uint LastReceivedPacketR
		{
			get
			{
				return this.m_LastReceivedPacketR;
			}
			set
			{
				this.m_LastReceivedPacketR = value;
			}
		}
		public uint LastReceivedPacketSeq
		{
			get
			{
				return this.m_LastReceivedPacketSeq;
			}
			set
			{
				this.m_LastReceivedPacketSeq = value;
			}
		}
		public bool Authed
		{
			get
			{
				return this.m_Authed;
			}
			set
			{
				this.m_Authed = value;
			}
		}
		public bool Server
		{
			get
			{
				return this.m_Server;
			}
			set
			{
				this.m_Server = value;
			}
		}
		public DateTime TimeoutTime
		{
			get
			{
				return this.m_TimeoutTime;
			}
			set
			{
				this.m_TimeoutTime = value;
			}
		}
		public object UserObject
		{
			get
			{
				return this.m_UserObject;
			}
			set
			{
				this.m_UserObject = value;
			}
		}
		public Connection(CommonUdp udp)
		{
			this.EncryptionKey = "";
			this.RemoteEP = null;
			this.LastSentPacket = 0u;
			this.LastReceivedPacket = 0u;
			this.LastSentPacketR = 1u;
			this.LastReceivedPacketR = 0u;
			this.LastReceivedPacketSeq = 0u;
			this.Authed = false;
			this.Server = false;
			this.m_Parent = udp;
			this.TimeoutTime = DateTime.Now.AddSeconds((double)UdpConsts.CONNECTION_TIMEOUT);
			this.CompoundCommand = null;
			this.m_RequestID = "";
			this.RQueue = new ReliableQueue();
		}
		public void ProcessCommandPacket(string command_packet)
		{
			try
			{
				int num = 0;
				Command command = new Command();
				command.OPCode = command_packet.Substring(num, 2);
				num += 2;
				command.SequenceNum = Util.BytesToUint(command_packet.Substring(num, 4));
				num += 4;
				command.Flags = (byte)command_packet[num];
				num++;
				command.NumFields = Util.BytesToShort(command_packet.Substring(num, 2));
				num += 2;
				command.FieldSizes = new short[(int)command.NumFields];
				for (short num2 = 0; num2 < command.NumFields; num2 += 1)
				{
					command.FieldSizes[(int)num2] = Util.BytesToShort(command_packet.Substring(num, 2));
					num += 2;
				}
				if ((command.Flags & UdpConsts.FLAGS_RELIABLE) > 0)
				{
					this.SendUnreliableCommand(0, UdpConsts.OPCODE_RELIABLEACK, new string[]
					{
						command.SequenceNum.ToString()
					});
					if (this.LastReceivedPacketR == command.SequenceNum)
					{
						return;
					}
					this.LastReceivedPacketR = command.SequenceNum;
				}
				else
				{
					this.LastReceivedPacket = command.SequenceNum;
					if ((command.Flags & UdpConsts.FLAGS_SEQUENCED) > 0)
					{
						if (this.LastReceivedPacketSeq > command.SequenceNum)
						{
							return;
						}
						this.LastReceivedPacketSeq = command.SequenceNum;
					}
				}
				if ((command.Flags & UdpConsts.FLAGS_ENCRYPTED) <= 0 || !(this.EncryptionKey != "") || !(Util.XORCrypt(command_packet.Substring(num, 2), this.EncryptionKey) != UdpConsts.ENCRYPT_CHECK_STRING))
				{
					num += 2;
					command.AllFields = command_packet.Substring(num);
					if ((command.Flags & UdpConsts.FLAGS_RELIABLE) > 0)
					{
						if ((command.Flags & UdpConsts.FLAGS_COMPOUNDPIECE) > 0)
						{
							if (this.CompoundCommand == null)
							{
								this.CompoundCommand = new Command();
								this.CompoundCommand.AllFields = command.AllFields;
								this.CompoundCommand.FieldSizes = command.FieldSizes;
								this.CompoundCommand.Flags = command.Flags;
								Command expr_1F3 = this.CompoundCommand;
								expr_1F3.Flags &= UdpConsts.FLAGS_COMPOUNDPIECE;
								this.CompoundCommand.NumFields = command.NumFields;
								this.CompoundCommand.SequenceNum = command.SequenceNum;
								this.CompoundCommand.OPCode = command.OPCode;
								return;
							}
							Command expr_245 = this.CompoundCommand;
							expr_245.AllFields += command.AllFields;
							return;
						}
						else
						{
							if ((command.Flags & UdpConsts.FLAGS_COMPOUNDEND) > 0)
							{
								Command expr_275 = this.CompoundCommand;
								expr_275.AllFields += command.AllFields;
								command = this.CompoundCommand;
								this.CompoundCommand = null;
							}
						}
					}
					command.Initialize();
					if ((command.Flags & UdpConsts.FLAGS_ENCRYPTED) > 0)
					{
						command.AllFields = "";
						for (int i = 0; i < (int)command.NumFields; i++)
						{
							command.Fields[i] = Util.XORCrypt(command.Fields[i], this.EncryptionKey);
							Command expr_2DA = command;
							expr_2DA.AllFields += command.Fields[i];
						}
					}
					this.ProcessCompletedCommand(command);
				}
			}
			catch
			{
			}
		}
		public void ProcessCompletedCommand(Command cmd)
		{
			if (!this.Authed && cmd.OPCode == UdpConsts.OPCODE_LOGINDETAILS)
			{
				this.m_Parent.ConnectionAuthing(this, cmd);
				return;
			}
			if (cmd.OPCode == UdpConsts.OPCODE_PING)
			{
				if (this.Authed)
				{
					this.UpdateTimeout();
					if (!this.Server)
					{
						this.SendUnreliableCommand(0, UdpConsts.OPCODE_PING, null);
					}
				}
				return;
			}
			if (cmd.OPCode == UdpConsts.OPCODE_LOGINACK)
			{
				if (cmd.Fields[0] == "OK")
				{
					this.Authed = true;
					this.m_Parent.AuthenticatedWithConnection(this, true, "");
					return;
				}
				this.m_Parent.AuthenticatedWithConnection(this, false, cmd.Fields[1]);
				this.m_Parent.RemoveConnection(this, false, cmd.Fields[1]);
				return;
			}
			else
			{
				if (cmd.OPCode == UdpConsts.OPCODE_DISCONNECT)
				{
					this.m_Parent.RemoveConnection(this, false, cmd.Fields[0]);
					return;
				}
				if (cmd.OPCode == UdpConsts.OPCODE_RELIABLEACK)
				{
					ReliableEntry reliableEntry = null;
					this.RQueue.GetCurrentReliableCommand(out reliableEntry);
					if (reliableEntry != null)
					{
						try
						{
							if (reliableEntry.SequenceNum == Convert.ToUInt32(cmd.Fields[0]))
							{
								this.RQueue.NextReliableCommand();
								ReliableEntry reliableEntry2 = null;
								this.RQueue.GetCurrentReliableCommand(out reliableEntry2);
								if (reliableEntry2 != null)
								{
									this.m_Parent.SendData(this.RemoteEP.Address.ToString(), this.RemoteEP.Port, reliableEntry2.CommandPacket);
								}
							}
						}
						catch (Exception)
						{
						}
					}
				}
				if (this.Authed)
				{
					this.m_Parent.CommandReceived(this, cmd);
				}
				return;
			}
		}
		public int SendUnreliableCommand(byte flags, string opcode, string[] fields)
		{
			flags &= UdpConsts.FLAGS_RELIABLE;
			return this.m_Parent.SendCommand(this.m_RemoteEP.Address.ToString(), this.m_RemoteEP.Port, this.m_EncryptionKey, this.m_LastSentPacket, flags, opcode, fields);
		}
		public int SendReliableCommand(byte flags, string opcode, string[] fields)
		{
			flags |= UdpConsts.FLAGS_RELIABLE;
			flags &= UdpConsts.FLAGS_SEQUENCED;
			return this.m_Parent.SendCommand(this.m_RemoteEP.Address.ToString(), this.m_RemoteEP.Port, this.m_EncryptionKey, this.m_LastSentPacketR, flags, opcode, fields);
		}
		public int CacheReliablePacket(string packet)
		{
			ReliableEntry reliableEntry = new ReliableEntry();
			reliableEntry.SequenceNum = this.m_LastSentPacketR;
			reliableEntry.CommandPacket = packet;
			return this.m_RQueue.AddReliableCommand(reliableEntry);
		}
		public void UpdateTimeout()
		{
			this.TimeoutTime = DateTime.Now.AddSeconds((double)UdpConsts.CONNECTION_TIMEOUT);
		}
		public void ResendReliablePacket()
		{
			ReliableEntry reliableEntry = null;
			this.RQueue.GetCurrentReliableCommand(out reliableEntry);
			if (reliableEntry != null)
			{
				this.m_Parent.SendData(this.RemoteEP.Address.ToString(), this.RemoteEP.Port, reliableEntry.CommandPacket);
			}
		}
	}
}
