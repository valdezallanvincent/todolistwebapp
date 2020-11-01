using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TodoWebApplication.Service;

namespace TodoWebApplication.Exceptions
{
    public class ConflictException : SystemException
    {
        public ResponseErrorModel ResponseErrorModel { get; set; }
        public ConflictException()
        {
        }

        public ConflictException(string message)
            : base(message)
        {
            this.ResponseErrorModel = new ResponseErrorModel(409, message);
        }
        public ConflictException(string message, SystemException innerException)
            : base(message, innerException)
        {
        }
        protected ConflictException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
