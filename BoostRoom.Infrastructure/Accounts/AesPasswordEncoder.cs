using System;
using System.Buffers.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BoostRoom.Accounts.Domain;

namespace BoostRoom.Infrastructure.Accounts
{
    public class AesPasswordEncoder : IPasswordEncoder
    {
        private static string StrPermutation
        {
            get
            {
                var bytes = new byte[8];
                return bytes.ToString();
            }
        }

        public string Encode(string password)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(password)));
        }

        public string Decode(string password)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(password)));
        }

        private static byte[] Encrypt(byte[] strData)
        {
            var bytes =
                new Rfc2898DeriveBytes(StrPermutation, new byte[8]);

            var memoryStream = new MemoryStream();

            Aes aes = new AesManaged();
            aes.Key = bytes.GetBytes(aes.KeySize / 8);
            aes.IV = bytes.GetBytes(aes.BlockSize / 8);

            var cryptoStream = new CryptoStream(memoryStream,
                aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(strData, 0, strData.Length);
            cryptoStream.Close();

            return memoryStream.ToArray();
        }

        private static byte[] Decrypt(byte[] strData)
        {
            var bytes =
                new Rfc2898DeriveBytes(StrPermutation, new byte[8]);

            var memoryStream = new MemoryStream();

            Aes aes = new AesManaged();
            aes.Key = bytes.GetBytes(aes.KeySize / 8);
            aes.IV = bytes.GetBytes(aes.BlockSize / 8);

            var cryptoStream = new CryptoStream(memoryStream,
                aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(strData, 0, strData.Length);
            cryptoStream.Close();

            return memoryStream.ToArray();
        }
    }
}