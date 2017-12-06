using UnityEngine;

namespace UICore.StylesSystem.Styles
{
    public class ButtonStyle : BaseStyle
    {
        [SerializeField] private Texture2D _unpressedTexture;

        public Texture2D UnpressedTexture { get { return _unpressedTexture; } }
        public float TileWidth { get { return Texture.width/4f * _pixelWidth; } }
        public float Height { get { return Texture.height*_pixelWidth; } }
    }
}
