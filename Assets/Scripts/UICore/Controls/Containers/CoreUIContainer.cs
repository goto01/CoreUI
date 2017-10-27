using System.Collections.Generic;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.Controls.Containers
{
    public class CoreUIContainer : CoreUIElement
    {
        protected List<CoreUIElement> _elements;
        private bool _containerFocused;

        public bool ContainerFocused
        {
            get { return _containerFocused; }
            set { _containerFocused = value; }
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
            Reorder();
        }

        public override bool Update(CoreUIEvent e)
        {
            var focused = false;
            for (var index = 0; index < _elements.Count; index++)
            {
                var elementFocus = _elements[index].Update(e);
                if (!focused && elementFocus) focused = true;
            }
            return focused || base.Update(e);
        }

        private void Reorder()
        {
            for (var index = 0; index < _elements.Count; index++) _elements[index].Order = Order + 1 + index;
        }
    }
}
