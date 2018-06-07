using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using COMMON.SerialPorts;

namespace IC.Command
{
    /// <summary>
    /// 串口通信命令类
    /// </summary>
    public class Command0:ICommand
    {
        public SerialPortHelper0 mycom = new SerialPortHelper0();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Command0()
        {
            mycom.PortNum = 1;
            
        }
        #region ICommand 成员
        /// <summary>
        /// 串口要发送的数据
        /// </summary>
        private List<byte> _btWrite;
        /// <summary>
        /// 串口接收到的数据
        /// </summary>
        public List<byte> btWrite
        {
            get { return _btWrite; }
            set { _btWrite = value; }
        }

        private List<byte> _btRead;
        public List<byte> btRead
        {
            get { return _btRead; }
            set { _btRead = value; }
        }

        /// <summary>
        /// 写入数据（字节数组）
        /// </summary>
        /// <returns></returns>
        public bool WriteData()
        {
            try
            {
                mycom.Open();
                //byte[] btWrite=new byte[_btWrite.Count];
                //_btWrite.CopyTo(btWrite, 0);
                if (mycom.Write(_btWrite) == _btWrite.Count)
                {
                    Thread.Sleep(_btWrite.Count * 25);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取数据（字节数组）
        /// </summary>
        /// <param name="ReadLength">要读取的字节数</param>
        /// <returns></returns>
        public bool ReadData(int ReadLength)
        {
            try
            {
                _btRead = mycom.Read(ReadLength);

                if (_btRead.Count > 0)
                {
                    mycom.Close();
                    return true;
                }
                else
                {
                    mycom.Close();
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
