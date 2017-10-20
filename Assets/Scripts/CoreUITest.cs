using Assets.Scripts.UICore;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CoreUITest: MonoBehaviour
    {
        protected virtual void Awake()
        {
            var instance = CoreUIEditor.Instance;
            instance.Window(new Rect(0, 0, 2, 2));
        }
    }
}
