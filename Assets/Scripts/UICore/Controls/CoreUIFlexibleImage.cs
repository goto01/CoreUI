using UICore.UICoreMeshes.Meshes;

namespace UICore.Controls
{
    public class CoreUIFlexibleImage : CoreUIElement
    {
        public float MinWidth{get { return ((FlexibleImageMesh) _coreUIMesh).MinWidth; } }

        public CoreUIFlexibleImage(FlexibleImageMesh mesh) : base(mesh)
        {
        }
    }
}
