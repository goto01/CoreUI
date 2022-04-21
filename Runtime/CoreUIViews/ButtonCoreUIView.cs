using System;

namespace CoreUI
{
	public class ButtonCoreUIView : BaseCoreUIView
	{
		public event EventHandler Click; 

		protected override CoreUIElement DrawElementInternal(CoreUIContainer parentContainer)
		{
			return CoreUIEditor.Instance.Button(Rect, parentContainer, Action, Style);
		}

		private void Action(int obj)
		{
			var handler = Click;
			if (handler != null) handler(this, EventArgs.Empty);
		}
	}
}