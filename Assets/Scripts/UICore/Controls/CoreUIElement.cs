using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.Controls
{
    public abstract class CoreUIElement
    {
        private BaseCoreUIMesh _coreUIMesh;

        public Mesh Mesh { get { return _coreUIMesh.Mesh; } }

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

        public abstract void Update();
        
        protected CoreUIElement(BaseCoreUIMesh mesh)
        {
            _coreUIMesh = mesh;
        }
    }
}
