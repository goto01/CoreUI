using UnityEditor;
using UnityEngine;

namespace CoreUI
{
    [CustomEditor(typeof (BaseStyle), true)]
    public class BaseStyleEditor : UnityEditor.Editor
    {
        protected const string StyleStaffPath = "CoreUI/StylesStaff/";

        protected SerializedProperty _texture;
        protected SerializedProperty _pixelWidth;
        private float _textureHeight;
        protected Rect _rect;
        private bool _foldout;

        protected Texture2D Texture { get { return _texture.objectReferenceValue as Texture2D;} }

        protected Vector2 TextureSize
        {
            get
            {
                var texture = Texture;
                return new Vector2(texture.width, texture.height);
            }
        }

        public override void OnInspectorGUI()
        {
            FindSerializedProperties();
            DrawTextureInspector();
            if (Texture == null) return;
            DrawInspector();
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        protected virtual void FindSerializedProperties()
        {
            _texture = serializedObject.FindProperty("_texture");
            _pixelWidth= serializedObject.FindProperty("_pixelWidth");
        }

        protected virtual void DrawTextureInspector()
        {
            EditorGUILayout.PropertyField(_pixelWidth);
            EditorGUILayout.ObjectField(_texture, typeof (Texture2D));
            var texture = _texture.objectReferenceValue as Texture2D;
            if (texture == null) return;
            DrawTexturePreview(texture);
        }

        protected virtual void DrawTexturePreview(Texture2D texture)
        {
            EditorGUILayout.LabelField(string.Format("Texture's size: {0} x {1}", texture.width, texture.height));
            _foldout = EditorGUILayout.Foldout(_foldout, "Texture");
            if (!_foldout) return;
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

        protected virtual void DrawInspector() { }
    }
}
