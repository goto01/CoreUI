using UICore.Components;
using UnityEditor;
using UnityEngine;

namespace CoreUI.Editor.ComponentEditors
{
    [CustomEditor(typeof(CoreUITextMesh))]
    class CoreUITextMeshEditor : UnityEditor.Editor
    {
        private SerializedProperty _text;
        private SerializedProperty _font;
        private SerializedProperty _color;
        private SerializedProperty _lineWidth;
        private SerializedProperty _textWrapping;
        private SerializedProperty _fillRectangleJizmosColor;
        private SerializedProperty _outlineRectangleGizmosColor;
        private SerializedProperty _selectedMode;

        private CoreUITextMesh Target { get { return target as CoreUITextMesh; } }

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
            _lineWidth = serializedObject.FindProperty("_lineWidth");
            _textWrapping = serializedObject.FindProperty("_textWrapping");
            _fillRectangleJizmosColor = serializedObject.FindProperty("_fillRectangleJizmosColor");
            _outlineRectangleGizmosColor = serializedObject.FindProperty("_outlineRectangleGizmosColor");
            _selectedMode = serializedObject.FindProperty("_selectedMode");
        }

        private void DrawInspector()
        {
            DrawModeSelector();
            switch (_selectedMode.intValue)
            {
                case 0:
                    DrawContentEditor();
                    break;
                case 1:
                    DrawSettingsEditor();
                    break;
                case 2:
                    DrawInfoEditor();
                    break;
            }
            
        }

        private void DrawModeSelector()
        {
            EditorGUILayout.BeginHorizontal();
            var mode0 = GUILayout.Toggle(_selectedMode.intValue == 0, "Content", EditorStyles.miniButtonLeft);
            if (mode0 && _selectedMode.intValue != 0) _selectedMode.intValue = 0;
            var mode1 = GUILayout.Toggle(_selectedMode.intValue == 1, "Settings", EditorStyles.miniButtonMid);
            if (mode1 && _selectedMode.intValue != 1) _selectedMode.intValue = 1;
            var mode2 = GUILayout.Toggle(_selectedMode.intValue == 2, "info", EditorStyles.miniButtonRight);
            if (mode2 && _selectedMode.intValue != 2) _selectedMode.intValue = 2;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        private void DrawContentEditor()
        {
            if (_font.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Select font in the settings tab!", MessageType.Error);
                return;
            }
            EditorGUILayout.PropertyField(_text);
            GUI.enabled = !string.IsNullOrEmpty(_text.stringValue);
            EditorGUILayout.PropertyField(_color);
            if (GUILayout.Button("Regenerate text mesh")) Target.GenerateData();
            GUI.enabled = true;
        }

        private void DrawSettingsEditor()
        {
            EditorGUILayout.PropertyField(_font);
            EditorGUILayout.PropertyField(_textWrapping);
            GUI.enabled = _textWrapping.boolValue;
            EditorGUILayout.PropertyField(_lineWidth);
            EditorGUILayout.PropertyField(_fillRectangleJizmosColor);
            EditorGUILayout.PropertyField(_outlineRectangleGizmosColor);
            GUI.enabled = true;
        }

        private void DrawInfoEditor()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Number of symbols");
            EditorGUILayout.LabelField(_text.stringValue.Length.ToString());
            EditorGUILayout.EndHorizontal();
        }
    }
}
