using UnityEngine;

namespace CoreUI
{
	public class ToggleCoreUIView : BaseCoreUIView
	{
		private bool _prevPressed;
		[SerializeField] private bool _pressed;

		public bool Pressed
		{
			get { return _pressed; }
			set { _pressed = value; }
		}
		
		protected override CoreUIElement DrawElementInternal(CoreUIContainer parentContainer)
		{
			_prevPressed = _pressed;
			return CoreUIEditor.Instance.Toggle(Rect, _pressed, parentContainer, OnClick, Style);
		}

		protected override void Update()
		{
			if (_pressed != _prevPressed)
			{
				var element = (CoreUIToggle) CurrentUIElement;
				element.Toggled = _pressed;
			}
			_prevPressed = _pressed;
			base.Update();
		}

		private void OnClick(bool pressed)
		{
			_pressed = _prevPressed = pressed;
		}
	}
}