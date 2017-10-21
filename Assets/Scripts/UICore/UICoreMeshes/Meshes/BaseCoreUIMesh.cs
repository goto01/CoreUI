using System.Collections.Generic;
using Assets.Scripts.UICore.StylesSystem.Styles;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Meshes
{
   public abstract class BaseCoreUIMesh
    {
        private Mesh _mesh;
        private Vector2 _position;
        private Vector2 _size;   
        private List<Vector3> _vertices;
        private List<Vector2> _uv;
        private List<int> _triangles;

        public Mesh Mesh { get { return _mesh; } }

        public List<Vector3> Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        public List<Vector2> Uv
        {
            get { return _uv; }
            set { _uv = value; }
        }

        public List<int> Triangles
        {
            get { return _triangles; }
            set { _triangles = value; }
        }

        public float X
        {
            get { return _position.x; }
            set
            {
                _position.x = value;
                UpdatePositions();
            }
        }

        public float Y
        {
            get { return _position.y; }
            set
            {
                _position.y = value; 
                UpdatePositions();
            }
        }

        public float Width
        {
            get { return _size.x; }
            set
            {
                _size.x = value;
                ApplySize();
            }
        }

        public float Height
        {
            get { return _size.y; }
            set
            {
                _size.y = value;
                ApplySize();
            }
        }

        protected BaseCoreUIMesh()
        {
            _mesh = new Mesh();
            _vertices = new List<Vector3>();
            _uv = new List<Vector2>();
            _triangles = new List<int>();
        }

        public void Init(BaseStyle style, Rect rect)
        {
            _position = rect.position;
            _size = rect.size;
            Generate(style);
            UpdateMeshInfo();
        }

        protected void PushUV(float uvx, float uvy)
        {
            PushUV(new Vector2(uvx, uvy));    
        }

        protected void PushUV(Vector2 uv)
        {
            _uv.Add(uv);    
        }

        protected void PushVertice(float x, float y)
        {
            PushVertice(new Vector3(x, y));
        }

        protected void PushVertice(Vector3 vertice)
        {
            _vertices.Add(vertice);    
        }

        protected void PushVertice(float x, float y, float uvx, float uvy)
        {
            PushVertice(new Vector3(x, y), new Vector2(uvx, uvy));
        }

        protected void PushVertice(Vector3 vertice, Vector2 uv)
        {
            _vertices.Add(vertice);
            _uv.Add(uv);
        }

        protected void UpdateMeshInfo()
        {
            UpdatePositions();
            _mesh.SetUVs(0, _uv);
            _mesh.SetTriangles(_triangles, 0);
        }

        protected void UpdatePositions()
        {
            _mesh.SetVertices(_vertices);
            var vertices = _mesh.vertices;
            for (var index = 0; index < _mesh.vertexCount; index++)
            {
                vertices[index].x += X;
                vertices[index].y += Y;
            }
            _mesh.vertices = vertices;
        }

        protected void Clear()
        {
            _vertices.Clear();
        }

        protected abstract void Generate(BaseStyle style);
        protected abstract void ApplySize();
    }
}
