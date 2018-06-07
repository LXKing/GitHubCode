using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Ext.Extension.TreePanelEx
{
    public class TreePanelBaseExt:TreePanel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public TreePanelBaseExt()
        {
            this.MinWidth = 200;
            this.MinHeight = 300;
            this.Title = "Tree";
            this.AutoScroll = true;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="datasource"></param>
        public void SetdataSource(IEnumerable<NodeEx> datasource)
        {
            if (this.Root.Count > 0)
                this.Root.Clear();

            var allNodes = datasource.ToList();
            allNodes.ForEach(x =>
            {
                if (x.Href == null)
                    x.Href = "#";
            });
            var rootnodes = allNodes.Where(x => string.IsNullOrEmpty(x.ParentNodeID)).ToList();
            var rootNode = new Node() { Text="根节点" };
            if (rootnodes.Count > 0)
                rootNode.Expanded = true;
            else
                this.RootVisible = false;
            rootNode.Children.AddRange(rootnodes);

            rootnodes.ToList().ForEach(x=> {
                AppendNode(x, datasource.ToList());
            });
            this.SetRootNode(rootNode);
            //this.Render(true);
        }
        public void SetdataSource<T>(Func<IEnumerable<T>,IEnumerable<NodeEx>> createNodeAction,IEnumerable<T> data)
        {
            var datasource = createNodeAction.Invoke(data).ToList();
            if (this.Root.Count > 0)
                this.Root.Clear();

            var allNodes = datasource.ToList();
            allNodes.ForEach(x =>
            {
                if (x.Href == null)
                    x.Href = "#";
            });
            var rootnodes = allNodes.Where(x => string.IsNullOrEmpty(x.ParentNodeID)).ToList();
            var rootNode = new Node() { Text = "根节点" };
            if (rootnodes.Count > 0)
                rootNode.Expanded = true;
            else
                this.RootVisible = false;
            rootNode.Children.AddRange(rootnodes);

            rootnodes.ToList().ForEach(x => {
                AppendNode(x, datasource.ToList());
            });
            this.SetRootNode(rootNode);
            
            //this.Render(true);
        }

        private void AppendNode(NodeEx parentNode,IEnumerable<NodeEx> datasource)
        {
            var ds = datasource.Where(x => x.ParentNodeID == parentNode.NodeID).ToList();
            if (ds.Count > 0)
                parentNode.Expanded = true;
            else
                parentNode.Leaf = true;
            ds.ForEach(x=> {
                parentNode.Children.Add(x);
                AppendNode(x, datasource);
            });
        }

        public void ExpandAllNode()
        {
            this.ExpandAll();
            //throw new NotImplementedException();
        }
    }
    public class TreePanelSubmittedNodeEventArgs : DirectEventArgs
    {
        public TreePanelSubmittedNodeEventArgs(Ext.Net.ParameterCollection extraParams)
            : base(extraParams)
        {

        }
        public ICollection<SubmittedNode> CheckedNodes;

        public ICollection<SubmittedNode> SelectNodes;
    }

    public class TreePanelNodeClickEventArgs : DirectEventArgs
    {
        public TreePanelNodeClickEventArgs(Ext.Net.ParameterCollection extraParams,NodeEx nodeClick)
            : base(extraParams)
        {
            NodeDbClick = nodeClick;
            //NodeID = extraParams["id"].ToString();
            //NodeText = extraParams["text"].ToString();
        }
        /// <summary>
        /// 双击的节点
        /// </summary>
        public NodeEx NodeDbClick
        {
            get;
            set;
        }
    }
}
