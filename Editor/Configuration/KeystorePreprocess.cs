using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace DreamCode.AutoKeystore.Editor.Configuration
{
    internal class KeystorePreprocess : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            var keystoreRepository = (KeystoreRepository)EditorPrefs.GetInt(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreRepository)}");
            KeystoreSettings.SetupRepository(keystoreRepository);
            KeystoreSettings.Load();
        }
    }
}