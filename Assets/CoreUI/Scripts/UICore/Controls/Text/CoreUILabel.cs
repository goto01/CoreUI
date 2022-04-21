using UICore.UICoreMeshes.Meshes;

namespace UICore.Controls.Text
{
    public class CoreUILabel : CoreUIText
    {
        public CoreUILabel(BaseCoreUIMesh mesh) : base(mesh)
        {
            
        }

        public CoreUILabel(BaseCoreUIMesh mesh, int sinPixelsOffset, float sinOffsetSpeed, float sinMultiplier,
            float horizontalPixelsOffset, float verticalPixelsOffset) : base (mesh, sinPixelsOffset, sinOffsetSpeed, sinMultiplier, horizontalPixelsOffset, verticalPixelsOffset)
        {
            
        }
    }
}
