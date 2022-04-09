using UnityEngine;

namespace CoreUI
{
    public abstract class SingletonMonoBahaviour<T> : MonoBehaviour, ISingletonMonoBehaviour where T: SingletonMonoBahaviour<T>, ISingletonMonoBehaviour
    {
        public static T Instance
        {
            get
            {
                return UnitySingleton<T>.Instance;
            }
        }

        protected void Awake()
        {
            UnitySingleton<T>.Awake(this as T);
        }

        public abstract void AwakeSingleton();
    }
}
