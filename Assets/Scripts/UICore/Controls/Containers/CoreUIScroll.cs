using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.Controls.Containers
{
    public class CoreUIScroll : CoreUIContainer
    {
        private float _originY;
        private float _originX;
        private float _viewHeight;
        private float _viewWidth;
        private float _scrollVerticalValue;
        private float _scrollHorizontalValue;

        public float OriginY
        {
            get { return _originY; }
            set { _originY = value; }
        }

        public float OriginX
        {
            get { return _originX; }
            set { _originX = value; }
        }

        public float ScrollVerticalValue
        {
            get { return _scrollVerticalValue; }
            set
            {
                _scrollVerticalValue = Mathf.Clamp01(value);
                UpdateScrollPosition();
                UpdateChildrenLimits();
            }
        }

        public float ScrollHorizontalValue
        {
            get { return _scrollHorizontalValue; }
            set
            {
                _scrollHorizontalValue = Mathf.Clamp01(value);
                UpdateScrollPosition();
                UpdateChildrenLimits();
            }
        }
        
        public CoreUIScroll(float viewWidth, float viewHeight, BaseCoreUIMesh mesh) : base(mesh)
        {
            _viewHeight = viewHeight;
            _viewWidth = viewWidth;
        }


        protected override bool Contains(Vector2 pos)
        {
            var r = new Rect(_originX, _originY - _viewHeight, _viewWidth, _viewHeight);
            return r.Contains(pos);
        }

        private void UpdateScrollPosition()
        {
            Y = Mathf.Lerp(_originY, _originY + (Height - _viewHeight), _scrollVerticalValue);
            X = Mathf.Lerp(_originX, _originX - (Width - _viewWidth), _scrollHorizontalValue);
        }

        protected override void ApplyLimits(CoreUIElement element)
        {
            UpdateChildrenLimits();
            base.ApplyLimits(element);
        }

        private void UpdateChildrenLimits()
        {
            VerticalTopLimit = _originY;
            VerticalBottomLimit = _originY - _viewHeight;
            HorizontalLeftLimit = _originX;
            HorizontalRightLimit = _originX + _viewWidth;
            //for (var index = 0; index < _elements.Count; index++)
            //{
            //    _elements[index].VerticalTopLimit = _originY;
            //    _elements[index].VerticalBottomLimit = _originY - _viewHeight;
            //    _elements[index].HorizontalLeftLimit = _originX;
            //    _elements[index].HorizontalRightLimit = _originX + _viewWidth;
            //}
        }
    }
}
