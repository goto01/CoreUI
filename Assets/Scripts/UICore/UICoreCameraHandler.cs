using Assets.Scripts.Singleton;
using UnityEngine;

namespace Assets.Scripts.UICore
{ 
    class UICoreCameraHandler : SingletonMonoBahaviour<UICoreCameraHandler>
    {
        private Transform _cameraTransform;
        private Camera _camera;

        public float Width { get { return 2*_camera.orthographicSize*_camera.aspect; } }
        public float VerticalTopLimit { get { return _cameraTransform.position.y + _camera.orthographicSize; } }
        public float VerticalBottomLimit { get { return _cameraTransform.position.y - _camera.orthographicSize; } }
        public float HorizontalLeftLimit { get { return _cameraTransform.position.x - Width/2; } }
        public float HorizontalRightLimit { get { return _cameraTransform.position.x + Width/2; } }

        public override void AwakeSingleton()
        {
            _camera = Camera.main;
            _cameraTransform = _camera.transform;
        }
    }
}
