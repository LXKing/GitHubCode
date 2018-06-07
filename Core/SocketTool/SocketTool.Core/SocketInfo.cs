using System;
namespace SocketTool.Core
{
	[Serializable]
	public class SocketInfo
	{
		public string Names
		{
			get;
			set;
		}
		public string Types
		{
			get;
			set;
		}
		public string Format
		{
			get;
			set;
		}
		public string ServerIp
		{
			get;
			set;
		}
		public int Port
		{
			get;
			set;
		}
		public string Protocol
		{
			get;
			set;
		}
		public string Data
		{
			get;
			set;
		}
		public bool IsAuto
		{
			get;
			set;
		}
		public SocketInfo()
		{
			this.Format = "UTF-8";
			this.Protocol = "Tcp";
			this.Port = 9087;
			this.ServerIp = "127.0.0.1";
			this.Data = "请录入测试数据";
            this.Names = string.Empty;
            this.Types = string.Empty;
		}
	}
}
