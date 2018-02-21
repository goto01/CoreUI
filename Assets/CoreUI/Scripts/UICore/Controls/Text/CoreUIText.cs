using UICore.UICoreMeshes.Meshes;
using UnityEngine;
using TextMesh = UICore.UICoreMeshes.Meshes.Text.TextMesh;

namespace UICore.Controls.Text
{
	public class CoreUIText : CoreUIElement
	{
		private TextMesh _textMesh;

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
		
		public override bool Update(CoreUIEvent e)
		{
			_textMesh.TextGenerator.Update();
			_textMesh.ApplyTextMesh();
			return base.Update(e);
		}

		public void ShowSymbols(int start, int symbols)
		{
			_textMesh.TextGenerator.ShowSymbols(start, symbols);
		}
	}
}