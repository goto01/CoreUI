using System.Collections.Generic;
using UnityEngine;

namespace CoreUI
{
    public class ButtonMesh : BaseCoreUIMesh
    {
        private Texture2D _unpressedTexture;

        public Texture2D UnpressedTexture { get { return _unpressedTexture; } }

        protected override void Generate(BaseStyle style)
        {
            var buttonStyle = style as ButtonStyle;
            Height = buttonStyle.Height;
            _unpressedTexture = buttonStyle.UnpressedTexture;
            GenerateVertices(buttonStyle.TileWidth);

            Triangles = new List<int>()
            {
                0, 1, 2, 0, 2, 3,
                3, 2, 4, 3, 4, 5,
                5, 4, 6, 5, 6, 7,
                8, 9, 10, 8, 10, 11,
                12, 13, 14, 12, 14, 15
            };
        }

        private void GenerateVertices(float tileWidth)
        {
            PushVertex(0, - Height, 0, 0);
            PushVertex(0, 0, 0, 1);
            PushVertex(tileWidth, 0, .25f, 1);
            PushVertex(tileWidth, - Height, .25f, 0);

            PushVertex(Width / 2f - tileWidth / 2f, 0, .5f, 1);
            PushVertex(Width / 2f - tileWidth / 2f, - Height, .5f, 0);

            PushVertex(Width / 2f + tileWidth / 2f, 0, .75f, 1);
            PushVertex(Width / 2f + tileWidth / 2f, -Height, .75f, 0);

            PushVertex(Width / 2f + tileWidth / 2f, - Height, .25f, 0);
            PushVertex(Width / 2f + tileWidth / 2f, 0, .25f, 1);
            PushVertex(Width - tileWidth, 0, .5f, 1);
            PushVertex(Width - tileWidth, - Height, .5f, 0);

            PushVertex(Width - tileWidth, - Height, .75f, 0);
            PushVertex(Width - tileWidth, 0, .75f, 1);
            PushVertex(Width, 0, 1, 1);
            PushVertex(Width, - Height, 1, 0);
        }

        protected override void ApplySize()
        {
        }
    }
}
