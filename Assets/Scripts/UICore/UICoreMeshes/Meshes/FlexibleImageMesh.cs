using System;
using System.Collections.Generic;
using Assets.Scripts.UICore.StylesSystem.Styles;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Meshes
{
    public enum FlexiblaImageOrientation
    {
        Horizontal,
        Vertical
    }

    public class FlexibleImageMesh : BaseCoreUIMesh
    {
        private FlexiblaImageOrientation _orientation;
        private float _borderWidth;
        private float _borderHeight;

        public float MinWidth { get { return _borderWidth*2; } }
        
        private FlexibleImageMesh() : base()
        {
            
        }

        public FlexibleImageMesh(FlexiblaImageOrientation orientation) : this()
        {
            _orientation = orientation;
        }

        protected override void Generate(BaseStyle style)
        {
            var flexibleImageStyle = style as FlexibleImageStyle;
            _borderWidth = flexibleImageStyle.BorderWidth;
            _borderHeight = flexibleImageStyle.BorderHeight;
            if (_orientation == FlexiblaImageOrientation.Horizontal) GenerateHorizontal();
            else GenerateVertical();
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
            PushVertice(X + _borderHeight, Y);
            PushVertice(X + _borderHeight, Y - _borderWidth);
            PushVertice(X, Y - _borderWidth);

            PushVertice(X, Y - _borderWidth);
            PushVertice(X + _borderHeight, Y - _borderWidth);
            PushVertice(X + _borderHeight, Y - Width + _borderWidth);
            PushVertice(X, Y - Width + _borderWidth);

            PushVertice(X, Y - Width + _borderWidth);
            PushVertice(X + _borderHeight, Y - Width + _borderWidth);
            PushVertice(X + _borderHeight, Y - Width);
            PushVertice(X, Y - Width);
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
            if (_orientation == FlexiblaImageOrientation.Horizontal) GenerateHorizontal();
            else GenerateVertical();
            UpdatePositions();
        }
    }
}
