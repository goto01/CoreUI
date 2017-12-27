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
        private SerializedProperty _sinPixelsOffset;
        private SerializedProperty _sinSpeedOffset;
        private SerializedProperty _sinMultiplier;
        private SerializedProperty _horizontalShakePixelsOffset;
        private SerializedProperty _verticalShakePixelsOffset;

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
            _sinPixelsOffset = serializedObject.FindProperty("_sinPixelsOffset");
            _sinSpeedOffset = serializedObject.FindProperty("_sinSpeedOffset");
            _sinMultiplier = serializedObject.FindProperty("_sinMultiplier");
            _horizontalShakePixelsOffset = serializedObject.FindProperty("_horizontalShakePixelsOffset");
            _verticalShakePixelsOffset = serializedObject.FindProperty("_verticalShakePixelsOffset");
        }

        private void DrawInspector()
        {
            DrawModeSelector();
            EditorGUILayout.BeginVertical(GUI.skin.box);
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
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
            GUI.enabled = !string.IsNullOrEmpty(_text.stringValue);
            EditorGUILayout.PropertyField(_color);
            if (GUILayout.Button("Regenerate text mesh")) Target.GenerateData();
            GUI.enabled = true;
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
            EditorGUILayout.LabelField("Content", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_text);
        }

        private void DrawSettingsEditor()
        {
            EditorGUILayout.LabelField("Text settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_font);
            EditorGUILayout.PropertyField(_textWrapping);
            GUI.enabled = _textWrapping.boolValue;
            EditorGUILayout.PropertyField(_lineWidth);
            EditorGUILayout.LabelField("Sin wave effect settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_sinPixelsOffset);
            EditorGUILayout.PropertyField(_sinSpeedOffset);
            EditorGUILayout.PropertyField(_sinMultiplier);
            EditorGUILayout.LabelField("Shake effect settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_horizontalShakePixelsOffset);
            EditorGUILayout.PropertyField(_verticalShakePixelsOffset);
            EditorGUILayout.LabelField("Debug settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_fillRectangleJizmosColor);
            EditorGUILayout.PropertyField(_outlineRectangleGizmosColor);
            GUI.enabled = true;
        }

        private void DrawInfoEditor()
        {
            EditorGUILayout.LabelField("Text info", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Number of symbols");
            EditorGUILayout.LabelField(_text.stringValue.Length.ToString());
            EditorGUILayout.EndHorizontal();
        }
    }
}
