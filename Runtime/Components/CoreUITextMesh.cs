using UnityEditor;
using UnityEngine;

namespace CoreUI
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
        [SerializeField] private int _sinPixelsOffset = 4;
        [SerializeField] private int _sinSpeedOffset = 1;
        [SerializeField] private float _sinMultiplier = 1;
        [SerializeField] private float _horizontalShakePixelsOffset;
        [SerializeField] private float _verticalShakePixelsOffset;
        [SerializeField] private Color _fillRectangleJizmosColor = new Color(1, 0, 0, .1f);
        [SerializeField] private Color _outlineRectangleGizmosColor = Color.black;
        [SerializeField] private int _selectedMode;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        
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
            UpdateScale();
            if (_font == null) return;
            UpdateText();
            UpdateColors();
            _generator.Update();
            ApplyMesh();
        }

        public void ShowSymbols(int start, int symbols)
        {
            _generator.ShowSymbols(start, symbols);
        }
        
        private void UpdateScale()
        {
            transform.localScale = Vector3.one;
        }
        
        private void UpdateText()
        {
            if (_generator.Text.Equals(_text)) return;
            GenerateData();
        }

        public void GenerateData()
        {
            InitSelf();
            _generator.ForceGenerateMeshData(_text, _color, _textWrapping, _lineWidth);
        }

        #if UNITY_EDITOR
        
        public void GenerateDataEditor()
        {
            _generator.Init(_font);
            _generator.InitEffects(_sinPixelsOffset, _sinSpeedOffset, _sinMultiplier, _horizontalShakePixelsOffset, _verticalShakePixelsOffset);
            _generator.ForceGenerateMeshData(_text, _color, _textWrapping, _lineWidth);
            ResetMesh();
            ApplyMesh();
        }
        
        #endif

        private void InitSelf()
        {
            if (!_generator.Inited)
            {
                _generator.Init(_font);
                ResetMesh();
            }
            _generator.InitEffects(_sinPixelsOffset, _sinSpeedOffset, _sinMultiplier, _horizontalShakePixelsOffset, _verticalShakePixelsOffset);
        }

        private void UpdateColors()
        {
            if (_color == _generator.Color) return;
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
            if (_textWrapping)
                Handles.DrawSolidRectangleWithOutline(new Rect(transform.position, new Vector2(_lineWidth, -_meshRenderer.bounds.size.y)), _fillRectangleJizmosColor, _outlineRectangleGizmosColor);
        }
#endif
    }
}
