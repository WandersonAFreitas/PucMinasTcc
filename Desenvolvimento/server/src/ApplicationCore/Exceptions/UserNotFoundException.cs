using System;

namespace ApplicationCore.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(long id) : base($"No user found with id {id}")
        {
        }

        protected UserNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
