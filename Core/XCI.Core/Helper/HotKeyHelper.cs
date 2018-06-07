using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace XCI.Helper
{
    /// <summary>
    /// Represents a system hot key.
    /// </summary>
    public class HotKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HotKey"/> by specific name, modifiers, and key.
        /// </summary>
        /// <param name="name">A <see cref="string"/> to identify the system hot key.</param>
        /// <param name="modifiers">Modify keys (Shift, Ctrl, Alt, and/or Windows Logo key).</param>
        /// <param name="key">Key code.</param>        
        /// <param name="combinationKey"></param>
        /// <param name="combinationModifiers"></param>
        public HotKey(string name, KeyModifiers modifiers, Keys key, KeyModifiers combinationModifiers = KeyModifiers.None, Keys combinationKey = Keys.None)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (key == Keys.None)
            {
                throw new ArgumentException("key cannot be Keys.None. Must specify a key for the hot key.", "key");
            }

            _name = name;
            _modifiers = modifiers;
            _key = key;
            if (combinationKey != Keys.None || combinationModifiers != KeyModifiers.None)
            {
                _combinationHotKey = new CombinationHotKey(combinationModifiers, combinationKey);
            }
        }


        /// <summary>
        /// Gets or sets the ID of the hot key.
        /// </summary>
        internal int Id { get; set; }

        private string _name;
        /// <summary>
        /// A <see cref="string"/> used to identify the hot key.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        private KeyModifiers _modifiers = KeyModifiers.None;
        /// <summary>
        /// Modifier keys of this hot key.
        /// </summary>
        public KeyModifiers Modifiers
        {
            get { return _modifiers; }
        }

        private Keys _key = Keys.None;
        /// <summary>
        /// Key of this hot key.
        /// </summary>
        public Keys Key
        {
            get { return _key; }
        }

        /// <summary>
        /// 注册状态
        /// </summary>
        public bool Status { get; set; }

        private CombinationHotKey _combinationHotKey;
        public CombinationHotKey CombinationHotKey
        {
            get { return _combinationHotKey; }
        }
        public override string ToString()
        {
            string mainKey = GetMainKeyName();
            if (!string.IsNullOrEmpty(GetCombinationKeyName()))
            {
                mainKey += "," + GetCombinationKeyName();
            }
            if (Status)
            {
                mainKey += "," + " 注册成功";
            }
            else
            {
                mainKey += "," + " 注册失败";
            }
            return mainKey;
        }
        /// <summary>
        /// 获取主键描述
        /// </summary>
        /// <returns></returns>
        public string GetMainKeyName()
        {
            return Modifiers.ToString() + "+" + Key.ToString();
        }
        /// <summary>
        /// 获取子键描述
        /// </summary>
        /// <returns></returns>
        public string GetCombinationKeyName()
        {
            if (CombinationHotKey == null)
            {
                return "";
            }
            return CombinationHotKey.ToString();
        }
    }

    /// <summary>
    ///原项目地址:http://hotkey.codeplex.com/
    ///1 此热键管理工具是在CodePlex上的一个工具的基础上修改而来,感谢原作者
    ///2 原工具主要支持以下功能
    ///   a 注册全局热键 
    ///   b 快捷键命名
    ///   c "双击"快捷键 
    ///3 修改后支持的功能
    ///   a 增加复合热键的支持，如Ctrl+E,Ctrl+L或Ctrl+E,L
    ///   b 去掉"双击"快捷键
    ///   c 增加快捷键状态提示事件,如果"按下Ctrl+E，等待按下第二个键"
    ///   d 增加快捷键转储，恢复功能
    ///   e 新的示例
    ///                                      2011.04.15 LvLaoSan
    /// Registers, listens, and unregisters system hot keys.
    /// </summary>
    public class SystemHotKeyListener : IDisposable
    {
        /// <summary>
        /// 注册的热键信息
        /// </summary>
        struct RegisteredHotKey
        {
            public KeyModifiers KeyModifiers;
            public Keys Key;
            public int Id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemHotKeyListener"/>.
        /// </summary>
        public SystemHotKeyListener()
        {
            _listeningForm = new Form();
            _listeningForm.Visible = false;
            _messageFilter = new SystemHotKeyMessageFilter(this);
            Application.AddMessageFilter(_messageFilter);
        }
        #region fields

        /// <summary>
        /// 是否正在等待按下复合键中的子键
        /// </summary>
        bool waitingCombinationKey;

        private Form _listeningForm;

        private ICollection<CombinationHotKey> _combinationHotKey = new List<CombinationHotKey>();

        /// <summary>
        /// 记录所有实际注册的热键,用于在切换应用程序时以注销或恢复热键
        /// </summary>
        private readonly List<RegisteredHotKey> RegisteredHotKeyes = new List<RegisteredHotKey>();

        internal Dictionary<int, HotKey> _hotKeys = new Dictionary<int, HotKey>();
        private readonly SystemHotKeyMessageFilter _messageFilter;

        private int nextId = 100;

        protected HotKey _lastPressedHotKey;
        protected DateTime _lastPressedTime = DateTime.MinValue;


        #endregion

        #region events

        /// <summary>
        /// Occurs when the registered system hot key was pressed.
        /// </summary>
        public event SystemHotKeyPressedEventHandler SystemHotKeyPressed;
        public event SystemHotKeyPressedEventHandler SystemHotKeyDoublePressed;

        /// <summary>
        /// 状态信息变化后
        /// </summary>
        public event Action<SystemHotKeyListener, String> StateInfoChanged;

        #endregion

        #region properties

        public ICollection<HotKey> HotKeys
        {
            get { return _hotKeys.Values; }
        }

        ICollection<CombinationHotKey> CombinationHotKey
        {
            get { return _combinationHotKey; }
            set { _combinationHotKey = value; }
        }

        #endregion

        #region methods

        /// <summary>
        /// 记录实际注册的热键
        /// 在应用程序失去变为非活动程序时注销所有已注册的热键，
        /// 在应用程序恢复为活动程序的同时要恢复热键注册
        /// </summary>
        public void KeepRegisteredHotKeyes()
        {
            RegisteredHotKeyes.Clear();
            foreach (var hotKey in HotKeys)
            {
                if (hotKey.Id > 0)
                {
                    RegisteredHotKeyes.Add(
                        new RegisteredHotKey { Id = hotKey.Id, Key = hotKey.Key, KeyModifiers = hotKey.Modifiers }
                        );
                }
            }
            foreach (var hotKey in CombinationHotKey)
            {
                if (hotKey.id > 0)
                {
                    RegisteredHotKeyes.Add(
                        new RegisteredHotKey { Id = hotKey.id, Key = hotKey.Key, KeyModifiers = hotKey.Modifiers }
                        );
                }
            }
            RegisteredHotKeyes.ForEach(
                hotkey => HotKeyApi.UnregisterHotKey(_listeningForm.Handle, hotkey.Id));
            InvokeStateInfoChanged("转储实际注册的热键");
        }
        /// <summary>
        /// 恢复热键
        /// 一般在应用程序恢复为活动程序时调用
        /// </summary>
        public void RestoreHotKeyes()
        {
            RegisteredHotKeyes.ForEach(
                hotkey => HotKeyApi.RegisterHotKey(_listeningForm.Handle, hotkey.Id, (uint)hotkey.KeyModifiers, hotkey.Key));
            RegisteredHotKeyes.Clear();
            InvokeStateInfoChanged("恢复了注册的热键");
        }

        /// <summary>
        /// Registers a new system hot key.
        /// </summary>
        /// <param name="hotKey">The <see cref="HotKey"/> that need to register.</param>
        /// <exception cref="InvalidOperationException">
        /// The same hot key or name already exists.
        /// </exception>
        public bool Register(HotKey hotKey)
        {
            // Check for duplicate hot key name.
            foreach (HotKey existingHotKey in _hotKeys.Values)
            {
                if (existingHotKey.Name == hotKey.Name)
                {
                    throw new InvalidOperationException("The hot key with name of '" + hotKey.Name + "' has been already registered.");
                }

                if (existingHotKey.Key == hotKey.Key && existingHotKey.Modifiers == hotKey.Modifiers)
                {
                    if (existingHotKey.CombinationHotKey == null || hotKey.CombinationHotKey == null)
                    {
                        throw new InvalidOperationException("The same hot key has been already registered.");
                    }
                    if (existingHotKey.Key == hotKey.CombinationHotKey.Key && existingHotKey.CombinationHotKey.Modifiers == hotKey.CombinationHotKey.Modifiers)
                    {
                        throw new InvalidOperationException("The same hot key has been already registered.");
                    }
                }
            }
            // Register the hot key.
            hotKey.Id = nextId;
            _hotKeys.Add(nextId, hotKey);
            var res = HotKeyApi.RegisterHotKey(_listeningForm.Handle, nextId, (uint)(hotKey.Modifiers), hotKey.Key);
            // Increase the internal hot key id.
            hotKey.Status = res;
            nextId++;
            return res;
        }

        /// <summary>
        /// 获取复合键
        /// </summary>
        /// <returns></returns>
        List<CombinationHotKey> GetCombinationHotKeys(HotKey mainHotKey)
        {
            var q = from _hotKey in HotKeys
                    where
                        _hotKey.Modifiers == mainHotKey.Modifiers && _hotKey.Key == mainHotKey.Key &&
                        _hotKey.CombinationHotKey != null
                    select _hotKey.CombinationHotKey;
            return q.ToList();
        }

        /// <summary>
        /// 注册复合快捷键的子键
        /// </summary>
        /// <param name="combinationHotKeys"></param>
        void RegisterCombinationKey(List<CombinationHotKey> combinationHotKeys)
        {
            _combinationHotKey.Clear();
            combinationHotKeys.ForEach(_hotKey =>
            {
                _combinationHotKey.Add(_hotKey);
                var find = HotKeys.AsQueryable().FirstOrDefault(
                    hotkey => hotkey.Key == _hotKey.Key && hotkey.Modifiers == _hotKey.Modifiers
                                                        );
                if (find == null)
                {
                    _hotKey.IsRegisted = true;
                    _hotKey.id = nextId;
                    HotKeyApi.RegisterHotKey(_listeningForm.Handle, nextId, (uint)(_hotKey.Modifiers), _hotKey.Key);
                    nextId++;
                }
            });
        }

        /// <summary>
        /// Unregisters the specific hot key.
        /// </summary>
        /// <param name="hotKey">The <see cref="HotKey"/> that needs to be unregistered.</param>
        /// <returns>A <see cref="bool"/> value that indicates whether the operation is successed.</returns>
       public bool Unregister(HotKey hotKey)
        {
            if (_hotKeys.ContainsKey(hotKey.Id))
            {
                var res = HotKeyApi.UnregisterHotKey(_listeningForm.Handle, hotKey.Id);
                _hotKeys.Remove(hotKey.Id);
                return res;
            }
            return false;
        }

        /// <summary>
        /// 反注册组合键的子键
        /// </summary>
        /// <returns></returns>
        void UnregisterCombinationKeys()
        {
            CombinationHotKey.ToList().ForEach(
                hotkey =>
                {
                    if (hotkey.IsRegisted)
                    {
                        HotKeyApi.UnregisterHotKey(_listeningForm.Handle, hotkey.id);

                    }
                });
            CombinationHotKey.Clear();
        }

        /// <summary>
        /// 注销本程序注册的所有系统热键
        /// </summary>
        public void UnregisterAll()
        {
            int[] idList = new int[_hotKeys.Count];
            _hotKeys.Keys.CopyTo(idList, 0);
            foreach (int id in idList)
            {
                HotKeyApi.UnregisterHotKey(_listeningForm.Handle, id);
                _hotKeys.Remove(id);
            }
            UnregisterCombinationKeys();
            InvokeStateInfoChanged("快捷键系统注销");
        }

        void InvokeSystemHotKeyPressed(HotKey hotKey, bool regCombinationKeys = false)
        {
            List<CombinationHotKey> combinationKeys = null;
            if (regCombinationKeys)
            {
                combinationKeys = GetCombinationHotKeys(hotKey);
            }
            if (combinationKeys == null || combinationKeys.Count == 0)
            {
                InvokeStateInfoChanged("");
                SystemHotKeyPressedEventArgs e = new SystemHotKeyPressedEventArgs(hotKey);
                //2011.04.15 lugreen 去掉对"双击"快捷键的支持
                //if (_lastPressedHotKey == hotKey)
                //{
                //    TimeSpan diff = DateTime.Now.Subtract(_lastPressedTime);
                //    if (diff.TotalMilliseconds <= SystemInformation.DoubleClickTime)
                //    {
                //        // Double click
                //        _lastPressedTime = DateTime.MinValue;

                //        OnSystemHotKeyDoublePressed(e);
                //        return;
                //    }
                //}                
                //_lastPressedTime = DateTime.Now;
                // Single click
                _lastPressedHotKey = null;
                OnSystemHotKeyPressed(e);
            }
            else
            {
                _lastPressedHotKey = hotKey;
                //_lastPressedTime = DateTime.Now;
                waitingCombinationKey = true;
                RegisterCombinationKey(combinationKeys);
                InvokeStateInfoChanged(string.Format("{0}已按下。正在等待按下第二个键...", _lastPressedHotKey.GetMainKeyName()));
                //注册子键                                
            }
        }
        /// <summary>
        /// 调用组合快捷键
        /// </summary>
        /// <param name="combinationHotKey"></param>
        internal void InvokeCombinationHotKeyPressed(CombinationHotKey combinationHotKey)
        {
            var q = from key in HotKeys
                    where key.Modifiers == _lastPressedHotKey.Modifiers && key.Key == _lastPressedHotKey.Key
                      && key.CombinationHotKey != null && key.CombinationHotKey.Key == combinationHotKey.Key &&
                      key.CombinationHotKey.Modifiers == combinationHotKey.Modifiers
                    select key;
            SystemHotKeyPressedEventArgs e = new SystemHotKeyPressedEventArgs(q.FirstOrDefault());
            OnSystemHotKeyPressed(e);
        }

        protected virtual void OnSystemHotKeyPressed(SystemHotKeyPressedEventArgs e)
        {
            if (SystemHotKeyPressed != null)
            {
                SystemHotKeyPressedEventHandler invoker = SystemHotKeyPressed;
                invoker(this, e);
            }
        }

        protected virtual void OnSystemHotKeyDoublePressed(SystemHotKeyPressedEventArgs e)
        {
            if (SystemHotKeyDoublePressed != null)
            {
                SystemHotKeyPressedEventHandler invoker = SystemHotKeyDoublePressed;
                invoker(this, e);
            }
        }

        internal void ProcessHotkey(Message m)
        {

            if (m.Msg == HotKeyApi.WM_HOTKEY)
            {
                KeyModifiers modifiers = GetModifiers(m.LParam);
                Keys key = GetKey(m.LParam);

                int sid = m.WParam.ToInt32();

                if (!waitingCombinationKey)
                {
                    Debug.WriteLine("nowaiting");
                    if (this._hotKeys.ContainsKey(sid))
                    {
                        Debug.WriteLine("containskey");
                        HotKey hotKey = this._hotKeys[sid];
                        this.InvokeSystemHotKeyPressed(hotKey, true);

                    }
                    else
                    {
                        Debug.WriteLine("nocontainskey");
                        InvokeStateInfoChanged("");
                    }
                }
                else
                {
                    Debug.WriteLine("waitingKey");
                    waitingCombinationKey = false;

                    //查找复合键                    
                    var finded = CombinationHotKey.FirstOrDefault(combinationKey => combinationKey.Key == key
                        && combinationKey.Modifiers == modifiers);

                    if (finded != null)
                    {
                        var findedHotKey = FindHotKey(_lastPressedHotKey, finded);
                        if (findedHotKey != null)
                        {
                            this.InvokeSystemHotKeyPressed(findedHotKey);
                        }
                        else
                        {
                            Debug.WriteLine("not found childkey");
                        }
                    }
                    else
                    {
                        var tmpHotkey = new CombinationHotKey(modifiers, key);
                        InvokeStateInfoChanged(string.Format("热键({0})未注册",
                            _lastPressedHotKey.GetMainKeyName() + "," + tmpHotkey));
                    }
                    UnregisterCombinationKeys();
                }
            }
            else
            {
                if (m.Msg == HotKeyApi.WM_KEYDOWN)
                {
                    Keys pressedKey = (Keys)m.WParam;
                    Debug.WriteLine("按下:" + pressedKey + "-" + (int)pressedKey);
                    if (waitingCombinationKey)
                    {
                        //如果按下了访问键，则查找是否注册了访问键相关的热键
                        if (pressedKey != Keys.ControlKey && pressedKey != Keys.ShiftKey && pressedKey != Keys.Alt)
                        {
                            String msg = String.Format("组合键({0},{1})不是命令", _lastPressedHotKey.GetMainKeyName(), pressedKey);
                            InvokeStateInfoChanged(msg);
                            waitingCombinationKey = false;
                            UnregisterCombinationKeys();
                        }
                    }

                }

            }
        }

        /// <summary>
        /// 查找具有指定主键的和子键的热键
        /// </summary>
        /// <param name="mainKey">主键过滤</param>
        /// <param name="combinationHotKey">子键过滤</param>
        /// <returns></returns>
        private HotKey FindHotKey(HotKey mainKey, CombinationHotKey combinationHotKey)
        {
            var q = from key in HotKeys
                    where key.CombinationHotKey != null
                          && key.Key == mainKey.Key
                          && key.Modifiers == mainKey.Modifiers
                          && key.CombinationHotKey.Key == combinationHotKey.Key
                          && key.CombinationHotKey.Modifiers == combinationHotKey.Modifiers
                    select key;
            var r = q.FirstOrDefault();
            return r;
        }

        Keys GetKey(IntPtr lparam)
        {
            return (Keys)(((int)lparam >> 16) & 0xFFFF);
        }
        KeyModifiers GetModifiers(IntPtr lparam)
        {
            KeyModifiers modifier = (KeyModifiers)((int)lparam & 0xFFFF);
            return modifier;
        }

        void InvokeStateInfoChanged(string stateMsg)
        {
            Action<SystemHotKeyListener, string> handler = StateInfoChanged;
            if (handler != null) handler(this, stateMsg);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases all the resources used by the <see cref="SystemHotKeyListener"/>.
        /// </summary>
        public void Dispose()
        {
            UnregisterAll();
            _listeningForm.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// Processes the system hot key message.
    /// </summary>
    internal class SystemHotKeyMessageFilter : IMessageFilter
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="SystemHotKeyMessageFilter"/> with
        /// the specific <see cref="SystemHotKeyListener"/>
        /// </summary>
        /// <param name="listner">An instance of the <see cref="SystemHotKeyListener"/>.</param>
        public SystemHotKeyMessageFilter(SystemHotKeyListener listner)
        {
            if (listner == null)
            {
                throw new ArgumentNullException("listner");
            }
            _listner = listner;
        }

        private SystemHotKeyListener _listner;

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            _listner.ProcessHotkey(m);
            //switch (m.Msg)
            //{
            //    case HotKeyApi.WM_HOTKEY:
            //        // Get the system hot key message.
            //        _listner.ProcessHotkey(m);
            //        break;
            //}
            return false;
        }
        #endregion
    }

    /// <summary>
    /// Provides data for the SystemHotKeyListener.SystemHotKeyPressed event.
    /// </summary>
    public class SystemHotKeyPressedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemHotKeyPressedEventArgs"/> class.
        /// </summary>
        /// <param name="hotKey">A <see cref="HotKey"/> instance that specifies which hot key was pressed.</param>
        public SystemHotKeyPressedEventArgs(HotKey hotKey)
        {
            _hotKey = hotKey;
        }

        private HotKey _hotKey;
        /// <summary>
        /// Gets the hot key that was just pressed.
        /// </summary>
        public HotKey PressedHotKey
        {
            get { return _hotKey; }
        }

        /// <summary>
        /// Gets the name of the hot key that was just pressed.
        /// </summary>
        public string HotKeyName
        {
            get { return _hotKey.Name; }
        }
    }

    public delegate void SystemHotKeyPressedEventHandler(object sender,SystemHotKeyPressedEventArgs e);

    /// <summary>
    /// Modifier keys.
    /// </summary>
    [Flags]
    public enum KeyModifiers
    {
        /// <summary>
        /// No modifier key was pressed.
        /// </summary>
        None = 0,
        /// <summary>
        /// Alt key was pressed.
        /// </summary>
        Alt = 1,
        /// <summary>
        /// Ctrl key was pressed.
        /// </summary>
        Control = 2,
        /// <summary>
        /// Shift key was pressed.
        /// </summary>
        Shift = 4,
        /// <summary>
        /// Windows Logo key was pressed.
        /// </summary>
        Windows = 8

    }

    /// <summary>
    /// 
    /// </summary>
    public class CombinationHotKey
    {
        public CombinationHotKey(KeyModifiers modifiers, Keys key)
        {
            _modifiers = modifiers;
            _key = key;
            IsRegisted = false;
        }
        private KeyModifiers _modifiers;
        /// <summary>
        /// Modifier keys of this hot key.
        /// </summary>
        public KeyModifiers Modifiers
        {
            get { return _modifiers; }
        }

        private Keys _key;
        /// <summary>
        /// Key of this hot key.
        /// </summary>
        public Keys Key
        {
            get { return _key; }
        }

        /// <summary>
        /// 是否注册了快捷键
        /// </summary>
        public bool IsRegisted { get; set; }

        public int id = -1;
        public override string ToString()
        {
            string modifi = "";
            string key = "";

            if (Modifiers != KeyModifiers.None)
            {
                modifi = Modifiers.ToString();
            }

            if (Key != Keys.None)
            {
                key = Key.ToString();
            }
            return (modifi + "+" + key).Trim('+');

        }
    }

    /// <summary>
    /// Contains windows APIs used in this library.
    /// </summary>
    public class HotKeyApi
    {
        /// <summary>
        /// Registers a system hot key.
        /// </summary>
        /// <param name="hWnd">Handle of the window.</param>
        /// <param name="id">Hot key identifier.</param>
        /// <param name="fsModifiers">Key modifiers.</param>
        /// <param name="vk">Virtual key code.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

        /// <summary>
        /// Unregisters a system hot key.
        /// </summary>
        /// <param name="hWnd">Handle of the window.</param>
        /// <param name="id">Hot key identifier.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// The windows message that indicates a hot key was pressed.
        /// </summary>
        //public const int WM_HOTKEY = 0x0312;
        //public const int WM_KEYDOWN =0x100;
        //public const int WM_SYSKEYDOWN = 0x104;
        //http://www.codeproject.com/KB/cs/cswindowsmessages.aspx
        public const int WM_NULL = 0x0000;
        public const int WM_CREATE = 0x0001;
        public const int WM_DESTROY = 0x0002;
        public const int WM_MOVE = 0x0003;
        public const int WM_SIZE = 0x0005;
        public const int WM_ACTIVATE = 0x0006;
        public const int WM_SETFOCUS = 0x0007;
        public const int WM_KILLFOCUS = 0x0008;
        public const int WM_ENABLE = 0x000A;
        public const int WM_SETREDRAW = 0x000B;
        public const int WM_SETTEXT = 0x000C;
        public const int WM_GETTEXT = 0x000D;
        public const int WM_GETTEXTLENGTH = 0x000E;
        public const int WM_PAINT = 0x000F;
        public const int WM_CLOSE = 0x0010;
        public const int WM_QUERYENDSESSION = 0x0011;
        public const int WM_QUERYOPEN = 0x0013;
        public const int WM_ENDSESSION = 0x0016;
        public const int WM_QUIT = 0x0012;
        public const int WM_ERASEBKGND = 0x0014;
        public const int WM_SYSCOLORCHANGE = 0x0015;
        public const int WM_SHOWWINDOW = 0x0018;
        public const int WM_WININICHANGE = 0x001A;
        public const int WM_SETTINGCHANGE = 0x001A;
        public const int WM_DEVMODECHANGE = 0x001B;
        public const int WM_ACTIVATEAPP = 0x001C;
        public const int WM_FONTCHANGE = 0x001D;
        public const int WM_TIMECHANGE = 0x001E;
        public const int WM_CANCELMODE = 0x001F;
        public const int WM_SETCURSOR = 0x0020;
        public const int WM_MOUSEACTIVATE = 0x0021;
        public const int WM_CHILDACTIVATE = 0x0022;
        public const int WM_QUEUESYNC = 0x0023;
        public const int WM_GETMINMAXINFO = 0x0024;
        public const int WM_PAINTICON = 0x0026;
        public const int WM_ICONERASEBKGND = 0x0027;
        public const int WM_NEXTDLGCTL = 0x0028;
        public const int WM_SPOOLERSTATUS = 0x002A;
        public const int WM_DRAWITEM = 0x002B;
        public const int WM_MEASUREITEM = 0x002C;
        public const int WM_DELETEITEM = 0x002D;
        public const int WM_VKEYTOITEM = 0x002E;
        public const int WM_CHARTOITEM = 0x002F;
        public const int WM_SETFONT = 0x0030;
        public const int WM_GETFONT = 0x0031;
        public const int WM_SETHOTKEY = 0x0032;
        public const int WM_GETHOTKEY = 0x0033;
        public const int WM_QUERYDRAGICON = 0x0037;
        public const int WM_COMPAREITEM = 0x0039;
        public const int WM_GETOBJECT = 0x003D;
        public const int WM_COMPACTING = 0x0041;
        public const int WM_COMMNOTIFY = 0x0044;
        public const int WM_WINDOWPOSCHANGING = 0x0046;
        public const int WM_WINDOWPOSCHANGED = 0x0047;
        public const int WM_POWER = 0x0048;
        public const int WM_COPYDATA = 0x004A;
        public const int WM_CANCELJOURNAL = 0x004B;
        public const int WM_NOTIFY = 0x004E;
        public const int WM_INPUTLANGCHANGEREQUEST = 0x0050;
        public const int WM_INPUTLANGCHANGE = 0x0051;
        public const int WM_TCARD = 0x0052;
        public const int WM_HELP = 0x0053;
        public const int WM_USERCHANGED = 0x0054;
        public const int WM_NOTIFYFORMAT = 0x0055;
        public const int WM_CONTEXTMENU = 0x007B;
        public const int WM_STYLECHANGING = 0x007C;
        public const int WM_STYLECHANGED = 0x007D;
        public const int WM_DISPLAYCHANGE = 0x007E;
        public const int WM_GETICON = 0x007F;
        public const int WM_SETICON = 0x0080;
        public const int WM_NCCREATE = 0x0081;
        public const int WM_NCDESTROY = 0x0082;
        public const int WM_NCCALCSIZE = 0x0083;
        public const int WM_NCHITTEST = 0x0084;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_NCACTIVATE = 0x0086;
        public const int WM_GETDLGCODE = 0x0087;
        public const int WM_SYNCPAINT = 0x0088;
        public const int WM_NCMOUSEMOVE = 0x00A0;
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCLBUTTONUP = 0x00A2;
        public const int WM_NCLBUTTONDBLCLK = 0x00A3;
        public const int WM_NCRBUTTONDOWN = 0x00A4;
        public const int WM_NCRBUTTONUP = 0x00A5;
        public const int WM_NCRBUTTONDBLCLK = 0x00A6;
        public const int WM_NCMBUTTONDOWN = 0x00A7;
        public const int WM_NCMBUTTONUP = 0x00A8;
        public const int WM_NCMBUTTONDBLCLK = 0x00A9;
        public const int WM_NCXBUTTONDOWN = 0x00AB;
        public const int WM_NCXBUTTONUP = 0x00AC;
        public const int WM_NCXBUTTONDBLCLK = 0x00AD;
        public const int WM_INPUT = 0x00FF;
        public const int WM_KEYFIRST = 0x0100;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_CHAR = 0x0102;
        public const int WM_DEADCHAR = 0x0103;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;
        public const int WM_SYSCHAR = 0x0106;
        public const int WM_SYSDEADCHAR = 0x0107;
        public const int WM_UNICHAR = 0x0109;
        public const int WM_KEYLAST_NT501 = 0x0109;
        public const int UNICODE_NOCHAR = 0xFFFF;
        public const int WM_KEYLAST_PRE501 = 0x0108;
        public const int WM_IME_STARTCOMPOSITION = 0x010D;
        public const int WM_IME_ENDCOMPOSITION = 0x010E;
        public const int WM_IME_COMPOSITION = 0x010F;
        public const int WM_IME_KEYLAST = 0x010F;
        public const int WM_INITDIALOG = 0x0110;
        public const int WM_COMMAND = 0x0111;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int WM_TIMER = 0x0113;
        public const int WM_HSCROLL = 0x0114;
        public const int WM_VSCROLL = 0x0115;
        public const int WM_INITMENU = 0x0116;
        public const int WM_INITMENUPOPUP = 0x0117;
        public const int WM_MENUSELECT = 0x011F;
        public const int WM_MENUCHAR = 0x0120;
        public const int WM_ENTERIDLE = 0x0121;
        public const int WM_MENURBUTTONUP = 0x0122;
        public const int WM_MENUDRAG = 0x0123;
        public const int WM_MENUGETOBJECT = 0x0124;
        public const int WM_UNINITMENUPOPUP = 0x0125;
        public const int WM_MENUCOMMAND = 0x0126;
        public const int WM_CHANGEUISTATE = 0x0127;
        public const int WM_UPDATEUISTATE = 0x0128;
        public const int WM_QUERYUISTATE = 0x0129;
        public const int WM_CTLCOLORMSGBOX = 0x0132;
        public const int WM_CTLCOLOREDIT = 0x0133;
        public const int WM_CTLCOLORLISTBOX = 0x0134;
        public const int WM_CTLCOLORBTN = 0x0135;
        public const int WM_CTLCOLORDLG = 0x0136;
        public const int WM_CTLCOLORSCROLLBAR = 0x0137;
        public const int WM_CTLCOLORSTATIC = 0x0138;
        public const int WM_MOUSEFIRST = 0x0200;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_MBUTTONUP = 0x0208;
        public const int WM_MBUTTONDBLCLK = 0x0209;
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_XBUTTONDOWN = 0x020B;
        public const int WM_XBUTTONUP = 0x020C;
        public const int WM_XBUTTONDBLCLK = 0x020D;
        public const int WM_MOUSELAST_5 = 0x020D;
        public const int WM_MOUSELAST_4 = 0x020A;
        public const int WM_MOUSELAST_PRE_4 = 0x0209;
        public const int WM_PARENTNOTIFY = 0x0210;
        public const int WM_ENTERMENULOOP = 0x0211;
        public const int WM_EXITMENULOOP = 0x0212;
        public const int WM_NEXTMENU = 0x0213;
        public const int WM_SIZING = 0x0214;
        public const int WM_CAPTURECHANGED = 0x0215;
        public const int WM_MOVING = 0x0216;
        public const int WM_POWERBROADCAST = 0x0218;
        public const int WM_DEVICECHANGE = 0x0219;
        public const int WM_MDICREATE = 0x0220;
        public const int WM_MDIDESTROY = 0x0221;
        public const int WM_MDIACTIVATE = 0x0222;
        public const int WM_MDIRESTORE = 0x0223;
        public const int WM_MDINEXT = 0x0224;
        public const int WM_MDIMAXIMIZE = 0x0225;
        public const int WM_MDITILE = 0x0226;
        public const int WM_MDICASCADE = 0x0227;
        public const int WM_MDIICONARRANGE = 0x0228;
        public const int WM_MDIGETACTIVE = 0x0229;
        public const int WM_MDISETMENU = 0x0230;
        public const int WM_ENTERSIZEMOVE = 0x0231;
        public const int WM_EXITSIZEMOVE = 0x0232;
        public const int WM_DROPFILES = 0x0233;
        public const int WM_MDIREFRESHMENU = 0x0234;
        public const int WM_IME_SETCONTEXT = 0x0281;
        public const int WM_IME_NOTIFY = 0x0282;
        public const int WM_IME_CONTROL = 0x0283;
        public const int WM_IME_COMPOSITIONFULL = 0x0284;
        public const int WM_IME_SELECT = 0x0285;
        public const int WM_IME_CHAR = 0x0286;
        public const int WM_IME_REQUEST = 0x0288;
        public const int WM_IME_KEYDOWN = 0x0290;
        public const int WM_IME_KEYUP = 0x0291;
        public const int WM_MOUSEHOVER = 0x02A1;
        public const int WM_MOUSELEAVE = 0x02A3;
        public const int WM_NCMOUSEHOVER = 0x02A0;
        public const int WM_NCMOUSELEAVE = 0x02A2;
        public const int WM_WTSSESSION_CHANGE = 0x02B1;
        public const int WM_TABLET_FIRST = 0x02c0;
        public const int WM_TABLET_LAST = 0x02df;
        public const int WM_CUT = 0x0300;
        public const int WM_COPY = 0x0301;
        public const int WM_PASTE = 0x0302;
        public const int WM_CLEAR = 0x0303;
        public const int WM_UNDO = 0x0304;
        public const int WM_RENDERFORMAT = 0x0305;
        public const int WM_RENDERALLFORMATS = 0x0306;
        public const int WM_DESTROYCLIPBOARD = 0x0307;
        public const int WM_DRAWCLIPBOARD = 0x0308;
        public const int WM_PAINTCLIPBOARD = 0x0309;
        public const int WM_VSCROLLCLIPBOARD = 0x030A;
        public const int WM_SIZECLIPBOARD = 0x030B;
        public const int WM_ASKCBFORMATNAME = 0x030C;
        public const int WM_CHANGECBCHAIN = 0x030D;
        public const int WM_HSCROLLCLIPBOARD = 0x030E;
        public const int WM_QUERYNEWPALETTE = 0x030F;
        public const int WM_PALETTEISCHANGING = 0x0310;
        public const int WM_PALETTECHANGED = 0x0311;
        public const int WM_HOTKEY = 0x0312;
        public const int WM_PRINT = 0x0317;
        public const int WM_PRINTCLIENT = 0x0318;
        public const int WM_APPCOMMAND = 0x0319;
        public const int WM_THEMECHANGED = 0x031A;
        public const int WM_HANDHELDFIRST = 0x0358;
        public const int WM_HANDHELDLAST = 0x035F;
        public const int WM_AFXFIRST = 0x0360;
        public const int WM_AFXLAST = 0x037F;
        public const int WM_PENWINFIRST = 0x0380;
        public const int WM_PENWINLAST = 0x038F;
        public const int WM_APP = 0x8000;
        public const int WM_USER = 0x0400;
        public const int EM_GETSEL = 0x00B0;
        public const int EM_SETSEL = 0x00B1;
        public const int EM_GETRECT = 0x00B2;
        public const int EM_SETRECT = 0x00B3;
        public const int EM_SETRECTNP = 0x00B4;
        public const int EM_SCROLL = 0x00B5;
        public const int EM_LINESCROLL = 0x00B6;
        public const int EM_SCROLLCARET = 0x00B7;
        public const int EM_GETMODIFY = 0x00B8;
        public const int EM_SETMODIFY = 0x00B9;
        public const int EM_GETLINECOUNT = 0x00BA;
        public const int EM_LINEINDEX = 0x00BB;
        public const int EM_SETHANDLE = 0x00BC;
        public const int EM_GETHANDLE = 0x00BD;
        public const int EM_GETTHUMB = 0x00BE;
        public const int EM_LINELENGTH = 0x00C1;
        public const int EM_REPLACESEL = 0x00C2;
        public const int EM_GETLINE = 0x00C4;
        public const int EM_LIMITTEXT = 0x00C5;
        public const int EM_CANUNDO = 0x00C6;
        public const int EM_UNDO = 0x00C7;
        public const int EM_FMTLINES = 0x00C8;
        public const int EM_LINEFROMCHAR = 0x00C9;
        public const int EM_SETTABSTOPS = 0x00CB;
        public const int EM_SETPASSWORDCHAR = 0x00CC;
        public const int EM_EMPTYUNDOBUFFER = 0x00CD;
        public const int EM_GETFIRSTVISIBLELINE = 0x00CE;
        public const int EM_SETREADONLY = 0x00CF;
        public const int EM_SETWORDBREAKPROC = 0x00D0;
        public const int EM_GETWORDBREAKPROC = 0x00D1;
        public const int EM_GETPASSWORDCHAR = 0x00D2;
        public const int EM_SETMARGINS = 0x00D3;
        public const int EM_GETMARGINS = 0x00D4;
        public const int EM_SETLIMITTEXT = EM_LIMITTEXT;
        public const int EM_GETLIMITTEXT = 0x00D5;
        public const int EM_POSFROMCHAR = 0x00D6;
        public const int EM_CHARFROMPOS = 0x00D7;
        public const int EM_SETIMESTATUS = 0x00D8;
        public const int EM_GETIMESTATUS = 0x00D9;
        public const int BM_GETCHECK = 0x00F0;
        public const int BM_SETCHECK = 0x00F1;
        public const int BM_GETSTATE = 0x00F2;
        public const int BM_SETSTATE = 0x00F3;
        public const int BM_SETSTYLE = 0x00F4;
        public const int BM_CLICK = 0x00F5;
        public const int BM_GETIMAGE = 0x00F6;
        public const int BM_SETIMAGE = 0x00F7;
        public const int STM_SETICON = 0x0170;
        public const int STM_GETICON = 0x0171;
        public const int STM_SETIMAGE = 0x0172;
        public const int STM_GETIMAGE = 0x0173;
        public const int STM_MSGMAX = 0x0174;
        public const int DM_GETDEFID = (WM_USER + 0);
        public const int DM_SETDEFID = (WM_USER + 1);
        public const int DM_REPOSITION = (WM_USER + 2);
        public const int LB_ADDSTRING = 0x0180;
        public const int LB_INSERTSTRING = 0x0181;
        public const int LB_DELETESTRING = 0x0182;
        public const int LB_SELITEMRANGEEX = 0x0183;
        public const int LB_RESETCONTENT = 0x0184;
        public const int LB_SETSEL = 0x0185;
        public const int LB_SETCURSEL = 0x0186;
        public const int LB_GETSEL = 0x0187;
        public const int LB_GETCURSEL = 0x0188;
        public const int LB_GETTEXT = 0x0189;
        public const int LB_GETTEXTLEN = 0x018A;
        public const int LB_GETCOUNT = 0x018B;
        public const int LB_SELECTSTRING = 0x018C;
        public const int LB_DIR = 0x018D;
        public const int LB_GETTOPINDEX = 0x018E;
        public const int LB_FINDSTRING = 0x018F;
        public const int LB_GETSELCOUNT = 0x0190;
        public const int LB_GETSELITEMS = 0x0191;
        public const int LB_SETTABSTOPS = 0x0192;
        public const int LB_GETHORIZONTALEXTENT = 0x0193;
        public const int LB_SETHORIZONTALEXTENT = 0x0194;
        public const int LB_SETCOLUMNWIDTH = 0x0195;
        public const int LB_ADDFILE = 0x0196;
        public const int LB_SETTOPINDEX = 0x0197;
        public const int LB_GETITEMRECT = 0x0198;
        public const int LB_GETITEMDATA = 0x0199;
        public const int LB_SETITEMDATA = 0x019A;
        public const int LB_SELITEMRANGE = 0x019B;
        public const int LB_SETANCHORINDEX = 0x019C;
        public const int LB_GETANCHORINDEX = 0x019D;
        public const int LB_SETCARETINDEX = 0x019E;
        public const int LB_GETCARETINDEX = 0x019F;
        public const int LB_SETITEMHEIGHT = 0x01A0;
        public const int LB_GETITEMHEIGHT = 0x01A1;
        public const int LB_FINDSTRINGEXACT = 0x01A2;
        public const int LB_SETLOCALE = 0x01A5;
        public const int LB_GETLOCALE = 0x01A6;
        public const int LB_SETCOUNT = 0x01A7;
        public const int LB_INITSTORAGE = 0x01A8;
        public const int LB_ITEMFROMPOINT = 0x01A9;
        public const int LB_MULTIPLEADDSTRING = 0x01B1;
        public const int LB_GETLISTBOXINFO = 0x01B2;
        public const int LB_MSGMAX_501 = 0x01B3;
        public const int LB_MSGMAX_WCE4 = 0x01B1;
        public const int LB_MSGMAX_4 = 0x01B0;
        public const int LB_MSGMAX_PRE4 = 0x01A8;
        public const int CB_GETEDITSEL = 0x0140;
        public const int CB_LIMITTEXT = 0x0141;
        public const int CB_SETEDITSEL = 0x0142;
        public const int CB_ADDSTRING = 0x0143;
        public const int CB_DELETESTRING = 0x0144;
        public const int CB_DIR = 0x0145;
        public const int CB_GETCOUNT = 0x0146;
        public const int CB_GETCURSEL = 0x0147;
        public const int CB_GETLBTEXT = 0x0148;
        public const int CB_GETLBTEXTLEN = 0x0149;
        public const int CB_INSERTSTRING = 0x014A;
        public const int CB_RESETCONTENT = 0x014B;
        public const int CB_FINDSTRING = 0x014C;
        public const int CB_SELECTSTRING = 0x014D;
        public const int CB_SETCURSEL = 0x014E;
        public const int CB_SHOWDROPDOWN = 0x014F;
        public const int CB_GETITEMDATA = 0x0150;
        public const int CB_SETITEMDATA = 0x0151;
        public const int CB_GETDROPPEDCONTROLRECT = 0x0152;
        public const int CB_SETITEMHEIGHT = 0x0153;
        public const int CB_GETITEMHEIGHT = 0x0154;
        public const int CB_SETEXTENDEDUI = 0x0155;
        public const int CB_GETEXTENDEDUI = 0x0156;
        public const int CB_GETDROPPEDSTATE = 0x0157;
        public const int CB_FINDSTRINGEXACT = 0x0158;
        public const int CB_SETLOCALE = 0x0159;
        public const int CB_GETLOCALE = 0x015A;
        public const int CB_GETTOPINDEX = 0x015B;
        public const int CB_SETTOPINDEX = 0x015C;
        public const int CB_GETHORIZONTALEXTENT = 0x015d;
        public const int CB_SETHORIZONTALEXTENT = 0x015e;
        public const int CB_GETDROPPEDWIDTH = 0x015f;
        public const int CB_SETDROPPEDWIDTH = 0x0160;
        public const int CB_INITSTORAGE = 0x0161;
        public const int CB_MULTIPLEADDSTRING = 0x0163;
        public const int CB_GETCOMBOBOXINFO = 0x0164;
        public const int CB_MSGMAX_501 = 0x0165;
        public const int CB_MSGMAX_WCE400 = 0x0163;
        public const int CB_MSGMAX_400 = 0x0162;
        public const int CB_MSGMAX_PRE400 = 0x015B;
        public const int SBM_SETPOS = 0x00E0;
        public const int SBM_GETPOS = 0x00E1;
        public const int SBM_SETRANGE = 0x00E2;
        public const int SBM_SETRANGEREDRAW = 0x00E6;
        public const int SBM_GETRANGE = 0x00E3;
        public const int SBM_ENABLE_ARROWS = 0x00E4;
        public const int SBM_SETSCROLLINFO = 0x00E9;
        public const int SBM_GETSCROLLINFO = 0x00EA;
        public const int SBM_GETSCROLLBARINFO = 0x00EB;
        public const int LVM_FIRST = 0x1000;// ListView messages
        public const int TV_FIRST = 0x1100;// TreeView messages;
        public const int HDM_FIRST = 0x1200;// Header messages;
        public const int TCM_FIRST = 0x1300;// Tab control messages;
        public const int PGM_FIRST = 0x1400;// Pager control messages;
        public const int ECM_FIRST = 0x1500;// Edit control messages;
        public const int BCM_FIRST = 0x1600;// Button control messages;
        public const int CBM_FIRST = 0x1700;// Combobox control messages;
        public const int CCM_FIRST = 0x2000;// Common control shared messages;
        public const int CCM_LAST = (CCM_FIRST + 0x200);
        public const int CCM_SETBKCOLOR = (CCM_FIRST + 1);
        public const int CCM_SETCOLORSCHEME = (CCM_FIRST + 2);
        public const int CCM_GETCOLORSCHEME = (CCM_FIRST + 3);
        public const int CCM_GETDROPTARGET = (CCM_FIRST + 4);
        public const int CCM_SETUNICODEFORMAT = (CCM_FIRST + 5);
        public const int CCM_GETUNICODEFORMAT = (CCM_FIRST + 6);
        public const int CCM_SETVERSION = (CCM_FIRST + 0x7);
        public const int CCM_GETVERSION = (CCM_FIRST + 0x8);
        public const int CCM_SETNOTIFYWINDOW = (CCM_FIRST + 0x9);
        public const int CCM_SETWINDOWTHEME = (CCM_FIRST + 0xb);
        public const int CCM_DPISCALE = (CCM_FIRST + 0xc);
        public const int HDM_GETITEMCOUNT = (HDM_FIRST + 0);
        public const int HDM_INSERTITEMA = (HDM_FIRST + 1);
        public const int HDM_INSERTITEMW = (HDM_FIRST + 10);
        public const int HDM_DELETEITEM = (HDM_FIRST + 2);
        public const int HDM_GETITEMA = (HDM_FIRST + 3);
        public const int HDM_GETITEMW = (HDM_FIRST + 11);
        public const int HDM_SETITEMA = (HDM_FIRST + 4);
        public const int HDM_SETITEMW = (HDM_FIRST + 12);
        public const int HDM_LAYOUT = (HDM_FIRST + 5);
        public const int HDM_HITTEST = (HDM_FIRST + 6);
        public const int HDM_GETITEMRECT = (HDM_FIRST + 7);
        public const int HDM_SETIMAGELIST = (HDM_FIRST + 8);
        public const int HDM_GETIMAGELIST = (HDM_FIRST + 9);
        public const int HDM_ORDERTOINDEX = (HDM_FIRST + 15);
        public const int HDM_CREATEDRAGIMAGE = (HDM_FIRST + 16);
        public const int HDM_GETORDERARRAY = (HDM_FIRST + 17);
        public const int HDM_SETORDERARRAY = (HDM_FIRST + 18);
        public const int HDM_SETHOTDIVIDER = (HDM_FIRST + 19);
        public const int HDM_SETBITMAPMARGIN = (HDM_FIRST + 20);
        public const int HDM_GETBITMAPMARGIN = (HDM_FIRST + 21);
        public const int HDM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int HDM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int HDM_SETFILTERCHANGETIMEOUT = (HDM_FIRST + 22);
        public const int HDM_EDITFILTER = (HDM_FIRST + 23);
        public const int HDM_CLEARFILTER = (HDM_FIRST + 24);
        public const int TB_ENABLEBUTTON = (WM_USER + 1);
        public const int TB_CHECKBUTTON = (WM_USER + 2);
        public const int TB_PRESSBUTTON = (WM_USER + 3);
        public const int TB_HIDEBUTTON = (WM_USER + 4);
        public const int TB_INDETERMINATE = (WM_USER + 5);
        public const int TB_MARKBUTTON = (WM_USER + 6);
        public const int TB_ISBUTTONENABLED = (WM_USER + 9);
        public const int TB_ISBUTTONCHECKED = (WM_USER + 10);
        public const int TB_ISBUTTONPRESSED = (WM_USER + 11);
        public const int TB_ISBUTTONHIDDEN = (WM_USER + 12);
        public const int TB_ISBUTTONINDETERMINATE = (WM_USER + 13);
        public const int TB_ISBUTTONHIGHLIGHTED = (WM_USER + 14);
        public const int TB_SETSTATE = (WM_USER + 17);
        public const int TB_GETSTATE = (WM_USER + 18);
        public const int TB_ADDBITMAP = (WM_USER + 19);
        public const int TB_ADDBUTTONSA = (WM_USER + 20);
        public const int TB_INSERTBUTTONA = (WM_USER + 21);
        public const int TB_ADDBUTTONS = (WM_USER + 20);
        public const int TB_INSERTBUTTON = (WM_USER + 21);
        public const int TB_DELETEBUTTON = (WM_USER + 22);
        public const int TB_GETBUTTON = (WM_USER + 23);
        public const int TB_BUTTONCOUNT = (WM_USER + 24);
        public const int TB_COMMANDTOINDEX = (WM_USER + 25);
        public const int TB_SAVERESTOREA = (WM_USER + 26);
        public const int TB_SAVERESTOREW = (WM_USER + 76);
        public const int TB_CUSTOMIZE = (WM_USER + 27);
        public const int TB_ADDSTRINGA = (WM_USER + 28);
        public const int TB_ADDSTRINGW = (WM_USER + 77);
        public const int TB_GETITEMRECT = (WM_USER + 29);
        public const int TB_BUTTONSTRUCTSIZE = (WM_USER + 30);
        public const int TB_SETBUTTONSIZE = (WM_USER + 31);
        public const int TB_SETBITMAPSIZE = (WM_USER + 32);
        public const int TB_AUTOSIZE = (WM_USER + 33);
        public const int TB_GETTOOLTIPS = (WM_USER + 35);
        public const int TB_SETTOOLTIPS = (WM_USER + 36);
        public const int TB_SETPARENT = (WM_USER + 37);
        public const int TB_SETROWS = (WM_USER + 39);
        public const int TB_GETROWS = (WM_USER + 40);
        public const int TB_SETCMDID = (WM_USER + 42);
        public const int TB_CHANGEBITMAP = (WM_USER + 43);
        public const int TB_GETBITMAP = (WM_USER + 44);
        public const int TB_GETBUTTONTEXTA = (WM_USER + 45);
        public const int TB_GETBUTTONTEXTW = (WM_USER + 75);
        public const int TB_REPLACEBITMAP = (WM_USER + 46);
        public const int TB_SETINDENT = (WM_USER + 47);
        public const int TB_SETIMAGELIST = (WM_USER + 48);
        public const int TB_GETIMAGELIST = (WM_USER + 49);
        public const int TB_LOADIMAGES = (WM_USER + 50);
        public const int TB_GETRECT = (WM_USER + 51);
        public const int TB_SETHOTIMAGELIST = (WM_USER + 52);
        public const int TB_GETHOTIMAGELIST = (WM_USER + 53);
        public const int TB_SETDISABLEDIMAGELIST = (WM_USER + 54);
        public const int TB_GETDISABLEDIMAGELIST = (WM_USER + 55);
        public const int TB_SETSTYLE = (WM_USER + 56);
        public const int TB_GETSTYLE = (WM_USER + 57);
        public const int TB_GETBUTTONSIZE = (WM_USER + 58);
        public const int TB_SETBUTTONWIDTH = (WM_USER + 59);
        public const int TB_SETMAXTEXTROWS = (WM_USER + 60);
        public const int TB_GETTEXTROWS = (WM_USER + 61);
        public const int TB_GETOBJECT = (WM_USER + 62);
        public const int TB_GETHOTITEM = (WM_USER + 71);
        public const int TB_SETHOTITEM = (WM_USER + 72);
        public const int TB_SETANCHORHIGHLIGHT = (WM_USER + 73);
        public const int TB_GETANCHORHIGHLIGHT = (WM_USER + 74);
        public const int TB_MAPACCELERATORA = (WM_USER + 78);
        public const int TB_GETINSERTMARK = (WM_USER + 79);
        public const int TB_SETINSERTMARK = (WM_USER + 80);
        public const int TB_INSERTMARKHITTEST = (WM_USER + 81);
        public const int TB_MOVEBUTTON = (WM_USER + 82);
        public const int TB_GETMAXSIZE = (WM_USER + 83);
        public const int TB_SETEXTENDEDSTYLE = (WM_USER + 84);
        public const int TB_GETEXTENDEDSTYLE = (WM_USER + 85);
        public const int TB_GETPADDING = (WM_USER + 86);
        public const int TB_SETPADDING = (WM_USER + 87);
        public const int TB_SETINSERTMARKCOLOR = (WM_USER + 88);
        public const int TB_GETINSERTMARKCOLOR = (WM_USER + 89);
        public const int TB_SETCOLORSCHEME = CCM_SETCOLORSCHEME;
        public const int TB_GETCOLORSCHEME = CCM_GETCOLORSCHEME;
        public const int TB_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int TB_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int TB_MAPACCELERATORW = (WM_USER + 90);
        public const int TB_GETBITMAPFLAGS = (WM_USER + 41);
        public const int TB_GETBUTTONINFOW = (WM_USER + 63);
        public const int TB_SETBUTTONINFOW = (WM_USER + 64);
        public const int TB_GETBUTTONINFOA = (WM_USER + 65);
        public const int TB_SETBUTTONINFOA = (WM_USER + 66);
        public const int TB_INSERTBUTTONW = (WM_USER + 67);
        public const int TB_ADDBUTTONSW = (WM_USER + 68);
        public const int TB_HITTEST = (WM_USER + 69);
        public const int TB_SETDRAWTEXTFLAGS = (WM_USER + 70);
        public const int TB_GETSTRINGW = (WM_USER + 91);
        public const int TB_GETSTRINGA = (WM_USER + 92);
        public const int TB_GETMETRICS = (WM_USER + 101);
        public const int TB_SETMETRICS = (WM_USER + 102);
        public const int TB_SETWINDOWTHEME = CCM_SETWINDOWTHEME;
        public const int RB_INSERTBANDA = (WM_USER + 1);
        public const int RB_DELETEBAND = (WM_USER + 2);
        public const int RB_GETBARINFO = (WM_USER + 3);
        public const int RB_SETBARINFO = (WM_USER + 4);
        public const int RB_GETBANDINFO = (WM_USER + 5);
        public const int RB_SETBANDINFOA = (WM_USER + 6);
        public const int RB_SETPARENT = (WM_USER + 7);
        public const int RB_HITTEST = (WM_USER + 8);
        public const int RB_GETRECT = (WM_USER + 9);
        public const int RB_INSERTBANDW = (WM_USER + 10);
        public const int RB_SETBANDINFOW = (WM_USER + 11);
        public const int RB_GETBANDCOUNT = (WM_USER + 12);
        public const int RB_GETROWCOUNT = (WM_USER + 13);
        public const int RB_GETROWHEIGHT = (WM_USER + 14);
        public const int RB_IDTOINDEX = (WM_USER + 16);
        public const int RB_GETTOOLTIPS = (WM_USER + 17);
        public const int RB_SETTOOLTIPS = (WM_USER + 18);
        public const int RB_SETBKCOLOR = (WM_USER + 19);
        public const int RB_GETBKCOLOR = (WM_USER + 20);
        public const int RB_SETTEXTCOLOR = (WM_USER + 21);
        public const int RB_GETTEXTCOLOR = (WM_USER + 22);
        public const int RB_SIZETORECT = (WM_USER + 23);
        public const int RB_SETCOLORSCHEME = CCM_SETCOLORSCHEME;
        public const int RB_GETCOLORSCHEME = CCM_GETCOLORSCHEME;
        public const int RB_BEGINDRAG = (WM_USER + 24);
        public const int RB_ENDDRAG = (WM_USER + 25);
        public const int RB_DRAGMOVE = (WM_USER + 26);
        public const int RB_GETBARHEIGHT = (WM_USER + 27);
        public const int RB_GETBANDINFOW = (WM_USER + 28);
        public const int RB_GETBANDINFOA = (WM_USER + 29);
        public const int RB_MINIMIZEBAND = (WM_USER + 30);
        public const int RB_MAXIMIZEBAND = (WM_USER + 31);
        public const int RB_GETDROPTARGET = (CCM_GETDROPTARGET);
        public const int RB_GETBANDBORDERS = (WM_USER + 34);
        public const int RB_SHOWBAND = (WM_USER + 35);
        public const int RB_SETPALETTE = (WM_USER + 37);
        public const int RB_GETPALETTE = (WM_USER + 38);
        public const int RB_MOVEBAND = (WM_USER + 39);
        public const int RB_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int RB_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int RB_GETBANDMARGINS = (WM_USER + 40);
        public const int RB_SETWINDOWTHEME = CCM_SETWINDOWTHEME;
        public const int RB_PUSHCHEVRON = (WM_USER + 43);
        public const int TTM_ACTIVATE = (WM_USER + 1);
        public const int TTM_SETDELAYTIME = (WM_USER + 3);
        public const int TTM_ADDTOOLA = (WM_USER + 4);
        public const int TTM_ADDTOOLW = (WM_USER + 50);
        public const int TTM_DELTOOLA = (WM_USER + 5);
        public const int TTM_DELTOOLW = (WM_USER + 51);
        public const int TTM_NEWTOOLRECTA = (WM_USER + 6);
        public const int TTM_NEWTOOLRECTW = (WM_USER + 52);
        public const int TTM_RELAYEVENT = (WM_USER + 7);
        public const int TTM_GETTOOLINFOA = (WM_USER + 8);
        public const int TTM_GETTOOLINFOW = (WM_USER + 53);
        public const int TTM_SETTOOLINFOA = (WM_USER + 9);
        public const int TTM_SETTOOLINFOW = (WM_USER + 54);
        public const int TTM_HITTESTA = (WM_USER + 10);
        public const int TTM_HITTESTW = (WM_USER + 55);
        public const int TTM_GETTEXTA = (WM_USER + 11);
        public const int TTM_GETTEXTW = (WM_USER + 56);
        public const int TTM_UPDATETIPTEXTA = (WM_USER + 12);
        public const int TTM_UPDATETIPTEXTW = (WM_USER + 57);
        public const int TTM_GETTOOLCOUNT = (WM_USER + 13);
        public const int TTM_ENUMTOOLSA = (WM_USER + 14);
        public const int TTM_ENUMTOOLSW = (WM_USER + 58);
        public const int TTM_GETCURRENTTOOLA = (WM_USER + 15);
        public const int TTM_GETCURRENTTOOLW = (WM_USER + 59);
        public const int TTM_WINDOWFROMPOINT = (WM_USER + 16);
        public const int TTM_TRACKACTIVATE = (WM_USER + 17);
        public const int TTM_TRACKPOSITION = (WM_USER + 18);
        public const int TTM_SETTIPBKCOLOR = (WM_USER + 19);
        public const int TTM_SETTIPTEXTCOLOR = (WM_USER + 20);
        public const int TTM_GETDELAYTIME = (WM_USER + 21);
        public const int TTM_GETTIPBKCOLOR = (WM_USER + 22);
        public const int TTM_GETTIPTEXTCOLOR = (WM_USER + 23);
        public const int TTM_SETMAXTIPWIDTH = (WM_USER + 24);
        public const int TTM_GETMAXTIPWIDTH = (WM_USER + 25);
        public const int TTM_SETMARGIN = (WM_USER + 26);
        public const int TTM_GETMARGIN = (WM_USER + 27);
        public const int TTM_POP = (WM_USER + 28);
        public const int TTM_UPDATE = (WM_USER + 29);
        public const int TTM_GETBUBBLESIZE = (WM_USER + 30);
        public const int TTM_ADJUSTRECT = (WM_USER + 31);
        public const int TTM_SETTITLEA = (WM_USER + 32);
        public const int TTM_SETTITLEW = (WM_USER + 33);
        public const int TTM_POPUP = (WM_USER + 34);
        public const int TTM_GETTITLE = (WM_USER + 35);
        public const int TTM_SETWINDOWTHEME = CCM_SETWINDOWTHEME;
        public const int SB_SETTEXTA = (WM_USER + 1);
        public const int SB_SETTEXTW = (WM_USER + 11);
        public const int SB_GETTEXTA = (WM_USER + 2);
        public const int SB_GETTEXTW = (WM_USER + 13);
        public const int SB_GETTEXTLENGTHA = (WM_USER + 3);
        public const int SB_GETTEXTLENGTHW = (WM_USER + 12);
        public const int SB_SETPARTS = (WM_USER + 4);
        public const int SB_GETPARTS = (WM_USER + 6);
        public const int SB_GETBORDERS = (WM_USER + 7);
        public const int SB_SETMINHEIGHT = (WM_USER + 8);
        public const int SB_SIMPLE = (WM_USER + 9);
        public const int SB_GETRECT = (WM_USER + 10);
        public const int SB_ISSIMPLE = (WM_USER + 14);
        public const int SB_SETICON = (WM_USER + 15);
        public const int SB_SETTIPTEXTA = (WM_USER + 16);
        public const int SB_SETTIPTEXTW = (WM_USER + 17);
        public const int SB_GETTIPTEXTA = (WM_USER + 18);
        public const int SB_GETTIPTEXTW = (WM_USER + 19);
        public const int SB_GETICON = (WM_USER + 20);
        public const int SB_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int SB_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int SB_SETBKCOLOR = CCM_SETBKCOLOR;
        public const int SB_SIMPLEID = 0x00ff;
        public const int TBM_GETPOS = (WM_USER);
        public const int TBM_GETRANGEMIN = (WM_USER + 1);
        public const int TBM_GETRANGEMAX = (WM_USER + 2);
        public const int TBM_GETTIC = (WM_USER + 3);
        public const int TBM_SETTIC = (WM_USER + 4);
        public const int TBM_SETPOS = (WM_USER + 5);
        public const int TBM_SETRANGE = (WM_USER + 6);
        public const int TBM_SETRANGEMIN = (WM_USER + 7);
        public const int TBM_SETRANGEMAX = (WM_USER + 8);
        public const int TBM_CLEARTICS = (WM_USER + 9);
        public const int TBM_SETSEL = (WM_USER + 10);
        public const int TBM_SETSELSTART = (WM_USER + 11);
        public const int TBM_SETSELEND = (WM_USER + 12);
        public const int TBM_GETPTICS = (WM_USER + 14);
        public const int TBM_GETTICPOS = (WM_USER + 15);
        public const int TBM_GETNUMTICS = (WM_USER + 16);
        public const int TBM_GETSELSTART = (WM_USER + 17);
        public const int TBM_GETSELEND = (WM_USER + 18);
        public const int TBM_CLEARSEL = (WM_USER + 19);
        public const int TBM_SETTICFREQ = (WM_USER + 20);
        public const int TBM_SETPAGESIZE = (WM_USER + 21);
        public const int TBM_GETPAGESIZE = (WM_USER + 22);
        public const int TBM_SETLINESIZE = (WM_USER + 23);
        public const int TBM_GETLINESIZE = (WM_USER + 24);
        public const int TBM_GETTHUMBRECT = (WM_USER + 25);
        public const int TBM_GETCHANNELRECT = (WM_USER + 26);
        public const int TBM_SETTHUMBLENGTH = (WM_USER + 27);
        public const int TBM_GETTHUMBLENGTH = (WM_USER + 28);
        public const int TBM_SETTOOLTIPS = (WM_USER + 29);
        public const int TBM_GETTOOLTIPS = (WM_USER + 30);
        public const int TBM_SETTIPSIDE = (WM_USER + 31);
        public const int TBM_SETBUDDY = (WM_USER + 32);
        public const int TBM_GETBUDDY = (WM_USER + 33);
        public const int TBM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int TBM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int DL_BEGINDRAG = (WM_USER + 133);
        public const int DL_DRAGGING = (WM_USER + 134);
        public const int DL_DROPPED = (WM_USER + 135);
        public const int DL_CANCELDRAG = (WM_USER + 136);
        public const int UDM_SETRANGE = (WM_USER + 101);
        public const int UDM_GETRANGE = (WM_USER + 102);
        public const int UDM_SETPOS = (WM_USER + 103);
        public const int UDM_GETPOS = (WM_USER + 104);
        public const int UDM_SETBUDDY = (WM_USER + 105);
        public const int UDM_GETBUDDY = (WM_USER + 106);
        public const int UDM_SETACCEL = (WM_USER + 107);
        public const int UDM_GETACCEL = (WM_USER + 108);
        public const int UDM_SETBASE = (WM_USER + 109);
        public const int UDM_GETBASE = (WM_USER + 110);
        public const int UDM_SETRANGE32 = (WM_USER + 111);
        public const int UDM_GETRANGE32 = (WM_USER + 112);
        public const int UDM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int UDM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int UDM_SETPOS32 = (WM_USER + 113);
        public const int UDM_GETPOS32 = (WM_USER + 114);
        public const int PBM_SETRANGE = (WM_USER + 1);
        public const int PBM_SETPOS = (WM_USER + 2);
        public const int PBM_DELTAPOS = (WM_USER + 3);
        public const int PBM_SETSTEP = (WM_USER + 4);
        public const int PBM_STEPIT = (WM_USER + 5);
        public const int PBM_SETRANGE32 = (WM_USER + 6);
        public const int PBM_GETRANGE = (WM_USER + 7);
        public const int PBM_GETPOS = (WM_USER + 8);
        public const int PBM_SETBARCOLOR = (WM_USER + 9);
        public const int PBM_SETBKCOLOR = CCM_SETBKCOLOR;
        public const int HKM_SETHOTKEY = (WM_USER + 1);
        public const int HKM_GETHOTKEY = (WM_USER + 2);
        public const int HKM_SETRULES = (WM_USER + 3);
        public const int LVM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int LVM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int LVM_GETBKCOLOR = (LVM_FIRST + 0);
        public const int LVM_SETBKCOLOR = (LVM_FIRST + 1);
        public const int LVM_GETIMAGELIST = (LVM_FIRST + 2);
        public const int LVM_SETIMAGELIST = (LVM_FIRST + 3);
        public const int LVM_GETITEMCOUNT = (LVM_FIRST + 4);
        public const int LVM_GETITEMA = (LVM_FIRST + 5);
        public const int LVM_GETITEMW = (LVM_FIRST + 75);
        public const int LVM_SETITEMA = (LVM_FIRST + 6);
        public const int LVM_SETITEMW = (LVM_FIRST + 76);
        public const int LVM_INSERTITEMA = (LVM_FIRST + 7);
        public const int LVM_INSERTITEMW = (LVM_FIRST + 77);
        public const int LVM_DELETEITEM = (LVM_FIRST + 8);
        public const int LVM_DELETEALLITEMS = (LVM_FIRST + 9);
        public const int LVM_GETCALLBACKMASK = (LVM_FIRST + 10);
        public const int LVM_SETCALLBACKMASK = (LVM_FIRST + 11);
        public const int LVM_FINDITEMA = (LVM_FIRST + 13);
        public const int LVM_FINDITEMW = (LVM_FIRST + 83);
        public const int LVM_GETITEMRECT = (LVM_FIRST + 14);
        public const int LVM_SETITEMPOSITION = (LVM_FIRST + 15);
        public const int LVM_GETITEMPOSITION = (LVM_FIRST + 16);
        public const int LVM_GETSTRINGWIDTHA = (LVM_FIRST + 17);
        public const int LVM_GETSTRINGWIDTHW = (LVM_FIRST + 87);
        public const int LVM_HITTEST = (LVM_FIRST + 18);
        public const int LVM_ENSUREVISIBLE = (LVM_FIRST + 19);
        public const int LVM_SCROLL = (LVM_FIRST + 20);
        public const int LVM_REDRAWITEMS = (LVM_FIRST + 21);
        public const int LVM_ARRANGE = (LVM_FIRST + 22);
        public const int LVM_EDITLABELA = (LVM_FIRST + 23);
        public const int LVM_EDITLABELW = (LVM_FIRST + 118);
        public const int LVM_GETEDITCONTROL = (LVM_FIRST + 24);
        public const int LVM_GETCOLUMNA = (LVM_FIRST + 25);
        public const int LVM_GETCOLUMNW = (LVM_FIRST + 95);
        public const int LVM_SETCOLUMNA = (LVM_FIRST + 26);
        public const int LVM_SETCOLUMNW = (LVM_FIRST + 96);
        public const int LVM_INSERTCOLUMNA = (LVM_FIRST + 27);
        public const int LVM_INSERTCOLUMNW = (LVM_FIRST + 97);
        public const int LVM_DELETECOLUMN = (LVM_FIRST + 28);
        public const int LVM_GETCOLUMNWIDTH = (LVM_FIRST + 29);
        public const int LVM_SETCOLUMNWIDTH = (LVM_FIRST + 30);
        public const int LVM_CREATEDRAGIMAGE = (LVM_FIRST + 33);
        public const int LVM_GETVIEWRECT = (LVM_FIRST + 34);
        public const int LVM_GETTEXTCOLOR = (LVM_FIRST + 35);
        public const int LVM_SETTEXTCOLOR = (LVM_FIRST + 36);
        public const int LVM_GETTEXTBKCOLOR = (LVM_FIRST + 37);
        public const int LVM_SETTEXTBKCOLOR = (LVM_FIRST + 38);
        public const int LVM_GETTOPINDEX = (LVM_FIRST + 39);
        public const int LVM_GETCOUNTPERPAGE = (LVM_FIRST + 40);
        public const int LVM_GETORIGIN = (LVM_FIRST + 41);
        public const int LVM_UPDATE = (LVM_FIRST + 42);
        public const int LVM_SETITEMSTATE = (LVM_FIRST + 43);
        public const int LVM_GETITEMSTATE = (LVM_FIRST + 44);
        public const int LVM_GETITEMTEXTA = (LVM_FIRST + 45);
        public const int LVM_GETITEMTEXTW = (LVM_FIRST + 115);
        public const int LVM_SETITEMTEXTA = (LVM_FIRST + 46);
        public const int LVM_SETITEMTEXTW = (LVM_FIRST + 116);
        public const int LVM_SETITEMCOUNT = (LVM_FIRST + 47);
        public const int LVM_SORTITEMS = (LVM_FIRST + 48);
        public const int LVM_SETITEMPOSITION32 = (LVM_FIRST + 49);
        public const int LVM_GETSELECTEDCOUNT = (LVM_FIRST + 50);
        public const int LVM_GETITEMSPACING = (LVM_FIRST + 51);
        public const int LVM_GETISEARCHSTRINGA = (LVM_FIRST + 52);
        public const int LVM_GETISEARCHSTRINGW = (LVM_FIRST + 117);
        public const int LVM_SETICONSPACING = (LVM_FIRST + 53);
        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54);
        public const int LVM_GETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 55);
        public const int LVM_GETSUBITEMRECT = (LVM_FIRST + 56);
        public const int LVM_SUBITEMHITTEST = (LVM_FIRST + 57);
        public const int LVM_SETCOLUMNORDERARRAY = (LVM_FIRST + 58);
        public const int LVM_GETCOLUMNORDERARRAY = (LVM_FIRST + 59);
        public const int LVM_SETHOTITEM = (LVM_FIRST + 60);
        public const int LVM_GETHOTITEM = (LVM_FIRST + 61);
        public const int LVM_SETHOTCURSOR = (LVM_FIRST + 62);
        public const int LVM_GETHOTCURSOR = (LVM_FIRST + 63);
        public const int LVM_APPROXIMATEVIEWRECT = (LVM_FIRST + 64);
        public const int LVM_SETWORKAREAS = (LVM_FIRST + 65);
        public const int LVM_GETWORKAREAS = (LVM_FIRST + 70);
        public const int LVM_GETNUMBEROFWORKAREAS = (LVM_FIRST + 73);
        public const int LVM_GETSELECTIONMARK = (LVM_FIRST + 66);
        public const int LVM_SETSELECTIONMARK = (LVM_FIRST + 67);
        public const int LVM_SETHOVERTIME = (LVM_FIRST + 71);
        public const int LVM_GETHOVERTIME = (LVM_FIRST + 72);
        public const int LVM_SETTOOLTIPS = (LVM_FIRST + 74);
        public const int LVM_GETTOOLTIPS = (LVM_FIRST + 78);
        public const int LVM_SORTITEMSEX = (LVM_FIRST + 81);
        public const int LVM_SETBKIMAGEA = (LVM_FIRST + 68);
        public const int LVM_SETBKIMAGEW = (LVM_FIRST + 138);
        public const int LVM_GETBKIMAGEA = (LVM_FIRST + 69);
        public const int LVM_GETBKIMAGEW = (LVM_FIRST + 139);
        public const int LVM_SETSELECTEDCOLUMN = (LVM_FIRST + 140);
        public const int LVM_SETTILEWIDTH = (LVM_FIRST + 141);
        public const int LVM_SETVIEW = (LVM_FIRST + 142);
        public const int LVM_GETVIEW = (LVM_FIRST + 143);
        public const int LVM_INSERTGROUP = (LVM_FIRST + 145);
        public const int LVM_SETGROUPINFO = (LVM_FIRST + 147);
        public const int LVM_GETGROUPINFO = (LVM_FIRST + 149);
        public const int LVM_REMOVEGROUP = (LVM_FIRST + 150);
        public const int LVM_MOVEGROUP = (LVM_FIRST + 151);
        public const int LVM_MOVEITEMTOGROUP = (LVM_FIRST + 154);
        public const int LVM_SETGROUPMETRICS = (LVM_FIRST + 155);
        public const int LVM_GETGROUPMETRICS = (LVM_FIRST + 156);
        public const int LVM_ENABLEGROUPVIEW = (LVM_FIRST + 157);
        public const int LVM_SORTGROUPS = (LVM_FIRST + 158);
        public const int LVM_INSERTGROUPSORTED = (LVM_FIRST + 159);
        public const int LVM_REMOVEALLGROUPS = (LVM_FIRST + 160);
        public const int LVM_HASGROUP = (LVM_FIRST + 161);
        public const int LVM_SETTILEVIEWINFO = (LVM_FIRST + 162);
        public const int LVM_GETTILEVIEWINFO = (LVM_FIRST + 163);
        public const int LVM_SETTILEINFO = (LVM_FIRST + 164);
        public const int LVM_GETTILEINFO = (LVM_FIRST + 165);
        public const int LVM_SETINSERTMARK = (LVM_FIRST + 166);
        public const int LVM_GETINSERTMARK = (LVM_FIRST + 167);
        public const int LVM_INSERTMARKHITTEST = (LVM_FIRST + 168);
        public const int LVM_GETINSERTMARKRECT = (LVM_FIRST + 169);
        public const int LVM_SETINSERTMARKCOLOR = (LVM_FIRST + 170);
        public const int LVM_GETINSERTMARKCOLOR = (LVM_FIRST + 171);
        public const int LVM_SETINFOTIP = (LVM_FIRST + 173);
        public const int LVM_GETSELECTEDCOLUMN = (LVM_FIRST + 174);
        public const int LVM_ISGROUPVIEWENABLED = (LVM_FIRST + 175);
        public const int LVM_GETOUTLINECOLOR = (LVM_FIRST + 176);
        public const int LVM_SETOUTLINECOLOR = (LVM_FIRST + 177);
        public const int LVM_CANCELEDITLABEL = (LVM_FIRST + 179);
        public const int LVM_MAPINDEXTOID = (LVM_FIRST + 180);
        public const int LVM_MAPIDTOINDEX = (LVM_FIRST + 181);
        public const int TVM_INSERTITEMA = (TV_FIRST + 0);
        public const int TVM_INSERTITEMW = (TV_FIRST + 50);
        public const int TVM_DELETEITEM = (TV_FIRST + 1);
        public const int TVM_EXPAND = (TV_FIRST + 2);
        public const int TVM_GETITEMRECT = (TV_FIRST + 4);
        public const int TVM_GETCOUNT = (TV_FIRST + 5);
        public const int TVM_GETINDENT = (TV_FIRST + 6);
        public const int TVM_SETINDENT = (TV_FIRST + 7);
        public const int TVM_GETIMAGELIST = (TV_FIRST + 8);
        public const int TVM_SETIMAGELIST = (TV_FIRST + 9);
        public const int TVM_GETNEXTITEM = (TV_FIRST + 10);
        public const int TVM_SELECTITEM = (TV_FIRST + 11);
        public const int TVM_GETITEMA = (TV_FIRST + 12);
        public const int TVM_GETITEMW = (TV_FIRST + 62);
        public const int TVM_SETITEMA = (TV_FIRST + 13);
        public const int TVM_SETITEMW = (TV_FIRST + 63);
        public const int TVM_EDITLABELA = (TV_FIRST + 14);
        public const int TVM_EDITLABELW = (TV_FIRST + 65);
        public const int TVM_GETEDITCONTROL = (TV_FIRST + 15);
        public const int TVM_GETVISIBLECOUNT = (TV_FIRST + 16);
        public const int TVM_HITTEST = (TV_FIRST + 17);
        public const int TVM_CREATEDRAGIMAGE = (TV_FIRST + 18);
        public const int TVM_SORTCHILDREN = (TV_FIRST + 19);
        public const int TVM_ENSUREVISIBLE = (TV_FIRST + 20);
        public const int TVM_SORTCHILDRENCB = (TV_FIRST + 21);
        public const int TVM_ENDEDITLABELNOW = (TV_FIRST + 22);
        public const int TVM_GETISEARCHSTRINGA = (TV_FIRST + 23);
        public const int TVM_GETISEARCHSTRINGW = (TV_FIRST + 64);
        public const int TVM_SETTOOLTIPS = (TV_FIRST + 24);
        public const int TVM_GETTOOLTIPS = (TV_FIRST + 25);
        public const int TVM_SETINSERTMARK = (TV_FIRST + 26);
        public const int TVM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int TVM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int TVM_SETITEMHEIGHT = (TV_FIRST + 27);
        public const int TVM_GETITEMHEIGHT = (TV_FIRST + 28);
        public const int TVM_SETBKCOLOR = (TV_FIRST + 29);
        public const int TVM_SETTEXTCOLOR = (TV_FIRST + 30);
        public const int TVM_GETBKCOLOR = (TV_FIRST + 31);
        public const int TVM_GETTEXTCOLOR = (TV_FIRST + 32);
        public const int TVM_SETSCROLLTIME = (TV_FIRST + 33);
        public const int TVM_GETSCROLLTIME = (TV_FIRST + 34);
        public const int TVM_SETINSERTMARKCOLOR = (TV_FIRST + 37);
        public const int TVM_GETINSERTMARKCOLOR = (TV_FIRST + 38);
        public const int TVM_GETITEMSTATE = (TV_FIRST + 39);
        public const int TVM_SETLINECOLOR = (TV_FIRST + 40);
        public const int TVM_GETLINECOLOR = (TV_FIRST + 41);
        public const int TVM_MAPACCIDTOHTREEITEM = (TV_FIRST + 42);
        public const int TVM_MAPHTREEITEMTOACCID = (TV_FIRST + 43);
        public const int CBEM_INSERTITEMA = (WM_USER + 1);
        public const int CBEM_SETIMAGELIST = (WM_USER + 2);
        public const int CBEM_GETIMAGELIST = (WM_USER + 3);
        public const int CBEM_GETITEMA = (WM_USER + 4);
        public const int CBEM_SETITEMA = (WM_USER + 5);
        public const int CBEM_DELETEITEM = CB_DELETESTRING;
        public const int CBEM_GETCOMBOCONTROL = (WM_USER + 6);
        public const int CBEM_GETEDITCONTROL = (WM_USER + 7);
        public const int CBEM_SETEXTENDEDSTYLE = (WM_USER + 14);
        public const int CBEM_GETEXTENDEDSTYLE = (WM_USER + 9);
        public const int CBEM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int CBEM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int CBEM_SETEXSTYLE = (WM_USER + 8);
        public const int CBEM_GETEXSTYLE = (WM_USER + 9);
        public const int CBEM_HASEDITCHANGED = (WM_USER + 10);
        public const int CBEM_INSERTITEMW = (WM_USER + 11);
        public const int CBEM_SETITEMW = (WM_USER + 12);
        public const int CBEM_GETITEMW = (WM_USER + 13);
        public const int TCM_GETIMAGELIST = (TCM_FIRST + 2);
        public const int TCM_SETIMAGELIST = (TCM_FIRST + 3);
        public const int TCM_GETITEMCOUNT = (TCM_FIRST + 4);
        public const int TCM_GETITEMA = (TCM_FIRST + 5);
        public const int TCM_GETITEMW = (TCM_FIRST + 60);
        public const int TCM_SETITEMA = (TCM_FIRST + 6);
        public const int TCM_SETITEMW = (TCM_FIRST + 61);
        public const int TCM_INSERTITEMA = (TCM_FIRST + 7);
        public const int TCM_INSERTITEMW = (TCM_FIRST + 62);
        public const int TCM_DELETEITEM = (TCM_FIRST + 8);
        public const int TCM_DELETEALLITEMS = (TCM_FIRST + 9);
        public const int TCM_GETITEMRECT = (TCM_FIRST + 10);
        public const int TCM_GETCURSEL = (TCM_FIRST + 11);
        public const int TCM_SETCURSEL = (TCM_FIRST + 12);
        public const int TCM_HITTEST = (TCM_FIRST + 13);
        public const int TCM_SETITEMEXTRA = (TCM_FIRST + 14);
        public const int TCM_ADJUSTRECT = (TCM_FIRST + 40);
        public const int TCM_SETITEMSIZE = (TCM_FIRST + 41);
        public const int TCM_REMOVEIMAGE = (TCM_FIRST + 42);
        public const int TCM_SETPADDING = (TCM_FIRST + 43);
        public const int TCM_GETROWCOUNT = (TCM_FIRST + 44);
        public const int TCM_GETTOOLTIPS = (TCM_FIRST + 45);
        public const int TCM_SETTOOLTIPS = (TCM_FIRST + 46);
        public const int TCM_GETCURFOCUS = (TCM_FIRST + 47);
        public const int TCM_SETCURFOCUS = (TCM_FIRST + 48);
        public const int TCM_SETMINTABWIDTH = (TCM_FIRST + 49);
        public const int TCM_DESELECTALL = (TCM_FIRST + 50);
        public const int TCM_HIGHLIGHTITEM = (TCM_FIRST + 51);
        public const int TCM_SETEXTENDEDSTYLE = (TCM_FIRST + 52);
        public const int TCM_GETEXTENDEDSTYLE = (TCM_FIRST + 53);
        public const int TCM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int TCM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int ACM_OPENA = (WM_USER + 100);
        public const int ACM_OPENW = (WM_USER + 103);
        public const int ACM_PLAY = (WM_USER + 101);
        public const int ACM_STOP = (WM_USER + 102);
        public const int MCM_FIRST = 0x1000;
        public const int MCM_GETCURSEL = (MCM_FIRST + 1);
        public const int MCM_SETCURSEL = (MCM_FIRST + 2);
        public const int MCM_GETMAXSELCOUNT = (MCM_FIRST + 3);
        public const int MCM_SETMAXSELCOUNT = (MCM_FIRST + 4);
        public const int MCM_GETSELRANGE = (MCM_FIRST + 5);
        public const int MCM_SETSELRANGE = (MCM_FIRST + 6);
        public const int MCM_GETMONTHRANGE = (MCM_FIRST + 7);
        public const int MCM_SETDAYSTATE = (MCM_FIRST + 8);
        public const int MCM_GETMINREQRECT = (MCM_FIRST + 9);
        public const int MCM_SETCOLOR = (MCM_FIRST + 10);
        public const int MCM_GETCOLOR = (MCM_FIRST + 11);
        public const int MCM_SETTODAY = (MCM_FIRST + 12);
        public const int MCM_GETTODAY = (MCM_FIRST + 13);
        public const int MCM_HITTEST = (MCM_FIRST + 14);
        public const int MCM_SETFIRSTDAYOFWEEK = (MCM_FIRST + 15);
        public const int MCM_GETFIRSTDAYOFWEEK = (MCM_FIRST + 16);
        public const int MCM_GETRANGE = (MCM_FIRST + 17);
        public const int MCM_SETRANGE = (MCM_FIRST + 18);
        public const int MCM_GETMONTHDELTA = (MCM_FIRST + 19);
        public const int MCM_SETMONTHDELTA = (MCM_FIRST + 20);
        public const int MCM_GETMAXTODAYWIDTH = (MCM_FIRST + 21);
        public const int MCM_SETUNICODEFORMAT = CCM_SETUNICODEFORMAT;
        public const int MCM_GETUNICODEFORMAT = CCM_GETUNICODEFORMAT;
        public const int DTM_FIRST = 0x1000;
        public const int DTM_GETSYSTEMTIME = (DTM_FIRST + 1);
        public const int DTM_SETSYSTEMTIME = (DTM_FIRST + 2);
        public const int DTM_GETRANGE = (DTM_FIRST + 3);
        public const int DTM_SETRANGE = (DTM_FIRST + 4);
        public const int DTM_SETFORMATA = (DTM_FIRST + 5);
        public const int DTM_SETFORMATW = (DTM_FIRST + 50);
        public const int DTM_SETMCCOLOR = (DTM_FIRST + 6);
        public const int DTM_GETMCCOLOR = (DTM_FIRST + 7);
        public const int DTM_GETMONTHCAL = (DTM_FIRST + 8);
        public const int DTM_SETMCFONT = (DTM_FIRST + 9);
        public const int DTM_GETMCFONT = (DTM_FIRST + 10);
        public const int PGM_SETCHILD = (PGM_FIRST + 1);
        public const int PGM_RECALCSIZE = (PGM_FIRST + 2);
        public const int PGM_FORWARDMOUSE = (PGM_FIRST + 3);
        public const int PGM_SETBKCOLOR = (PGM_FIRST + 4);
        public const int PGM_GETBKCOLOR = (PGM_FIRST + 5);
        public const int PGM_SETBORDER = (PGM_FIRST + 6);
        public const int PGM_GETBORDER = (PGM_FIRST + 7);
        public const int PGM_SETPOS = (PGM_FIRST + 8);
        public const int PGM_GETPOS = (PGM_FIRST + 9);
        public const int PGM_SETBUTTONSIZE = (PGM_FIRST + 10);
        public const int PGM_GETBUTTONSIZE = (PGM_FIRST + 11);
        public const int PGM_GETBUTTONSTATE = (PGM_FIRST + 12);
        public const int PGM_GETDROPTARGET = CCM_GETDROPTARGET;
        public const int BCM_GETIDEALSIZE = (BCM_FIRST + 0x0001);
        public const int BCM_SETIMAGELIST = (BCM_FIRST + 0x0002);
        public const int BCM_GETIMAGELIST = (BCM_FIRST + 0x0003);
        public const int BCM_SETTEXTMARGIN = (BCM_FIRST + 0x0004);
        public const int BCM_GETTEXTMARGIN = (BCM_FIRST + 0x0005);
        public const int EM_SETCUEBANNER = (ECM_FIRST + 1);
        public const int EM_GETCUEBANNER = (ECM_FIRST + 2);
        public const int EM_SHOWBALLOONTIP = (ECM_FIRST + 3);
        public const int EM_HIDEBALLOONTIP = (ECM_FIRST + 4);
        public const int CB_SETMINVISIBLE = (CBM_FIRST + 1);
        public const int CB_GETMINVISIBLE = (CBM_FIRST + 2);
        public const int LM_HITTEST = (WM_USER + 0x300);
        public const int LM_GETIDEALHEIGHT = (WM_USER + 0x301);
        public const int LM_SETITEM = (WM_USER + 0x302);
        public const int LM_GETITEM = (WM_USER + 0x303);

    }

}