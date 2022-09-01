using Packages.DreamCode.AutoKeystore.Editor.Windows;
using UnityEditor;
using UnityEngine;

namespace Packages.DreamCode.AutoKeystore.Editor.Menu
{
    public class KeystoreMenuItem : UnityEditor.Editor
    {
        #region PRIVATE_VARIABLES

        private static KeystoreEditorWindow _keystoreWindow;
        private const string _winTitle = "AutoKeystore";
        private static readonly Vector2 _windowMinSize = new Vector2(200, 380);

        #endregion
        
        [MenuItem("DreamCode/Android/AutoKeystore")]
        private static void Init()
        {
            _keystoreWindow = EditorWindow.GetWindow<KeystoreEditorWindow>();
            _keystoreWindow.titleContent.text = _winTitle;
            _keystoreWindow.minSize = _windowMinSize;
        }
    }
}
