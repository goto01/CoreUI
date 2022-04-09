using UnityEditor;
using UnityEngine;

namespace Editor.Windows.DialogWindows
{
	public static class Dialog
	{
		private const string Yes = "Yes";
		private const string No = "No";
		private const int Width = 200;
		private const int Height = 200;
		
		public static T ShowDialog<T>(string title, DialogType dialogType) where T : BaseDialogWindow<T>
		{
			var window = ScriptableObject.CreateInstance<T>();
			var parentRect = EditorWindow.focusedWindow.position;
			window.ShowUtility();
			window.titleContent = new GUIContent(title);
			window.Init(dialogType, Yes, No, true, true, parentRect, new Vector2(Width, Height));
			window.UpdatePosition();
			return window;
		}
	}
}