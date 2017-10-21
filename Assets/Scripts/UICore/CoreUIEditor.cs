using Assets.Scripts.Singleton;
using Assets.Scripts.UICore.Controls;
using Assets.Scripts.UICore.Controls.Containers;
using Assets.Scripts.UICore.Presentation;
using Assets.Scripts.UICore.StylesSystem.Repository;
using Assets.Scripts.UICore.UICoreMeshes.Factory;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts.UICore
{
    public class CoreUIEditor : SingletonMonoBahaviour<CoreUIEditor>
    {
        private const string DefaultWindowStyle = "Default Window Style";
        private const string DefaultImageStyle = "Default Image Style";
        private const string DefaultFlexibleImageStyle = "Default Flexible Image Style";

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

        public CoreUIFlexibleImage FlexibleImage(Rect rect, FlexiblaImageOrientation orientation, string styleName = DefaultFlexibleImageStyle)
        {
            var mesh = _factory.CreateFlexibleImage(rect, orientation, styleName);
            var element = new CoreUIFlexibleImage(mesh);
            CreatePresentationItem(element);
            return element;
        }

        public CoreUIFlexibleImage FlexibleImage(Rect rect, string styleName = DefaultFlexibleImageStyle)
        {
            return FlexibleImage(rect, FlexiblaImageOrientation.Horizontal, styleName);
        }

        private void CreatePresentationItem(CoreUIElement element)
        {
            var presentationItem = Instantiate(_presentationPrefab);
            presentationItem.Init(element);
        }
    }
}
