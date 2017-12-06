using System;
using UnityEngine;

namespace UICore.StylesSystem.Styles
{
    [Serializable]
    public class WindowStyle : BaseStyle
    {
        [SerializeField] private bool _has9Tiles;

        public bool Has9Tiles { get { return _has9Tiles; } }
        public float BorderWidth { get { return _texture.width/2f*_pixelWidth; } }
    }
}
