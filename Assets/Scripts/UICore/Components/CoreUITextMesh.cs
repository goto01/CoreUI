using Assets.Scripts.UICore.StylesSystem.Styles.Font;
using Assets.Scripts.UICore.UICoreMeshes.Generators;
using UnityEngine;

namespace UICore.Components
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    [ExecuteInEditMode]
    public class CoreUITextMesh : MonoBehaviour
    {
        [SerializeField] [TextArea(10, 20)] private string _text;
        [SerializeField] private Color _color;
        [SerializeField] private CoreUIFont _font;
        [SerializeField] private CoreUITextGenerator _generator;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        protected virtual void OnEnable()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            InitSelf();
        }

        protected virtual void Update()
        {
            if (_font == null) return;
            UpdateText();
            UpdateColors();
        }

        private void UpdateText()
        {
            if (_generator.Text.Equals(_text)) return;
            InitSelf();
            _generator.GenerateMeshData(_text, _color);
            ApplyMesh();
        }

        private void InitSelf()
        {
            if (!_generator.Inited) _generator.Init(_font);
        }

        private void UpdateColors()
        {
            if (_color.Equals(_generator.Color)) return;
            _generator.UpdateColors(_color);
            _meshFilter.sharedMesh.colors = _generator.Colors;
        }

        private void ApplyMesh()
        {
            if (_meshFilter.sharedMesh == null) _meshFilter.sharedMesh = new Mesh();
            _meshFilter.sharedMesh.Clear();
            _meshFilter.sharedMesh.vertices = _generator.Vertices;
            _meshFilter.sharedMesh.uv = _generator.UV;
            _meshFilter.sharedMesh.triangles = _generator.Triangles;
            _meshFilter.sharedMesh.colors = _generator.Colors;
        }
    }
}
