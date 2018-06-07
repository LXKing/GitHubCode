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
    public class Command1 : IC.Command.ICommand,IDisposable
    {
        public SerialPortHelper1 mycom = new SerialPortHelper1();
        /// <summary>
        /// 构造函数
        /// </summary>
        public Command1()
        {
            mycom.Sp_PortNum = 1;
            
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
            set 
            { 
                _btRead = value;
            }
        }
        private string _btHexString = string.Empty;
        /// <summary>
        /// 十六进制字符串，空格进行分割
        /// </summary>
        public string btHexString
        {
            get
            {
                return _btHexString;
            }
        }

        /// <summary>
        /// 写入数据（字节数组）
        /// </summary>
        /// <returns></returns>
        public bool WriteData()
        {
            try
            {
                var cmdType = btWrite.Take(2).ByteToHexString(" ");
                
                mycom = new SerialPortHelper1();
                mycom.Open();
                
                if (mycom.SendData(_btWrite))
                {
                    switch (cmdType)
                    {
                        case "FF A1":
                            Thread.Sleep(btWrite.Count * 300);
                            break;
                        case "FF A2":
                            Thread.Sleep((btWrite.Count * 400) > 3000 ? 3000 : (btWrite.Count * 400));
                            break;
                        case "FF A3":
                            Thread.Sleep(btWrite.Count * 300);
                            break;
                        case "FF A4":
                            Thread.Sleep(btWrite.Count * 200);
                            break;
                    }
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
                mycom.ReadData(ReadLength);
                _btRead = mycom.ReceviedData.Data.ToList();
                _btHexString = mycom.ReceviedData.HexString;
                if (_btRead.Count > 0)
                {
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                mycom.Close();
                mycom.Dispose();
            }
        }

        #endregion

        public void Dispose()
        {
            this.mycom.Dispose();
        }
    }
}
