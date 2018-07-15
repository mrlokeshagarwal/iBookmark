namespace iBookmark.Helper.Security
{
    public interface IEncryptorDecryptor
    {
        string Encrypt(string stringToEncrypt);
        string Decrypt(string stringToDecrypt);

    }
}
