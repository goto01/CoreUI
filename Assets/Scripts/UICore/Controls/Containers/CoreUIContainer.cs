using System.Collections.Generic;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.Controls.Containers
{
    public class CoreUIContainer : CoreUIElement
    {
        protected List<CoreUIElement> _elements;
        
        public override float VerticalBottomLimit
        {
            get { return base.VerticalBottomLimit; }
            set
            {
                base.VerticalBottomLimit = value;
                ApplyChildrenLimits();
            }
        }

        public override float VerticalTopLimit
        {
            get { return base.VerticalTopLimit; }
            set
            {
                base.VerticalTopLimit = value;
                ApplyChildrenLimits();
            }
        }

        public override float HorizontalLeftLimit
        {
            get { return base.HorizontalLeftLimit; }
            set
            {
                base.HorizontalLeftLimit = value;
                ApplyChildrenLimits();
            }
        }

        public override float HorizontalRightLimit
        {
            get
            {
                return base.HorizontalRightLimit;
            }
            set
            {
                base.HorizontalRightLimit = value;
                ApplyChildrenLimits();
            }
        }

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                UpdateChildrenPosition(Position, value);
                base.Position = value;
            }
        }

        public override float X
        {
            get { return base.X; }
            set
            {
                var p = Position;
                p.x = value;
                UpdateChildrenPosition(Position, p);
                base.X = value;
            }
        }

        public override float Y
        {
            get { return base.Y; }
            set
            {
                var p = Position;
                p.y = value;
                UpdateChildrenPosition(Position, p);
                base.Y = value;
            }
        }

        public override bool Active
        {
            get { return base.Active; }
            set
            {
                if (base.Active == value) return;
                base.Active = value;
                for (var index = 0; index < _elements.Count; index++) _elements[index].Active = value;
            }
        }

        public override int Order
        {
            get { return base.Order; }
            set
            {
                base.Order = value;
                Reorder();
            }
        }

        public List<CoreUIElement> Elements { get { return _elements; } }

        public int Count { get { return _elements.Count; } }

        protected CoreUIContainer(BaseCoreUIMesh mesh) : base(mesh) 
        {
            _elements = new List<CoreUIElement>();
        }

        public void AddElement(CoreUIElement element)
        {
            _elements.Add(element);
            element.Order = Order + _elements.Count;
            element.Position += Position;
            ApplyLimits(element);
        }

        public void AddElementBefore(CoreUIElement element, CoreUIElement before)
        {
            for (var index = 0; index < _elements.Count; index++)
            {
                if (_elements[index] == before)
                {
                    _elements.Insert(index, element);
                    break;
                }
            }
            element.Position += Position;
            Reorder();
            ApplyLimits(element);
        }

        public override bool Update(CoreUIEvent e)
        {
            var focused = base.Update(e);
            if (!focused) e.DropPointerData();
            for (var index = 0; index < _elements.Count; index++)
            {
                var elementFocus = _elements[index].Update(e);
                focused = focused || elementFocus;
            }
            return focused;
        }

        private void Reorder()
        {
            for (var index = 0; index < _elements.Count; index++) _elements[index].Order = Order + 1 + index;
        }

        protected void UpdateChildrenPosition(Vector2 oldPosition, Vector2 newPosition)
        {
            for (var index = 0; index < _elements.Count; index++)
            {
                _elements[index].ResetParentPosition(oldPosition, newPosition);
            }
        }

        public override void ResetParentPosition(Vector2 oldPosition, Vector2 newPosition)
        {
            base.ResetParentPosition(oldPosition, newPosition);
            UpdateChildrenPosition(oldPosition, newPosition);
        }

        private void ApplyChildrenLimits()
        {
            for (var index = 0; index < _elements.Count; index++)
            {
                _elements[index].VerticalBottomLimit = VerticalBottomLimit;
                _elements[index].VerticalTopLimit = VerticalTopLimit;
                _elements[index].HorizontalLeftLimit = HorizontalLeftLimit;
                _elements[index].HorizontalRightLimit = HorizontalRightLimit;
            }
        }

        protected virtual void ApplyLimits(CoreUIElement element)
        {
            element.VerticalTopLimit = VerticalTopLimit;
            element.VerticalBottomLimit = VerticalBottomLimit;
            element.HorizontalLeftLimit = HorizontalLeftLimit;
            element.HorizontalRightLimit = HorizontalRightLimit;
        }

        protected virtual bool ElementEnable(CoreUIEvent e)
        {
            return _coreUIMesh.Rect.Contains(e.PointerPosition);
        }
    }
}
