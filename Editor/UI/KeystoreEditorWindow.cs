using System;
using System.IO;
using DreamCode.AutoKeystore.Editor.Configuration;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DreamCode.AutoKeystore.Editor.UI
{
    internal class KeystoreEditorWindow : EditorWindow
    {
        private static readonly Vector2 _windowMinSize = new(200, 380);
        private const string PackagePath = "Packages/com.dreamcode.mobile.android-keystore";
        private const string AssetsPath = PackagePath + "/Editor/Assets";
        private const string LayoutsPath = AssetsPath + "/Layouts";
        private const string StylesPath = AssetsPath + "/Styles";
        private const string WindowLayoutPath = LayoutsPath + "/KeystoreWindow.uxml";

        private TextField _keystoreName;
        private TextField _keystorePass;
        private TextField _keyaliasName;
        private TextField _keyaliasPass;
        private EnumField _repositoryField;
        private KeystoreRepository _currentRepository;

        [MenuItem("Tools/DreamCode/Android/AutoKeystore")]
        internal static void ShowWindow()
        {
            var window = GetWindow<KeystoreEditorWindow>();
            window.titleContent.text = nameof(AutoKeystore);
            window.minSize = _windowMinSize;
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
            rootVisualElement.Q<Button>("DonateBtn").clicked += OnDonateBtnClicked;
            _keystorePass.RegisterCallback<FocusInEvent>(OnKeystorePassFocusIn);
            _keystorePass.RegisterCallback<FocusOutEvent>(OnKeystorePassFocusOut);
            _keyaliasPass.RegisterCallback<FocusInEvent>(OnKeyaliasPassFocusIn);
            _keyaliasPass.RegisterCallback<FocusOutEvent>(OnKeyaliasPassFocusOut);
            _repositoryField.RegisterValueChangedCallback(OnRepositoryFieldChanged);
        }

        private void RemoveListeners()
        {
            rootVisualElement.Q<Button>("SaveBtn").clicked -= OnSaveBtnClicked;
            rootVisualElement.Q<Button>("DonateBtn").clicked -= OnDonateBtnClicked;
            _keystorePass.UnregisterCallback<FocusInEvent>(OnKeystorePassFocusIn);
            _keystorePass.UnregisterCallback<FocusOutEvent>(OnKeystorePassFocusOut);
            _keyaliasPass.UnregisterCallback<FocusInEvent>(OnKeyaliasPassFocusIn);
            _keyaliasPass.UnregisterCallback<FocusOutEvent>(OnKeyaliasPassFocusOut);
            _repositoryField.UnregisterValueChangedCallback(OnRepositoryFieldChanged);
        }

        private void OnSaveBtnClicked()
        {
            SaveSettings();
            Close();
        }

        private void OnDonateBtnClicked() => Application.OpenURL("https://punkto.me/eCUIF99");

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

        private void OnRepositoryFieldChanged(ChangeEvent<Enum> evt)
        {
            _currentRepository = (KeystoreRepository)evt.newValue;
        }

        private void SetupWindowLayout()
        {
            _keystoreName = rootVisualElement.Q<TextField>("KeystorePath");
            _keystorePass = rootVisualElement.Q<TextField>("KeystorePass");
            _keystorePass.isPasswordField = true;
            _keyaliasName = rootVisualElement.Q<TextField>("KeyaliasName");
            _keyaliasPass = rootVisualElement.Q<TextField>("KeyaliasPass");
            _keyaliasPass.isPasswordField = true;
            _repositoryField = rootVisualElement.Q<EnumField>("Storage");
        }

        private void LoadSettings()
        {
            var keystoreRepository = (KeystoreRepository)EditorPrefs.GetInt(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreRepository)}");
            _repositoryField.Init(keystoreRepository);
            KeystoreSettings.SetupRepository(keystoreRepository);

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

            EditorPrefs.SetInt(
                $"{PlayerSettings.applicationIdentifier}-{nameof(KeystoreRepository)}",
                (int)_currentRepository);

            KeystoreSettings.SetupRepository(_currentRepository);
            KeystoreSettings.Save(keystoreName, keystorePass, keyaliasName, keyaliasPass);
        }
    }
}