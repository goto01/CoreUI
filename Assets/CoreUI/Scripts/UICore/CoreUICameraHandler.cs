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
        public Vector2 CameraPosition { get { InitIfRequired(); return _cameraTransform.position + Vector3.forward; } }
        public Vector2 PointerPosition { get { InitIfRequired(); return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - CameraPosition;} }
        public Vector2 LeftTopPosition { get { InitIfRequired(); return CameraPosition + new Vector2(HorizontalLeftLimit, VerticalTopLimit); }}
        public Vector2 RightTopPosition {get { InitIfRequired(); return CameraPosition + new Vector2(HorizontalRightLimit, VerticalTopLimit); }}
        public Vector2 LeftBottomPosition { get { InitIfRequired(); return CameraPosition + new Vector2(HorizontalLeftLimit, VerticalBottomLimit); }}
        public Vector2 RightBottomPosition {get { InitIfRequired(); return CameraPosition + new Vector2(HorizontalRightLimit, VerticalBottomLimit); }}
        
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
