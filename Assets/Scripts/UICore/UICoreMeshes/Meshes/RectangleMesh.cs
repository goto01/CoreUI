using System.Collections.Generic;
using Assets.Scripts.UICore.StylesSystem.Styles;

namespace Assets.Scripts.UICore.UICoreMeshes.Meshes
{
    public class RectangleMesh : BaseCoreUIMesh
    {
        protected override void Generate(BaseStyle style)
        {
            GenerateMesh();
        }

        private void GenerateMesh()
        {
            PushVertice(0, 0, 0, 1);
            PushVertice(Width, 0, 1, 1);
            PushVertice(0, -Height, 0, 0);
            PushVertice(Width, -Height, 1, 0);

            Triangles = new List<int>()
            {
                0, 1, 2,
                2, 1, 3,
            };
        }
    }
}
