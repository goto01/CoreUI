using System.Collections.Generic;
using Singleton;
using UICore.Controls;
using UICore.Controls.Containers;
using UICore.Presentation.Presentations;
using UICore.Settings;
using UnityEngine;

namespace UICore.Presentation
{
    class CoreUIPresentation : SingletonMonoBahaviour<CoreUIPresentation>
    {
        public const int CoreUIQueue = 3000;

        [SerializeField] private PresentationSettings _presentationSettings;
        [SerializeField] private CoreUIPresentationParent _coreUiPresentationParentPrefab;
        [SerializeField] private CoreUISimplePresentation _presentationPrefab;
        [SerializeField] private CoreUIContainerPresentation _containerPrefab;
        [SerializeField] private int _containerOrder = 0;
        [SerializeField] private int _containerOrderStep = 100;
        private CoreUIPresentationParent _coreUiPresentationParent;

        public Color InactiveTintColor{get { return _presentationSettings.InactiveTintColor; }}
        
        #if UNITY_EDITOR
        protected virtual void Reset()
        {
            CoreUIInitializator.SetCoreUIExecutionOrder();
        }
        #endif
        
        public override void AwakeSingleton()
        {
            Init();
        }

        private void InitIfRequired()
        {
            if (_coreUiPresentationParent == null) Init();
        }
        
        private void Init()
        {
            _coreUiPresentationParent = Instantiate(_coreUiPresentationParentPrefab);
            _coreUiPresentationParent.Init();
        }

        protected virtual void Update()
        {
            var e = GetEvent();
            UpdatePresentation(ref e);
        }

        protected virtual void LateUpdate()
        {
            _coreUiPresentationParent.Position = CoreUICameraHandler.Instance.CameraPosition;
        }

        public CoreUISimplePresentation CreateSimplePresentation(CoreUIElement element)
        {
            var presentation = CreateSimplePresentation();
            presentation.Init(element);
            return presentation;
        }

        public CoreUIContainerPresentation CreateContainerPresentation(CoreUIContainer element)
        {
            var presentation = CreateContainerPresentation();
            presentation.Init(element);
            element.Order = GetContainerOrder();
            element.VerticalTopLimit = CoreUICameraHandler.Instance.VerticalTopLimit;
            element.VerticalBottomLimit = CoreUICameraHandler.Instance.VerticalBottomLimit;
            element.HorizontalLeftLimit = CoreUICameraHandler.Instance.HorizontalLeftLimit;
            element.HorizontalRightLimit = CoreUICameraHandler.Instance.HorizontalRightLimit;
            return presentation;
        }

        private CoreUISimplePresentation CreateSimplePresentation()
        {
            InitIfRequired();
            var item = Instantiate(_presentationPrefab);
            _coreUiPresentationParent.Presentations.Add(item);
            item.transform.parent = _coreUiPresentationParent.Transform;
            return item;
        }

        private CoreUIContainerPresentation CreateContainerPresentation()
        {
            InitIfRequired();
            var container = Instantiate(_containerPrefab);
            _coreUiPresentationParent.Presentations.Add(container);
            container.transform.parent = _coreUiPresentationParent.Transform;
            return container;
        }

        private CoreUIEvent GetEvent()
        {
            return new CoreUIEvent(CoreUICameraHandler.Instance.PointerPosition, Input.mouseScrollDelta.y, Input.GetMouseButtonDown(0), Input.GetMouseButtonUp(0),
                Input.inputString, Input.GetKeyDown(KeyCode.LeftArrow), Input.GetKeyDown(KeyCode.RightArrow));
        }

        private void UpdatePresentation(ref CoreUIEvent e)
        {
            InitIfRequired();
            for (var index = 0; index < _coreUiPresentationParent.Presentations.Count; index++)
            {
                _coreUiPresentationParent.Presentations[index].UpdateSelf(ref e);
            }
        }

        private int GetContainerOrder()
        {
            return (_containerOrder += _containerOrderStep);
        }
    }
}
