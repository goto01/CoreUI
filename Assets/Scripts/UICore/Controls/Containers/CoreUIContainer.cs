using System.Collections.Generic;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.Controls.Containers
{
    public abstract class CoreUIContainer : CoreUIElement
    {
        private List<CoreUIElement> _elements;

        public List<CoreUIElement> Elements { get { return _elements; } }

        protected CoreUIContainer(BaseCoreUIMesh mesh) : base(mesh) 
        {
            _elements = new List<CoreUIElement>();
        }

        public void AddElement(CoreUIElement element)
        {
            _elements.Add(element);
        }

        public override void Update()
        {
            for (var index = 0; index < _elements.Count; index++) _elements[index].Update();
        }
    }
}
