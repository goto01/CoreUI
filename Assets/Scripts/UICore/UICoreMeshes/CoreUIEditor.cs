using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.UICoreMeshes.Meshes.Factory;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes
{
    public class CoreUIEditor : MonoBehaviour
    {
        private const string DefaultWindow = "Default Window Style";

        private MeshesFactory _factory;
        [SerializeField] private StylesRepository _repository;
        private MeshRenderer _meshRenderer;
        private MeshFilter _meshFilter;

        protected virtual void Awake()
        {
            _factory = new MeshesFactory(_repository);
            var window = Window(new Rect(0, 0, 1, 1), "Wood Window Style");
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = window.Mesh;
        }

        public CoreUIWindow Window(Rect rect, string styleName = DefaultWindow)
        {
            var mesh = _factory.CreateWindow(rect, styleName);
            return new CoreUIWindow(mesh);
        }
    }
}
