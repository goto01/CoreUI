using Assets.Scripts.UICore;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CoreUITest: MonoBehaviour
    {
        private const float _pixelSize = 1/32f;

        protected virtual void Awake()
        {
            CoreUIEditor.Instance.Button(new Rect(1, 1, _pixelSize*30, 0), ()=>Debug.Log("Up"));
            CoreUIEditor.Instance.Button(new Rect(1, -1, _pixelSize*30, 0));
            CoreUIEditor.Instance.Button(new Rect(-1, 1, _pixelSize*30, 0));
            CoreUIEditor.Instance.Button(new Rect(-1, -1, _pixelSize*30, 0), ()=>Debug.Log("Down"));
        }
    }
}
