using System.Collections.Generic;
using Assets.Scripts.UICore.StylesSystem.Styles;

namespace Assets.Scripts.UICore.UICoreMeshes.Meshes
{
    public enum CoreUIOrientation
    {
        Horizontal,
        Vertical
    }

    public class FlexibleImageMesh : BaseCoreUIMesh
    {
        private CoreUIOrientation _orientation;
        private float _borderWidth;
        private float _borderHeight;

        public float MinWidth { get { return _borderWidth*2; } }

        public float BorderWidth { get { return _borderWidth; } }
        
        private FlexibleImageMesh() : base()
        {
            
        }

        public FlexibleImageMesh(CoreUIOrientation orientation) : this()
        {
            _orientation = orientation;
        }

        protected override void Generate(BaseStyle style)
        {
            var flexibleImageStyle = style as FlexibleImageStyle;
            _borderWidth = flexibleImageStyle.BorderWidth;
            _borderHeight = flexibleImageStyle.BorderHeight;
            if (_orientation == CoreUIOrientation.Horizontal)
            {
                Height = _borderHeight;
                GenerateHorizontal();
            }
            else
            {
                Height = Width;
                Width = _borderHeight;
                GenerateVertical();
            }
            ApplyUV();

            Triangles = new List<int>()
            {
                0, 1, 2, 0, 2, 3,
                4, 5, 6, 4, 6, 7,
                8, 9, 10, 8, 10, 11,
            };
        }

        private void GenerateHorizontal()
        {
            PushVertice(X, Y - _borderHeight);
            PushVertice(X, Y);
            PushVertice(X + _borderWidth, Y);
            PushVertice(X + _borderWidth, Y - _borderHeight);

            PushVertice(X + _borderWidth, Y - _borderHeight);
            PushVertice(X + _borderWidth, Y);
            PushVertice(X + Width - _borderWidth, Y);
            PushVertice(X + Width - _borderWidth, Y - _borderHeight);

            PushVertice(X + Width - _borderWidth, Y - _borderHeight);
            PushVertice(X + Width - _borderWidth, Y);
            PushVertice(X + Width, Y);
            PushVertice(X + Width, Y - _borderHeight);
        }

        private void GenerateVertical()
        {
            PushVertice(X, Y);
            PushVertice(X + Width, Y);
            PushVertice(X + Width, Y - _borderWidth);
            PushVertice(X, Y - _borderWidth);

            PushVertice(X, Y - _borderWidth);
            PushVertice(X + Width, Y - _borderWidth);
            PushVertice(X + Width, Y - Height + _borderWidth);
            PushVertice(X, Y - Height + _borderWidth);

            PushVertice(X, Y - Height + _borderWidth);
            PushVertice(X + Width, Y - Height + _borderWidth);
            PushVertice(X + Width, Y - Height);
            PushVertice(X, Y - Height);
        }

        private void ApplyUV()
        {
            PushUV(0, 0);
            PushUV(0, 1);
            PushUV(.25f, 1);
            PushUV(.25f, 0);

            PushUV(.25f, 0);
            PushUV(.25f, 1);
            PushUV(.5f, 1);
            PushUV(.5f, 0);

            PushUV(.5f, 0);
            PushUV(.5f, 1);
            PushUV(.75f, 1);
            PushUV(.75f, 0);
        }
        
        protected override void ApplySize()
        {
            Clear();
            if (_orientation == CoreUIOrientation.Horizontal) GenerateHorizontal();
            else GenerateVertical();
            UpdatePositions();
        }
    }
}
