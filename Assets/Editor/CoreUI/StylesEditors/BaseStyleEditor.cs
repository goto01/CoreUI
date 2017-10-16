using Assets.Scripts.UICore.StylesSystem.Styles;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.CoreUI.StylesEditors
{
    [CustomEditor(typeof (BaseStyle), true)]
    public abstract class BaseStyleEditor : UnityEditor.Editor
    {
        private SerializedProperty _texture;
        private float _textureHeight;
        protected Rect _rect;

        public override void OnInspectorGUI()
        {
            FindSerializedProperties();
            DrawTextureInspector();
            DrawInspector();
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        protected virtual void FindSerializedProperties()
        {
            _texture = serializedObject.FindProperty("_texture");
        }

        private void DrawTextureInspector()
        {
            EditorGUILayout.ObjectField(_texture, typeof (Texture2D));
            var texture = _texture.objectReferenceValue as Texture2D;
            if (texture == null) return;
            DrawTexturePreview(texture);
        }

        private void DrawTexturePreview(Texture2D texture)
        {
            var lastRect = GUILayoutUtility.GetLastRect();
            var controlHeight = GetTextureControlHeight(lastRect.width, texture);
            _rect = GUILayoutUtility.GetRect(lastRect.width, lastRect.width, controlHeight, controlHeight);
            _rect.height = controlHeight;
            GUI.Box(_rect, string.Empty);
            GUI.DrawTexture(_rect, texture, ScaleMode.ScaleAndCrop);
        }

        private float GetTextureControlHeight(float width, Texture2D texture)
        {
            if (Event.current.type == EventType.Repaint) return (_textureHeight = texture.height*width/texture.width);
            return _textureHeight;
        }

        protected abstract void DrawInspector();
    }
}
