using System.Collections.Generic;
using System.Text;
using UICore.StylesSystem.Styles.Font;
using UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace UICore.Controls.Text
{
	public class CoreUITextField : CoreUIText
	{
		private List<char> _symbols = new List<char>();
		private StringBuilder _stringBuilder = new StringBuilder();
		private bool _focusedForEdit;
		private CoreUIFlexibleImage _cursor;
		
		public CoreUITextField(BaseCoreUIMesh mesh, CoreUIFlexibleImage cursor) : base(mesh)
		{
			_cursor = cursor;
			_focusedForEdit = false;
		}

		public CoreUITextField(BaseCoreUIMesh mesh, int sinPixelsOffset, float sinOffsetSpeed, float sinMultiplier, float horizontalPixelsOffset, float verticalPixelsOffset) : 
			base(mesh, sinPixelsOffset, sinOffsetSpeed, sinMultiplier, horizontalPixelsOffset, verticalPixelsOffset)
		{
		}

		public override bool Update(CoreUIEvent e)
		{
			var focus = base.Update(e);
			if (focus && e.PointerDown) _focusedForEdit = true;
			if (_focusedForEdit && !focus && e.PointerDown) _focusedForEdit = false;
			if (_focusedForEdit)
			{
				var text = Input.inputString;
				if (text.Length != 0)
				{
					var font = (CoreUIFont) _coreUIMesh.Style;
					_symbols.Clear();
					var hasBackspace = false;
					for (var index = 0; index < text.Length; index++)
					{
						if (text[index] == '')
						{
							hasBackspace = true;
							continue;
						} 
						if (!font.ContainsSymbol(text[index]) && text[index] != ' ' && text[index] != ' ') continue;
						_symbols.Add(text[index]);
					}
					if (hasBackspace && Text.Length > 0) Text = Text.Substring(0, Text.Length - 1);
					_stringBuilder.Clear();
					for (var index = 0; index < _symbols.Count; index++)
						_stringBuilder.Append(_symbols[index]);
					Text += _stringBuilder.ToString();
					_cursor.OriginWidth = _coreUIMesh.Mesh.bounds.size.y;
				}
			}
			return focus;
		}
	}
}