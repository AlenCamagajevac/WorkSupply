using System;

namespace WorkSupply.Core.Exceptions
{
    public class UserAlreadyInRoleException : Exception
    {
        public UserAlreadyInRoleException(string message) : base(message)
        {
            
        }
    }
}