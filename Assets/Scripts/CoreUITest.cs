using Assets.Scripts.UICore;
using Assets.Scripts.UICore.Controls.Containers;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CoreUITest: MonoBehaviour
    {
        private const float _pixelSize = 1/32f;
        private CoreUIContainer _window0;
        private CoreUIContainer _window1;

        protected virtual void Awake()
        {
            //CoreUIEditor.Instance.Button(new Rect(1, 1, _pixelSize*30, 0), ()=>Debug.Log("Up"));
            //CoreUIEditor.Instance.Button(new Rect(1, -1, _pixelSize*30, 0));
            //CoreUIEditor.Instance.Button(new Rect(-1, 1, _pixelSize*30, 0));
            //CoreUIEditor.Instance.Button(new Rect(-1, -1, _pixelSize*30, 0), ()=>Debug.Log("Down"));
            _window0 = CoreUIEditor.Instance.Window(new Rect(0, 0, _pixelSize*100, _pixelSize*100), "Item Window Style");
            CoreUIEditor.Instance.Slider(new Rect(_pixelSize*6f, -_pixelSize*6f, _pixelSize*70, 0), _window0, "RPG Slider Style");
            _window0.ContainerFocused = true;

            _window1 = CoreUIEditor.Instance.Window(new Rect(_pixelSize*10, -_pixelSize*10, _pixelSize*80, _pixelSize*50), "Item Window Style");
            CoreUIEditor.Instance.Button(new Rect(_pixelSize * 17, -_pixelSize * 17, _pixelSize * 35, 0), _window1);
            _window1.ContainerFocused = false;
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _window0.ContainerFocused = !_window0.ContainerFocused;
                _window1.ContainerFocused = !_window1.ContainerFocused;
            }
        }
    }
}
