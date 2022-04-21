using UnityEditor;

namespace CoreUI
{
    [CustomEditor(typeof(SliderStyle))]
    class SliderStyleEditor : FlexibleImageStyleEditor
    {
        private SerializedProperty _point;

        protected override void FindSerializedProperties()
        {
            base.FindSerializedProperties();
            _point = serializedObject.FindProperty("_point");
        }

        protected override void DrawTextureInspector()
        {
            EditorGUILayout.PropertyField(_point);
            base.DrawTextureInspector();
        }
    }
}
