using System;
using System.Collections;
using System.Net.Sockets;
namespace SocketTool.Core
{
	public class SocketUtil
	{
		public static string LastError = string.Empty;
		private static Hashtable ErrorMsgMap = new Hashtable();
		public static bool SetKeepAlive(Socket socket, ulong turnOnAfter, ulong keepAliveInterval)
		{
			int num = 4;
			int num2 = 8;
			try
			{
				byte[] array = new byte[3 * num];
				ulong[] array2 = new ulong[3];
				if (turnOnAfter == 0uL || keepAliveInterval == 0uL)
				{
					array2[0] = 0uL;
				}
				else
				{
					array2[0] = 1uL;
				}
				array2[1] = turnOnAfter;
				array2[2] = keepAliveInterval;
				for (int i = 0; i < array2.Length; i++)
				{
					array[i * num + 3] = (byte)(array2[i] >> (num - 1) * num2 & 255uL);
					array[i * num + 2] = (byte)(array2[i] >> (num - 2) * num2 & 255uL);
					array[i * num + 1] = (byte)(array2[i] >> (num - 3) * num2 & 255uL);
					array[i * num] = (byte)(array2[i] >> (num - 4) * num2 & 255uL);
				}
				byte[] bytes = BitConverter.GetBytes(0);
                socket.IOControl(-1744830460, array, bytes);//socket.IOControl((IOControlCode)((ulong)-1744830460), array, bytes);
                
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}
		private static void SetErrorMsg()
		{
			SocketUtil.ErrorMsgMap[10004] = "操作被取消";
			SocketUtil.ErrorMsgMap[10013] = "请求的地址是一个广播地址，但不设置标志";
			SocketUtil.ErrorMsgMap[10014] = "无效的参数";
			SocketUtil.ErrorMsgMap[10022] = "套接字没有绑定，无效的地址，或听不调用之前接受";
			SocketUtil.ErrorMsgMap[10024] = "没有更多的文件描述符，接受队列是空的";
			SocketUtil.ErrorMsgMap[10035] = "套接字是非阻塞的，指定的操作将阻止";
			SocketUtil.ErrorMsgMap[10036] = "一个阻塞的Winsock操作正在进行中";
			SocketUtil.ErrorMsgMap[10037] = "操作完成，没有阻塞操作正在进行中";
			SocketUtil.ErrorMsgMap[10038] = "描述符不是一个套接字";
			SocketUtil.ErrorMsgMap[10039] = "目标地址是必需的";
			SocketUtil.ErrorMsgMap[10040] = "数据报太大，无法进入缓冲区，将被截断";
			SocketUtil.ErrorMsgMap[10041] = "指定的端口是为这个套接字错误类型";
			SocketUtil.ErrorMsgMap[10042] = "股权不明，或不支持的";
			SocketUtil.ErrorMsgMap[10043] = "指定的端口是不支持";
			SocketUtil.ErrorMsgMap[10044] = "套接字类型不支持在此地址族";
			SocketUtil.ErrorMsgMap[10045] = " Socket是不是一个类型，它支持面向连接的服务";
			SocketUtil.ErrorMsgMap[10047] = "地址族不支持";
			SocketUtil.ErrorMsgMap[10048] = "地址在使用中";
			SocketUtil.ErrorMsgMap[10049] = "地址是不是可以从本地机器";
			SocketUtil.ErrorMsgMap[10050] = "网络子系统失败";
			SocketUtil.ErrorMsgMap[10051] = "网络可以从这个主机在这个时候不能达到";
			SocketUtil.ErrorMsgMap[10052] = "连接超时设置SO_KEEPALIVE时";
			SocketUtil.ErrorMsgMap[10053] = "连接被中止，由于超时或其他故障";
			SocketUtil.ErrorMsgMap[10054] = "连接被重置连接被远程端重置远程端";
			SocketUtil.ErrorMsgMap[10055] = "无缓冲区可用空间";
			SocketUtil.ErrorMsgMap[10056] = "套接字已连接";
			SocketUtil.ErrorMsgMap[10057] = "套接字未连接";
			SocketUtil.ErrorMsgMap[10058] = "套接字已关闭";
			SocketUtil.ErrorMsgMap[10060] = "尝试连接超时";
			SocketUtil.ErrorMsgMap[10061] = "连接被强制拒绝";
			SocketUtil.ErrorMsgMap[10101] = "监听服务已关闭";
			SocketUtil.ErrorMsgMap[10201] = "套接字已创建此对象";
			SocketUtil.ErrorMsgMap[10202] = "套接字尚未创建此对象";
			SocketUtil.ErrorMsgMap[11001] = "权威的答案：找不到主机";
			SocketUtil.ErrorMsgMap[11002] = "非权威的答案：找不到主机";
			SocketUtil.ErrorMsgMap[11003] = "非可恢复的错误";
			SocketUtil.ErrorMsgMap[11004] = "有效的名称，没有请求类型的数据记录";
		}
		public static string DescrError(int ErrorCode)
		{
			if (SocketUtil.ErrorMsgMap.Count == 0)
			{
				SocketUtil.SetErrorMsg();
			}
			return string.Concat(SocketUtil.ErrorMsgMap[ErrorCode]);
		}
		public static bool HandleSocketError(SocketException socketExc)
		{
			bool result = false;
			if (socketExc != null)
			{
				result = true;
			}
			return result;
		}
	}
}
