namespace CoreUI
{
    public class FlexibleImageStyle : BaseStyle
    {
        public float BorderWidth { get { return _texture.width/4f*_pixelWidth; } }
        public float BorderHeight { get { return _texture.height*_pixelWidth; } }
    }
}
