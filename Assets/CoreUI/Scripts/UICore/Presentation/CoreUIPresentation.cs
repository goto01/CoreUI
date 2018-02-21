using System.Collections.Generic;
using Singleton;
using UICore.Controls;
using UICore.Controls.Containers;
using UICore.Presentation.Presentations;
using UnityEngine;

namespace UICore.Presentation
{
    class CoreUIPresentation : SingletonMonoBahaviour<CoreUIPresentation>
    {
        public const int CoreUIQueue = 3000;

        [SerializeField] private Transform _presentationParentPrefab;
        [SerializeField] private CoreUISimplePresentation _presentationPrefab;
        [SerializeField] private CoreUIContainerPresentation _containerPrefab;
        [SerializeField] private List<CoreUISimplePresentation> _presentations;
        [SerializeField] private int _containerOrder = 0;
        [SerializeField] private int _containerOrderStep = 100;
        private CoreUIContainer _defaultContainer;
        private Transform _presentationParent;

        #if UNITY_EDITOR
        protected virtual void Reset()
        {
            CoreUIInitializator.SetCoreUIExecutionOrder();
        }
        #endif
        
        public override void AwakeSingleton()
        {
            _defaultContainer = CoreUIEditor.Instance.Window(new Rect(0, 0, 0, 0), "Empty Window Style");
            _presentationParent = Instantiate(_presentationParentPrefab);
        }

        protected virtual void Update()
        {
            var e = GetEvent();
            UpdatePresentation(e);
        }

        protected virtual void LateUpdate()
        {
            _presentationParent.position = CoreUICameraHandler.Instance.CameraPosition;
        }

        public CoreUISimplePresentation CreateSimplePresentation(CoreUIElement element)
        {
            var presentation = CreateSimplePresentation();
            presentation.Init(element);
            element.Active = true;
            return presentation;
        }

        public CoreUIContainerPresentation CreateContainerPresentation(CoreUIContainer element)
        {
            var presentation = CreateContainerPresentation();
            presentation.Init(element);
            element.Order = GetContainerOrder();
            element.Active = true;
            element.VerticalTopLimit = CoreUICameraHandler.Instance.VerticalTopLimit;
            element.VerticalBottomLimit = CoreUICameraHandler.Instance.VerticalBottomLimit;
            element.HorizontalLeftLimit = CoreUICameraHandler.Instance.HorizontalLeftLimit;
            element.HorizontalRightLimit = CoreUICameraHandler.Instance.HorizontalRightLimit;
            return presentation;
        }

        private CoreUISimplePresentation CreateSimplePresentation()
        {
            var item = Instantiate(_presentationPrefab);
            _presentations.Add(item);
            item.transform.parent = _presentationParent;
            return item;
        }

        private CoreUIContainerPresentation CreateContainerPresentation()
        {
            var container = Instantiate(_containerPrefab);
            _presentations.Add(container);
            container.transform.parent = _presentationParent;
            return container;
        }

        private CoreUIEvent GetEvent()
        {
            return new CoreUIEvent()
            {
                PointerPosition = CoreUICameraHandler.Instance.PointerPosition,
                ScrollDir = Input.mouseScrollDelta.y,
                PointerDown = Input.GetMouseButtonDown(0),
                PointerUp = Input.GetMouseButtonUp(0),
            };
        }

        private void UpdatePresentation(CoreUIEvent e)
        {
            for (var index = 0; index < _presentations.Count; index++) _presentations[index].UpdateSelf(e);
        }

        private int GetContainerOrder()
        {
            return (_containerOrder += _containerOrderStep);
        }
    }
}
