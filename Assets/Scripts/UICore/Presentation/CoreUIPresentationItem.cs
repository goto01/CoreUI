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
        }

        private void InitMaterial()
        {
            var material = new Material(Shader.Find(ShaderPath));
            material.SetTexture(TextureName, _element.Texture);
            _renderer.material = material;
        }
    }
}
