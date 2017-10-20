using Assets.Scripts.UICore;
using Assets.Scripts.UICore.Controls.Containers;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CoreUITest: MonoBehaviour
    {
        private MeshFilter _meshFilter;
        private CoreUIWindow _window;
        private bool _space;

        protected virtual void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();

            var instance = CoreUIEditor.Instance;
            _window = instance.Window(new Rect(1, 1, 2, 2));
            _meshFilter.mesh = _window.Mesh;
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _space = !_space;
                if (_space)
                {
                    _window.X = 1;
                    _window.Y = 1;
                }
                else
                {
                    _window.X = -1;
                    _window.Y = -1;
                }
            }
        }
    }
}
