using System;
using System.Runtime.Serialization;

namespace FreeCourse.Web.Exeptions
{
    public class UnAuthorizeExceptions : Exception
    {
        public UnAuthorizeExceptions()
        {
        }

        public UnAuthorizeExceptions(string message) : base(message)
        {
        }

        public UnAuthorizeExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnAuthorizeExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
