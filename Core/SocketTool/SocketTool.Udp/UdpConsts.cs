using System;
namespace SocketTool.Udp
{
	public class UdpConsts
	{
		public static int MAX_EXCEPTIONS = 10;
		public static int MAX_COMMAND_LEN = 512;
		public static int MAX_FIELD_LEN = 2000;
		public static int MAX_FIELDS = 50;
		public static int CONNECTION_TIMEOUT = 15;
		public static int CONNECTION_CLEAN_TIME = 3;
		public static int CONNECTION_PING_DELAY = 5;
		public static int CONNECTION_RELIABLE_RETRY = 2;
		public static int CONNECTION_TIMEOUT_CHECK = 2;
		public static int CONNECTION_TIMEOUT_TIME = 5;
		public static int BAN_CHECK_TIME = 20;
		public static int DEFAULT_MAX_WARNINGS = 3;
		public static int MAX_RELIABLE_QUEUED = 25;
		public static string ENCRYPT_CHECK_STRING = "GC";
		public static string OPCODE_COMPOUNDPIECE = "00";
		public static string OPCODE_CONNECTIONREQUEST = "01";
		public static string OPCODE_CONNECTIONACK = "02";
		public static string OPCODE_LOGINDETAILS = "03";
		public static string OPCODE_LOGINACK = "04";
		public static string OPCODE_DISCONNECT = "05";
		public static string OPCODE_PING = "06";
		public static string OPCODE_RELIABLEACK = "07";
		public static byte FLAGS_NONE = 0;
		public static byte FLAGS_CONNECTIONLESS = 1;
		public static byte FLAGS_ENCRYPTED = 2;
		public static byte FLAGS_COMPOUNDPIECE = 4;
		public static byte FLAGS_COMPOUNDEND = 8;
		public static byte FLAGS_RELIABLE = 16;
		public static byte FLAGS_SEQUENCED = 32;
		public static int UDP_OK = 0;
		public static int UDP_FAIL = 1000;
		public static int UDP_NOTFOUND = 1001;
		public static int UDP_ALREADYCONNECTED = 1002;
		public static int UDP_ALREADYINQUEUE = 1003;
		public static int UDP_UNRELIABLETOOLONG = 1004;
		public static int UDP_RELIABLENODESTINATION = 1005;
		public static int UDP_UNABLETORESOLVE = 1006;
		public static int UDP_RELIABLEQUEUEFULL = 1007;
		public static int UDP_STATE_IDLE = 0;
		public static int UDP_STATE_LISTENING = 1;
		public static int UDP_STATE_CLOSING = 2;
	}
}
