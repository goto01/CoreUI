using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.Controls.Containers
{
    public class CoreUISlider : CoreUIContainer
    {
        private float _value = .5f;
        private float _borderWidth;
        private CoreUIOrientation _orientation;

        public float Value
        {
            get { return _value; }
            set { _value = Mathf.Clamp01(value); }
        }

        private CoreUIElement Point { get { return _elements[0]; } }

        public CoreUISlider(SliderMesh mesh, CoreUIImage point, CoreUIOrientation orientation) : base(mesh)
        {
            _orientation = orientation;
            _borderWidth = mesh.BorderWidth;
            point.Texture = mesh.Point;
            AddElement(point);
        }

        public override void Update()
        {
            UpdatePointPosition();
            base.Update();
        }

        private void UpdatePointPosition()
        {
            if (_orientation == CoreUIOrientation.Horizontal) UpdateHorizontalPointPosition();
            else UpdateVerticalPointPosition();
        }

        private void UpdateHorizontalPointPosition()
        {
            var x = X + Mathf.Lerp(X + _borderWidth, X + Width - _borderWidth, _value);
            var y = Y - Height / 2f;
            Point.CenterX = x;
            Point.CenterY = y;
        }

        private void UpdateVerticalPointPosition()
        {
            var x = X + Width/2;
            var y = Mathf.Lerp(Y - _borderWidth, Y - Height + _borderWidth, _value);
            Point.CenterX = x;
            Point.CenterY = y;
        }
    }
}
