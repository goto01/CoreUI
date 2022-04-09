using UnityEditor;
using UnityEngine;

namespace CoreUI.Editor.ComponentEditors
{
	[CustomEditor(typeof(CoreUIEditor))]
	public class CoreUIEditorEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			EditorGUILayout.LabelField(string.Format("CoreUI\nVersion: {0}", CoreUIEditor.Version), GUILayout.Height(32));
			if (GUILayout.Button("Github")) Application.OpenURL("https://github.com/goto01/CoreUI");
			EditorGUILayout.LabelField(string.Empty, GUILayout.Height(15));
			base.OnInspectorGUI();		
		}
	}
}