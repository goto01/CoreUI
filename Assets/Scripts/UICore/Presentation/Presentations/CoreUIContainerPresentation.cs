using System.Collections.Generic;
using Assets.Scripts.UICore.Controls;
using Assets.Scripts.UICore.Controls.Containers;
using UnityEngine;

namespace Assets.Scripts.UICore.Presentation.Presentations
{
    class CoreUIContainerPresentation : CoreUISimplePresentation
    {
        private CoreUIContainer Container { get { return (CoreUIContainer) _element; } }
        
        public override void Init(CoreUIElement element)
        {
            if (!(element is CoreUIContainer))
            {
                Debug.LogErrorFormat("Can't init container presentation because element isn't container (passed element is {0})", element.GetType().Name);
            }
            base.Init(element);
        }

        public override void UpdateSelf(CoreUIEvent e)
        {
            base.UpdateSelf(e);
            if (!Active) return;
            var container = Container;
            for (var index = 0; index < container.Count; index++) container.Elements[index].Update(e);
        }
    }
}
