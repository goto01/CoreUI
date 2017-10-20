using Assets.Scripts.UICore.StylesSystem.Styles;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class WindowCoreUI : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private MeshFilter _meshFilter;
        [SerializeField]
        private Mesh _mesh;
        [SerializeField]
        private bool _orderable;
        [SerializeField]
        private WindowStyle _windowStyle;

        protected virtual void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshFilter = GetComponent<MeshFilter>();

            var window = new WindowMesh();
            window.BorderWidth = .03125f * 4;
            window.Init(_windowStyle, 1, 1);
            _meshFilter.mesh = window.Mesh;
        }

        protected virtual void Update()
        {
            if (_orderable && Input.GetKeyDown(KeyCode.UpArrow)) _meshRenderer.material.renderQueue++;
            if (_orderable && Input.GetKeyDown(KeyCode.DownArrow)) _meshRenderer.material.renderQueue--;
        }
    }
}