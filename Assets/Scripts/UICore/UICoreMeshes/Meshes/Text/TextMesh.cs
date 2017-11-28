using System.Linq;
using Assets.Scripts.UICore.StylesSystem.Styles;
using Assets.Scripts.UICore.StylesSystem.Styles.Font;
using Assets.Scripts.UICore.UICoreMeshes.Generators;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Meshes.Text
{
    public class TextMesh : BaseCoreUIMesh
    {
        private string _text;

        public TextMesh(string text) : base()
        {
            _text = text;
        }

        protected override void Generate(BaseStyle style)
        {
            var font = style as CoreUIFont;
            using (var generator = new CoreUITextGenerator())
            {
                generator.Init(font);
                generator.GenerateMeshData(_text, Color.white);
                Vertices = generator.Vertices.ToList();
                Triangles = generator.Triangles.ToList();
                Uv = generator.UV.ToList();
            }
        }

        protected override void ApplySize()
        {
        }
    }
}
