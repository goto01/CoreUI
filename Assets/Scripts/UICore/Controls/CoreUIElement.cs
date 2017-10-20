using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.Controls
{
    public abstract class CoreUIElement
    {
        private BaseCoreUIMesh _coreUIMesh;

        public Mesh Mesh { get { return _coreUIMesh.Mesh; } }

        public abstract void Update();
        
        protected CoreUIElement(BaseCoreUIMesh mesh)
        {
            _coreUIMesh = mesh;
        }
    }
}
