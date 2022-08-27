using Packages.DreamCode.AutoKeystore.Editor.Windows;
using UnityEditor;

namespace Packages.DreamCode.AutoKeystore.Editor.Menu
{
    public class KeystoreMenuItem : UnityEditor.Editor
    {

        #region PUBLIC_VARIABLES

        private static KeystoreEditorWindow _keystoreWindow;
        private const string _winTitle = "Android AutoKeystore";

        #endregion
        
        [MenuItem("DreamCode/Android/AutoKeystore")]
        private static void Init()
        {
            _keystoreWindow = EditorWindow.GetWindow<KeystoreEditorWindow>();
            _keystoreWindow.titleContent.text = _winTitle;
        }
    }
}
