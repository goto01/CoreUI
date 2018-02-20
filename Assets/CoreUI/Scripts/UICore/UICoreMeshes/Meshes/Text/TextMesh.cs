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
            Vertices.Clear();
            Triangles.Clear();
            Uv.Clear();
            for (var index = 0; index < _textGenerator.Vertices.Length; index++) Vertices.Add(_textGenerator.Vertices[index]);
            for (var index = 0; index < _textGenerator.Triangles.Length; index++) Triangles.Add(_textGenerator.Triangles[index]);
            for (var index = 0; index < _textGenerator.UV.Length; index++) Uv.Add(_textGenerator.UV[index]);
        }

        protected override void ApplySize()
        {
        }
    }
}
