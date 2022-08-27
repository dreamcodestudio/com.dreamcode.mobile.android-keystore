using UnityEditor;
using UnityEngine;

namespace Packages.DreamCode.AutoKeystore.Editor.Windows
{
    public class KeystoreEditorWindow : EditorWindow
    {

        #region PRIVATE_VARIABLES
        
        private string _keystoreName;
        private string _keystorePass;
        private string _keyaliasName;
        private string _keyaliasPass;
        private const string _keystoreExt = ".keystore";

        #endregion

        #region UNITY_EVENTS

        private void Awake()
        {
            LoadSettings();
        }

        private void OnGUI()
        {

            GUILayout.Space(10f);
            GUILayout.Label("Project Keystore");
            GUILayout.Space(8f);
            GUILayout.Label("Path");
            GUILayout.BeginHorizontal();
            _keystoreName = GUILayout.TextField(_keystoreName);
            GUILayout.Label(_keystoreExt);
            GUILayout.EndHorizontal();
            GUILayout.Label("Password");
            GUILayout.BeginHorizontal();
            _keystorePass = GUILayout.TextField(_keystorePass);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
            
            GUILayout.Label("Project Key");
            GUILayout.Space(8f);
            GUILayout.Label("Alias");
            GUILayout.BeginHorizontal();
            _keyaliasName = GUILayout.TextField(_keyaliasName);
            GUILayout.EndHorizontal();
            GUILayout.Label("Password");
            GUILayout.BeginHorizontal();
            _keyaliasPass = GUILayout.TextField(_keyaliasPass);
            GUILayout.EndHorizontal();

            GUILayout.Space(10f);
            if (GUILayout.Button("Save"))
            {
                SaveSettings();
            }
        }

        #endregion

        private void LoadSettings()
        {
            _keystoreName = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeystoreName");
            _keystorePass = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeystorePass");
            _keyaliasName = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeyaliasName");
            _keyaliasPass = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeyaliasPass");
        }

        private void SaveSettings()
        {
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeystoreName",
                _keystoreName);
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeystorePass",
                _keystorePass);
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeyaliasName",
                _keyaliasName);
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeyaliasPass",
                _keyaliasPass);
            Close();
            
            //Project Keystore
            PlayerSettings.Android.keystoreName = _keystoreName + _keystoreExt;
            PlayerSettings.Android.keystorePass = _keystorePass;
            //Project Key
            PlayerSettings.Android.keyaliasName = _keyaliasName;
            PlayerSettings.Android.keyaliasPass = _keyaliasPass;
        }
    }
}
