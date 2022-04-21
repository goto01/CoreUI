using System;
using Staff;
using UnityEditor;
using UnityEngine;

namespace Editor.Windows.DialogWindows
{
	public enum DialogType
	{
		YesNo,
		Yes,
		No,
	}
	
	public abstract class BaseDialogWindow<T> : EditorWindow where T : BaseDialogWindow<T>
	{
		private DialogType _dialogType;
		private string _yes;
		private string _no;
		private bool _closeOnNo;
		private bool _closeOnYes;
		private Rect _parentRect;
		protected Vector2 _size;
		protected bool _yesPossible;
		protected bool _noPossible;
		
		public event Staff.EventHandler<T> Yes;
		public event Staff.EventHandler<T> No;
		
		public void Init(DialogType dialogType, string yes, string no, bool closeOnYes, bool closeOnNo, Rect parentRect, Vector2 size)
		{
			_parentRect = parentRect;
			_size = size;
			_yesPossible = true;
			_noPossible = true;
			_dialogType = dialogType;
			_yes = yes;
			_no = no;
			_closeOnNo = closeOnNo;
			_closeOnYes = closeOnYes;
		}

		protected virtual void OnGUI()
		{
			UpdatePosition();
			DrawContentEditor();
			DrawYesNoButtons();
		}

		private void OnDestroy()
		{
			Yes = null;
			No = null;
		}

		public void UpdatePosition()
		{
			position = new Rect(_parentRect.x + _parentRect.width/2f - _size.x/2f,
				_parentRect.y  + _parentRect.height/2f - _size.y/2f, _size.x, _size.y);
		}

		protected abstract void DrawContentEditor();

		private void DrawYesNoButtons()
		{
			if (_dialogType == DialogType.YesNo)
			{
				EditorGUILayout.BeginHorizontal();
				DrawYesButton();
				DrawNoButton();
				EditorGUILayout.EndHorizontal();
				return;
			}
			DrawYesButton();
			DrawNoButton();
		}
		
		private void DrawYesButton()
		{
			if (_dialogType == DialogType.No) return;
			GUI.enabled = _yesPossible;
			if (!GUILayout.Button(_yes)) return;
			Yes.Raise((T)this);
			if (_closeOnYes) Close();
		}

		private void DrawNoButton()
		{
			if (_dialogType == DialogType.Yes) return;
			GUI.enabled = _noPossible;
			if (!GUILayout.Button(_no)) return;
			No.Raise((T)this);
			if (_closeOnNo) Close();
		}
	}
}