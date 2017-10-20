using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.UICoreMeshes.Meshes.Factory;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes
{
    public class CoreUIEditor : MonoBehaviour
    {
        private MeshesFactory _factory;
        [SerializeField] private StylesRepository _repository;
        private MeshRenderer _meshRenderer;
        private MeshFilter _meshFilter;

        protected virtual void Awake()
        {
            _factory = new MeshesFactory(_repository);
            var window = Window(1, 1);
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = window.Mesh;
        }

        public CoreUIWindow Window(float width, float height)
        {
            var mesh = _factory.CreateWindow(width, height);
            return new CoreUIWindow(mesh);
        }
    }
}
