using System.Collections.Generic;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;

namespace Assets.Scripts.UICore.Controls.Containers
{
    public abstract class CoreUIContainer : CoreUIElement
    {
        protected List<CoreUIElement> _elements;

        public List<CoreUIElement> Elements { get { return _elements; } }

        protected CoreUIContainer(BaseCoreUIMesh mesh) : base(mesh) 
        {
            _elements = new List<CoreUIElement>();
        }

        public void AddElement(CoreUIElement element)
        {
            _elements.Add(element);
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
    }
}
