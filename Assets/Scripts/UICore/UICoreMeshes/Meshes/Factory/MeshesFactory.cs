using System.Collections.Generic;
using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.StylesSystem.Styles;
using UnityEngine;

namespace Assets.Scripts.UICore.UICoreMeshes.Meshes.Factory
{
    public class MeshesFactory
    {
        private const string DefaultWindow = "Default Window Style";

        private Dictionary<string, BaseStyle> _styles;

        public MeshesFactory(StylesRepository repository)
        {
            InitStyles(repository);
        }

        public WindowMesh CreateWindow(float width, float height, string windowStyleName = DefaultWindow)
        {
            return CreateMesh<WindowMesh>(windowStyleName, width, height);
        }

        public T CreateMesh<T>(string styleName, float width, float height) where T: BaseCoreUIMesh, new()
        {
            var mesh = new T();
            mesh.Init(_styles[styleName], width, height);
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
