using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DreamCode.AutoKeystore.Editor.UI
{
    internal class KeystoreEditorWindow : EditorWindow
    {
        private static readonly Vector2 s_windowMinSize = new(200, 380);
        private const string PackagePath = "Packages/com.dreamcode.mobile.android-keystore";
        private const string AssetsPath = PackagePath + "/Editor/Assets";
        private const string LayoutsPath = AssetsPath + "/Layouts";
        private const string StylesPath = AssetsPath + "/Styles";
        private const string WindowLayoutPath = LayoutsPath + "/KeystoreWindow.uxml";
        private TextField _keystoreName;
        private TextField _keystorePass;
        private TextField _keyaliasName;
        private TextField _keyaliasPass;

        [MenuItem("Tools/DreamCode/Android/AutoKeystore")]
        internal static void ShowWindow()
        {
            var window = GetWindow<KeystoreEditorWindow>();
            window.titleContent.text = nameof(AutoKeystore);
            window.minSize = s_windowMinSize;
        }

        private void CreateGUI()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(WindowLayoutPath);
            visualTree.CloneTree(rootVisualElement);
            SetupWindowLayout();
            LoadSettings();
            RegisterListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void RegisterListeners()
        {
            rootVisualElement.Q<Button>("SaveBtn").clicked += OnSaveBtnClicked;
            _keystorePass.RegisterCallback<FocusInEvent>(OnKeystorePassFocusIn);
            _keystorePass.RegisterCallback<FocusOutEvent>(OnKeystorePassFocusOut);
            _keyaliasPass.RegisterCallback<FocusInEvent>(OnKeyaliasPassFocusIn);
            _keyaliasPass.RegisterCallback<FocusOutEvent>(OnKeyaliasPassFocusOut);
        }

        private void RemoveListeners()
        {
            rootVisualElement.Q<Button>("SaveBtn").clicked -= OnSaveBtnClicked;
            _keystorePass.UnregisterCallback<FocusInEvent>(OnKeystorePassFocusIn);
            _keystorePass.UnregisterCallback<FocusOutEvent>(OnKeystorePassFocusOut);
            _keyaliasPass.UnregisterCallback<FocusInEvent>(OnKeyaliasPassFocusIn);
            _keyaliasPass.UnregisterCallback<FocusOutEvent>(OnKeyaliasPassFocusOut);
        }

        private void OnSaveBtnClicked()
        {
            SaveSettings();
            Close();
        }

        private void OnKeystorePassFocusIn(FocusInEvent e)
        {
            _keystorePass.isPasswordField = false;
        }

        private void OnKeystorePassFocusOut(FocusOutEvent evt)
        {
            _keystorePass.isPasswordField = true;
        }

        private void OnKeyaliasPassFocusIn(FocusInEvent evt)
        {
            _keyaliasPass.isPasswordField = false;
        }

        private void OnKeyaliasPassFocusOut(FocusOutEvent evt)
        {
            _keyaliasPass.isPasswordField = true;
        }

        private void SetupWindowLayout()
        {
            _keystoreName = rootVisualElement.Q<TextField>("KeystorePath");
            _keystorePass = rootVisualElement.Q<TextField>("KeystorePass");
            _keystorePass.isPasswordField = true;
            _keyaliasName = rootVisualElement.Q<TextField>("KeyaliasName");
            _keyaliasPass = rootVisualElement.Q<TextField>("KeyaliasPass");
            _keyaliasPass.isPasswordField = true;
        }

        private void LoadSettings()
        {
            KeystoreSettings.Load();
            _keystoreName.value = Path.GetFileNameWithoutExtension(KeystoreSettings.Name);
            _keystorePass.value = KeystoreSettings.Password;
            _keyaliasName.value = KeystoreSettings.AliasName;
            _keyaliasPass.value = KeystoreSettings.AliasPassword;
        }

        private void SaveSettings()
        {
            var keystoreName = _keystoreName.value;
            var keystorePass = _keystorePass.value;
            var keyaliasName = _keyaliasName.value;
            var keyaliasPass = _keyaliasPass.value;
            KeystoreSettings.Save(keystoreName, keystorePass, keyaliasName, keyaliasPass);
        }
    }
}