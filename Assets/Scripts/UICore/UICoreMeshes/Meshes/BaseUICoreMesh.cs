using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Meshes
{
   public abstract class BaseUICoreMesh
    {
        private Mesh _mesh;
        protected float _width;
        protected float _height;
        private List<Vector3> _vertices;
        private List<Vector2> _uv;
        private List<int> _triangles;

        public Mesh Mesh { get { return _mesh; } }

        public Vector3[] Vertices
        {
            get { return _mesh.vertices; }
            set { _mesh.vertices = value; }
        }

        public Vector2[] Uv
        {
            get { return _mesh.uv; }
            set { _mesh.uv = value; }
        }

        public int[] Triangles
        {
            get { return _mesh.triangles; }
            set { _mesh.triangles = value; }
        }

        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        protected BaseUICoreMesh()
        {
            _mesh = new Mesh();
            _vertices = new List<Vector3>();
            _uv = new List<Vector2>();
            _triangles = new List<int>();
            _width = 1;
            _height = 1;
        }

        public abstract void Init();

        protected void PushVertice(Vector3 vertice, Vector2 uv)
        {
            _vertices.Add(vertice);
            _uv.Add(uv);
        }

        protected void UpdateMeshInfo()
        {
            _mesh.SetVertices(_vertices);
            _mesh.SetUVs(0, _uv);
            _mesh.SetTriangles(_triangles, 0);
        }
    }
}
