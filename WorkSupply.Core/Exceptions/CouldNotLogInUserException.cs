using System;

namespace WorkSupply.Core.Exceptions
{
    public class CouldNotLogInUserException : Exception
    {
        public CouldNotLogInUserException(string message) : base(message)
        {
            
        }
    }
}