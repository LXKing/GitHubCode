using System;
using XCI.Core;

namespace XCI.Component
{
    [Serializable]
    public class ComponentEntity
    {
        public string InterfaceName { get; set; }

        public XCIList<ConfigEntity> ProviderList { get; set; }

        public override bool Equals(object obj)
        {
            return this.InterfaceName.Equals(((ComponentEntity)obj).InterfaceName);
        }

        public override int GetHashCode()
        {
            return this.InterfaceName.GetHashCode();
        }
    }
}