using UnityEngine;

namespace CoreUI
{
    public class SliderStyle : FlexibleImageStyle
    {
        [SerializeField] private Texture2D _point;

        public Texture2D Point { get { return _point; } }
    }
}
