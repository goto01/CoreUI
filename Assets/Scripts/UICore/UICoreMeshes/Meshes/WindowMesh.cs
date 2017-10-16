using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Meshes
{
    class WindowMesh : BaseUICoreMesh
    {
        private float _borderWidth;

        public float BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }

        public override void Init()
        {
            PushVertice(new Vector3(0, 0), new Vector2(0, 1));
            PushVertice(new Vector3(0, BorderWidth), new Vector2(.5f, 1));
            PushVertice(new Vector3(BorderWidth, BorderWidth), new Vector2(.5f, 0));
            PushVertice(new Vector3(BorderWidth, 0), new Vector2(0, 0));

            PushVertice(new Vector3(0, _height - _borderWidth), new Vector2(0, 0));
            PushVertice(new Vector3(0, _height), new Vector2(0, 1));
            PushVertice(new Vector3(_borderWidth, _height), new Vector2(.5f, 1));
            PushVertice(new Vector3(_borderWidth, _height-_borderWidth), new Vector2(0.5f, 0));

            PushVertice(new Vector3(_width - _borderWidth, _height - _borderWidth), new Vector2(.5f, 0));
            PushVertice(new Vector3(_width - _borderWidth, _height), new Vector2(0, 0));
            PushVertice(new Vector3(_width, _height), new Vector2(0, 1));
            PushVertice(new Vector3(_width, _height - _borderWidth), new Vector2(.5f, 1));

            PushVertice(new Vector3(_width - _borderWidth, 0), new Vector2(.5f, 1));
            PushVertice(new Vector3(_width - _borderWidth, _borderWidth), new Vector2(.5f, 0));
            PushVertice(new Vector3(_width, _borderWidth), new Vector2(0, 0));
            PushVertice(new Vector3(_width, 0), new Vector2(0, 1));

            PushVertice(new Vector3(0, _borderWidth), new Vector2(.5f, 1));
            PushVertice(new Vector3(0, _height - _borderWidth), new Vector2(1, 1));
            PushVertice(new Vector3(_borderWidth, _height - _borderWidth), new Vector2(1, 0));
            PushVertice(new Vector3(_borderWidth, _borderWidth), new Vector2(.5f, 0));

            PushVertice(new Vector3(_borderWidth, _height-_borderWidth), new Vector2(.5f, 0));
            PushVertice(new Vector3(_borderWidth, _height), new Vector2(.5f, 1));
            PushVertice(new Vector3(_width - _borderWidth, _height), new Vector2(1, 1));
            PushVertice(new Vector3(_width - _borderWidth, _height - _borderWidth), new Vector2(1, 0));

            PushVertice(new Vector3(_width - _borderWidth, _borderWidth), new Vector2(1, 0));
            PushVertice(new Vector3(_width - _borderWidth, _height - _borderWidth), new Vector2(.5f, 0));
            PushVertice(new Vector3(_width, _height - _borderWidth), new Vector2(.5f, 1));
            PushVertice(new Vector3(_width, _borderWidth), new Vector2(1, 1));

            PushVertice(new Vector3(_borderWidth, 0), new Vector2(1, 1));
            PushVertice(new Vector3(_borderWidth, _borderWidth), new Vector2(1, 0));
            PushVertice(new Vector3(_width - _borderWidth, _borderWidth), new Vector2(.5f, 0));
            PushVertice(new Vector3(_width - _borderWidth, 0), new Vector2(.5f, 1));

            PushVertice(new Vector3(_borderWidth, _borderWidth), new Vector2(0, 0));
            PushVertice(new Vector3(_borderWidth, _height - _borderWidth, 0), new Vector2(0, 0));
            PushVertice(new Vector3(_width - _borderWidth, _height - _borderWidth), new Vector2(0, 0));
            PushVertice(new Vector3(_width - _borderWidth, _borderWidth), new Vector2(0, 0));

            UpdateMeshInfo();
            Triangles = new int[]
            {
                0, 1, 2, 0, 2, 3,           // left botttom corner
                4, 5, 6, 4, 6 , 7,          // left top corner
                8, 9, 10, 8, 10, 11,        // right top corner
                12, 13, 14, 12, 14, 15,     // right bottom corner
                16, 17, 18, 16, 18, 19,     // left border
                20, 21, 22, 20, 22, 23,     // top border
                24, 25, 26, 24, 26, 27,     // right border
                28, 29, 30, 28, 30, 31,     // bottom border
                32, 33, 34, 32, 34, 35,     // content area
            };
        }
    }
}
