using Editor.Windows.DialogWindows;
using UnityEditor;
using UnityEngine;

namespace Editor.CoreUI.Windows.DialogWindows
{
	public class CreateFontDialogWindow : BaseDialogWindow<CreateFontDialogWindow>
	{
		private const int Width = 400;
		private const int Height = 150;
		
		public string Name;
		public Texture2D Texture;

		protected override void DrawContentEditor()
		{
			_size.x = Width;
			_size.y = Height;
			Name = EditorGUILayout.TextField("Name", Name);
			Texture = EditorGUILayout.ObjectField("Texture", Texture, typeof(Texture2D), false) as Texture2D;
			_yesPossible = Texture != null && !string.IsNullOrEmpty(Name);
		}
	}
}