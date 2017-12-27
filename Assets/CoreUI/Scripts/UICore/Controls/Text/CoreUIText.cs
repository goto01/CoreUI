using UICore.UICoreMeshes.Meshes;
using UICore.UICoreMeshes.Meshes.Text;

namespace UICore.Controls.Text
{
	public class CoreUIText : CoreUIElement
	{
		private TextMesh _textMesh;
		
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
			_textMesh.UpdateMeshInfo();
			return base.Update(e);
		}
	}
}