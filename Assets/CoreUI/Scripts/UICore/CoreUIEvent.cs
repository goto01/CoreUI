using UnityEngine;

namespace UICore
{
    public struct CoreUIEvent
    {
        public Vector2 PointerPosition;
        public bool HasPointerDown;
        public bool HasPointerUp;
        public float ScrollDir;
        public bool PointerDown;
        public bool PointerUp;

        public CoreUIEvent(Vector2 pointerPosition, float scrollDir, bool pointerDown, bool pointerUp)
        {
            PointerPosition = pointerPosition;
            ScrollDir = scrollDir;
            HasPointerDown = PointerDown = pointerDown;
            HasPointerUp = PointerUp = pointerUp;
        }
        
        public void ReleasePointerDown()
        {
            PointerDown = false;
        }

        public void ReleasePointerUp()
        {
            PointerUp = false;
        }

        public void ReleaseScrollDir()
        {
            ScrollDir = 0;
        }
    }
}
