using UnityEngine;

namespace CoreUI
{
	public sealed class ScrollCoreUIView : BaseCoreUIView
	{
		[Header("Scroll")] 
		[SerializeField] private float _actualScrollWidthPixels;
		[SerializeField] private float _actualScrollHeightPixels;
		[SerializeField] private SliderCoreUIView _verticalSlider;
		[SerializeField] private SliderCoreUIView _horizontalSlider;
		private Vector2 _prevLocalPosition;

		protected override void Awake()
		{
			base.Awake();
			_prevLocalPosition = Transform.localPosition;
		}
		
		protected override CoreUIElement DrawElementInternal(CoreUIContainer parentContainer)
		{
			var actualRect = Rect;
			actualRect.width = _actualScrollWidthPixels * PixelSize;
			actualRect.height = _actualScrollHeightPixels * PixelSize;
			return CoreUIEditor.Instance.Scroll(actualRect, Width, Height, null, null, parentContainer, Style);
		}

		protected override void Update()
		{
			var scroll = (CoreUIScroll) CurrentUIElement;
			var verticalValue = GetVerticalValue();
			var horizontalValue = GetHorizontalValue();
			var localPosDelta = ((Vector2)Transform.localPosition - _prevLocalPosition).sqrMagnitude;
			if (Mathf.Abs(scroll.ScrollVerticalValue - verticalValue) > Mathf.Epsilon ||
			    Mathf.Abs(scroll.ScrollHorizontalValue - horizontalValue) > Mathf.Epsilon)
			{
				SetScrollValues(scroll, verticalValue, horizontalValue);
				Transform.position = scroll.Position;
			} else if (Mathf.Abs(localPosDelta) > Mathf.Epsilon)
			{
				SetScrollValues(scroll, verticalValue, horizontalValue);
				scroll.ResetParentPosition(_prevLocalPosition, Transform.localPosition);
			}
			SetScrollValues(scroll, verticalValue, horizontalValue);
			UpdateActualResize(scroll);
			_prevLocalPosition = Transform.localPosition;
			scroll.ResizeActualSize(_actualScrollWidthPixels * PixelSize, _actualScrollHeightPixels * PixelSize);
			base.Update();
		}

		private void UpdateActualResize(CoreUIScroll scroll)
		{
			var newActualWidth = _actualScrollWidthPixels * PixelSize;
			var newActualHeight = _actualScrollHeightPixels * PixelSize;
			if (Mathf.Abs(scroll.Width - newActualWidth) > Mathf.Epsilon ||
			    Mathf.Abs(scroll.Height - newActualHeight) > Mathf.Epsilon)
			{
				scroll.ResizeActualSize(newActualWidth, newActualHeight);
				Transform.position = scroll.Position;
			}
		}
		
		private float GetVerticalValue()
		{
			if (_verticalSlider == null) return 0;
			var slider = (CoreUISlider) _verticalSlider.CurrentUIElement;
			return 1 - slider.Value;
		}
		
		private float GetHorizontalValue()
		{
			if (_horizontalSlider == null) return 0;
			var slider = (CoreUISlider) _horizontalSlider.CurrentUIElement;
			return slider.Value;
		}

		private void SetScrollValues(CoreUIScroll scroll, float verticalValue, float horizontalValue)
		{
			scroll.ScrollVerticalValue = verticalValue;
			scroll.ScrollHorizontalValue = horizontalValue;
		}
	}
}