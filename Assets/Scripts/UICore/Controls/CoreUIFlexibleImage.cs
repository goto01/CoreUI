using Assets.Scripts.UICore.UICoreMeshes.Meshes;

namespace Assets.Scripts.UICore.Controls
{
    public class CoreUIFlexibleImage : CoreUIElement
    {
        public float MinWidth{get { return ((FlexibleImageMesh) _coreUIMesh).MinWidth; } }

        public CoreUIFlexibleImage(FlexibleImageMesh mesh) : base(mesh)
        {
        }
    }
}
