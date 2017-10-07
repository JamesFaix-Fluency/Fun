using System;

namespace TestApp.DomainLayer
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base (message)
        {

        }
    }
}
