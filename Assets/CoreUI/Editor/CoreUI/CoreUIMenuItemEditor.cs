using Assets.Editor.CoreUI.Windows.Font;
using Editor.CoreUI.Windows.DialogWindows;
using UICore.Settings;
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
            CoreUIStyleCreator.CreateStyle<WindowStyle>();
        }

        [MenuItem(CreateMenu + "/Image style", false, 1102)]
        public static void CreateImageStyle()
        {
            CoreUIStyleCreator.CreateStyle<ImageStyle>();
        }

        [MenuItem(CreateMenu + "/Flexible image style", false, 1102)]
        public static void CreateFlexibleImageStyle()
        {
            CoreUIStyleCreator.CreateStyle<FlexibleImageStyle>();
        }

        [MenuItem(CreateMenu + "/Slider style", false, 1103)]
        public static void CreateSliderStyle()
        {
            CoreUIStyleCreator.CreateStyle<SliderStyle>();
        }

        [MenuItem(CreateMenu + "/Button style", false, 1104)]
        public static void CreateButtonStyle()
        {
            CoreUIStyleCreator.CreateStyle<ButtonStyle>();
        }

        [MenuItem(CreateMenu + "/Scroll style", false, 1105)]
        public static void CreateScrollStyle()
        {
            CoreUIStyleCreator.CreateStyle<ScrollStyle>();
        }

        [MenuItem(CreateMenu + "/Font", false, 1106)]
        public static void CreateFont()
        {
            var window = Dialog.ShowDialog<CreateFontDialogWindow>("Create font", DialogType.YesNo);
            window.Yes += sender =>
            {
                var font = CoreUIStyleCreator.CreateStyle<CoreUIFont>(sender.Name);
                var material = new Material(Shader.Find("CoreUI/CoreUITextMeshShader"));
                material.SetTexture("_MainTex", sender.Texture);
                font.Material = material;
                ObjectCreatorHelper.CreateAsset(material, string.Format("{0}.mat", sender.Name));
                font.Texture = sender.Texture;
            }; 
        }

        [MenuItem(CreateMenu + "/Styles repository", false, 1000)]
        public static void CreateStylesRepository()
        {
            ObjectCreatorHelper.CreateAsset<StylesRepository>();
        }

        [MenuItem(CreateMenu + "/Settings container", false, 1001)]
        public static void CreateSettingsContainer()
        {
            ObjectCreatorHelper.CreateAsset<PresentationSettings>();
        }

        [MenuItem(CoreUIWindows + "/Font editor", false, 0)]
        public static void OpenFontEditorWindow()
        {
            FontEditorWindow.ShowSelf();
        }
    }
}
