using Ext.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ext.Extension.TreePanelEx
{
    public class WindowTreeBase:Window
    {
        public event EventHandler<TreePanelNodeClickEventArgs> NodeClick;
        public event EventHandler<TreePanelNodeClickEventArgs> NodeDbClick;
        public event EventHandler<TreePanelSubmittedNodeEventArgs> SubmittedNode_Click;
        [Meta, Category(""), DefaultValue(""), Description("")]
        public string TreeID
        {
            get
            {
                return tree.ID;
            }
            set
            {
                tree.ID = value;
            }
        }

        private TreePanelBaseExt tree = new TreePanelBaseExt();
        private Button buttonOK    = new Button() { Text = "确定",Width=90,Height=25, TextAlign=ButtonTextAlign.Center , Icon=Icon.Accept, StandOut=true};
        private Button buttonClose= new Button() { Text = "取消", Width = 90, Height = 25, TextAlign = ButtonTextAlign.Center ,Icon=Icon.Cancel, StandOut = true };
        public TreePanelBaseExt Tree
        {
            get
            {
                return tree;
            }

            set
            {
                tree = value;
            }
        }
        public WindowTreeBase()
        {
            this.MinHeight = 230;
            this.MinWidth = 202;
            this.Layout = "fit";
            //this.ID = "window";

            //tree.ID = this.ID+"treeMian";
            tree.Title = string.Empty;
            tree.Height = 200;
            tree.DirectEvents.ItemDblClick.Event += ItemDbClick_Event;

            tree.DirectEvents.ItemClick.Event += ItemClick_Event;
            tree.DirectEvents.SelectionChange.Event += SelectionChange_Event;


            buttonOK.DirectEvents.Click.Event += ButtonOK_Click;
            buttonClose.DirectEvents.Click.Event += ButtonClose_Click;


            this.Items.Add(tree);
            this.Buttons.Add(buttonOK);
            this.Buttons.Add(buttonClose);
            
        }

        private void SelectionChange_Event(object sender, DirectEventArgs e)
        {
            ;
        }

        private void ItemClick_Event(object sender, DirectEventArgs e)
        {
            if (NodeClick != null)
            {
                var node = tree.SelectedNodes.FirstOrDefault();
                NodeClick.Invoke(sender,
                    new TreePanelNodeClickEventArgs(e.ExtraParams,
                                                                                new NodeEx()
                                                                                {
                                                                                    NodeID = node.NodeID,
                                                                                    Text = node.Text,
                                                                                    Checked = node.Checked
                                                                                }
                                                                            )
                        );
            }
        }

        void ItemDbClick_Event(object sender, DirectEventArgs e)
        {
            if (NodeDbClick!=null)
            {
                var node=tree.SelectedNodes.FirstOrDefault();
                NodeDbClick.Invoke(sender, 
                    new TreePanelNodeClickEventArgs(e.ExtraParams, 
                                                                                new NodeEx() 
                                                                                { 
                                                                                    NodeID = node.NodeID, 
                                                                                    Text = node.Text,
                                                                                    Checked=node.Checked
                                                                                }
                                                                            )
                        );
            }
        }

        private void ButtonClose_Click(object sender, DirectEventArgs e)
        {
            this.Close();
            this.Hidden = true;
        }

        private void ButtonOK_Click(object sender, DirectEventArgs e)
        {
            if(SubmittedNode_Click!=null)
            {
                TreePanelSubmittedNodeEventArgs e1 = 
                    new TreePanelSubmittedNodeEventArgs(e.ExtraParams) {
                        CheckedNodes = tree.CheckedNodes != null ? tree.CheckedNodes : new List<SubmittedNode>(),
                     SelectNodes= tree.SelectedNodes != null ? tree.SelectedNodes : new List<SubmittedNode>(),
                    };
                SubmittedNode_Click.Invoke(sender, e1);
            }
        }

        public void SetWindowTitle(string title)
        {
            this.Title = title;
        }
    }
}
