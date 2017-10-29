using Assets.Scripts.UICore;
using Assets.Scripts.UICore.Controls;
using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CoreUITest: MonoBehaviour
    {
        private const float _pixelSize = 1/32f;
        private CoreUIContainer _window0;
        private CoreUISlider _slider;
        [SerializeField] private Texture2D _texture;

        protected virtual void Awake()
        {
            //CoreUIEditor.Instance.Button(new Rect(1, 1, _pixelSize*30, 0), ()=>Debug.Log("Up"));
            //CoreUIEditor.Instance.Button(new Rect(1, -1, _pixelSize*30, 0));
            //CoreUIEditor.Instance.Button(new Rect(-1, 1, _pixelSize*30, 0));
            //CoreUIEditor.Instance.Button(new Rect(-1, -1, _pixelSize*30, 0), ()=>Debug.Log("Down"));
            _window0 = CoreUIEditor.Instance.Window(new Rect(1, 1, _pixelSize*100, _pixelSize*200), "Item Window Style");
            //CoreUIEditor.Instance.Slider(new Rect(0, 0, _pixelSize*50, 0), _window0, "RPG Slider Style");
            CoreUIEditor.Instance.Button(new Rect(0, -_pixelSize*10, _pixelSize*50, 0), _window0, () =>
            {
                _window0.Y -= .1f;
            });
            //CoreUIEditor.Instance.FlexibleImage(new Rect(0, -_pixelSize*20, _pixelSize*50, 0), _window0);
            //CoreUIEditor.Instance.Image(new Rect(0, -_pixelSize*30, 0, 0), _window0, _texture);
            var w = CoreUIEditor.Instance.Window(new Rect(0, -_pixelSize*60, _pixelSize*90, _pixelSize*100), _window0, "Item Window Style");
            CoreUIEditor.Instance.Slider(new Rect(0, 0, _pixelSize * 50, 0), w, "RPG Slider Style");

            var nw = CoreUIEditor.Instance.Window(new Rect(-_pixelSize*100, _pixelSize*50, _pixelSize*20, _pixelSize*200), "Item Window Style");
            _slider = CoreUIEditor.Instance.Slider(new Rect(_pixelSize*6, -_pixelSize*6, _pixelSize*188, 0), nw, CoreUIOrientation.Vertical, "RPG Slider Style");
        }

        protected virtual void Update()
        {
            var value = _slider.Value;
            _window0.Y = Mathf.Lerp(_pixelSize*100, -_pixelSize*100, value);
        }
    }
}
