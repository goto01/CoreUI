using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CoreUI
{
    [CustomEditor(typeof(WindowStyle))]
    public class WindowStyleEditor : BaseStyleEditor
    {
        private const string NineTiles = "9 Tiles";
        private const string ThreeTiles = "3 Tiles";

        private SerializedProperty _has9Tiles;
        private readonly string[] _variants = { NineTiles, ThreeTiles };
        private Texture2D _nineTilesTexture;
        private Texture2D _threeTilesTexture;
        private IDictionary<int, Texture2D> _selectedLayoutTexture;

        private Texture2D SelectedLayout { get { return _selectedLayoutTexture[SelectedIndex]; } }

        private int SelectedIndex
        {
            get { return _has9Tiles.boolValue ? 0 : 1; }
            set { _has9Tiles.boolValue = value == 0; }
        }

        protected virtual void OnEnable()
        {
            _nineTilesTexture = EditorGUIUtility.Load(StyleStaffPath + "Window9TilesLayout.png") as Texture2D;
            _threeTilesTexture = EditorGUIUtility.Load(StyleStaffPath + "Window3TilesLayout.png") as Texture2D;
            _selectedLayoutTexture = new Dictionary<int, Texture2D>()
            {
                {0, _nineTilesTexture},
                {1, _threeTilesTexture},
            };
        }

        protected override void FindSerializedProperties()
        {
            base.FindSerializedProperties();
            _has9Tiles = serializedObject.FindProperty("_has9Tiles");
        }

        protected override void DrawInspector()
        {
            GUI.DrawTexture(_rect, SelectedLayout);
            if (_has9Tiles.boolValue) EditorGUILayout.LabelField(string.Format("Tile's size: {0} x {1}", TextureSize.x / 2, TextureSize.y / 5));
            else EditorGUILayout.LabelField(string.Format("Tile's size: {0} x {1}", TextureSize.x / 2, TextureSize.y / 2));
            SelectedIndex = EditorGUILayout.Popup("Window's style layout", SelectedIndex, _variants);
        }
    }
}
