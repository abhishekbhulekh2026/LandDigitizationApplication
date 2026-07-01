using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserModel;
namespace Utility
{
    public class EncryptDecryptHelper
    {
        private readonly string  _key;
        private readonly string  _iv;

        public EncryptDecryptHelper(IOptions<EncryptionSettings> settings)
        {
            _key = settings.Value.Key;
            _iv = settings.Value.IV;
        }

        // Encrypt a string to a byte array using a key and an IV
        public string EncryptStringToBytes_Aes(string plainText)
        {
            string Key = ""; string IV = "";
            Key = _key;
            IV = _iv;

            string encryptedStr = "";
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;


            byte[] bytes_Key = Encoding.UTF8.GetBytes(Key);
            byte[] bytes_IV = Encoding.UTF8.GetBytes(IV);
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = bytes_Key;
                aesAlg.IV = bytes_IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                        //encryptedStr = System.Text.Encoding.ASCII.GetString(encrypted);
                        encryptedStr = Convert.ToBase64String(encrypted);
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encryptedStr;
        }

        // Decrypt a byte array to a string using a key and an IV
        public string DecryptStringFromBytes_Aes(string cipherText)
        {
            string Key = ""; string IV = "";
            Key = _key;
            IV = _iv;

            //cipherText = "Zz5dp96xIBO2vLhGz/1QsBYp5YwftREkVCavRpPwNj6NOEmMugQQBdqsBNWUtGVxfyErnwYM/m0IfW8OdNzGHpsh+UFcb0KUMKd3x+ctg8ATuUTEu5bMzleLCVxSRiaO+mnNa34eic7ya5JqJJak7F8y60u5Ul718Pg+FyH291uhgJ1brgz/250jarhRjA2orLsD8i8Ul6INtU1vkNsyl+r3VDSkLaklonBsuRQz8S5koH4UCtFNd9k1velKPOq0gm2KlcE+UHF4hMgQZp4OTg==";
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            //byte[] bytes_cipherText = Encoding.UTF8.GetBytes(cipherText);
            byte[] bytes_cipherText = Convert.FromBase64String(cipherText);
            byte[] bytes_Key = Encoding.UTF8.GetBytes(Key);
            byte[] bytes_IV = Encoding.UTF8.GetBytes(IV);
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = bytes_Key;
                aesAlg.IV = bytes_IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(bytes_cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public void GenerateKeys()
        {
            using var aes = Aes.Create();
            aes.KeySize = 256; // AES-256
            aes.GenerateKey();
            aes.GenerateIV();

            string key = Convert.ToBase64String(aes.Key); // 32-byte key
            string iv = Convert.ToBase64String(aes.IV);   // 16-byte IV
        }
    }
}
