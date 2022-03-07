using UnityEngine;

namespace UICore
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
        
        public Vector2 PointerPosition { get { return _pointerPosition; } }
        public bool HasPointerDown { get { return _hasPointerDown; } }
        public bool HasPointerUp { get { return _hasPointerUp; } }
        public float ScrollDir { get { return _scrollDir; } }
        public bool PointerDown { get { return _pointerDown; } }
        public bool PointerUp { get { return _pointerUp; } }
        public string InputString { get { return _inputString; } }

        public CoreUIEvent(Vector2 pointerPosition, float scrollDir, bool pointerDown, bool pointerUp, string inputString)
        {
            _pointerPosition = pointerPosition;
            _scrollDir = scrollDir;
            _hasPointerDown = _pointerDown = pointerDown;
            _hasPointerUp = _pointerUp = pointerUp;
            _inputString = inputString;
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
    }
}
