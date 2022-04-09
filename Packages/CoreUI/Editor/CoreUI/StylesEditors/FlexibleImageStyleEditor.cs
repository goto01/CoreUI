﻿using UnityEditor;
using UnityEngine;

namespace CoreUI
{
    [CustomEditor(typeof(FlexibleImageStyle))]
    class FlexibleImageStyleEditor : BaseStyleEditor
    {
        private Texture2D _layout;

        protected virtual void OnEnable()
        {
            _layout = EditorGUIUtility.Load(StyleStaffPath + "FlexibleImageLayout.png") as Texture2D;
        }

        protected override void DrawInspector()
        {
            GUI.DrawTexture(_rect, _layout);
        }
    }
}
