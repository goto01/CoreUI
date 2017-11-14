using Assets.Scripts.UICore.StylesSystem.Styles.Font;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.CoreUI.StylesEditors
{
    [CustomEditor(typeof(CoreUIFont))]
    class CoreUIFontEditor : BaseStyleEditor
    {
        private SerializedProperty _pixelsInterval;
        private SerializedProperty _alphabet;

        protected override void FindSerializedProperties()
        {
            base.FindSerializedProperties();
            _pixelsInterval = serializedObject.FindProperty("_pixelsInterval");
            _alphabet = serializedObject.FindProperty("_alphabet");
        }

        protected override void DrawInspector()
        {
            EditorGUILayout.PropertyField(_pixelsInterval);
            EditorGUILayout.PropertyField(_alphabet, true);
        }
    }
}
