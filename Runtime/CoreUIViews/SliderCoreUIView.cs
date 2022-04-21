using UnityEngine;

namespace CoreUI
{
	public sealed class SliderCoreUIView : BaseCoreUIView
	{
		[SerializeField] private CoreUIOrientation _orientation;
		
		protected override CoreUIElement DrawElementInternal(CoreUIContainer parentContainer)
		{
			return CoreUIEditor.Instance.Slider(Rect, parentContainer, _orientation, Style);
		}
	}
}