using System;
using System.Security.Cryptography;
using System.Text;

namespace TodoWebApplication.Service.Helper
{
    public class Cryptographer : ICryptographer
    {
        public string Encrypt(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);

        }
    }
}
