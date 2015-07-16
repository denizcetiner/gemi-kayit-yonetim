using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gemi.Helpers
{
    public class PasswordMethods
    {
        public string Hash(string Value)
        {
            var Hash = System.Security.Cryptography.SHA1.Create();
            var Encoder = new System.Text.ASCIIEncoding();
            var Combined = Encoder.GetBytes(Value ?? "");
            return BitConverter.ToString(Hash.ComputeHash(Combined)).ToLower().Replace("-", "");
        }
    }
}
