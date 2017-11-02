using System.Collections.Generic;
using System.ComponentModel.Design;
using Assets.Scripts.Singleton;
using Assets.Scripts.UICore.Controls;
using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.Presentation.Presentations;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

namespace Assets.Scripts.UICore.Presentation
{
    class CoreUIPresentation : SingletonMonoBahaviour<CoreUIPresentation>
    {
        public const int CoreUIQueue = 3000;

        [SerializeField] private CoreUISimplePresentation _presentationPrefab;
        [SerializeField] private CoreUIContainerPresentation _containerPrefab;
        [SerializeField] private List<CoreUISimplePresentation> _presentations;
        [SerializeField] private int _containerOrder = 0;
        [SerializeField] private int _containerOrderStep = 100;
        private CoreUIContainer _defaultContainer;
        
        public override void AwakeSingleton()
        {
            _defaultContainer = CoreUIEditor.Instance.Window(new Rect(0, 0, 0, 0), "Empty Window Style");
        }

        protected virtual void Update()
        {
            var e = GetEvent();
            UpdatePresentation(e);
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
            element.VerticalTopLimit = UICoreCameraHandler.Instance.VerticalTopLimit;
            element.VerticalBottomLimit = UICoreCameraHandler.Instance.VerticalBottomLimit;
            element.HorizontalLeftLimit = UICoreCameraHandler.Instance.HorizontalLeftLimit;
            element.HorizontalRightLimit = UICoreCameraHandler.Instance.HorizontalRightLimit;
            return presentation;
        }

        private CoreUISimplePresentation CreateSimplePresentation()
        {
            var item = Instantiate(_presentationPrefab);
            _presentations.Add(item);
            return item;
        }

        private CoreUIContainerPresentation CreateContainerPresentation()
        {
            var container = Instantiate(_containerPrefab);
            _presentations.Add(container);
            return container;
        }

        private CoreUIEvent GetEvent()
        {
            return new CoreUIEvent()
            {
                PointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition),
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
