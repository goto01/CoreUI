using UnityEngine;

namespace CoreUI
{
	public class LabelCoreUIView : BaseCoreUIView
	{
		[SerializeField] private string _text;
		
		protected override CoreUIElement DrawElementInternal(CoreUIContainer parentContainer)
		{
			return CoreUIEditor.Instance.Label(Rect, _text, parentContainer, Style);
		}
	}
}