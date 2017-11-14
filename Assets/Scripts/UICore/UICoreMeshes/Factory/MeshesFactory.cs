using System.Collections.Generic;
using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.StylesSystem.Styles;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Factory
{
    public class MeshesFactory
    {
        private StylesRepository _repository;

        public MeshesFactory(StylesRepository repository)
        {
            _repository = repository;
            _repository.Init();
        }

        public WindowMesh CreateWindow(Rect rect, string styleName)
        {
            var window = new WindowMesh();
            window.Init(GetStyle(styleName), rect);
            return window;
        }

        public RectangleMesh CreateImage(Rect rect, Texture2D texture, string styleName)
        {
            var image = new RectangleMesh();
            var style = GetStyle(styleName);
            image.Init(style, rect);
            image.Texture = texture;
            return image;
        }

        public FlexibleImageMesh CreateFlexibleImage(Rect rect, CoreUIOrientation orientation, string styleName)
        {
            var flexibleImage = new FlexibleImageMesh(orientation);
            flexibleImage.Init(GetStyle(styleName), rect);
            return flexibleImage;
        }

        public SliderMesh CreateSlider(Rect rect, CoreUIOrientation orientation, string styleName)
        {
            var slider = new SliderMesh(orientation);
            slider.Init(GetStyle(styleName), rect);
            return slider;
        }

        public ButtonMesh CreateButton(Rect rect, string styleName)
        {
            var button = new ButtonMesh();
            button.Init(GetStyle(styleName), rect);
            return button;
        }

        public ScrollMesh CreateScroll(Rect rect, string styleName)
        {
            var scroll = new ScrollMesh();
            scroll.Init(GetStyle(styleName), rect);
            return scroll;
        }

        public Meshes.Text.TextMesh CreateLabel(Rect rect, string text, string fontName)
        {
            var label = new Meshes.Text.TextMesh(text);
            label.Init(GetStyle(fontName), rect);
            return label;
        }

        private BaseStyle GetStyle(string styleName)
        {
            return _repository.GetStyle(styleName);
        }
    }
}
