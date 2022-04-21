using UnityEditor;
using UnityEngine;

namespace CoreUI
{
    [CustomEditor(typeof(CoreUIFont))]
    class CoreUIFontEditor : BaseStyleEditor
    {
        private SerializedProperty _pixelsInterval;
        private SerializedProperty _alphabet;
        private SerializedProperty _material;
        
        private CoreUIFont Target{get { return (CoreUIFont) target; }}
        
        protected override void FindSerializedProperties()
        {
            base.FindSerializedProperties();
            _pixelsInterval = serializedObject.FindProperty("_pixelsInterval");
            _alphabet = serializedObject.FindProperty("_alphabet");
            _material = serializedObject.FindProperty("_material");
        }

        protected override void DrawInspector()
        {
            EditorGUILayout.PropertyField(_pixelsInterval);
            EditorGUILayout.PropertyField(_alphabet, true);
            EditorGUILayout.PropertyField(_material);
            EditorGUILayout.Space();
            if (GUILayout.Button("Regenerate dictionaty")) Target.InitSelf();
        }
    }
}
