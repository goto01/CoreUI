using System;
using Singleton;
using UICore.Controls;
using UICore.Controls.Containers;
using UICore.Controls.Text;
using UICore.Presentation;
using UICore.Settings;
using UICore.StylesSystem.Repository;
using UICore.UICoreMeshes.Factory;
using UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace UICore
{
    public class CoreUIEditor : SingletonMonoBahaviour<CoreUIEditor>
    {
        public const string Version = "0.1.1";
        private const string DefaultWindowStyle = "Default Window Style";
        private const string DefaultImageStyle = "Default Image Style";
        private const string DefaultFlexibleImageStyle = "Default Flexible Image Style";
        private const string DefaultSliderStyle= "Default Slider Style";
        private const string DefaultButtonStyle = "Default Button Style";
        private const string DefaultScrollStyle = "Default Scroll Style";
        private const string DefaultFontName = "Default Font";
        private const string DefaultToggleStyle = "Default Toggle Style";

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
            element.Active = true;
            element.Enabled = true;
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

        public CoreUIButton Button(Rect rect, CoreUIContainer container, Action<int> action, string styleName = DefaultButtonStyle)
        {
            var mesh = _factory.CreateButton(rect, styleName);
            var element = new CoreUIButton(mesh, action);
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            container.AddElement(element);
            return element;
        }

        public CoreUIScroll Scroll(Rect rect, float viewWidth, float viewHeight, string styleName = DefaultScrollStyle)
        {
            return Scroll(rect, viewWidth, viewHeight, default(CoreUISlider), default(CoreUISlider), styleName);
        }

        public CoreUIScroll Scroll(Rect rect, float viewWidth, float viewHeight, CoreUISlider horizontalSlider, CoreUISlider verticalSlider, string styleName = DefaultScrollStyle)
        {
            var mesh = _factory.CreateScroll(rect, styleName);
            var element = new CoreUIScroll(viewWidth, viewHeight, mesh, horizontalSlider, verticalSlider);    
            element.OriginY = element.Position.y;
            element.OriginX = element.Position.x;
            CoreUIPresentation.Instance.CreateContainerPresentation(element);
            return element;
        }

        public CoreUIScroll Scroll(Rect rect, float viewWidth, float viewHeight, CoreUIContainer container, string styleName = DefaultScrollStyle)
        {
            return Scroll(rect, viewWidth, viewHeight, null, null, container, styleName);
        }

        public CoreUIScroll Scroll(Rect rect, float viewWidth, float viewHeight, CoreUISlider horizontalSlider, CoreUISlider verticalSlider, CoreUIContainer container, string styleName = DefaultScrollStyle)
        {
            var mesh = _factory.CreateScroll(rect, styleName);
            var element = new CoreUIScroll(viewWidth, viewHeight, mesh, horizontalSlider, verticalSlider);
            container.AddElement(element);
            element.OriginY = element.Position.y;
            element.OriginX = element.Position.x;
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            return element;
        }
        
        public CoreUILabel Label(Rect rect, string text, CoreUIContainer container, string fontName = DefaultFontName)
        {
            var mesh = _factory.CreateLabel(rect, text, fontName);
            var element = new CoreUILabel(mesh);
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            container.AddElement(element);
            return element;
        }
        
        public CoreUILabel Label(Rect rect, string text, CoreUIContainer container, int sinPixelsOffset, float sinOffsetSpeed, float sinMultiplier,
            float horizontalPixelsOffset, float verticalPixelsOffset, string fontName = DefaultFontName)
        {
            var mesh = _factory.CreateLabel(rect, text, fontName);
            var element = new CoreUILabel(mesh, sinPixelsOffset, sinOffsetSpeed, sinMultiplier, horizontalPixelsOffset, verticalPixelsOffset);
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            container.AddElement(element);
            return element;
        }

        public CoreUIToggle Toggle(Rect rect, bool pressed, CoreUIContainer container, Action<bool> action, string styleName = DefaultToggleStyle)
        {
            var mesh = _factory.CreateToggle(rect, styleName);
            var element = new CoreUIToggle(mesh, action, pressed);
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            container.AddElement(element);
            return element;
        }

        public CoreUITextField TextField(Rect rect, string text, Color cursorColor, CoreUIContainer container, string cursorStyle, string fontName = DefaultFontName)
        { 
            var backgroundImage = Image(rect, container, null);
            var cursorImage = FlexibleImage(rect, container, CoreUIOrientation.Vertical, cursorStyle);
            cursorImage.Color = cursorColor;
            var mesh = _factory.CreateLabel(rect, text, fontName);
            var element = new CoreUITextField(mesh, cursorImage, backgroundImage);
            element.Color = Color.black;
            CoreUIPresentation.Instance.CreateSimplePresentation(element);
            container.AddElementBefore(element, cursorImage);
            return element;
        }
    }
}
