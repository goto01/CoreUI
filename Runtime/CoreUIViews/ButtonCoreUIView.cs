namespace CoreUI
{
	public class ButtonCoreUIView : BaseCoreUIView

	{
		protected override CoreUIElement DrawElementInternal(CoreUIContainer parentContainer)
		{
			return CoreUIEditor.Instance.Button(Rect, parentContainer, Action, Style);
		}

		private void Action(int obj)
		{
			
		}
	}
}