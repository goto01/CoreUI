using UnityEngine;

namespace CoreUI
{
    public struct CoreUIEvent
    {
        private Vector2 _pointerPosition;
        private bool _hasPointerDown;
        private bool _hasPointerUp;
        private float _scrollDir;
        private bool _pointerDown;
        private bool _pointerUp;
        private string _inputString;
        private bool _leftArrowDown;
        private bool _rightArrowDown;
        private float _deltaTime;
        private bool _deleteDown;
        private bool _enterDown;
        
        public Vector2 PointerPosition { get { return _pointerPosition; } }
        public bool HasPointerDown { get { return _hasPointerDown; } }
        public bool HasPointerUp { get { return _hasPointerUp; } }
        public float ScrollDir { get { return _scrollDir; } }
        public bool PointerDown { get { return _pointerDown; } }
        public bool PointerUp { get { return _pointerUp; } }
        public string InputString { get { return _inputString; } }
        public bool LeftArrowDown { get { return _leftArrowDown; } }
        public bool RightArrowDown { get { return _rightArrowDown; } }
        public float DeltaTime { get { return _deltaTime; } }
        public bool DeleteDown { get { return _deleteDown; } }
        public bool EnterDown { get { return _enterDown; } }

        public CoreUIEvent(float deltaTime, Vector2 pointerPosition, float scrollDir, bool pointerDown, bool pointerUp, string inputString, bool leftArrowDown, bool rightArrowDown,
            bool deleteDown, bool enterDown)
        {
            _pointerPosition = pointerPosition;
            _scrollDir = scrollDir;
            _hasPointerDown = _pointerDown = pointerDown;
            _hasPointerUp = _pointerUp = pointerUp;
            _inputString = inputString;
            _leftArrowDown = leftArrowDown;
            _rightArrowDown = rightArrowDown;
            _deltaTime = deltaTime;
            _deleteDown = deleteDown;
            _enterDown = enterDown;
        }
        
        public void ReleasePointerDown()
        {
            _pointerDown = false;
        }

        public void ReleasePointerUp()
        {
            _pointerUp = false;
        }

        public void ReleaseScrollDir()
        {
            _scrollDir = 0;
        }

        public void ReleaseInputString()
        {
            _inputString = string.Empty;
        }

        public void ReleaseLeftArrowDown()
        {
            _leftArrowDown = false;
        }

        public void ReleaseRightArrowDown()
        {
            _rightArrowDown = false;
        }
        
        public void ReleaseDeleteDown()
        {
            _deleteDown = false;
        }

        public void ReleaseEnterDown()
        {
            _enterDown = false;
        }
    }
}
