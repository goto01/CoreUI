using UnityEditor;

namespace CoreUI
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
