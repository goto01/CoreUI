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
		private int _index = 0;
		
		public CoreUITextField(BaseCoreUIMesh mesh, CoreUIFlexibleImage cursor) : base(mesh)
		{
			_cursor = cursor;
			_focusedForEdit = false;
			_cursor.OriginWidth = _coreUIMesh.Mesh.bounds.size.y * 1.5f;
			_cursor.Enabled = false;
		}

		public CoreUITextField(BaseCoreUIMesh mesh, int sinPixelsOffset, float sinOffsetSpeed, float sinMultiplier, float horizontalPixelsOffset, float verticalPixelsOffset) : 
			base(mesh, sinPixelsOffset, sinOffsetSpeed, sinMultiplier, horizontalPixelsOffset, verticalPixelsOffset)
		{
		}

		public override bool Update(ref CoreUIEvent e)
		{
			var focus = base.Update(ref e);
			if (focus && e.PointerDown)
			{
				_focusedForEdit = _cursor.Enabled = true;
				e.ReleasePointerDown();
				_index = Mathf.Max(0, Text.Length);
			}
			if (_focusedForEdit && !focus && e.HasPointerDown) _focusedForEdit = _cursor.Enabled = false;
			if (_focusedForEdit)
			{
				if (e.LeftArrowDown) _index = Mathf.Max(0, _index - 1);
				if (e.RightArrowDown) _index = Mathf.Min(Text.Length, _index + 1);
				var position = Position;
				position.x += TextGenerator.GetTextWidth(_index);
				position.y = _coreUIMesh.Mesh.bounds.center.y + _cursor.OriginWidth / 2f;
				_cursor.Position = position;
				var text = e.InputString;
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
					_stringBuilder.Clear();
					_stringBuilder.Append(Text);
					if (hasBackspace && Text.Length > 0 && _index > 0) _stringBuilder.Remove(_index-- - 1, 1);
					for (var index = 0; index < _symbols.Count; index++)
						_stringBuilder.Insert(_index, _symbols[index]);	
					_index += _symbols.Count;
					Text = _stringBuilder.ToString();
					_cursor.OriginWidth = _coreUIMesh.Mesh.bounds.size.y * 1.5f;
					e.ReleaseInputString();
				}
			}
			return focus;
		}
	}
}