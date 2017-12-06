using Assets.Editor.CoreUI.Windows.Font;
using UICore.StylesSystem.Repository;
using UICore.StylesSystem.Styles;
using UICore.StylesSystem.Styles.Font;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.CoreUI
{
    public class CoreUIMenuItemEditor : ScriptableObject
    {
        private const string CreateMenu = "Assets/Create/Core UI";
        private const string CoreUI = "CoreUI";
        private const string CoreUIWindows = CoreUI + "/Windows";
        
        [MenuItem(CreateMenu + "/Window style", false, 1101)]
        public static void CreateWindowStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof (WindowStyle));
        }

        [MenuItem(CreateMenu + "/Image style", false, 1102)]
        public static void CreateImageStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof(ImageStyle));
        }

        [MenuItem(CreateMenu + "/Flexible image style", false, 1102)]
        public static void CreateFlexibleImageStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof(FlexibleImageStyle));
        }

        [MenuItem(CreateMenu + "/Slider style", false, 1103)]
        public static void CreateSliderStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof(SliderStyle));
        }

        [MenuItem(CreateMenu + "/Button style", false, 1104)]
        public static void CreateButtonStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof (ButtonStyle));
        }

        [MenuItem(CreateMenu + "/Scroll style", false, 1105)]
        public static void CreateScrollStyle()
        {
            ObjectCreatorHelper.CreateAsset(typeof (ScrollStyle));
        }

        [MenuItem(CreateMenu + "/Font", false, 1106)]
        public static void CreateFont()
        {
            ObjectCreatorHelper.CreateAsset(typeof (CoreUIFont));
        }

        [MenuItem(CreateMenu + "/Styles repository", false, 1000)]
        public static void CreateStylesRepository()
        {
            ObjectCreatorHelper.CreateAsset(typeof (StylesRepository));
        }

        [MenuItem(CoreUIWindows + "/Font editor", false, 0)]
        public static void OpenFontEditorWindow()
        {
            FontEditorWindow.Show();
        }
    }
}
