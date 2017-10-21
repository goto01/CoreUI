using Assets.Scripts.UICore.StylesSystem.Styles;
using UnityEditor;
using UnityEngine;

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
