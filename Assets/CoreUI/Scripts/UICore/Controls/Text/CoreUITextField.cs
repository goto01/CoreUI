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
		private float _timer;
		
		public CoreUITextField(BaseCoreUIMesh mesh, CoreUIFlexibleImage cursor) : base(mesh)
		{
			_cursor = cursor;
			_focusedForEdit = false;
			_cursor.OriginWidth = _coreUIMesh.Mesh.bounds.size.y * 1.5f;
			_cursor.Enabled = false;
			_timer = 0;
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
				_timer = 0;
				e.ReleasePointerDown();
				_index = Mathf.Max(0, Text.Length);
				var minDelta = float.MaxValue;
				var horizontalOffset = e.PointerPosition.x - Position.x;
				for (var index = 0; index <= Text.Length; index++)
				{
					var delta = Mathf.Abs(horizontalOffset - TextGenerator.GetTextWidth(index));
					if (minDelta < delta)
					{
						_index = index - 1;
						break;
					}
					minDelta = delta;
				}
			}
			if (_focusedForEdit && !focus && e.HasPointerDown) _focusedForEdit = _cursor.Enabled = false;
			if (_focusedForEdit)
			{
				_timer += e.DeltaTime;
				_cursor.Value = Mathf.Pow(_timer / .2f, .2f);
				if (_timer > .2f)
				{
					_timer = 0;
					_cursor.Enabled = !_cursor.Enabled;
				}
				if (e.LeftArrowDown)
				{
					_index = Mathf.Max(0, _index - 1);
					e.ReleaseLeftArrowDown();
				}
				if (e.RightArrowDown)
				{
					_index = Mathf.Min(Text.Length, _index + 1);
					e.ReleaseRightArrowDown();
				}
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
				if (e.DeleteDown && _index < Text.Length)
				{
					_stringBuilder.Clear();
					_stringBuilder.Append(Text);
					_stringBuilder.Remove(_index, 1);
					Text = _stringBuilder.ToString();
					e.ReleaseDeleteDown();
				}
			}
			return focus;
		}
	}
}