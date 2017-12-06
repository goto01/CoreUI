using UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace UICore.Controls
{
    public class CoreUISlider : CoreUIElement
    {
        private float _delta = .1f;
        private float _value = .5f;
        private float _borderWidth;
        private CoreUIOrientation _orientation;
        private CoreUIElement _point;

        public float Value
        {
            get { return _value; }
            set { _value = Mathf.Clamp01(value); }
        }

        private CoreUIElement Point
        {
            get { return _point; }
            set { _point = value; }
        }

        public CoreUISlider(SliderMesh mesh, CoreUIImage point, CoreUIOrientation orientation) : base(mesh)
        {
            _orientation = orientation;
            _borderWidth = mesh.BorderWidth;
            _point = point;
        }

        public override bool Update(CoreUIEvent e)
        {
            var focus = base.Update(e);
            if (focus) HandleEvent(e);
            HandleMouse(e);
            UpdatePointPosition();
            return focus;
        }

        private void UpdatePointPosition()
        {
            if (_orientation == CoreUIOrientation.Horizontal) UpdateHorizontalPointPosition();
            else UpdateVerticalPointPosition();
        }

        private void UpdateHorizontalPointPosition()
        {
            var x = Mathf.Lerp(X + _borderWidth, X + Width - _borderWidth, _value);
            var y = Y - Height / 2f;
            Point.CenterX = x;
            Point.CenterY = y;
        }

        private void UpdateVerticalPointPosition()
        {
            var x = X + Width/2;
            var y = Mathf.Lerp(Y - _borderWidth, Y - Height + _borderWidth, 1-_value);
            Point.CenterX = x;
            Point.CenterY = y;
        }

        private void HandleEvent(CoreUIEvent e)
        {
            Value = Value + e.ScrollDir*_delta;
        }

        private void HandleMouse(CoreUIEvent e)
        {
            if (!Point.Pressed) return;
            if (_orientation == CoreUIOrientation.Horizontal) Value = Mathf.InverseLerp(X + _borderWidth, X + Width - _borderWidth, e.PointerPosition.x);
            else Value = Mathf.InverseLerp(Y - Height + _borderWidth, Y - _borderWidth, e.PointerPosition.y);
        }
    }
}
