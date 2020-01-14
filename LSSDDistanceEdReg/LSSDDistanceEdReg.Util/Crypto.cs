using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LSSD.DistanceEdReg.Util
{
    public static class Crypto
    {
        public static string SHA256(string input)
        {
            if (input == null)
            {
                return SHA256(string.Empty);
            }

            using (SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedValue = sha256.ComputeHash(Encoding.Default.GetBytes(input));

                StringBuilder returnMe = new StringBuilder();
                for (int i = 0; i < hashedValue.Length; i++)
                {
                    returnMe.Append(hashedValue[i].ToString("x2"));
                }
                return returnMe.ToString();
            }
        }
    }
}
