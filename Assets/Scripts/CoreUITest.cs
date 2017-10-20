using Assets.Scripts.UICore;
using Assets.Scripts.UICore.Controls;
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

            _window = CoreUIEditor.Instance.Window(new Rect(0, 0, 1, 1), "Item Window Style");
            _meshFilter.mesh = _window.Mesh;
        }
    }
}
