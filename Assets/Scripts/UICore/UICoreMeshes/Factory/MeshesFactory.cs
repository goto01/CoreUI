using System.Collections.Generic;
using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.StylesSystem.Styles;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Factory
{
    public class MeshesFactory
    {
        private Dictionary<string, BaseStyle> _styles;

        public MeshesFactory(StylesRepository repository)
        {
            InitStyles(repository);
        }

        public WindowMesh CreateWindow(Rect rect, string styleName)
        {
            var window = new WindowMesh();
            window.Init(GetStyle(styleName), rect);
            return window;
        }

        public RectangleMesh CreateImage(Rect rect, string styleName)
        {
            var image = new RectangleMesh();
            image.Init(GetStyle(styleName), rect);
            return image;
        }

        public FlexibleImageMesh CreateFlexibleImage(Rect rect, FlexiblaImageOrientation orientation, string styleName)
        {
            var flexibleImage = new FlexibleImageMesh(orientation);
            flexibleImage.Init(GetStyle(styleName), rect);
            return flexibleImage;
        }

        private BaseStyle GetStyle(string styleName)
        {
            return _styles[styleName];
        }
        
        private void InitStyles(StylesRepository repository)
        {
            _styles = new Dictionary<string, BaseStyle>();
            for (var index = 0; index < repository.Styles.Length; index++)
            {
                var style = repository.Styles[index];
                if (_styles.ContainsKey(style.name))
                {
                    Debug.LogErrorFormat("You have several styles with name - {0}", style.name);
                    return;
                }
                _styles[style.name] = style;
            }
        }
    }
}
