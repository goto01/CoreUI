using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoreUI
{
	[EditorTool("Element editor", typeof(BaseCoreUIView))]
	public class BaseCoreUIViewControl : EditorTool
	{
		private const float SmallButtonSize = 20;
		private const float SizeLabelWidth = 50;
		private const float SmallSpace = 2;
		
		public override void OnToolGUI(EditorWindow window)
		{
			if (!(window is SceneView sceneView))
				return;
			if (!(target is BaseCoreUIView element)) return;
			var position = sceneView.camera.WorldToScreenPoint(element.transform.position);
			Handles.BeginGUI();
			position.y = sceneView.position.height - position.y - 20f;
			GUI.Label(new Rect(position, new Vector2(SizeLabelWidth, SmallButtonSize)), "Width:");
			GUI.Label(new Rect(position + new Vector3(0, SmallButtonSize + SmallSpace), new Vector2(SizeLabelWidth, 20)), "Height:");
			GUI.Label(new Rect(position + new Vector3(0, SmallButtonSize * 2 + SmallSpace * 2), new Vector2(SizeLabelWidth, SmallButtonSize)), "X:");
			GUI.Label(new Rect(position + new Vector3(0, SmallButtonSize * 3 + SmallSpace * 3), new Vector2(SizeLabelWidth, SmallButtonSize)), "Y:");
			if (GUI.Button(new Rect(position + new Vector3(SizeLabelWidth, 0), new Vector2(SmallButtonSize, SmallButtonSize)), "+"))
			{
				Undo.RecordObject(element, "Inc element width");
				element.WidthPixels++;
				EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
			}
			if (GUI.Button(new Rect(position + new Vector3(SmallButtonSize + SmallSpace + SizeLabelWidth, 0), new Vector2(SmallButtonSize, SmallButtonSize)), "-"))
			{
				Undo.RecordObject(element, "Dec element width");
				element.WidthPixels--;
				EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
			}
			if (GUI.Button(new Rect(position + new Vector3(SizeLabelWidth, SmallButtonSize + SmallSpace), new Vector2(SmallButtonSize, SmallButtonSize)), "+"))
			{
				Undo.RecordObject(element, "Inc element height");
				element.HeightPixels++;
				EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
			}
			if (GUI.Button(new Rect(position + new Vector3(SmallButtonSize + SmallSpace + SizeLabelWidth, SmallButtonSize + SmallSpace), new Vector2(SmallButtonSize, SmallButtonSize)), "-"))
			{
				Undo.RecordObject(element, "Dec element height");
				element.HeightPixels--;
				EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
			}
			if (GUI.Button(new Rect(position + new Vector3(SizeLabelWidth, SmallButtonSize * 2 + SmallSpace * 2), new Vector2(SmallButtonSize, SmallButtonSize)), "+"))
			{
				Undo.RecordObject(element.gameObject, "Inc element X");
				var p = element.transform.position;
				p.x += element.PixelSizeEditor;
				element.transform.position = p;
				EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
			}
			if (GUI.Button(new Rect(position + new Vector3(SmallButtonSize + SmallSpace + SizeLabelWidth, SmallButtonSize * 2 + SmallSpace * 2), new Vector2(SmallButtonSize, SmallButtonSize)), "-"))
			{
				Undo.RecordObject(element.gameObject, "Dec element X");
				var p = element.transform.position;
				p.x -= element.PixelSizeEditor;
				element.transform.position = p;
				EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
			}
			if (GUI.Button(new Rect(position + new Vector3(SizeLabelWidth, SmallButtonSize * 3 + SmallSpace * 3), new Vector2(SmallButtonSize, SmallButtonSize)), "+"))
			{
				Undo.RecordObject(element.gameObject, "Inc element Y");
				var p = element.transform.position;
				p.y += element.PixelSizeEditor;
				element.transform.position = p;
				EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
			}
			if (GUI.Button(new Rect(position + new Vector3(SmallButtonSize + SmallSpace + SizeLabelWidth, SmallButtonSize * 3 + SmallSpace * 3), new Vector2(SmallButtonSize, SmallButtonSize)), "-"))
			{
				Undo.RecordObject(element.gameObject, "Dec element Y");
				var p = element.transform.position;
				p.y -= element.PixelSizeEditor;
				element.transform.position = p;
				EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
			}
			Handles.EndGUI();
		}
	}
}