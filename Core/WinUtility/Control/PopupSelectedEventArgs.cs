using System;

namespace XCI.WinUtility
{
    public class PopupSelectedEventArgs : EventArgs
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public object SelectedObject { get; set; }
    }
}