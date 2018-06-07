namespace XCI.WinUtility
{
    using XCI.Extension;
    public class XCITimeEdit : XCITextEdit
    {
        public XCITimeEdit()
        {
            this.Properties.Mask.EditMask = "([01]?[0-9]|2[0-3]):[0-5]\\d";
            this.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.Properties.Mask.AutoComplete = DevExpress.XtraEditors.Mask.AutoCompleteType.Optimistic;
            this.Properties.Mask.ShowPlaceHolders = false;
        }

        //protected override void OnEditValueChanged()
        //{
        //    base.OnEditValueChanged();
        //    if (!string.IsNullOrEmpty(Text))
        //    {
        //        int length = Text.IndexOf(':');
        //        if (length <= 0)
        //        {
        //            return;
        //        }
        //        string hour = Text.Substring(0, length);
        //        if (hour.ToInt() < 10)
        //        {
        //            this.EditViewInfo.OwnerEdit.Text = "0" + Text;
        //        }
        //    }
        //}
    }
}