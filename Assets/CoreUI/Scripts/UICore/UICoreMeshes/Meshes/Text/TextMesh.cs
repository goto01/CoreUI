using System.Linq;
using UICore.StylesSystem.Styles;
using UICore.StylesSystem.Styles.Font;
using UICore.UICoreMeshes.Generators;
using UnityEngine;

namespace UICore.UICoreMeshes.Meshes.Text
{
    public class TextMesh : BaseCoreUIMesh
    {
        private string _text;
        private CoreUITextGenerator _textGenerator;
        
        public CoreUITextGenerator TextGenerator{get { return _textGenerator; }}
        
        public TextMesh(string text) : base()
        {
            _text = text;
        }

        protected override void Generate(BaseStyle style)
        {
            var font = style as CoreUIFont;
            _textGenerator = new CoreUITextGenerator();
            _textGenerator.Init(font);
            _textGenerator.GenerateMeshData(_text, Color.white);
            Vertices = _textGenerator.Vertices.ToList();
            Triangles = _textGenerator.Triangles.ToList();
            Uv = _textGenerator.UV.ToList();
        }

        public void ApplyTextMesh()
        {
            var clearMesh = (_textGenerator.Vertices.Length != Vertices.Count);
            Vertices.Clear();
            Triangles.Clear();
            Uv.Clear();
            for (var index = 0; index < _textGenerator.Vertices.Length; index++) Vertices.Add(_textGenerator.Vertices[index]);
            for (var index = 0; index < _textGenerator.Triangles.Length; index++) Triangles.Add(_textGenerator.Triangles[index]);
            for (var index = 0; index < _textGenerator.UV.Length; index++) Uv.Add(_textGenerator.UV[index]);
            
            var vertices = _textGenerator.Vertices;
            for (var index = 0; index < vertices.Length; index++)
            {
                vertices[index].x += X;
                vertices[index].y += Y;
            }
            _mesh.MarkDynamic();
            if (clearMesh)
            {
                _mesh.Clear();
                _mesh.vertices = vertices;
                _mesh.uv = _textGenerator.UV;
                _mesh.triangles = _textGenerator.Triangles;
            }
            else _mesh.vertices = vertices;
            _mesh.RecalculateBounds();
        }

        protected override void ApplySize()
        {
        }
    }
}
