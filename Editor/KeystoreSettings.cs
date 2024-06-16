using System;
using DreamCode.AutoKeystore.Editor.Configuration;
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
        private static readonly KeystoreRepositoryFactory _factory = new();
        private static IKeystoreRepository _keystoreRepository;

        public static void SetupRepository(KeystoreRepository repository)
        {
            _keystoreRepository = _factory.Create(repository);
        }

        public static void Load()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                return;
            if (_keystoreRepository == null)
                throw new InvalidOperationException(
                    $"{nameof(KeystoreSettings)}-{nameof(_keystoreRepository)} is not configured");
            var projectKeystore = _keystoreRepository.LoadProjectKeystore(_crypter);
            if (string.IsNullOrEmpty(projectKeystore.name) || string.IsNullOrEmpty(projectKeystore.password))
                return;
            projectKeystore.name += KeystoreExt;

            var projectKey = _keystoreRepository.LoadProjectKey(_crypter);
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
            if (_keystoreRepository == null)
                throw new InvalidOperationException(
                    $"{nameof(KeystoreSettings)}-{nameof(_keystoreRepository)} is not configured");
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(aliasPassword))
                return;
            var encryptedPassword = _crypter.Encrypt(password);
            var encryptedAliasPassword = _crypter.Encrypt(aliasPassword);

            _keystoreRepository.Save(
                name,
                aliasName,
                encryptedPassword,
                encryptedAliasPassword
            );

            PlayerSettings.Android.keystoreName = name + KeystoreExt;
            PlayerSettings.Android.keystorePass = password;
            PlayerSettings.Android.keyaliasName = aliasName;
            PlayerSettings.Android.keyaliasPass = aliasPassword;
        }
    }
}