using System.Linq;
using Assets.Scripts.UICore.StylesSystem.Styles;
using Assets.Scripts.UICore.StylesSystem.Styles.Font;
using Assets.Scripts.UICore.UICoreMeshes.Generators;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Meshes.Text
{
    public class TextMesh : BaseCoreUIMesh
    {
        private CoreUITextGenerator _textGenerator;
        private string _text;

        public TextMesh(string text) : base()
        {
            _text = text;
            _textGenerator = new CoreUITextGenerator();
        }

        protected override void Generate(BaseStyle style)
        {
            var font = style as CoreUIFont;
            _textGenerator.Init(font);
            _textGenerator.GenerateMeshData(_text, Color.white);
            Vertices = _textGenerator.Vertices.ToList();
            Triangles = _textGenerator.Triangles.ToList();
            Uv = _textGenerator.UV.ToList();
        }

        protected override void ApplySize()
        {
        }
    }
}
