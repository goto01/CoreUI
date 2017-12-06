using UnityEngine;

namespace UICore
{
    public struct CoreUIEvent
    {
        public Vector2 PointerPosition;
        public float ScrollDir;
        public bool PointerDown;
        public bool PointerUp;

        public void DropPointerData()
        {
            ScrollDir = 0;
            PointerDown = false;
        }
    }
}
