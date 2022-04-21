using System;
using UnityEngine;

namespace CoreUI
{
	public class FlexibleImageCoreUIView : BaseCoreUIView
	{
		[SerializeField] private CoreUIOrientation _orientation;
		[SerializeField] [Range(0, 1)] private float _fill;
		
		protected override CoreUIElement DrawElementInternal(CoreUIContainer parentContainer)
		{
			return CoreUIEditor.Instance.FlexibleImage(Rect, parentContainer, _orientation, Style);
		}

		protected override void Update()
		{
			var element = (CoreUIFlexibleImage) CurrentUIElement;
			base.Update();
			if (Math.Abs(element.Value - _fill) > Mathf.Epsilon)
			{
				element.Value = _fill;
			}
		}

		protected override void TryResize()
		{
			var element = (CoreUIFlexibleImage) CurrentUIElement;
			element.ResizeOriginSize(Width, Height);
		}
	}
}