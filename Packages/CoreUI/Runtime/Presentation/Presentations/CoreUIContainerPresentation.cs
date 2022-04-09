using UnityEngine;

namespace CoreUI
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

        public override void UpdateSelf(ref CoreUIEvent e)
        {
            base.UpdateSelf(ref e);
            _element.Update(ref e);
        }
    }
}
