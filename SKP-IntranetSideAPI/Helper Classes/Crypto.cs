using Newtonsoft.Json;
using SKP_IntranetSideAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.Helper_Classes
{
    public class Crypto : IDisposable
    {
        readonly AesManaged _algorithm;
        readonly byte[] _salt;
        private readonly string CryptKey = "992142484233823";

        public Crypto()
        {
            {
                _salt = Convert.FromBase64String("4556484548529632");
                _algorithm = new AesManaged
                {
                    Padding = PaddingMode.Zeros
                };
            }
        }
        public string Encrypter(string json, string salt)
        {
            if (salt == null)
                salt = CryptKey;
            return Convert.ToBase64String(Task.Run(() => Encrypt(Encoding.UTF8.GetBytes(json), salt)).Result);
        }

        public byte[] Decrypter(string json, string salt)
        {
            if (salt == null)
                salt = CryptKey;
            var js = JsonConvert.DeserializeObject<APIReqModel>(json);
            return Task.Run(() => Decrypt(Convert.FromBase64String(js.Json), salt)).Result;
        }
        //Encryption Task, used to encrypt data, using existing salt code
        public async Task<byte[]> Encrypt(byte[] bytesToEncrypt, string pass)
        {
            var taskResult = Task.Run(() =>
            {
                var passwordHash = GeneratePasswordHash(pass);
                var key = GenerateKey(passwordHash);
                var IV = GenerateIV(passwordHash);
                ICryptoTransform Encryption = _algorithm.CreateEncryptor(key, IV);
                return TransformBytes(Encryption, bytesToEncrypt);
            });
            await taskResult;
            return taskResult.Result;
        }

        //Decryption Task, used to decrypt data, using existing salt code
        public async Task<byte[]> Decrypt(byte[] bytesToDecrypt, string pass)
        {
            var task = Task.Run(() =>
            {
                var passwordHash = GeneratePasswordHash(pass);
                var key = GenerateKey(passwordHash);
                var IV = GenerateIV(passwordHash);
                var decrypt = _algorithm.CreateDecryptor(key, IV);
                return TransformBytes(decrypt, bytesToDecrypt);
            });
            await task;
            return task.Result;
        }

        //used to create password hashed key
        private Rfc2898DeriveBytes GeneratePasswordHash(string pass) =>
            new Rfc2898DeriveBytes(pass, _salt);

        //converts password key to bytes
        private byte[] GenerateKey(Rfc2898DeriveBytes passwordHash) =>
            passwordHash.GetBytes(_algorithm.KeySize / 8);

        //Generates initialization vector (IV)
        private byte[] GenerateIV(Rfc2898DeriveBytes passwordHash) =>
            passwordHash.GetBytes(_algorithm.BlockSize / 8);

        // Writes the decrypted / encrypted version to an array, then returns result
        private byte[] TransformBytes(ICryptoTransform transformer, byte[] bytesToTransform)
        {
            byte[] contentResult;
            using (var bufferstream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(bufferstream, transformer, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytesToTransform, 0, bytesToTransform.Length);
                    cryptoStream.FlushFinalBlock();
                    contentResult = bufferstream.ToArray();
                    cryptoStream.Close();
                }
                bufferstream.Close();
            }
            return contentResult;
        }
        protected virtual void Dispose(bool isDisposin)
        {
            if (isDisposin)
                _algorithm.Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
