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

        public WindowMesh CreateWindow(Rect rect, string windowStyleName)
        {
            return CreateMesh<WindowMesh>(windowStyleName, rect);
        }

        public RectangleMesh CreateImage(Rect rect, string imageStyleName)
        {
            return CreateMesh<RectangleMesh>(imageStyleName, rect);
        }

        public T CreateMesh<T>(string styleName, Rect rect) where T: BaseCoreUIMesh, new()
        {
            var mesh = new T();
            mesh.Init(_styles[styleName], rect);
            return mesh;
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
