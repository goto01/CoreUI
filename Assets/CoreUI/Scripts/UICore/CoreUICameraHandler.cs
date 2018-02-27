using Singleton;
using UnityEngine;

namespace UICore
{ 
    class CoreUICameraHandler : SingletonMonoBahaviour<CoreUICameraHandler>
    {
        private Transform _cameraTransform;
        private Camera _camera;

        public float Width { get { InitIfRequired(); return 2*_camera.orthographicSize*_camera.aspect; } }
        public float VerticalTopLimit { get { InitIfRequired(); return  _camera.orthographicSize; } }
        public float VerticalBottomLimit { get { InitIfRequired(); return - _camera.orthographicSize; } }
        public float HorizontalLeftLimit { get { InitIfRequired(); return - Width/2; } }
        public float HorizontalRightLimit { get { InitIfRequired(); return Width/2; } }
        public Vector3 CameraPosition { get { InitIfRequired(); return _cameraTransform.position + Vector3.forward; } }
        public Vector2 PointerPosition { get { InitIfRequired(); return Camera.main.ScreenToWorldPoint(Input.mousePosition) - CameraPosition;} }
        public Vector3 LeftTopPosition { get { InitIfRequired(); return CameraPosition + new Vector3(HorizontalLeftLimit, VerticalTopLimit, 0); }}
        public Vector3 RightTopPosition {get { InitIfRequired(); return CameraPosition + new Vector3(HorizontalRightLimit, VerticalTopLimit, 0); }}
        public Vector3 LeftBottomPosition { get { InitIfRequired(); return CameraPosition + new Vector3(HorizontalLeftLimit, VerticalBottomLimit, 0); }}
        public Vector3 RightBottomPosition {get { InitIfRequired(); return CameraPosition + new Vector3(HorizontalRightLimit, VerticalBottomLimit, 0); }}
        
        public override void AwakeSingleton()
        {
            InitCamera();
        }

        private void InitIfRequired()
        {
            if (_camera == null) InitCamera();
        }
        
        private void InitCamera()
        {
            _camera = Camera.main;
            if (_camera == null) Debug.LogError("Scene doesn't have camera with 'MainCamera' tag");
            _cameraTransform = _camera.transform;
        }
    }
}
