using System;
using Assets.Scripts.Singleton;
using Assets.Scripts.UICore.Controls;
using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.Presentation;
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

        private MeshesFactory _factory;
        [SerializeField] private StylesRepository _repository;
        [SerializeField] private CoreUIPresentationItem _presentationPrefab;
        
        public override void AwakeSingleton()
        {
            _factory = new MeshesFactory(_repository);
            DontDestroyOnLoad(gameObject);
        }

        public CoreUIWindow Window(Rect rect, string styleName = DefaultWindowStyle)
        {
            var mesh = _factory.CreateWindow(rect, styleName);
            var element = new CoreUIWindow(mesh);
            CreatePresentationItem(element);
            return element;
        }

        public CoreUIImage Image(Rect rect, Texture2D texture, string styleName = DefaultImageStyle)
        {
            var mesh = _factory.CreateImage(rect, texture, styleName);
            mesh.Texture = texture;
            var element = new CoreUIImage(mesh);
            CreatePresentationItem(element);
            return element;
        }

        public CoreUIFlexibleImage FlexibleImage(Rect rect, CoreUIOrientation orientation, string styleName = DefaultFlexibleImageStyle)
        {
            var mesh = _factory.CreateFlexibleImage(rect, orientation, styleName);
            var element = new CoreUIFlexibleImage(mesh);
            CreatePresentationItem(element);
            return element;
        }

        public CoreUIFlexibleImage FlexibleImage(Rect rect, string styleName = DefaultFlexibleImageStyle)
        {
            return FlexibleImage(rect, CoreUIOrientation.Horizontal, styleName);
        }

        public CoreUISlider Slider(Rect rect, CoreUIOrientation orientation, string styleName = DefaultSliderStyle)
        {
            var mesh = _factory.CreateSlider(rect, orientation, styleName);
            var point = Image(rect, null);
            var element = new CoreUISlider(mesh, point, orientation);
            CreatePresentationItem(element);
            return element;
        }

        public CoreUISlider Slider(Rect rect, string styleName = DefaultSliderStyle)
        {
            return Slider(rect, CoreUIOrientation.Horizontal, styleName);
        }

        public CoreUIButton Button(Rect rect, string styleName = DefaultButtonStyle)
        {
            return Button(rect, null, styleName);
        }

        public CoreUIButton Button(Rect rect, Action action, string styleName = DefaultButtonStyle)
        {
            var mesh = _factory.CreateButton(rect, styleName);
            var element = new CoreUIButton(mesh, action);
            CreatePresentationItem(element);
            return element;
        }

        private void CreatePresentationItem(CoreUIElement element)
        {
            var presentationItem = Instantiate(_presentationPrefab);
            presentationItem.Init(element);
        }
    }
}
