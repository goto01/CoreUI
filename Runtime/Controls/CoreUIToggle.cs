﻿using System;
using UnityEngine;

namespace CoreUI
{
	public class CoreUIToggle : CoreUIButton
	{
		private bool _pressed;
		private Action<bool> _action;

		public bool Toggled
		{
			get { return _pressed; }
			set { _pressed = value; }
		}
		
		public override Texture2D Texture
		{
			get { return _pressed ? _pressedTexture : _unpressedTexture; }
			set { base.Texture = value; }
		}

		public CoreUIToggle(BaseCoreUIMesh mesh) : base(mesh)
		{
			
		}

		public CoreUIToggle(BaseCoreUIMesh mesh, Action<bool> action) : base(mesh, null)
		{
			_action = action;
		}

		public CoreUIToggle(BaseCoreUIMesh mesh, Action<bool> action, bool pressed) : this(mesh, null)
		{
			_pressed = pressed;
			_action = action;
		}

		protected override void InvokePressing()
		{
			_pressed = !_pressed;
			if (_action != null) _action.Invoke(_pressed);
		}
	}
}