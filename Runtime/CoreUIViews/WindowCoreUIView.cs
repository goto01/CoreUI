namespace CoreUI
{
	public sealed class WindowCoreUIView : BaseCoreUIView
	{
		protected override CoreUIElement DrawElementInternal(CoreUIContainer parentContainer)
		{
			if (parentContainer != null) return CoreUIEditor.Instance.Window(Rect, parentContainer, Style);
			return CoreUIEditor.Instance.Window(Rect, Style);
		}
	}
}