using System;
namespace SocketTool.Udp
{
	internal class Command : ICommand
	{
		private string m_OPCode;
		private uint m_SequenceNum;
		private byte m_Flags;
		private short m_NumFields;
		private short[] m_FieldSizes;
		private string[] m_Fields;
		private string m_AllFields;
		public string OPCode
		{
			get
			{
				return this.m_OPCode;
			}
			set
			{
				this.m_OPCode = value;
			}
		}
		public uint SequenceNum
		{
			get
			{
				return this.m_SequenceNum;
			}
			set
			{
				this.m_SequenceNum = value;
			}
		}
		public byte Flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}
		public short NumFields
		{
			get
			{
				return this.m_NumFields;
			}
			set
			{
				this.m_NumFields = value;
			}
		}
		public short[] FieldSizes
		{
			get
			{
				return this.m_FieldSizes;
			}
			set
			{
				this.m_FieldSizes = value;
			}
		}
		public string[] Fields
		{
			get
			{
				return this.m_Fields;
			}
			set
			{
				this.m_Fields = value;
			}
		}
		public string AllFields
		{
			get
			{
				return this.m_AllFields;
			}
			set
			{
				this.m_AllFields = value;
			}
		}
		public int Initialize()
		{
			if (this.NumFields == 0)
			{
				return UdpConsts.UDP_OK;
			}
			int result;
			try
			{
				int num = 0;
				this.Fields = new string[(int)this.NumFields];
				for (int i = 0; i < (int)this.NumFields; i++)
				{
					this.Fields[i] = this.AllFields.Substring(num, (int)this.FieldSizes[i]);
					num += (int)this.FieldSizes[i];
				}
				result = UdpConsts.UDP_OK;
			}
			catch
			{
				this.Fields = null;
				result = UdpConsts.UDP_FAIL;
			}
			return result;
		}
	}
}
