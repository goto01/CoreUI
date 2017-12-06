using Singleton;
using UnityEngine;

namespace UICore
{ 
    class CoreUICameraHandler : SingletonMonoBahaviour<CoreUICameraHandler>
    {
        private Transform _cameraTransform;
        private Camera _camera;

        public float Width { get { return 2*_camera.orthographicSize*_camera.aspect; } }
        public float VerticalTopLimit { get { return  _camera.orthographicSize; } }
        public float VerticalBottomLimit { get { return - _camera.orthographicSize; } }
        public float HorizontalLeftLimit { get { return - Width/2; } }
        public float HorizontalRightLimit { get { return Width/2; } }
        public Vector3 CameraPosition { get { return _cameraTransform.position + Vector3.forward; } }
        public Vector2 PointerPosition { get { return Camera.main.ScreenToWorldPoint(Input.mousePosition) - CameraPosition;} }

        public override void AwakeSingleton()
        {
            _camera = Camera.main;
            _cameraTransform = _camera.transform;
        }
    }
}
