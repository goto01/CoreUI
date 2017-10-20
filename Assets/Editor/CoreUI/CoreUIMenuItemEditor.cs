using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.StylesSystem.Styles;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.CoreUI
{
    public class CoreUIMenuItemEditor : ScriptableObject
    {
        private const string Menu = "Assets/Create/Core UI";
        
        [MenuItem(Menu + "/Window style", false, 1001)]
        public static void CreateWindowStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof (WindowStyle));
        }

        [MenuItem(Menu + "/Styles repository", false, 1000)]
        public static void CreateStylesRepository()
        {
            ObjectCreatorHelper.CreateAsset(typeof (StylesRepository));
        }
    }
}
