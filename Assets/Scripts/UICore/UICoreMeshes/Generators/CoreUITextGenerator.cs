using System;
using System.Collections.Generic;
using Assets.Scripts.UICore.StylesSystem.Styles.Font;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Generators
{
    [Serializable]
    public class CoreUITextGenerator
    {
        [SerializeField] private float _horizontalOffset;
        [SerializeField] private float _verticalOffset;
        [SerializeField] private CoreUIFont _font;
        [SerializeField] private Vector3[] _vertices;
        [SerializeField] private int[] _triangles;
        [SerializeField] private Vector2[] _uv;
        private IDictionary<char, Action> _symbolHandlers;

        public Vector3[] Vertices { get { return _vertices; } }
        public int[] Triangles { get { return _triangles; } }
        public Vector2[] UV { get { return _uv; } }

        public void Init(CoreUIFont font)
        {
            _font = font;
            _horizontalOffset = 0;
            _verticalOffset = -_font.FontHeight;
            InitHandlers();
        }

        public void Update()
        {
            
        }

        public void GenerateMeshData(string text, Color color)
        {
            ResetMeshData(text);
            GenerateVertices(text);
        }

        private void InitHandlers()
        {
            _symbolHandlers = new Dictionary<char, Action>()
            {
                {' ', () => _horizontalOffset += _font.Space }
            };
        }

        private void GenerateVertices(string text)
        {
            for (var index = 0; index < text.Length; index++)
            {
                var c = text[index];
                if (CheckSymbolForHandler(c))
                {
                    HandleSymbol(c);
                    continue;
                }
                var symbol = _font.GetSymbol(c);
                GenerateSymbolVertices(index, symbol);
                _horizontalOffset += symbol.Width + _font.Interval;
            }
        }

        private bool CheckSymbolForHandler(char symbol)
        {
            return _symbolHandlers.ContainsKey(symbol);
        }

        private void HandleSymbol(char symbol)
        {
            _symbolHandlers[symbol].Invoke();
        }

        private void ResetMeshData(string text)
        {
            _vertices = new Vector3[text.Length * 4];
            _triangles = new int[text.Length * 6];
            _uv = new Vector2[text.Length * 4];
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
        }
    }
}
