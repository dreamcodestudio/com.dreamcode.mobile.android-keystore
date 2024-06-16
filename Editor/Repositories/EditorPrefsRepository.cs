using UnityEditor;

namespace DreamCode.AutoKeystore.Editor
{
    public sealed class EditorPrefsRepository : IKeystoreRepository
    {
        public (string name, string password) LoadProjectKeystore(ICrypter crypter)
        {
            var name = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.Name)}");
            var prefsPassword = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.Password)}");
            var decryptedPassword = crypter.Decrypt(prefsPassword);

            return (name, decryptedPassword);
        }

        public (string name, string password) LoadProjectKey(ICrypter crypter)
        {
            var name = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.AliasName)}");
            var prefsAliasPassword =
                EditorPrefs.GetString(
                    $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.AliasPassword)}");
            var decryptedAliasPassword = crypter.Decrypt(prefsAliasPassword);

            return (name, decryptedAliasPassword);
        }

        public void Save(string name, string aliasName, string encryptedPassword, string encryptedAliasPassword)
        {
            EditorPrefs.SetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.Name)}",
                name);
            EditorPrefs.SetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.Password)}",
                encryptedPassword);
            EditorPrefs.SetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.AliasName)}",
                aliasName);
            EditorPrefs.SetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.AliasPassword)}",
                encryptedAliasPassword);
        }
    }
}