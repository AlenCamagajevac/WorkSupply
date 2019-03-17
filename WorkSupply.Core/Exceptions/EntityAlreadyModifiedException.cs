using System;

namespace WorkSupply.Core.Exceptions
{
    public class EntityAlreadyModifiedException : Exception
    {
        public EntityAlreadyModifiedException(string message) : base(message)
        {
            
        }
    }
}