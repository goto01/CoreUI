using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.Controls
{
    public abstract class CoreUIElement
    {
        protected BaseCoreUIMesh _coreUIMesh;
        private bool _focused;
        private bool _pressed;

        public bool Pressed { get { return _pressed; } }

        public BaseCoreUIMesh CoreUIMesh { get { return _coreUIMesh; } }

        public Mesh Mesh { get { return _coreUIMesh.Mesh; } }

        public Texture2D Texture
        {
            get { return _coreUIMesh.Texture; }
            set { _coreUIMesh.Texture = value; }
        }

        public bool TextureChanged
        {
            get { return _coreUIMesh.TextureChanged; }
            set { _coreUIMesh.TextureChanged = value; }
        }

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

        public float CenterX
        {
            get { return X + Width/2f; }
            set { X = value - Width/2f; }
        }

        public float CenterY
        {
            get { return Y - Height/2f; }
            set { Y = value + Height/2f; }
        }

        public virtual bool Update(CoreUIEvent e)
        {
            var focus = _coreUIMesh.Rect.Contains(e.PointerPosition);
            if (focus && e.PointerDown) _pressed = true;
            if (e.PointerUp) _pressed = false;
            return focus;
        }
        
        protected CoreUIElement(BaseCoreUIMesh mesh)
        {
            _coreUIMesh = mesh;
        }
    }
}
