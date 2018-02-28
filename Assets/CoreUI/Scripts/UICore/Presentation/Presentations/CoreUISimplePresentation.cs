using UICore.Controls;
using UICore.Controls.Text;
using UnityEngine;

namespace UICore.Presentation.Presentations
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class CoreUISimplePresentation : MonoBehaviour
    {
        private const string ShaderPath = "CoreUI/CoreUISimpleShader";
        private const string TextureName = "_MainTex";
        private const string YTopLimit = "_YTopLimit";
        private const string YBottomLimit = "_YBottomLimit";
        private const string XLeftLimit = "_XLeftLimit";
        private const string XRightLimit = "_XRightLimit";
        private const string Color = "_Color";

        private MeshFilter _mesh;
        private MeshRenderer _renderer;
        protected CoreUIElement _element;
        
        public bool Active
        {
            get { return _element.Active; }
            set { _element.Active = value; }
        }

        public bool Enabled
        {
            get { return _element.Enabled; }
            set { _element.Enabled = value; }
        }

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
            UpdateActive();
            UpdateVertialLimit();
            _mesh.mesh = _element.Mesh;
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

        private void UpdateActive()
        {
            _renderer.material.SetColor(Color, Active ? _element.Color : _element.Color * CoreUIPresentation.Instance.InactiveTintColor);
            _renderer.enabled = Enabled;
        }

        private void UpdateVertialLimit()
        {
            _renderer.material.SetFloat(YTopLimit, _element.VerticalTopLimit);
            _renderer.material.SetFloat(YBottomLimit, _element.VerticalBottomLimit);
            _renderer.material.SetFloat(XLeftLimit, _element.HorizontalLeftLimit);
            _renderer.material.SetFloat(XRightLimit, _element.HorizontalRightLimit);
        }
    }
}
