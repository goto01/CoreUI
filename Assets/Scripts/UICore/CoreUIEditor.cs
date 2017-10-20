using Assets.Scripts.Singleton;
using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.UICoreMeshes.Factory;
using UnityEngine;

namespace Assets.Scripts.UICore
{
    public class CoreUIEditor : SingletonMonoBahaviour<CoreUIEditor>
    {
        private const string DefaultWindow = "Default Window Style";

        private MeshesFactory _factory;
        [SerializeField] private StylesRepository _repository;
        
        public override void AwakeSingleton()
        {
            _factory = new MeshesFactory(_repository);
            DontDestroyOnLoad(gameObject);
        }

        public CoreUIWindow Window(Rect rect, string styleName = DefaultWindow)
        {
            var mesh = _factory.CreateWindow(rect, styleName);
            return new CoreUIWindow(mesh);
        }
    }
}
