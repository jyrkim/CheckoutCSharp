using System;

namespace Checkout.Exceptions
{
    class RequiredParameterMissingException : Exception
    {
        public RequiredParameterMissingException()
        {
        }

        public RequiredParameterMissingException(string message)
        : base(message)
        {

        }

        public RequiredParameterMissingException(string message, Exception inner)
        : base(message, inner)
        {

        }
    }
}