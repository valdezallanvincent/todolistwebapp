using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebApplication.Service.Helper
{
    public interface ICryptographer
    {
        string Encrypt(string password);
    }
}
