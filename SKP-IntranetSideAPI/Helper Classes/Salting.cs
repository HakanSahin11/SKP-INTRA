using SKP_IntranetSideAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.Helper_Classes
{
    public class Salting
    {
        public static byte[] GenerateSalt()
        {
            Random rng = new Random();
            var saltBytes = new byte[rng.Next(20, 25)];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(saltBytes);
            return saltBytes;
        }

        public static SaltHash HashSalt(string pass, byte[] saltBytes)
        {
            try
            {
                if (saltBytes == null)
                    saltBytes = GenerateSalt();

                //Converts passwords to bytes
                byte[] passBytes = Encoding.UTF8.GetBytes(pass);

                //Makes new empty byte with lenght of the salt and pass combined
                byte[] passSaltBytes = new byte[passBytes.Length + saltBytes.Length];

                //Adds password bytes to the result byte array
                for (int i = 0; i < passBytes.Length; i++)
                    passSaltBytes[i] = passBytes[i];

                //Adds the salt bytes to the resulting byte array
                for (int i = 0; i < saltBytes.Length; i++)
                    passSaltBytes[i + passBytes.Length] = saltBytes[i];

                //computes password bytes with salt bytes with sha256
                HashAlgorithm hash = new SHA256Managed();
                byte[] Hashing = hash.ComputeHash(passSaltBytes);

                byte[] ResultHashSalt = new byte[Hashing.Length + saltBytes.Length];

                //fills the result byte with hashing(pass) bytes 
                for (int i = 0; i < Hashing.Length; i++)
                    ResultHashSalt[i] = Hashing[i];

                //fills the result byte with salting bytes after pass bytes
                for (int i = 0; i < saltBytes.Length; i++)
                    ResultHashSalt[i + Hashing.Length] = saltBytes[i];

                //returns readable Base64String of the result byte
                return new SaltHash(Convert.ToBase64String(ResultHashSalt), Convert.ToBase64String(saltBytes));
            }
            catch
            {
                throw new Exception("Error Code 5.2 - Error at Helper clases Salitng(hash) Error");
            }
        }
    }
}
