using System.Collections;
using UICore.StylesSystem.Styles.Font;
using UICore.UICoreMeshes.Generators;
using UnityEditor;
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
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _lineWidth;
        [SerializeField] private bool _textWrapping;

        protected virtual void OnEnable()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            if (!string.IsNullOrEmpty(_text)) GenerateData();
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
            GenerateData();
        }

        private void GenerateData()
        {
            InitSelf();
            _generator.GenerateMeshData(_text, _color, _textWrapping, _lineWidth);
            ApplyMesh();
        }

        private void InitSelf()
        {
            if (!_generator.Inited)
            {
                _generator.Init(_font);
                ResetMesh();
            }
        }

        private void UpdateColors()
        {
            if (_color.Equals(_generator.Color)) return;
            _generator.UpdateColors(_color);
            _meshFilter.sharedMesh.colors = _generator.Colors;
        }

        private void ApplyMesh()
        {
            if (_meshFilter.sharedMesh == null) ResetMesh();
            _meshFilter.sharedMesh.Clear();
            _meshFilter.sharedMesh.vertices = _generator.Vertices;
            _meshFilter.sharedMesh.uv = _generator.UV;
            _meshFilter.sharedMesh.triangles = _generator.Triangles;
            _meshFilter.sharedMesh.colors = _generator.Colors;
        }

        private void ResetMesh()
        {
            _meshFilter.sharedMesh = new Mesh();
            _meshRenderer.material = _font.Material;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.DrawSolidRectangleWithOutline(new Rect(transform.position, new Vector2(12.5f, -20f)), new Color(1, 0, 0, .1f), Color.black);
        }
#endif
    }
}
