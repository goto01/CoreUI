using UICore.StylesSystem.Styles;
using UnityEngine;

namespace UICore.UICoreMeshes.Meshes
{
    public class SliderMesh : FlexibleImageMesh
    {
        private Texture2D _point;

        public Texture2D Point { get { return _point; } }

        public SliderMesh(CoreUIOrientation orientation) : base(orientation)
        {
        }

        public override void Init(BaseStyle style, Rect rect)
        {
            _point = (style as SliderStyle).Point;
            base.Init(style, rect);
        }
    }
}
