using System.Collections.Generic;
using Assets.Scripts.UICore.StylesSystem.Styles;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Meshes
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
            PushVertice(0, - Height, 0, 0);
            PushVertice(0, 0, 0, 1);
            PushVertice(tileWidth, 0, .25f, 1);
            PushVertice(tileWidth, - Height, .25f, 0);

            PushVertice(Width / 2f - tileWidth / 2f, 0, .5f, 1);
            PushVertice(Width / 2f - tileWidth / 2f, - Height, .5f, 0);

            PushVertice(Width / 2f + tileWidth / 2f, 0, .75f, 1);
            PushVertice(Width / 2f + tileWidth / 2f, -Height, .75f, 0);

            PushVertice(Width / 2f + tileWidth / 2f, - Height, .25f, 0);
            PushVertice(Width / 2f + tileWidth / 2f, 0, .25f, 1);
            PushVertice(Width - tileWidth, 0, .5f, 1);
            PushVertice(Width - tileWidth, - Height, .5f, 0);

            PushVertice(Width - tileWidth, - Height, .75f, 0);
            PushVertice(Width - tileWidth, 0, .75f, 1);
            PushVertice(Width, 0, 1, 1);
            PushVertice(Width, - Height, 1, 0);
        }

        protected override void ApplySize()
        {
        }
    }
}
