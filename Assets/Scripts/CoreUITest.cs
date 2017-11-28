using Assets.Scripts.UICore;
using Assets.Scripts.UICore.Controls;
using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.StylesSystem.Styles.Font;
using Assets.Scripts.UICore.UICoreMeshes.Generators;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CoreUITest: MonoBehaviour
    {
        private const float _pixelSize = 1/32f;
        private CoreUIContainer _window;
        private CoreUISlider _slider;
        private CoreUISlider _sliderHorizontal;
        private CoreUIScroll _scroll;
        [SerializeField] private CoreUIFont _font;

        protected virtual void Awake()
        {
            _window = CoreUIEditor.Instance.Window(new Rect(-_pixelSize*100, _pixelSize*50, _pixelSize*200, _pixelSize*200), "Item Window Style");
            _slider = CoreUIEditor.Instance.Slider(new Rect(_pixelSize*6, -_pixelSize*6, _pixelSize*188, 0), _window, CoreUIOrientation.Vertical, "RPG Slider Style");
            _scroll = CoreUIEditor.Instance.Scroll(new Rect(_pixelSize * 20, -_pixelSize * 6, _pixelSize * 300, _pixelSize * 300), _pixelSize * 150, _pixelSize * 150, _window);
            _sliderHorizontal = CoreUIEditor.Instance.Slider( new Rect(_pixelSize*20, -_pixelSize*180, _pixelSize*180, 0), _window, CoreUIOrientation.Horizontal, "RPG Slider Style");
            var w = CoreUIEditor.Instance.Window(new Rect(0, 0, _pixelSize * 300, _pixelSize * 300), _scroll, "Item Window Style");
            CoreUIEditor.Instance.Button(new Rect(0, 0, _pixelSize*300, 0), _scroll, () => { Debug.Log("BUTTON1"); });
            CoreUIEditor.Instance.Label(new Rect(0, 0, 0, 0), "The quick brown fox jumps over the lazy dog", _window, "Wave Font").Color = Color.blue;
        }

        protected virtual void Update()
        {
            //_scroll.ScrollVerticalValue = _slider.Value;
            //_scroll.ScrollHorizontalValue = _sliderHorizontal.Value;
            if (Input.GetKeyDown(KeyCode.Space)) _window.Active = !_window.Active;
        }
    }
}
