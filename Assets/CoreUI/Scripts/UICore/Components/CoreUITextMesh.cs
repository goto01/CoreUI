using System.Collections;
using UICore.StylesSystem.Styles.Font;
using UICore.UICoreMeshes.Generators;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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

        [SerializeField] private Color _fillRectangleJizmosColor = new Color(1, 0, 0, .1f);
        [SerializeField] private Color _outlineRectangleGizmosColor = Color.black;
        [SerializeField] private int _selectedMode;

        protected virtual void Reset()
        {
            OnEnable();
        }

        protected virtual void OnEnable()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            if (!string.IsNullOrEmpty(_text)) GenerateData();
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(FadeOut());
            if (_font == null) return;
            UpdateText();
            UpdateColors();
        }

        private void UpdateText()
        {
            if (_generator.Text.Equals(_text)) return;
            GenerateData();
        }

        ///Only for editor
        public void GenerateData()
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

        private IEnumerator FadeOut()
        {
            _color.a = 1;
            while (_color.a > 0)
            {
                _color.a -= .01f;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        }


#if UNITY_EDITOR
    private void OnDrawGizmos()
        {
            if (_textWrapping)
                Handles.DrawSolidRectangleWithOutline(new Rect(transform.position, new Vector2(_lineWidth, -_meshRenderer.bounds.size.y)), _fillRectangleJizmosColor, _outlineRectangleGizmosColor);
        }
#endif
    }
}
