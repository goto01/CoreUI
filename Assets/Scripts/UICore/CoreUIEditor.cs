using System;
using Assets.Scripts.Singleton;
using Assets.Scripts.UICore.Controls;
using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.Presentation;
using Assets.Scripts.UICore.Presentation.Presentations;
using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.UICoreMeshes.Factory;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore
{
    public class CoreUIEditor : SingletonMonoBahaviour<CoreUIEditor>
    {
        private const string DefaultWindowStyle = "Default Window Style";
        private const string DefaultImageStyle = "Default Image Style";
        private const string DefaultFlexibleImageStyle = "Default Flexible Image Style";
        private const string DefaultSliderStyle= "Default Slider Style";
        private const string DefaultButtonStyle = "Default Button Style";
        private const string DefaultScrollStyle = "Default Scroll Style";

        private MeshesFactory _factory;
        [SerializeField] private StylesRepository _repository;
        
        public override void AwakeSingleton()
        {
            _factory = new MeshesFactory(_repository);
            DontDestroyOnLoad(gameObject);
        }

        public CoreUIWindow Window(Rect rect, string styleName = DefaultWindowStyle)
        {
            var mesh = _factory.CreateWindow(rect, styleName);
            var element = new CoreUIWindow(mesh);
            CoreUIPresentation.Instance.CreateContainerPresentation(element);
            return element;
        }

        public CoreUIWindow Window(Rect rect, CoreUIContainer container, string styleName = DefaultWindowStyle)
        {
            var mesh = _factory.CreateWindow(rect, styleName);
            var element = new CoreUIWindow(mesh);
            container.AddElement(element);
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            return element;
        }

        public CoreUIImage Image(Rect rect, CoreUIContainer container, Texture2D texture, string styleName = DefaultImageStyle)
        {
            var mesh = _factory.CreateImage(rect, texture, styleName);
            mesh.Texture = texture;
            var element = new CoreUIImage(mesh);
            container.AddElement(element);
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            return element;
        }

        public CoreUIFlexibleImage FlexibleImage(Rect rect, CoreUIContainer container, CoreUIOrientation orientation, string styleName = DefaultFlexibleImageStyle)
        {
            var mesh = _factory.CreateFlexibleImage(rect, orientation, styleName);
            var element = new CoreUIFlexibleImage(mesh);
            container.AddElement(element);
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            return element;
        }

        public CoreUIFlexibleImage FlexibleImage(Rect rect, CoreUIContainer container, string styleName = DefaultFlexibleImageStyle)
        {
            return FlexibleImage(rect, container, CoreUIOrientation.Horizontal, styleName);
        }

        public CoreUISlider Slider(Rect rect, CoreUIContainer container, CoreUIOrientation orientation, string styleName = DefaultSliderStyle)
        {
            var mesh = _factory.CreateSlider(rect, orientation, styleName);
            var point = Image(rect, container, null);
            point.Texture = mesh.Point;
            var element = new CoreUISlider(mesh, point, orientation);
            container.AddElementBefore(element, point);
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            return element;
        }

        public CoreUISlider Slider(Rect rect, CoreUIContainer container, string styleName = DefaultSliderStyle)
        {
            return Slider(rect, container, CoreUIOrientation.Horizontal, styleName);
        }

        public CoreUIButton Button(Rect rect, CoreUIContainer container, string styleName = DefaultButtonStyle)
        {
            return Button(rect, container, null, styleName);
        }

        public CoreUIButton Button(Rect rect, CoreUIContainer container, Action action, string styleName = DefaultButtonStyle)
        {
            var mesh = _factory.CreateButton(rect, styleName);
            var element = new CoreUIButton(mesh, action);
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            container.AddElement(element);
            return element;
        }

        public CoreUIScroll Scroll(Rect rect, float viewWidth, float viewHeight, string styleName = DefaultScrollStyle)
        {
            var mesh = _factory.CreateScroll(rect, styleName);
            var element = new CoreUIScroll(viewWidth, viewHeight, mesh);    
            element.OriginY = element.Position.y;
            element.OriginX = element.Position.x;
            Debug.Log(element.Position.x);
            CoreUIPresentation.Instance.CreateContainerPresentation(element);
            return element;
        }

        public CoreUIScroll Scroll(Rect rect, float viewWidth, float viewHeight, CoreUIContainer container, string styleName = DefaultScrollStyle)
        {
            var mesh = _factory.CreateScroll(rect, styleName);
            var element = new CoreUIScroll(viewWidth, viewHeight, mesh);
            container.AddElement(element);
            element.OriginY = element.Position.y;
            element.OriginX = element.Position.x;
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            return element;
        }
    }
}
