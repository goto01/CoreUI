﻿using UICore.UICoreMeshes.Generators;
using UICore.UICoreMeshes.Meshes;
using UnityEngine;
using TextMesh = UICore.UICoreMeshes.Meshes.Text.TextMesh;

namespace UICore.Controls.Text
{
	public abstract class CoreUIText : CoreUIElement
	{
		private TextMesh _textMesh;

		public Color FontColor
		{
			get { return _textMesh.TextGenerator.Color; }
			set { _textMesh.TextGenerator.UpdateColors(value); }
		}

		public string Text
		{
			get { return _textMesh.TextGenerator.Text; }
			set
			{
				_textMesh.TextGenerator.GenerateMeshData(value);
				_textMesh.ApplyTextMesh();
				_textMesh.UpdateMeshInfo();
			}
		}
		
		protected CoreUITextGenerator TextGenerator { get { return _textMesh.TextGenerator; } }
		
		public CoreUIText(BaseCoreUIMesh mesh) : base(mesh)
		{
			_textMesh = (TextMesh) mesh;
			_textMesh.TextGenerator.InitEffects(0, 0, 0, 0, 0);
		}

		public CoreUIText(BaseCoreUIMesh mesh, int sinPixelsOffset, float sinOffsetSpeed, float sinMultiplier,
			float horizontalPixelsOffset, float verticalPixelsOffset) : this(mesh)
		{
			_textMesh.TextGenerator.InitEffects(sinPixelsOffset, sinOffsetSpeed, sinMultiplier, horizontalPixelsOffset, verticalPixelsOffset);
		}
		
		public override bool Update(ref CoreUIEvent e)
		{
			_textMesh.TextGenerator.Update();
			_textMesh.ApplyTextMesh();
			return base.Update(ref e);
		}

		public void ShowSymbols(int start, int symbols)
		{
			_textMesh.TextGenerator.ShowSymbols(start, symbols);
		}
	}
}