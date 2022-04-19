using System.Collections.Generic;
using UnityEngine;

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
            PushVertices();
            ApplyLayout();
        }

        private void PushVertices()
        {
            PushVertex(0, - Height, 0, 0);
            PushVertex(0, 0, 0, 1);
            PushVertex(Width, 0, 1, 1);
            PushVertex(Width, - Height, 1, 0);
        }

        private void ApplyLayout()
        {
            SetVertex(0, new Vector2(0, - Height));
            SetVertex(1, new Vector2(0, 0));
            SetVertex(2, new Vector2(Width, 0));
            SetVertex(3, new Vector2(Width, - Height));
        }

        protected override void ApplySize()
        {
            ApplyLayout();
            UpdateMeshInfo();
        }
    }
}
