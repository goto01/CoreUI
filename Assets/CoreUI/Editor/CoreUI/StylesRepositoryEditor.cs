using System;
using Editor.CoreUI.Windows.DialogWindows;
using Editor.Windows.DialogWindows;
using UICore.StylesSystem.Repository;
using UICore.StylesSystem.Styles;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Assets.Editor.CoreUI
{
	[CustomEditor(typeof(StylesRepository))]
	public class StylesRepositoryEditor : UnityEditor.Editor
	{
		private SerializedProperty _styles;
		private ReorderableList _stylesReordableList;
		
		private bool ReordableListInited{get { return _stylesReordableList != null; }}
		
		public override void OnInspectorGUI()
		{
			FindProperties();
			Init();
			DrawInspector();
			serializedObject.ApplyModifiedProperties();
		}

		private void FindProperties()
		{
			_styles = serializedObject.FindProperty("_styles");
		}

		private void Init()
		{
			if (!ReordableListInited)
			{
				_stylesReordableList = new ReorderableList(serializedObject, _styles, false, true, true, true);
				_stylesReordableList.drawElementCallback += DrawElementCallback;
				_stylesReordableList.drawHeaderCallback += DrawHeaderCallback;
				_stylesReordableList.onAddCallback += OnAddCallback;
				_stylesReordableList.onRemoveCallback += OnRemoveCallback;
			}
		}

		private void OnRemoveCallback(ReorderableList list)
		{
			var window = Dialog.ShowDialog<YesNoDialogWindow>("Delete style", DialogType.YesNo);
			window.Message = "Are you sure?";
			window.Yes += RemoveElement;
		}

		private void RemoveElement(YesNoDialogWindow sender)
		{
			if (_styles.GetArrayElementAtIndex(_stylesReordableList.index) != null) _styles.DeleteArrayElementAtIndex(_stylesReordableList.index); 
			_styles.DeleteArrayElementAtIndex(_stylesReordableList.index);
			serializedObject.ApplyModifiedProperties();
		}

		private void OnAddCallback(ReorderableList list)
		{
			Dialog.ShowDialog<AddStyleDialogWindow>("Add style", DialogType.YesNo).Yes += AddElement;
		}

		private void AddElement(AddStyleDialogWindow sender)
		{
			var index = _styles.arraySize;
			_styles.arraySize++;
			_styles.GetArrayElementAtIndex(index).objectReferenceValue = sender.Style;
			serializedObject.ApplyModifiedProperties();
		}

		private void DrawHeaderCallback(Rect rect)
		{
			GUI.Label(rect, "Styles");
		}

		private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
		{
			GUI.Label(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), string.Format("{0} -Style", index));
			if (_styles.GetArrayElementAtIndex(index).objectReferenceValue == null) return;
			var width = Mathf.Max(80, (rect.width - 60)/2);
			EditorGUI.ObjectField(new Rect(rect.x + 60, rect.y, width, EditorGUIUtility.singleLineHeight), _styles.GetArrayElementAtIndex(index).objectReferenceValue,
				typeof(BaseStyle), false);
			GUI.Label(new Rect(rect.x + 60 + width, rect.y, width, rect.height), string.Format("Type: {0}", _styles.GetArrayElementAtIndex(index).objectReferenceValue.GetType().Name));
			if (isFocused) GUI.Label(new Rect(10, 40, 100, 40), GUI.tooltip);
		}

		private void DrawInspector()
		{
			EditorGUILayout.LabelField("Styles repository", EditorStyles.boldLabel);
			EditorGUILayout.LabelField(string.Format("Number of styles {0}", _styles.arraySize));
			EditorGUILayout.Space();
			serializedObject.Update();
			_stylesReordableList.DoLayoutList();
		}
	}
}