using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class WindowCoreUI : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private MeshFilter _meshFilter;
        [SerializeField] private Mesh _mesh;

        protected virtual void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshFilter = GetComponent<MeshFilter>();

            var window = new WindowMesh();
            window.BorderWidth = .1f;
            window.Init();
            _meshFilter.mesh = window.Mesh;
        }
    }
}