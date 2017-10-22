using Assets.Scripts.UICore;
using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CoreUITest: MonoBehaviour
    {
        [SerializeField] private Texture2D _texture;
        private float _value;
        private float _valueDelta = .1f;
        private CoreUISlider _slider1;
        private CoreUISlider _slider2;

        protected virtual void Awake()
        {
            _slider1 = CoreUIEditor.Instance.Slider(new Rect(0, 0, 3, 0), "RPG Slider Style");
            _slider2 = CoreUIEditor.Instance.Slider(new Rect(0, 0, 3, 0), CoreUIOrientation.Vertical, "RPG Slider Style");
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.P)) _value += _valueDelta;
            if (Input.GetKeyDown(KeyCode.M)) _value -= _valueDelta;
            _slider1.Value = _value;
            _slider2.Value = _value;
        }
    }
}
