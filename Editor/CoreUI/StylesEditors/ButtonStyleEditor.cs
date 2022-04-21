using UnityEditor;
using UnityEngine;

namespace CoreUI
{
    [CustomEditor(typeof(ButtonStyle))]
    class ButtonStyleEditor : BaseStyleEditor
    {
        private Texture2D _layout;
        private SerializedProperty _unpressedTexture;

        protected virtual void OnEnable()
        {
            _layout = EditorGUIUtility.Load(StyleStaffPath + "ButtonLayout.png") as Texture2D;
        }

        protected override void FindSerializedProperties()
        {
            base.FindSerializedProperties();
            _unpressedTexture = serializedObject.FindProperty("_unpressedTexture");
        }

        protected override void DrawInspector()
        {
            GUI.DrawTexture(_rect, _layout);
            EditorGUILayout.PropertyField(_unpressedTexture);
        }
    }
}
