using Assets.Scripts.UICore.Controls;
using UnityEngine;

namespace Assets.Scripts.UICore.Presentation.Presentations
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class CoreUISimplePresentation : MonoBehaviour
    {
        private const string ShaderPath = "CoreUI/CoreUISimpleShader";
        private const string TextureName = "_MainTex";

        private MeshFilter _mesh;
        private MeshRenderer _renderer;
        protected CoreUIElement _element;

        public virtual void Init(CoreUIElement element)
        {
            _mesh = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
            _element = element;
            _mesh.mesh = element.Mesh;
            InitMaterial();
        }

        public virtual void UpdateSelf(CoreUIEvent e)
        {
            UpdateTexture();
            UpdateQueue();
        }

        private void InitMaterial()
        {
            var material = new Material(Shader.Find(ShaderPath));
            _renderer.material = material;
            SetTexture();
        }

        private void UpdateTexture()
        {
            if (_element.TextureChanged) SetTexture();
        }

        private void SetTexture()
        {
            _renderer.material.SetTexture(TextureName, _element.Texture);
        }

        private void UpdateQueue()
        {
            _renderer.material.renderQueue = CoreUIPresentation.CoreUIQueue + _element.Order;
        }
    }
}
