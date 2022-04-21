using UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace UICore.Controls
{
    public class CoreUIFlexibleImage : CoreUIElement
    {
        private float _value;
        private float _originWidth;
        
        public FlexibleImageMesh FlexibleImageMesh{get { return ((FlexibleImageMesh) _coreUIMesh); }}
        public float MinWidth{get { return FlexibleImageMesh.MinWidth; } }
        public float Value
        {
            get { return _value; }
            set
            {
                _value = Mathf.Clamp01(value);
                if (FlexibleImageMesh.Orientation == CoreUIOrientation.Horizontal) 
                    _coreUIMesh.Resize(Mathf.Max(_originWidth * _value, MinWidth), _coreUIMesh.Height);
                else
                    _coreUIMesh.Resize(_coreUIMesh.Width, Mathf.Max(_originWidth * _value, MinWidth));
            }
        }

        public float OriginWidth
        {
            get { return _originWidth; }
            set
            {
                _originWidth = value;
                if (FlexibleImageMesh.Orientation == CoreUIOrientation.Horizontal) 
                    _coreUIMesh.Resize(Mathf.Max(_originWidth * _value, MinWidth), _coreUIMesh.Height);
                else
                    _coreUIMesh.Resize(_coreUIMesh.Width, Mathf.Max(_originWidth * _value, MinWidth));
            }
        }
        
        public CoreUIFlexibleImage(FlexibleImageMesh mesh) : base(mesh)
        {
            _originWidth = FlexibleImageMesh.Orientation == CoreUIOrientation.Horizontal ? mesh.Width : mesh.Height;
        }
    }
}
