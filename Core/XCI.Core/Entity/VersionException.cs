using System;

namespace XCI.Component
{
    public class VersionException : SystemException
    {
        public EntityBase NewEntity { get; set; }

        public VersionException():base()
        {
            
        }

        public VersionException(string message,EntityBase entity):base(message)
        {
            this.NewEntity = entity;
        }

        public VersionException(string message,Exception innerException)  
            :base(message,innerException)
        {
            
        }

    }
}