using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace University.Models.Utilities
{
    public class Encripcion
    {
        private const string strPermutation = "ouiveyxaqtd";
        private const int bytePermutation1 = 0x19;
        private const int bytePermutation2 = 0x59;
        private const int bytePermutation3 = 0x17;
        private const int bytePermutation4 = 0x41;

        public static string Encriptar(string data)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(data)));
        }

        public static string Desencriptar(string data)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(data)));
        }

        private static byte[] Encrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(strPermutation,
            new byte[] { bytePermutation1,
                         bytePermutation2,
                         bytePermutation3,
                         bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        private static byte[] Decrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(strPermutation,
            new byte[] { bytePermutation1,
                         bytePermutation2,
                         bytePermutation3,
                         bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
    }
}