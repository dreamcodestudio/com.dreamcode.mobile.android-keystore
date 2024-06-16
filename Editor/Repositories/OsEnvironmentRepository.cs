using System;
using UnityEditor;

namespace DreamCode.AutoKeystore.Editor
{
    public sealed class OsEnvironmentRepository : IKeystoreRepository
    {
        public (string name, string password) LoadProjectKeystore(ICrypter crypter)
        {
            var name = Environment.GetEnvironmentVariable(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.Name)}");
            var prefsPassword = Environment.GetEnvironmentVariable(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.Password)}");
            var decryptedPassword = crypter.Decrypt(prefsPassword);

            return (name, decryptedPassword);
        }

        public (string name, string password) LoadProjectKey(ICrypter crypter)
        {
            var name = Environment.GetEnvironmentVariable(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.AliasName)}");
            var prefsAliasPassword = Environment.GetEnvironmentVariable(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.AliasPassword)}");
            var decryptedAliasPassword = crypter.Decrypt(prefsAliasPassword);

            return (name, decryptedAliasPassword);
        }

        public void Save(string name, string aliasName, string encryptedPassword, string encryptedAliasPassword)
        {
            Environment.SetEnvironmentVariable(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.Name)}",
                name
            );
            Environment.SetEnvironmentVariable(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.AliasName)}",
                aliasName
            );
            Environment.SetEnvironmentVariable(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.Password)}",
                encryptedPassword
            );
            Environment.SetEnvironmentVariable(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(KeystoreSettings.AliasPassword)}",
                encryptedAliasPassword
            );
        }
    }
}