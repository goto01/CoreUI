using UnityEngine;

namespace CoreUI
{
	public class ImageCoreUIView : BaseCoreUIView
	{
		[SerializeField] private Texture2D _texture;
		
		protected override CoreUIElement DrawElementInternal(CoreUIContainer parentContainer)
		{
			return CoreUIEditor.Instance.Image(Rect, parentContainer, _texture, Style);
		}
	}
}