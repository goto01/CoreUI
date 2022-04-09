using UnityEditor;
using UnityEngine;

namespace CoreUI
{
    public class ObjectCreatorHelper
    {
        public static T CreateAsset<T>(string name = null) where T:ScriptableObject
        {
            if (name == null) name = typeof(T).Name;
            var asset = ScriptableObject.CreateInstance<T>();
            ProjectWindowUtil.CreateAsset(asset, string.Format("{0}.asset", name));
            return (T)asset;
        }

        public static void CreateAsset(UnityEngine.Object asset, string name)
        {
            ProjectWindowUtil.CreateAsset(asset, name);
        }
    }
}
