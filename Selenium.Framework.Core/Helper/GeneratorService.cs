using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Selenium.Framework.Core.Helper
{
    static public class GeneratorService
    {
        #region Hash

        static private string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        static public string GetMD5Hash(int length = 5,string salt="salt")
        {
            Thread.Sleep(10);
            string key = DateTime.Now.ToString("yyyyMMdd-HHmmssfff");
            string hash = CalculateMD5Hash(key + salt);
            return hash.Substring(1,length);
        }


        #endregion
    }
}
