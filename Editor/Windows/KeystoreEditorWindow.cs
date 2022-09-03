using UnityEditor;
using UnityEngine.UIElements;

namespace Packages.DreamCode.AutoKeystore.Editor.Windows
{
    public class KeystoreEditorWindow : EditorWindow
    {

        #region PRIVATE_VARIABLES
        
        private TextField _keystoreName;
        private TextField _keystorePass;
        private TextField _keyaliasName;
        private TextField _keyaliasPass;
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
            _keystorePass.RegisterCallback<FocusInEvent>(FocusIn_KeystorePass);
            _keystorePass.RegisterCallback<FocusOutEvent>(FocusOut_KeystorePass);
            _keyaliasPass.RegisterCallback<FocusInEvent>(FocusIn_KeyaliasPass);
            _keyaliasPass.RegisterCallback<FocusOutEvent>(FocusOut_KeyaliasPass);
        }

        private void RemoveListeners()
        {
            rootVisualElement.Q<Button>("SaveBtn").clicked -= OnClick_SaveBtn;
            _keystorePass.UnregisterCallback<FocusInEvent>(FocusIn_KeystorePass);
            _keystorePass.UnregisterCallback<FocusOutEvent>(FocusOut_KeystorePass);
            _keyaliasPass.UnregisterCallback<FocusInEvent>(FocusIn_KeyaliasPass);
            _keyaliasPass.UnregisterCallback<FocusOutEvent>(FocusOut_KeyaliasPass);
        }
        
        private void LoadSettings()
        {
            _keystoreName = rootVisualElement.Q<TextField>("KeystorePath");
            _keystorePass = rootVisualElement.Q<TextField>("KeystorePass");
            _keystorePass.isPasswordField = true;
            _keyaliasName = rootVisualElement.Q<TextField>("KeyaliasName");
            _keyaliasPass = rootVisualElement.Q<TextField>("KeyaliasPass");
            _keyaliasPass.isPasswordField = true;
            //
            _keystoreName.value = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeystoreName");
            _keystorePass.value = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeystorePass");
            _keyaliasName.value = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeyaliasName");
            _keyaliasPass.value = EditorPrefs.GetString(PlayerSettings.applicationIdentifier + "dcKeyaliasPass");
        }

        private void FocusIn_KeystorePass(FocusInEvent evt)
        {
            _keystorePass.isPasswordField = false;
        }
        
        private void FocusOut_KeystorePass(FocusOutEvent evt)
        {
            _keystorePass.isPasswordField = true;
        }
        
        private void FocusIn_KeyaliasPass(FocusInEvent evt)
        {
            _keyaliasPass.isPasswordField = false;
        }
        
        private void FocusOut_KeyaliasPass(FocusOutEvent evt)
        {
            _keyaliasPass.isPasswordField = true;
        }
        
        private void SaveSettings()
        {
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeystoreName",
                _keystoreName.value);
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeystorePass",
                _keystorePass.value);
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeyaliasName",
                _keyaliasName.value);
            EditorPrefs.SetString(PlayerSettings.applicationIdentifier  + "dcKeyaliasPass",
                _keyaliasPass.value);
            //Project Keystore
            PlayerSettings.Android.keystoreName = _keystoreName.value + _keystoreExt;
            PlayerSettings.Android.keystorePass = _keystorePass.value;
            //Project Key
            PlayerSettings.Android.keyaliasName = _keyaliasName.value;
            PlayerSettings.Android.keyaliasPass = _keyaliasPass.value;
            Close();
        }
    }
}
