using UICore.StylesSystem.Styles;
using UnityEditor;

namespace Assets.Editor.CoreUI.StylesEditors
{
    [CustomEditor(typeof(ImageStyle))]
    class ImageStyleEditor : BaseStyleEditor
    {
        protected override void DrawTextureInspector()
        {
            EditorGUILayout.PropertyField(_pixelWidth);
        }
    }
}
