using UICore.Components;
using UnityEditor;

namespace CoreUI.Editor.ComponentEditors
{
    [CustomEditor(typeof(CoreUITextMesh))]
    class CoreUITextMeshEditor : UnityEditor.Editor
    {
        private SerializedProperty _text;
        private SerializedProperty _font;
        private SerializedProperty _color;

        public override void OnInspectorGUI()
        {
            FindProperties();
            DrawInspector();
            serializedObject.ApplyModifiedProperties();
        }

        private void FindProperties()
        {
            _text = serializedObject.FindProperty("_text");
            _font = serializedObject.FindProperty("_font");
            _color = serializedObject.FindProperty("_color");
        }

        private void DrawInspector()
        {
            EditorGUILayout.PropertyField(_font);
            if (_font.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Select font!", MessageType.Error);
                return;
            }
            EditorGUILayout.PropertyField(_text);
            EditorGUILayout.PropertyField(_color);
        }
    }
}
