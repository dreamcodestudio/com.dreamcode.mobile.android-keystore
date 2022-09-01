using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
        
        private void CreateGUI()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.dreamcode.mobile.android-keystore/Editor/UI/Templates/KeystoreWindow.uxml");
            visualTree.CloneTree(rootVisualElement);
            
            LoadSettings();
            RegisterListeners();
        }


        private void OnDisable()
        {
            RemoveListeners();
        }

        #endregion

        private void OnClick_SaveBtn()
        {
            SaveSettings();
        }

        private void RegisterListeners()
        {
            rootVisualElement.Q<Button>("SaveBtn").clicked += OnClick_SaveBtn;
        }

        private void RemoveListeners()
        {
            rootVisualElement.Q<Button>("SaveBtn").clicked -= OnClick_SaveBtn;
        }
        
        private void LoadSettings()
        {
            _keystoreName = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeystoreName");
            _keystorePass = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeystorePass");
            _keyaliasName = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeyaliasName");
            _keyaliasPass = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeyaliasPass");
            //
            rootVisualElement.Q<TextField>("KeystorePath").value = _keystoreName;
            rootVisualElement.Q<TextField>("KeystorePass").value = _keystorePass;
            rootVisualElement.Q<TextField>("KeyaliasName").value = _keyaliasName;
            rootVisualElement.Q<TextField>("KeyaliasPass").value = _keyaliasPass;
        }

        private void SaveSettings()
        {
            _keystoreName = rootVisualElement.Q<TextField>("KeystorePath").value;
            _keystorePass = rootVisualElement.Q<TextField>("KeystorePass").value;
            _keyaliasName = rootVisualElement.Q<TextField>("KeyaliasName").value;
            _keyaliasPass = rootVisualElement.Q<TextField>("KeyaliasPass").value;
            //
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeystoreName",
                _keystoreName);
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeystorePass",
                _keystorePass);
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeyaliasName",
                _keyaliasName);
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeyaliasPass",
                _keyaliasPass);
            //Project Keystore
            PlayerSettings.Android.keystoreName = _keystoreName + _keystoreExt;
            PlayerSettings.Android.keystorePass = _keystorePass;
            //Project Key
            PlayerSettings.Android.keyaliasName = _keyaliasName;
            PlayerSettings.Android.keyaliasPass = _keyaliasPass;
            Close();
        }
    }
}
