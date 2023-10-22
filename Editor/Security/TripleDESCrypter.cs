using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace DreamCode.AutoKeystore.Editor
{
    public sealed class TripleDESCrypter : ICrypter
    {
        private readonly string _secretKey;

        public TripleDESCrypter(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string Encrypt(string value)
        {
            var inputBytes = Encoding.UTF8.GetBytes(value);
            var md5ServiceProvider = new MD5CryptoServiceProvider();
            var tripleServiceProvider = new TripleDESCryptoServiceProvider();

            tripleServiceProvider.Key = md5ServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(_secretKey));
            tripleServiceProvider.Mode = CipherMode.ECB;
            var cryptoTransform = tripleServiceProvider.CreateEncryptor();

            var finalBlock = cryptoTransform.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            return Convert.ToBase64String(finalBlock);
        }

        public string Decrypt(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return string.Empty;
            var result = base64String;
            try
            {
                var inputBytes = Convert.FromBase64String(base64String);
                var md5ServiceProvider = new MD5CryptoServiceProvider();
                var tripleServiceProvider = new TripleDESCryptoServiceProvider();

                tripleServiceProvider.Key = md5ServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(_secretKey));
                tripleServiceProvider.Mode = CipherMode.ECB;
                var cryptoTransform = tripleServiceProvider.CreateDecryptor();

                var finalBlock = cryptoTransform.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                result = Encoding.UTF8.GetString(finalBlock);
            }
            catch (CryptographicException cryptoEx)
            {
                Debug.LogWarning($"{nameof(TripleDESCrypter)}-{cryptoEx.Message}");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"{nameof(TripleDESCrypter)}-{e.Message}");
            }

            return result;
        }
    }
}