using Assets.Scripts.UICore.Controls;
using UnityEngine;

namespace Assets.Scripts.UICore.Presentation
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    class CoreUIPresentationItem : MonoBehaviour
    {
        private MeshFilter _mesh;
        private CoreUIElement _element;

        public void Init(CoreUIElement element)
        {
            _mesh = GetComponent<MeshFilter>();
            _element = element;
            _mesh.mesh = element.Mesh;
        }

        protected virtual void Update()
        {
            _element.Update();
        }
    }
}
