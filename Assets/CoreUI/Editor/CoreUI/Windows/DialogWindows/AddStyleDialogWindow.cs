using CoreUI;
using Editor.Windows.DialogWindows;
using UnityEditor;
using UnityEngine;

namespace Editor.CoreUI.Windows.DialogWindows
{
	public class AddStyleDialogWindow : BaseDialogWindow<AddStyleDialogWindow>
	{
		private const int Width = 400;
		private const int Height = 100;
		
		public BaseStyle Style;

		protected override void DrawContentEditor()
		{
			_size.x = Width;
			_size.y = Height;
			Style = EditorGUILayout.ObjectField("Style", Style, typeof(BaseStyle), false) as BaseStyle;
			_yesPossible = Style != null;
		}
	}
}