using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace OpticalFiber
{
    public class MD5
    {
        public static string GetMD5Hash(string str)
        {
            MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();
            StringBuilder stringBuilder = new StringBuilder();
            byte[] data = mD5.ComputeHash(Encoding.UTF8.GetBytes(str));
            for(int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
