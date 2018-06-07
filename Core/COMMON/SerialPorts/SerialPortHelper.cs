using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace COMMON.SerialPorts
{
    /// <summary>
    /// 串口通信类
    /// </summary>
    public class SerialPortHelper
    {
        /// <summary>
        /// 默认端口号:COM1
        /// 波特率:9600
        /// 数据位:8
        /// 奇偶校验:None(无)
        /// 标准停止位数:1
        /// 超时时间:100ms:
        /// </summary>
        public SerialPortHelper()
        {
            // 
            // TODO: 在此处添加构造函数逻辑 
            //
        }
        /// <summary>
        /// 端口号：1、2、3、4......
        /// </summary>
        public int PortNum=1; //1,2,3,4
        /// <summary>
        /// 波特率：1200,2400,4800,9600
        /// </summary>
        public int BaudRate=9600; //1200,2400,4800,9600
        /// <summary>
        /// 每个字节的标准数据位长度(5--8之间)。
        /// </summary>
        public byte ByteSize=8;
        /// <summary>
        /// 获取或设置奇偶校验检查协议
        /// 0:不发生奇偶校验检查
        /// 1:设置奇偶校验位，使位数等于奇数。
        /// 2:设置奇偶校验位，使位数等于偶数。
        /// 3:将奇偶校验位保留为 1。
        /// 4:将奇偶校验位保留为 0。
        /// </summary>
        public byte Parity=0; // 0-4=no,odd,even,mark,space
        /// <summary>
        /// 标准停止位数
        /// </summary>
        public byte StopBits=1; // 0,1,2 = 1, 1.5, 2 
        /// <summary>
        /// 获取或设置读取操作未完成时发生超时之前的毫秒数
        /// </summary>
        public int ReadTimeout=100; //10

        //comm port win32 file handle
        private int hComm = -1;
        /// <summary>
        /// 
        /// </summary>
        public bool Opened = false;

        //win32 api constants
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const int OPEN_EXISTING = 3;
        private const int INVALID_HANDLE_VALUE = -1;

        [StructLayout(LayoutKind.Sequential)]
        private struct DCB
        {
            //taken from c struct in platform sdk 
            public int DCBlength;           // sizeof(DCB) 
            public int BaudRate;            // current baud rate 
            public int fBinary;          // binary mode, no EOF check 
            public int fParity;          // enable parity checking 
            public int fOutxCtsFlow;      // CTS output flow control 
            public int fOutxDsrFlow;      // DSR output flow control 
            public int fDtrControl;       // DTR flow control type 
            public int fDsrSensitivity;   // DSR sensitivity 
            public int fTXContinueOnXoff; // XOFF continues Tx 
            public int fOutX;          // XON/XOFF out flow control 
            public int fInX;           // XON/XOFF in flow control 
            public int fErrorChar;     // enable error replacement 
            public int fNull;          // enable null stripping 
            public int fRtsControl;     // RTS flow control 
            public int fAbortOnError;   // abort on error 
            public int fDummy2;        // reserved 
            public ushort wReserved;          // not currently used 
            public ushort XonLim;             // transmit XON threshold 
            public ushort XoffLim;            // transmit XOFF threshold 
            public byte ByteSize;           // number of bits/byte, 4-8 
            public byte Parity;             // 0-4=no,odd,even,mark,space 
            public byte StopBits;           // 0,1,2 = 1, 1.5, 2 
            public char XonChar;            // Tx and Rx XON character 
            public char XoffChar;           // Tx and Rx XOFF character 
            public char ErrorChar;          // error replacement character 
            public char EofChar;            // end of input character 
            public char EvtChar;            // received event character 
            public ushort wReserved1;         // reserved; do not use 
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }

        [DllImport("kernel32.dll")]
        private static extern int CreateFile(
         string lpFileName,                         // file name
         uint dwDesiredAccess,                      // access mode
         int dwShareMode,                          // share mode
         int lpSecurityAttributes, // SD
         int dwCreationDisposition,                // how to create
         int dwFlagsAndAttributes,                 // file attributes
         int hTemplateFile                        // handle to template file
         );
        [DllImport("kernel32.dll")]
        private static extern bool GetCommState(
         int hFile,  // handle to communications device
         ref DCB lpDCB    // device-control block
         );
        [DllImport("kernel32.dll")]
        private static extern bool BuildCommDCB(
         string lpDef,  // device-control string
         ref DCB lpDCB     // device-control block
         );
        [DllImport("kernel32.dll")]
        private static extern bool SetCommState(
         int hFile,  // handle to communications device
         ref DCB lpDCB    // device-control block
         );
        [DllImport("kernel32.dll")]
        private static extern bool GetCommTimeouts(
         int hFile,                  // handle to comm device
         ref COMMTIMEOUTS lpCommTimeouts  // time-out values
         );
        [DllImport("kernel32.dll")]
        private static extern bool SetCommTimeouts(
         int hFile,                  // handle to comm device
         ref COMMTIMEOUTS lpCommTimeouts  // time-out values
         );
        [DllImport("kernel32.dll")]
        private static extern bool ReadFile(
         int hFile,                // handle to file
         byte[] lpBuffer,             // data buffer
         int nNumberOfBytesToRead,  // number of bytes to read
         ref int lpNumberOfBytesRead, // number of bytes read
         ref OVERLAPPED lpOverlapped    // overlapped buffer
         );
        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(
         int hFile,                    // handle to file
         byte[] lpBuffer,                // data buffer
         int nNumberOfBytesToWrite,     // number of bytes to write
         ref int lpNumberOfBytesWritten,  // number of bytes written
         ref OVERLAPPED lpOverlapped        // overlapped buffer
         );
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(
         int hObject   // handle to object
         );
        /// <summary>
        /// 打开串口
        /// </summary>
        public void Open()
        {
            DCB dcbCommPort = new DCB();
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();
            // OPEN THE COMM PORT.
            hComm = CreateFile("COM" + PortNum, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
            // IF THE PORT CANNOT BE OPENED, BAIL OUT.
            if (hComm == INVALID_HANDLE_VALUE)
            {
                throw (new ApplicationException("Comm Port Can Not Be Opened"));
            }
            // SET THE COMM TIMEOUTS.
            GetCommTimeouts(hComm, ref ctoCommPort);
            ctoCommPort.ReadTotalTimeoutConstant = ReadTimeout;
            ctoCommPort.ReadTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutConstant = 0;
            SetCommTimeouts(hComm, ref ctoCommPort);

            // SET BAUD RATE, PARITY, WORD SIZE, AND STOP BITS.
            // THERE ARE OTHER WAYS OF DOING SETTING THESE BUT THIS IS THE EASIEST.
            // IF YOU WANT TO LATER ADD CODE FOR OTHER BAUD RATES, REMEMBER
            // THAT THE ARGUMENT FOR BuildCommDCB MUST BE A POINTER TO A STRING.
            // ALSO NOTE THAT BuildCommDCB() DEFAULTS TO NO HANDSHAKING.

            dcbCommPort.DCBlength = Marshal.SizeOf(dcbCommPort);
            GetCommState(hComm, ref dcbCommPort);
            dcbCommPort.BaudRate = BaudRate;
            dcbCommPort.Parity = Parity;
            dcbCommPort.ByteSize = ByteSize;
            dcbCommPort.StopBits = StopBits;
            SetCommState(hComm, ref dcbCommPort);

            Opened = true;

        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            if (hComm != INVALID_HANDLE_VALUE)
            {
                CloseHandle(hComm);
                Opened = false;
            }
        }
        /// <summary>
        /// 读取串口接收到的数据，返回一个字节数组
        /// </summary>
        /// <param name="BytesCount">要读取的字节数</param>
        /// <returns></returns>
        public List<byte> Read(int BytesCount)
        {
            byte[] BufBytes;
            byte[] OutBytes;
            BufBytes = new byte[BytesCount];
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesRead = 0;
                ReadFile(hComm, BufBytes, BytesCount, ref BytesRead, ref ovlCommPort);
                OutBytes = new byte[BytesRead];
                Array.Copy(BufBytes, OutBytes, BytesRead);
            }
            else
            {
                throw (new ApplicationException("端口未打开!"));
            }
            List<byte> listByte = new List<byte>();
            foreach(byte bt in OutBytes)
            {
                listByte.Add(bt);
            }
            return listByte;
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="listByte">发送字节数组</param>
        /// <returns>返回值int 表示写入端口的数据个数</returns>
        public int Write(List<byte> listByte)
        {
            byte[] WriteBytes = new byte[listByte.Count];
            for (int i = 0; i < listByte.Count;i++ )
            {
                WriteBytes[i] = listByte[i];
            }

            int BytesWritten = 0;
            if (hComm != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                WriteFile(hComm, WriteBytes, WriteBytes.Length, ref BytesWritten, ref ovlCommPort);
            }
            else
            {
                throw (new ApplicationException("Comm Port Not Open"));
            }
            return BytesWritten;
        }
    }
}
