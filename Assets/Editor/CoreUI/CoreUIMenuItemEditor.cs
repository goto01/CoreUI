using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.StylesSystem.Styles;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.CoreUI
{
    public class CoreUIMenuItemEditor : ScriptableObject
    {
        private const string Menu = "Assets/Create/Core UI";
        
        [MenuItem(Menu + "/Window style", false, 1101)]
        public static void CreateWindowStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof (WindowStyle));
        }

        [MenuItem(Menu + "/Image style", false, 1102)]
        public static void CreateImageStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof(ImageStyle));
        }

        [MenuItem(Menu + "/Flexible image style", false, 1102)]
        public static void CreateFlexibleImageStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof(FlexibleImageStyle));
        }

        [MenuItem(Menu + "/Slider style", false, 1103)]
        public static void CreateSliderStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof(SliderStyle));
        }

        [MenuItem(Menu + "/Button style", false, 1104)]
        public static void CreateButtonStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof (ButtonStyle));
        }

        [MenuItem(Menu + "/Scroll style", false, 1105)]
        public static void CreateScrollStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof (ScrollStyle));
        }

        [MenuItem(Menu + "/Styles repository", false, 1000)]
        public static void CreateStylesRepository()
        {
            ObjectCreatorHelper.CreateAsset(typeof (StylesRepository));
        }
    }
}
