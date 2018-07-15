using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace iBookmark.Helper.Security
{
    public class RijndaelManagedEncryptorDecryptor : IEncryptorDecryptor
    {
        private readonly string _saltKey;
        private readonly int _keySize;
        private readonly string _passPhrase;

        public RijndaelManagedEncryptorDecryptor(string saltKey, int keySize, string passPhrase)
        {
            _saltKey = saltKey;
            _keySize = keySize;
            _passPhrase = passPhrase;
        }

        public string Encrypt(string stringToEncrypt)
        {
            if (stringToEncrypt == null)
            {
                stringToEncrypt = string.Empty;
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(stringToEncrypt);
            byte[] cipherTextBytes;

            var encryptor = GetEncryptor(_passPhrase, _saltKey);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                }
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public string Decrypt(string stringToDecrypt)
        {
            try
            {
                byte[] plainTextBytes;
                if (string.IsNullOrEmpty(stringToDecrypt))
                    return string.Empty;

                byte[] cipherTextBytes = Convert.FromBase64String(stringToDecrypt);
                int decryptedByteCount;

                var decryptor = GetDecryptor(_passPhrase, _saltKey);

                using (var memoryStream = new MemoryStream(cipherTextBytes))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        plainTextBytes = new byte[cipherTextBytes.Length];
                        decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    }
                }
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (FormatException e)
            {
                return string.Empty;
            }
        }

        private ICryptoTransform GetDecryptor(string thisPassPhrase, string salt = null)
        {
            var symmetricKey = GetSymmetricKey(thisPassPhrase, salt);
            return symmetricKey.CreateDecryptor();
        }

        private ICryptoTransform GetEncryptor(string thisPassPhrase, string salt = null)
        {
            var symmetricKey = GetSymmetricKey(thisPassPhrase, salt);
            return symmetricKey.CreateEncryptor();
        }


        private RijndaelManaged GetSymmetricKey(string thisPassPhrase, string salt = null)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(salt ?? _saltKey);
            var password = new PasswordDeriveBytes(thisPassPhrase, null);
            byte[] keyBytes = password.GetBytes(_keySize / 8);
            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            symmetricKey.Key = keyBytes;
            symmetricKey.IV = initVectorBytes;
            return symmetricKey;
        }
    }
}
