using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gemi.Helpers
{
    public class PasswordMethods
    {
        /// <summary>
        /// Sha1 hasher
        /// </summary>
        /// <param name="Value"></param>
        /// <returns>Girilen string'in sha1 ile hash edilmiş değerini döndürür.</returns>
        public string Hash(string Value)
        {
            var Hash = System.Security.Cryptography.SHA1.Create();
            var Encoder = new System.Text.ASCIIEncoding();
            var Combined = Encoder.GetBytes(Value ?? "");
            return BitConverter.ToString(Hash.ComputeHash(Combined)).ToLower().Replace("-", "");
        }
    }
}
