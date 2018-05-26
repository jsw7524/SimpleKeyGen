using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleKeyGen
{
    class KeyGen
    {
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        int Random(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        string GetSalt()
        {
            int r = Random(10000, 30000);
            return r.ToString();
        }
        public string GenerateKey(string name)
        {
            return GenerateKey(name, GetSalt());
        }
        public string GenerateKey(string name, string salt)
        {
            string encryptSource = "jsw" + name + "7524" + salt + "SKG";
            MD5 md5Hash = MD5.Create();
            string hash = GetMd5Hash(md5Hash, encryptSource);
            return hash.Substring(0, 4) + salt + hash.Substring(4);

        }

        public bool VerifyKey(string key, string name)
        {
            string salt = key.Substring(4, 5);
            string recovered = GenerateKey(name, salt);
            if (key == recovered)
            {
                return true;
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var user = "jsw7524";
            var keyGen = new KeyGen();
            var aKey = keyGen.GenerateKey(user);
            Console.WriteLine(aKey);
            Console.WriteLine(keyGen.VerifyKey(aKey, "jsw7523"));
            Console.ReadLine();
        }
    }
}
