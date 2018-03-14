using System;
using UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace UICore.Controls
{
    public class CoreUIButton : CoreUIElement
    {
        private int _id;
        protected Texture2D _pressedTexture;
        protected Texture2D _unpressedTexture;
        private Action<int> _action;
        private bool _prevPressed;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public override Texture2D Texture
        {
            get { return Active ? base.Texture : _unpressedTexture; }
            set { base.Texture = value; }
        }

        public CoreUIButton(BaseCoreUIMesh mesh) : this(mesh, null)
        {
        }

        public CoreUIButton(BaseCoreUIMesh mesh, Action<int> action) : base(mesh)
        {
            _pressedTexture = mesh.Texture;
            _unpressedTexture = ((ButtonMesh)mesh).UnpressedTexture;
            _action = action;
            _coreUIMesh.Texture = _unpressedTexture;
        }

        public override bool Update(CoreUIEvent e)
        {
            var focused = base.Update(e);
            HandlePressing(e, focused);
            _prevPressed = Pressed;
            return focused;
        }

        private void HandlePressing(CoreUIEvent e, bool focused)
        {
            if (_prevPressed && e.PointerUp && focused) InvokePressing();
            if (Pressed) _coreUIMesh.Texture = _pressedTexture;
            else _coreUIMesh.Texture = _unpressedTexture;
        }

        protected virtual void InvokePressing()
        {
            if (_action != null) _action.Invoke(_id); 
        }
    }
}
