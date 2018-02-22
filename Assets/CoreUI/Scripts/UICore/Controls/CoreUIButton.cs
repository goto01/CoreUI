using System;
using UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace UICore.Controls
{
    public class CoreUIButton : CoreUIElement
    {
        private int _id;
        private Texture2D _pressed;
        private Texture2D _unpressed;
        private Action<int> _action;
        private bool _prevPressed;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public override Texture2D Texture
        {
            get { return Active ? base.Texture : _unpressed; }
            set { base.Texture = value; }
        }

        public CoreUIButton(BaseCoreUIMesh mesh) : this(mesh, null)
        {
        }

        public CoreUIButton(BaseCoreUIMesh mesh, Action<int> action) : base(mesh)
        {
            _pressed = mesh.Texture;
            _unpressed = ((ButtonMesh)mesh).UnpressedTexture;
            _action = action;
            _coreUIMesh.Texture = _unpressed;
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
            if (_prevPressed && e.PointerUp && _action != null && focused) _action.Invoke(_id); 
            if (Pressed) _coreUIMesh.Texture = _pressed;
            else _coreUIMesh.Texture = _unpressed;
        }
    }
}
