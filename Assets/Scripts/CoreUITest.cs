using Assets.Scripts.UICore;
using Assets.Scripts.UICore.Controls;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CoreUITest: MonoBehaviour
    {
        [SerializeField]private float _health = 1;
        private float _healthDelta = .1f;
        private CoreUIFlexibleImage _healthBar;
        private CoreUIFlexibleImage _healthBar1;

        protected virtual void Awake()
        {
            _healthBar = CoreUIEditor.Instance.FlexibleImage(new Rect(0, 0, 3, 0));
            _healthBar1 = CoreUIEditor.Instance.FlexibleImage(new Rect(0, 0, 3, 0), FlexiblaImageOrientation.Vertical);
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.P)) _health += _healthDelta;
            if (Input.GetKeyDown(KeyCode.M)) _health -= _healthDelta;
            _healthBar.Width = 3 * _health;
            _healthBar1.Width = 3 * _health;
        }
    }
}
