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
            PushVertex(0, - Height, 0, 0);
            PushVertex(0, 0, 0, 1);
            PushVertex(Width, 0, 1, 1);
            PushVertex(Width, - Height, 1, 0);
        }

        protected override void ApplySize()
        {
        }
    }
}
