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
            GenerateVertices();
            SetVertices(buttonStyle.TileWidth);
            Triangles = new List<int>()
            {
                0, 1, 2, 0, 2, 3,
                3, 2, 4, 3, 4, 5,
                5, 4, 6, 5, 6, 7,
                8, 9, 10, 8, 10, 11,
                12, 13, 14, 12, 14, 15
            };
        }

        private void GenerateVertices()
        {
            PushVertex(0, 0, 0, 0);
            PushVertex(0, 0, 0, 1);
            PushVertex(0, 0, .25f, 1);
            PushVertex(0, 0, .25f, 0);

            PushVertex(0, 0, .5f, 1);
            PushVertex(0, 0, .5f, 0);

            PushVertex(0, 0, .75f, 1);
            PushVertex(0, 0, .75f, 0);

            PushVertex(0, 0, .25f, 0);
            PushVertex(0, 0, .25f, 1);
            PushVertex(0, 0, .5f, 1);
            PushVertex(0, 0, .5f, 0);

            PushVertex(0, 0, .75f, 0);
            PushVertex(0, 0, .75f, 1);
            PushVertex(0, 0, 1, 1);
            PushVertex(0, 0, 1, 0);
        }
        
        private void SetVertices(float tileWidth)
        {
            var index = 0;
            SetVertex(index++, 0, - Height);
            SetVertex(index++, 0, 0);
            SetVertex(index++, tileWidth, 0);
            SetVertex(index++, tileWidth, - Height);
            
            SetVertex(index++, Width / 2f - tileWidth / 2f, 0);
            SetVertex(index++, Width / 2f - tileWidth / 2f, - Height);
            
            SetVertex(index++, Width / 2f + tileWidth / 2f, 0);
            SetVertex(index++, Width / 2f + tileWidth / 2f, -Height);
            
            SetVertex(index++, Width / 2f + tileWidth / 2f, - Height);
            SetVertex(index++, Width / 2f + tileWidth / 2f, 0);
            SetVertex(index++, Width - tileWidth, 0);
            SetVertex(index++, Width - tileWidth, - Height);
            
            SetVertex(index++, Width - tileWidth, - Height);
            SetVertex(index++, Width - tileWidth, 0);
            SetVertex(index++, Width, 0);
            SetVertex(index, Width, - Height);
        }

        protected override void ApplySize()
        {
            var buttonStyle = (ButtonStyle) Style;
            SetVertices(buttonStyle.TileWidth);
        }
    }
}
