using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Packages.DreamCode.AutoKeystore.Editor
{
    public class KeystorePreprocess : IPreprocessBuildWithReport
    {
        private const string _keystoreExt = ".keystore";

        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            Autocomplete();
        }

        private static void Autocomplete()
        {
            var keystoreName = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeystoreName");
            var keystorePass = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeystorePass");
            var keyaliasName = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeyaliasName");
            var keyaliasPass = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeyaliasPass");

            //Project Keystore
            PlayerSettings.Android.keystoreName = keystoreName + _keystoreExt;
            PlayerSettings.Android.keystorePass = keystorePass;
            //Project Key
            PlayerSettings.Android.keyaliasName = keyaliasName;
            PlayerSettings.Android.keyaliasPass = keyaliasPass;
        }
    }
}