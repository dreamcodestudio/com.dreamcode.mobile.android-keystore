namespace DreamCode.AutoKeystore.Editor
{
    public interface ICrypter
    {
        string Encrypt(string value);
        string Decrypt(string base64String);
    }
}