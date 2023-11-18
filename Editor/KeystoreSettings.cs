using UnityEditor;

namespace DreamCode.AutoKeystore.Editor
{
    internal static class KeystoreSettings
    {
        internal static string Name { get; private set; }
        internal static string Password { get; private set; }
        internal static string AliasName { get; private set; }
        internal static string AliasPassword { get; private set; }
        private const string KeystoreExt = ".keystore";
        private static readonly ICrypter _crypter = new TripleDESCrypter(nameof(KeystoreSettings));

        public static void Load()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                return;
            var projectKeystore = LoadProjectKeystore();
            if (string.IsNullOrEmpty(projectKeystore.name) || string.IsNullOrEmpty(projectKeystore.password))
                return;
            var projectKey = LoadProjectKey();
            if (string.IsNullOrEmpty(projectKey.name) || string.IsNullOrEmpty(projectKey.password))
                return;
            PlayerSettings.Android.keystoreName = Name = projectKeystore.name;
            PlayerSettings.Android.keystorePass = Password = projectKeystore.password;
            PlayerSettings.Android.keyaliasName = AliasName = projectKey.name;
            PlayerSettings.Android.keyaliasPass = AliasPassword = projectKey.password;
        }

        public static void Save(string name, string password, string aliasName, string aliasPassword)
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                return;
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(aliasPassword))
                return;
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
            PlayerSettings.Android.keystoreName = name + KeystoreExt;
            PlayerSettings.Android.keystorePass = password;
            PlayerSettings.Android.keyaliasName = aliasName;
            PlayerSettings.Android.keyaliasPass = aliasPassword;
        }

        private static (string name, string password) LoadProjectKeystore()
        {
            var name = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(Name)}");
            name += KeystoreExt;
            var prefsPassword = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(Password)}");
            var decryptedPassword = _crypter.Decrypt(prefsPassword);

            return (name, decryptedPassword);
        }

        private static (string name, string password) LoadProjectKey()
        {
            var name = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(AliasName)}");
            var prefsAliasPassword =
                EditorPrefs.GetString(
                    $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(AliasPassword)}");
            var decryptedAliasPassword = _crypter.Decrypt(prefsAliasPassword);

            return (name, decryptedAliasPassword);
        }
    }
}