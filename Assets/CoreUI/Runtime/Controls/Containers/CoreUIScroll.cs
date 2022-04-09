using UnityEngine;

namespace CoreUI
{
    public class CoreUIScroll : CoreUIContainer
    {
        private float _originY;
        private float _originX;
        private float _viewHeight;
        private float _viewWidth;
        private float _scrollVerticalValue;
        private float _scrollHorizontalValue;
        private CoreUISlider _horizontalSlider;
        private CoreUISlider _verticalSlider;
        private float _parentVerticalBottomLimit;
        private float _parentVerticalTopLimit;
        private float _parentHorizontalLeftLimit;
        private float _parentHorizontalRightLimit;

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
        
        public override float VerticalBottomLimit
        {
            get { return base.VerticalBottomLimit; }
            set { _parentVerticalBottomLimit = value; }
        }

        public override float VerticalTopLimit
        {
            get { return base.VerticalTopLimit; }
            set { _parentVerticalTopLimit = value; }
        }

        public override float HorizontalLeftLimit
        {
            get { return base.HorizontalLeftLimit; }
            set { _parentHorizontalLeftLimit = value; }
        }

        public override float HorizontalRightLimit
        {
            get { return base.HorizontalRightLimit; }
            set { _parentHorizontalRightLimit = value; }
        }
        
        public CoreUIScroll(float viewWidth, float viewHeight, BaseCoreUIMesh mesh, CoreUISlider horizontalSlider, CoreUISlider verticalSlider) : base(mesh)
        {
            _viewHeight = viewHeight;
            _viewWidth = viewWidth;
            _horizontalSlider = horizontalSlider;
            _verticalSlider = verticalSlider;
        }

        public override bool Update(ref CoreUIEvent e)
        {
            var focus = base.Update(ref e);
            if (_verticalSlider != null) ScrollVerticalValue = 1 - _verticalSlider.Value;
            if (_horizontalSlider != null) ScrollHorizontalValue = _horizontalSlider.Value;
            return focus;
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
            _verticalTopLimit = Mathf.Min(_originY, _parentVerticalTopLimit);
            _verticalBottomLimit = Mathf.Max(_originY - _viewHeight, _parentVerticalBottomLimit);
            _horizontalLeftLimit = Mathf.Max(_originX, _parentHorizontalLeftLimit);
            _horizontalRightLimit = Mathf.Min(_originX + _viewWidth, _parentHorizontalRightLimit);
            ApplyChildrenLimits();
        }

        public override void ResetParentPosition(Vector2 oldPosition, Vector2 newPosition)
        {
            OriginX = OriginX - oldPosition.x + newPosition.x;
            OriginY = OriginY - oldPosition.y + newPosition.y;
            base.ResetParentPosition(oldPosition, newPosition);
        }
    }
}
