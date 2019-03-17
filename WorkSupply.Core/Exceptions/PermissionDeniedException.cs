using System;

namespace WorkSupply.Core.Exceptions
{
    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException(string message) : base(message)
        {
            
        }
    }
}