using Assets.Scripts.Singleton;
using Assets.Scripts.UICore.Controls;
using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.Presentation;
using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.UICoreMeshes.Factory;
using UnityEngine;

namespace Assets.Scripts.UICore
{
    public class CoreUIEditor : SingletonMonoBahaviour<CoreUIEditor>
    {
        private const string DefaultWindowStyle = "Default Window Style";
        private const string DefaultImageStyle = "Default Image Style";

        private MeshesFactory _factory;
        [SerializeField] private StylesRepository _repository;
        [SerializeField] private CoreUIPresentationItem _presentationPrefab;
        
        public override void AwakeSingleton()
        {
            _factory = new MeshesFactory(_repository);
            DontDestroyOnLoad(gameObject);
        }

        public CoreUIWindow Window(Rect rect, string styleName = DefaultWindowStyle)
        {
            var mesh = _factory.CreateWindow(rect, styleName);
            var element = new CoreUIWindow(mesh);
            CreatePresentationItem(element);
            return element;
        }

        public CoreUIImage Image(Rect rect, Texture2D texture, string styleName = DefaultImageStyle)
        {
            var mesh = _factory.CreateImage(rect, styleName);
            var element = new CoreUIImage(mesh);
            CreatePresentationItem(element);
            return element;
        }

        private void CreatePresentationItem(CoreUIElement element)
        {
            var presentationItem = Instantiate(_presentationPrefab);
            presentationItem.Init(element);
        }
    }
}
