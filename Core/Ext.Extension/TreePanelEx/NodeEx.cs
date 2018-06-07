using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ext.Extension.TreePanelEx
{
    public class NodeEx:Ext.Net.Node
    {
        /// <summary>
        /// 父节点ID
        /// </summary>
        public string ParentNodeID
        { get; set; }

        public string SEQUENCE { get; set; }
    }
}
