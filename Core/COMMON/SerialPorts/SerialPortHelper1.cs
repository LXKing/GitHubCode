using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Collections;
namespace COMMON.SerialPorts
{
    public class SerialPortHelper1:IDisposable
    {
        private SerialPort sp = new SerialPort();

        #region 属性
        private int _sp_PortNum = 3;

        public int Sp_PortNum
        {
            get { return _sp_PortNum; }
            set
            {
                _sp_PortNum = value;
                sp.PortName = string.Format("COM{0}", _sp_PortNum);
            }
        }
        private Parity _sp_Parity = Parity.None;

        public Parity Sp_Parity
        {
            get { return _sp_Parity; }
            set
            {
                _sp_Parity = value;
                sp.Parity = _sp_Parity;
            }
        }

        private int _sp_DataBits = 8;

        public int Sp_DataBits
        {
            get { return _sp_DataBits; }
            set
            {
                _sp_DataBits = value;
                sp.DataBits = _sp_DataBits;
            }
        }

        private StopBits _sp_StopBits = StopBits.One;
        public StopBits Sp_StopBits
        {
            get { return _sp_StopBits; }
            set
            {
                _sp_StopBits = value;
                sp.StopBits = _sp_StopBits;
            }
        }

        private int _sp_BaudRate = 9600;

        public int Sp_BaudRate
        {
            get { return _sp_BaudRate; }
            set
            {
                _sp_BaudRate = value;
                sp.BaudRate = _sp_BaudRate;
            }
        }

        private SerialPortData receviedData = new SerialPortData();

        private int _sp_ReadTimeout = 2000;

        public int Sp_ReadTimeout
        {
            get { return _sp_ReadTimeout; }
            set
            {
                _sp_ReadTimeout = value;
                sp.ReadTimeout = _sp_ReadTimeout;
            }
        }
        #endregion

        public SerialPortData ReceviedData
        {
            get 
            { 
                return receviedData;
            }
        }
        

        public SerialPortHelper1()
        {
            sp.DataReceived += sp_DataReceived;
        }
        public bool IsOpen()
        {
            return sp.IsOpen;
        }
        public bool Open()
        {
            if (!IsOpen())
            {
                try
                {
                    sp.Open();
                    return true;
                }
                catch (System.IO.IOException e)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        public void Close()
        {
            if (IsOpen())
            {
                sp.Close();
                sp.Dispose();
            }
        }
        public void Dispose()
        {
            sp.Dispose();
        }

        public bool SendData(IEnumerable<byte> sendData)
        {
            try
            {
                this.Open();
                receviedData.Clear();
                receviedData = new SerialPortData();
                sp.Write(sendData.ToArray(), 0, sendData.Count());
                return  true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }

        public IEnumerable<byte> ReadData(int length)
        {
            while(receviedData.Data.Count()<length)
            {
                ;
            }
            var result =  receviedData.Data.ToList().Take(length).ToList();
            return result;
        }

        /// <summary>
        /// 发完后关闭串口
        /// </summary>
        /// <param name="stringData"></param>
        public void SendStringData(string stringData,bool autoClosed=true)
        {
            try
            {
                this.Open();
                sp.WriteLine(stringData);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(autoClosed)
                    Close();
            }
        }
        public string ReadStringData(bool autoClosed = true)
        {
            try
            {
                this.Open();
                var result  = sp.ReadLine();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (autoClosed)
                    Close();
            }
        }
        public void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = sp.BytesToRead;
            var btArray=new byte[n];
            sp.Read(btArray,0, n);
            receviedData.AddRange(btArray);
        }
        public void ClearBuffer()
        {
            receviedData.Clear();
        }
        
    }
    public class SerialPortData
    {
        public SerialPortData()
        {
            
        }
        private List<byte> data=new List<byte>();
        public IEnumerable<byte> Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value.ToList();
            }
        }
        private List<string> hexData = new List<string>();
        public IEnumerable<string> HexData
        {
            get
            {
                return hexData;
            }
        }
        private string hexString = string.Empty;
        public string HexString
        {
            get
            {
                return hexString;
            }
        }

        public void AddRange(IEnumerable<byte> dataList)
        {
            this.data.AddRange(dataList);
            dataList.ForEach(x =>
            {
                hexData.Add(x.ToString("X2"));
            });
            hexString = string.Join(" ", hexData);
        }

        public void Clear()
        {
            this.data.Clear();
            hexString = string.Empty;
            hexData.Clear();
        }
    }
}
