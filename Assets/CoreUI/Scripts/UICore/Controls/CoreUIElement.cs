using UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace UICore.Controls
{
    public abstract class CoreUIElement
    {
        protected BaseCoreUIMesh _coreUIMesh;
        private bool _focused;
        private bool _pressed;
        private int _order;
        private bool _active;
        private bool _enabled;
        protected float _verticalTopLimit;
        protected float _verticalBottomLimit;
        protected float _horizontalLeftLimit;
        protected float _horizontalRightLimit;
        private Color _color;

        public virtual Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public virtual Vector2 Position
        {
            get { return _coreUIMesh.Position; }
            set { _coreUIMesh.Position = value; }
        }

        public virtual bool Active
        {
            get { return _active;}
            set
            {
                _active = value;
                _pressed = false;
            }
        }
        
        public virtual bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public virtual int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public bool Pressed { get { return _pressed; } }

        public BaseCoreUIMesh CoreUIMesh { get { return _coreUIMesh; } }

        public Mesh Mesh { get { return _coreUIMesh.Mesh; } }

        public virtual Texture2D Texture
        {
            get { return _coreUIMesh.Texture; }
            set { _coreUIMesh.Texture = value; }
        }

        public bool TextureChanged
        {
            get { return _coreUIMesh.TextureChanged; }
            set { _coreUIMesh.TextureChanged = value; }
        }

        public virtual float X
        {
            get { return _coreUIMesh.X; }
            set { _coreUIMesh.X = value; }
        }

        public virtual float Y
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

        public virtual float VerticalTopLimit
        {
            get { return _verticalTopLimit; }
            set { _verticalTopLimit = value; }
        }

        public virtual float VerticalBottomLimit
        {
            get { return _verticalBottomLimit; }
            set { _verticalBottomLimit = value; }
        }

        public virtual float HorizontalLeftLimit
        {
            get { return _horizontalLeftLimit; }
            set { _horizontalLeftLimit = value; }
        }

        public virtual float HorizontalRightLimit
        {
            get { return _horizontalRightLimit;}
            set { _horizontalRightLimit = value; }
        }

        public virtual bool Update(CoreUIEvent e)
        {
            var focus = Contains(e.PointerPosition);
            if (focus && e.PointerDown) _pressed = true;
            if (e.PointerUp) _pressed = false;
            return focus;
        }

        public virtual void ResetParentPosition(Vector2 oldPosition, Vector2 newPosition)
        {
            _coreUIMesh.ResetParentPosition(oldPosition, newPosition);
        }
        
        protected CoreUIElement(BaseCoreUIMesh mesh)
        {
            _coreUIMesh = mesh;
            _color = new Color(1,1,1,1);
        }

        protected virtual bool Contains(Vector2 pos)
        {
            return _coreUIMesh.Rect.Contains(pos);
        }
    }
}
