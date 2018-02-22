using System.Collections.Generic;
using UICore.StylesSystem.Styles;
using UnityEngine;

namespace UICore.UICoreMeshes.Meshes
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
        public CoreUIOrientation Orientation{get { return _orientation; }}
        
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
            PushVertice(0, -_borderHeight);
            PushVertice(0, 0);
            PushVertice(0 + _borderWidth, 0);
            PushVertice(0 + _borderWidth, - _borderHeight);
                        
            PushVertice(0 + _borderWidth, - _borderHeight);
            PushVertice(0 + _borderWidth, 0);
            PushVertice(0 + Width - _borderWidth, 0);
            PushVertice(0 + Width - _borderWidth, - _borderHeight);
                        
            PushVertice(0 + Width - _borderWidth, - _borderHeight);
            PushVertice(0 + Width - _borderWidth, 0);
            PushVertice(0 + Width, 0);
            PushVertice(0 + Width, - _borderHeight);
        }

        private void GenerateVertical()
        {
            PushVertice(0, 0);
            PushVertice(0 + Width, 0);
            PushVertice(0 + Width, - _borderWidth);
            PushVertice(0, - _borderWidth);
                        
            PushVertice(0, - _borderWidth);
            PushVertice(0 + Width, - _borderWidth);
            PushVertice(0 + Width, - Height + _borderWidth);
            PushVertice(0, - Height + _borderWidth);
                        
            PushVertice(0, - Height + _borderWidth);
            PushVertice(0 + Width, - Height + _borderWidth);
            PushVertice(0 + Width, - Height);
            PushVertice(0, - Height);
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
            //Clear();
            if (_orientation == CoreUIOrientation.Horizontal) ApplyHorizontal();
            else ApplyVertical();
            UpdatePositions();
        }

        private void ApplyHorizontal()
        {
            Vertices[6] = new Vector3(Width - _borderWidth, 0);
            Vertices[7] = new Vector3(Width - _borderWidth, - _borderHeight);
            
            Vertices[8] = new Vector3(Width - _borderWidth, -_borderHeight);
            Vertices[9] = new Vector3(Width - _borderWidth, 0);
            Vertices[10] = new Vector3(Width, 0);
            Vertices[11] = new Vector3(Width, - _borderHeight);
        }

        private void ApplyVertical()
        {
            Vertices[6] = new Vector3(Width, - Height + _borderWidth);
            Vertices[7] = new Vector3(0, - Height + _borderWidth);
            
            Vertices[8] = new Vector3(0, - Height + _borderWidth);
            Vertices[9] = new Vector3(Width, - Height + _borderWidth);
            Vertices[10] = new Vector3(Width, - Height);
            Vertices[11] = new Vector3(0, - Height);
        }
    }
}
