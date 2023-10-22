using UnityEditor;
using UnityEngine;

namespace DreamCode.AutoKeystore.Editor
{
    internal static class KeystoreSettings
    {
        public static string Name;
        public static string Password;
        public static string AliasName;
        public static string AliasPassword;
        private const string KeystoreExt = ".keystore";
        private static readonly ICrypter _crypter = new TripleDESCrypter(nameof(KeystoreSettings));

        public static void Load()
        {
            var prefsPassword = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(Password)}");
            var prefsAliasPassword =
                EditorPrefs.GetString(
                    $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(AliasPassword)}");
            var decryptedPassword = _crypter.Decrypt(prefsPassword);
            var decryptedAliasPassword = _crypter.Decrypt(prefsAliasPassword);

            Name = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(Name)}");
            Password = decryptedPassword;
            AliasName = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(AliasName)}");
            AliasPassword = decryptedPassword;
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                return;
            PlayerSettings.Android.keystoreName = Name + KeystoreExt;
            PlayerSettings.Android.keystorePass = decryptedPassword;
            PlayerSettings.Android.keyaliasName = AliasName;
            PlayerSettings.Android.keyaliasPass = decryptedAliasPassword;
        }

        public static void Save(string name, string password, string aliasName, string aliasPassword)
        {
            var encryptedPassword = _crypter.Encrypt(password);
            var encryptedAliasPassword = _crypter.Encrypt(aliasPassword);
            EditorPrefs.SetString($"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(Name)}",
                name);
            EditorPrefs.SetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(Password)}",
                encryptedPassword);
            EditorPrefs.SetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(AliasName)}", aliasName);
            EditorPrefs.SetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(AliasPassword)}",
                encryptedAliasPassword);
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                return;
            PlayerSettings.Android.keystoreName = name + KeystoreExt;
            PlayerSettings.Android.keystorePass = password;
            PlayerSettings.Android.keyaliasName = aliasName;
            PlayerSettings.Android.keyaliasPass = aliasPassword;
        }
    }
}