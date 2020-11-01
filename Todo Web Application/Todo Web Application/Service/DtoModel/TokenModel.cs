using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebApplication.Service
{
    public class TokenModel
    {
        public string Token { get; set; }
        public long TokenExpiresAt { get; set; }
    }
}
