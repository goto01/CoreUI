using UnityEngine;

namespace UICore.StylesSystem.Styles
{
    public class SliderStyle : FlexibleImageStyle
    {
        [SerializeField] private Texture2D _point;

        public Texture2D Point { get { return _point; } }
    }
}
