using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
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
    }
}
