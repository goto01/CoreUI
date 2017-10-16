using Assets.Scripts.UICore.StylesSystem.Styles;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.CoreUI
{
    public class CoreUIMenuItemEditor : ScriptableObject
    {
        private const string Menu = "Assets/Create/Core UI/";

        [MenuItem(Menu + "Window style", false, 0)]
        public static void CreateWindowStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof (WindowStyle));
        }
    }
}
