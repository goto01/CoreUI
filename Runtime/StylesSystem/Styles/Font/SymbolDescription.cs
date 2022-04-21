using System;
using UnityEngine;

namespace CoreUI
{
    [Serializable]
    public class SymbolDescription
    {
        [SerializeField] private char _symbol;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private UV _uv;
        [SerializeField] private int _pixelsVerticalOffset;
        [SerializeField] private Vector2 _size;
        [SerializeField] private bool _inited;
        [SerializeField] private float _verticalOffset;

        public bool Inited { get { return _inited; } }
        public char Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }
        public Sprite Sprite { get { return _sprite; } }
        public UV UV
        {
            get { return _uv; }
            set { _uv = value; }
        }

        public int PixelsVerticalOffset
        {
            get { return _pixelsVerticalOffset; }
            set { _pixelsVerticalOffset = value; }
        }

        public float VerticalOffset { get { return _verticalOffset;} }
        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public float Width { get { return Size.x; } }
        public float Height { get { return Size.y; } }

        public SymbolDescription()
        {
            _sprite = new Sprite();
            _uv = new UV(0, 0, 0, 0);
        }

        public void Init(float pixelWidth, int textureWidth, int textureHeight)
        {
            _inited = true;
            Size = new Vector2(Sprite.Width * pixelWidth, Sprite.Height * pixelWidth);
            _uv = new UV((float)Sprite.HorizontalOffset/textureWidth, (float)Sprite.VerticalOffset/textureHeight,
                (float)(Sprite.HorizontalOffset + Sprite.Width)/ textureWidth, (float)(Sprite.VerticalOffset + Sprite.Height)/ textureHeight);
            _verticalOffset = _pixelsVerticalOffset*pixelWidth;
        }
    }
}
