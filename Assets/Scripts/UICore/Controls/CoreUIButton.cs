using System;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.Controls
{
    public class CoreUIButton : CoreUIElement
    {
        private Texture2D _pressed;
        private Texture2D _unpressed;
        private Action _action;

        public CoreUIButton(BaseCoreUIMesh mesh) : this(mesh, null)
        {
        }

        public CoreUIButton(BaseCoreUIMesh mesh, Action action) : base(mesh)
        {
            _pressed = mesh.Texture;
            _unpressed = ((ButtonMesh)mesh).UnpressedTexture;
            _action = action;
            _coreUIMesh.Texture = _unpressed;
        }

        public override bool Update(CoreUIEvent e)
        {
            var focus = base.Update(e);
            HandlePressing(e);
            return focus;
        }

        private void HandlePressing(CoreUIEvent e)
        {
            if (Pressed && e.PointerDown && _action != null) _action.Invoke(); 
            if (Pressed) _coreUIMesh.Texture = _pressed;
            else _coreUIMesh.Texture = _unpressed;
        }
    }
}
