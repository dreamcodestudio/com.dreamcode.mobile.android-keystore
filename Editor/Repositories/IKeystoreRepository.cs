namespace DreamCode.AutoKeystore.Editor
{
    public interface IKeystoreRepository
    {
        (string name, string password) LoadProjectKeystore(ICrypter crypter);
        (string name, string password) LoadProjectKey(ICrypter crypter);
        void Save(string name, string aliasName, string encryptedPassword, string encryptedAliasPassword);
    }
}