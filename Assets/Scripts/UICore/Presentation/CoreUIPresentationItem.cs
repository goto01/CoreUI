using Assets.Scripts.UICore.Controls;
using UnityEngine;

namespace Assets.Scripts.UICore.Presentation
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    class CoreUIPresentationItem : MonoBehaviour
    {
        private const string ShaderPath = "CoreUI/CoreUISimpleShader";
        private const string TextureName = "_MainTex";

        private MeshFilter _mesh;
        private MeshRenderer _renderer;
        private CoreUIElement _element;

        public void Init(CoreUIElement element)
        {
            _mesh = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
            _element = element;
            _mesh.mesh = element.Mesh;
            InitMaterial();
        }

        protected virtual void Update()
        {
            _element.Update();
            UpdateTexture();
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
    }
}
