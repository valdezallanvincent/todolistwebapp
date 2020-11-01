using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebApplication.Service
{
    public class ResponseErrorModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public ResponseErrorModel(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
