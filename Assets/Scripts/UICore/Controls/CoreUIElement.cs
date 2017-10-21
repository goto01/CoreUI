using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.Controls
{
    public abstract class CoreUIElement
    {
        protected BaseCoreUIMesh _coreUIMesh;

        public Mesh Mesh { get { return _coreUIMesh.Mesh; } }

        public Texture2D Texture { get { return _coreUIMesh.Texture; } }

        public float X
        {
            get { return _coreUIMesh.X; }
            set { _coreUIMesh.X = value; }
        }

        public float Y
        {
            get { return _coreUIMesh.Y; }
            set { _coreUIMesh.Y = value; }
        }

        public float Width
        {
            get { return _coreUIMesh.Width; }
            set { _coreUIMesh.Width = value; }
        }

        public float Height
        {
            get { return _coreUIMesh.Height; }
            set { _coreUIMesh.Height = value; }
        }

        public abstract void Update();
        
        protected CoreUIElement(BaseCoreUIMesh mesh)
        {
            _coreUIMesh = mesh;
        }
    }
}
