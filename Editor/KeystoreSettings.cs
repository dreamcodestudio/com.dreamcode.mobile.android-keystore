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

        public static void Load()
        {
            Name = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(Name)}");
            Password = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(Password)}");
            AliasName = EditorPrefs.GetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(AliasName)}");
            AliasPassword =
                EditorPrefs.GetString(
                    $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(AliasPassword)}");
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                return;
            PlayerSettings.Android.keystoreName = Name + KeystoreExt;
            PlayerSettings.Android.keystorePass = Password;
            PlayerSettings.Android.keyaliasName = AliasName;
            PlayerSettings.Android.keyaliasPass = AliasPassword;
        }

        public static void Save(string name, string password, string aliasName, string aliasPassword)
        {
            EditorPrefs.SetString($"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(Name)}",
                name);
            EditorPrefs.SetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(Password)}", password);
            EditorPrefs.SetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(AliasName)}", aliasName);
            EditorPrefs.SetString(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreSettings)}-{nameof(AliasPassword)}",
                aliasPassword);
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                return;
            PlayerSettings.Android.keystoreName = name + KeystoreExt;
            PlayerSettings.Android.keystorePass = password;
            PlayerSettings.Android.keyaliasName = aliasName;
            PlayerSettings.Android.keyaliasPass = aliasPassword;
        }
    }
}