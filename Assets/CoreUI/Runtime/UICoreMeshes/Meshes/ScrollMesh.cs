using System.Collections.Generic;

namespace CoreUI
{
    public class ScrollMesh : BaseCoreUIMesh
    {
        protected override void Generate(BaseStyle style)
        {
            GenerateVertices();

            Triangles = new List<int>()
            {
                0, 1, 2, 0, 2, 3
            };
        }

        private void GenerateVertices()
        {
            PushVertice(0, - Height, 0, 0);
            PushVertice(0, 0, 0, 1);
            PushVertice(Width, 0, 1, 1);
            PushVertice(Width, - Height, 1, 0);
        }

        protected override void ApplySize()
        {
        }
    }
}
