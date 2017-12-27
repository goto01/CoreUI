using System;
using System.Collections.Generic;
using System.Linq;
using UICore.StylesSystem.Styles.Font;
using UnityEngine;

namespace UICore.UICoreMeshes.Generators
{
    [Serializable]
    public class CoreUITextGenerator : IDisposable
    {
        [SerializeField] private int _index;
        [SerializeField] private float _horizontalOffset;
        [SerializeField] private float _verticalOffset;
        [SerializeField] private CoreUIFont _font;
        [SerializeField] private Vector3[] _vertices;
        [SerializeField] private Vector3[] _outputVertices;
        [SerializeField] private int[] _triangles;
        [SerializeField] private Vector2[] _uv;
        [SerializeField] private Color[] _colors;
        [SerializeField] private string _text;
        [SerializeField] private Color _color;
        [SerializeField] private bool _wrapping;
        [SerializeField] private bool _sinMode;
        [SerializeField] private List<int> _sinOffsetIndices;
        [SerializeField] private float _sinOffset;
        [SerializeField] private float _sinOffsetSpeed;
        [SerializeField] private float _sinMultiplier;
        [SerializeField] private float _shakeHorizontalOffset;
        [SerializeField] private float _shakeVerticalOffset;
        [SerializeField] private List<int> _shakeOffsetIndices;
        private IDictionary<char, Func<SymbolHandlerType>> _symbolHandlers;
        
        public Vector3[] Vertices { get { return _outputVertices; } }
        public int[] Triangles { get { return _triangles; } }
        public Vector2[] UV { get { return _uv; } }
        public Color Color { get { return _color; } }
        public Color[] Colors { get { return _colors; } }
        public string Text { get { return _text; } }
        public bool Inited { get { return _font != null && _symbolHandlers != null; } }

        public void InitEffects(int sinPixelsOffset, float sinOffsetSpeed, float sinMultiplier,
            float horizontalPixelsOffset, float verticalPixelsOffset)
        {
            _sinOffset = sinPixelsOffset * _font.PixelWidth;    
            _sinOffsetSpeed = sinOffsetSpeed;
            _sinMultiplier = sinMultiplier;
            _shakeHorizontalOffset = _font.PixelWidth * horizontalPixelsOffset;
            _shakeVerticalOffset = _font.PixelWidth * verticalPixelsOffset;
        }
        
        public void Init(CoreUIFont font)
        {
            _text = string.Empty;
            _font = font;
            ResetOffsets();
            InitHandlers();
        }

        public void GenerateMeshData(string text, Color color, bool wrapping, float lineWidth)
        {
            if (_text.Equals(text) && _wrapping == wrapping) return;
            _sinOffsetIndices.Clear();
            _shakeOffsetIndices.Clear();
            _sinMode = false;
            _index = 0;
            _text = text;
            _color = color;
            _wrapping = wrapping;
            ResetMeshData(text);
            GenerateVertices(text, wrapping, lineWidth);
            CopyVertices();
        }

        /// <summary>
        /// Generate text mesh data even text didn't change, Use only in Editor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="wrapping"></param>
        /// <param name="lineWidth"></param>
        public void ForceGenerateMeshData(string text, Color color, bool wrapping, float lineWidth)
        {
            _sinOffsetIndices.Clear();
            _shakeOffsetIndices.Clear();
            _sinMode = false;
            _index = 0;
            _text = text;
            _color = color;
            _wrapping = wrapping;
            ResetMeshData(text);
            GenerateVertices(text, wrapping, lineWidth);
            CopyVertices();
        }

        public void Update()
        {
            for (var index = 0; index < _sinOffsetIndices.Count; index++)
            {
                var verticeIndex = _sinOffsetIndices[index] * 4;
                var offset = Mathf.Sin((_vertices[verticeIndex].x + Time.time * _sinOffsetSpeed)*_sinMultiplier) * _sinOffset;
                _outputVertices[verticeIndex].y = _vertices[verticeIndex].y + offset;
                _outputVertices[verticeIndex + 1].y = _vertices[verticeIndex+1].y + offset;
                _outputVertices[verticeIndex + 2].y = _vertices[verticeIndex+2].y + offset;
                _outputVertices[verticeIndex + 3].y = _vertices[verticeIndex+3].y + offset;
            }
            for (var index = 0; index < _shakeOffsetIndices.Count; index++)
            {
                var verticeIndex = _shakeOffsetIndices[index] * 4;
                var offset = new Vector3(UnityEngine.Random.Range(-_shakeHorizontalOffset, _shakeHorizontalOffset), UnityEngine.Random.Range(-_shakeVerticalOffset, _shakeVerticalOffset));
                _outputVertices[verticeIndex] = _vertices[verticeIndex] + offset;
                _outputVertices[verticeIndex + 1] = _vertices[verticeIndex+1] + offset;
                _outputVertices[verticeIndex + 2] = _vertices[verticeIndex+2] + offset;
                _outputVertices[verticeIndex + 3] = _vertices[verticeIndex+3] + offset;
            }
        }
        
        public void GenerateMeshData(string text, Color color)
        {
            GenerateMeshData(text, color, false, -1);
        }

        public void UpdateColors(Color color)
        {
            if (_color == color) return;
            _color = color;
            for (var index = 0; index < _text.Length; index++)
                if (!CheckSymbolForHandler(_text[index])) GenerateColors(index*4);
        }

        public void ShowSymbols(int start, int symbols)
        {
            for (var index = 0; index < _text.Length; index++)
            {
                var alfa = 0;
                if (index >= start && index < start + symbols) alfa = 1;
                var verticeIndex = index * 4;
                _colors[verticeIndex].a = alfa;
                _colors[verticeIndex+1].a = alfa;
                _colors[verticeIndex+2].a = alfa;
                _colors[verticeIndex+3].a = alfa;
            }
        }
        
        private void InitHandlers()
        {
            _symbolHandlers = new Dictionary<char, Func<SymbolHandlerType>>()
            {
                {' ', () => { _horizontalOffset += _font.Space; return SymbolHandlerType.Separative; } },
                {' ', () => { _horizontalOffset += _font.Space; return SymbolHandlerType.Separative; } },
                {'	', () => { _horizontalOffset += _font.Space*4; return SymbolHandlerType.Separative; } },
                {'\n', () => { ShiftLine(); return SymbolHandlerType.NotSeparative; } },
                {'\r', () => { return SymbolHandlerType.NotSeparative;}},
                {'~', HandleSinOffset},
                {'±', HandleShakeOffset},
            };
        }

        private void GenerateVertices(string text, bool wrapping, float lineWidth)
        {
            for (_index = 0; _index < text.Length; _index++)
            {
                var c = text[_index];
                if (CheckSymbolForHandler(c))
                {
                    var symbolHandlerType = HandleSymbol(c);
                    if (wrapping && symbolHandlerType == SymbolHandlerType.Separative) PredictWidthOfWord(_index, text, lineWidth);
                    continue;
                }
                var symbol = _font.GetSymbol(c);
                GenerateSymbolVertices(_index, symbol);
                _horizontalOffset += symbol.Width + _font.Interval;
            }
        }

        private void CopyVertices()
        {
            _outputVertices = new Vector3[_vertices.Length];
            Array.Copy(_vertices, _outputVertices, _vertices.Length);
        }

        private void PredictWidthOfWord(int index, string text, float lineWidth)
        {
            var predictHorizontalOffset = _horizontalOffset;
            index++;
            for (;index < text.Length && !CheckSymbolForHandler(text[index]); predictHorizontalOffset += _font.GetSymbol(text[index]).Width + _font.Interval ,index++);
            if (predictHorizontalOffset >= lineWidth - Mathf.Epsilon)
            {
                ShiftLine();
            }
        }

        public bool CheckSymbolForHandler(char symbol)
        {
            return _symbolHandlers.ContainsKey(symbol);
        }

        private SymbolHandlerType HandleSymbol(char symbol)
        {
            return _symbolHandlers[symbol].Invoke();
        }
        
        private void ResetMeshData(string text)
        {
            ResetOffsets();
            _vertices = new Vector3[text.Length * 4];
            _triangles = new int[text.Length * 6];
            _uv = new Vector2[text.Length * 4];
            _colors = new Color[text.Length * 4];
        }

        private void ResetOffsets()
        {
            _horizontalOffset = 0;
            _verticalOffset = -_font.FontHeight;
        }

        private void ShiftLine()
        {
            _horizontalOffset = 0;
            _verticalOffset -= _font.FontHeight;
        }

        private void GenerateSymbolVertices(int symbolIndex, SymbolDescription symbol)
        {
            var verticesStartIndex = symbolIndex*4;
            _vertices[verticesStartIndex] = new Vector3(_horizontalOffset, _verticalOffset - symbol.VerticalOffset);
            _vertices[verticesStartIndex + 1] = new Vector3(_horizontalOffset, _verticalOffset + symbol.Height - symbol.VerticalOffset);
            _vertices[verticesStartIndex + 2] = new Vector3(_horizontalOffset + symbol.Width, _verticalOffset + symbol.Height - symbol.VerticalOffset);
            _vertices[verticesStartIndex + 3] = new Vector3(_horizontalOffset + symbol.Width, _verticalOffset - symbol.VerticalOffset);

            _uv[verticesStartIndex] = new Vector2(symbol.UV.X, symbol.UV.Y);
            _uv[verticesStartIndex + 1] = new Vector2(symbol.UV.X, symbol.UV.MaxY);
            _uv[verticesStartIndex + 2] = new Vector2(symbol.UV.MaxX, symbol.UV.MaxY);
            _uv[verticesStartIndex + 3] = new Vector2(symbol.UV.MaxX, symbol.UV.Y);
             
            var triangleStartIndex = symbolIndex*6;
            _triangles[triangleStartIndex] = verticesStartIndex;
            _triangles[triangleStartIndex + 1] = verticesStartIndex + 1;
            _triangles[triangleStartIndex + 2] = verticesStartIndex + 2;
            _triangles[triangleStartIndex + 3] = verticesStartIndex;
            _triangles[triangleStartIndex + 4] = verticesStartIndex + 2;
            _triangles[triangleStartIndex + 5] = verticesStartIndex + 3;

            GenerateColors(verticesStartIndex);
        }

        private void GenerateColors(int index)
        {
            _colors[index] = _color;
            _colors[index + 1] = _color;
            _colors[index + 2] = _color;
            _colors[index + 3] = _color;
        }

        private SymbolHandlerType HandleSinOffset()
        {
            if (_index >= _text.Length - 1 && !_sinMode) return SymbolHandlerType.NotSeparative;
            if (_sinMode)
            {
                _sinMode = false;
                for (var i = _sinOffsetIndices.Last()+1; i < _index; i++) _sinOffsetIndices.Add(i);
                return SymbolHandlerType.NotSeparative;
            }
            _sinOffsetIndices.Add(_index+1);
            _sinMode = true;
            return SymbolHandlerType.NotSeparative;
        }

        private SymbolHandlerType HandleShakeOffset()
        {
            
            if (_index >= _text.Length - 1 && !_sinMode) return SymbolHandlerType.NotSeparative;
            if (_sinMode)
            {
                _sinMode = false;
                for (var i = _shakeOffsetIndices.Last()+1; i < _index; i++) _shakeOffsetIndices.Add(i);
                return SymbolHandlerType.NotSeparative;
            }
            _shakeOffsetIndices.Add(_index+1);
            _sinMode = true;
            return SymbolHandlerType.NotSeparative;
        }
            
        public void Dispose()
        {
            _vertices = null;
            _triangles = null;
            _uv = null;
            _colors = null;
        }
    }
}
