using Assets.Scripts.UICore;
using Assets.Scripts.UICore.UICoreMeshes.Meshes;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CoreUITest: MonoBehaviour
    {
        private const float Pixel = 1f / 32;
        private const float Space = Pixel * 2;
        private const float Tile = Pixel * 48;
        private const float BackgroundWindowSize = Pixel * 152;
        private const float SmallTile = Pixel * 32;
        private const float SmallSpace = Pixel * 9;

        [SerializeField] private Texture2D _texture0;
        [SerializeField] private Texture2D _texture1;
        [SerializeField] private Texture2D _texture2;
        [SerializeField] private Texture2D _texture3;
        [SerializeField] private Texture2D _texture4;
        [SerializeField] private Texture2D _texture5;
        [SerializeField] private Texture2D _texture6;
        [SerializeField] private Texture2D _texture7;
        [SerializeField] private Texture2D _texture8;
        [SerializeField] private Texture2D _uiBack;

        protected virtual void Awake()
        {
            CoreUIEditor.Instance.Window(new Rect(-1, 1, BackgroundWindowSize + 2, BackgroundWindowSize + 2), "Background Window Style");

            CoreUIEditor.Instance.Window(new Rect(Space, -Space * 2 - Tile, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space * 2 + Tile, -Space * 2 - Tile, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space * 3 + Tile * 2, -Space * 2 - Tile, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space, -Space * 3 - Tile * 2, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space * 2 + Tile, -Space * 3 - Tile * 2, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space * 3 + Tile * 2, -Space * 3 - Tile * 2, Tile, Tile), "Item Window Style");

            CoreUIEditor.Instance.Window(new Rect(Space + SmallSpace * 2 + SmallTile, -Space, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space * 2 + SmallSpace * 3 + SmallTile + Tile, -Space - SmallSpace, SmallTile, SmallTile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(SmallSpace, -Space - SmallSpace, SmallTile, SmallTile), "Item Window Style");

            CoreUIEditor.Instance.Image(new Rect(Space, -Space*2 - Tile, Tile, Tile), _texture0);
            CoreUIEditor.Instance.Image(new Rect(Space * 2 + Tile, -Space * 2 - Tile, Tile, Tile), _texture1);
            CoreUIEditor.Instance.Image(new Rect(Space * 3 + Tile * 2, -Space * 2 - Tile, Tile, Tile), _texture2);
            CoreUIEditor.Instance.Image(new Rect(Space, -Space * 3 - Tile * 2, Tile, Tile), _texture3);
            CoreUIEditor.Instance.Image(new Rect(Space * 2 + Tile, -Space * 3 - Tile * 2, Tile, Tile), _texture4);
            CoreUIEditor.Instance.Image(new Rect(Space * 3 + Tile * 2, -Space * 3 - Tile * 2, Tile, Tile), _texture5);
            CoreUIEditor.Instance.Image(new Rect(Space + SmallSpace * 2 + SmallTile, -Space, Tile, Tile), _texture7);
            CoreUIEditor.Instance.Image(new Rect(Space * 2 + SmallSpace * 3 + SmallTile + Tile, -Space - SmallSpace, SmallTile, SmallTile), _texture8);
            CoreUIEditor.Instance.Image(new Rect(SmallSpace, -Space - SmallSpace, SmallTile, SmallTile), _texture8);

            CoreUIEditor.Instance.Window(new Rect(Space, -Space*4 - Tile*3, Tile*3 + Space*2, Tile), "UI Window Style");
            CoreUIEditor.Instance.Image(new Rect(Space + 0.03125f * 7, -Space*4 - Tile*3 - .03125f * 6, 0, 0), _uiBack);
            CoreUIEditor.Instance.FlexibleImage(new Rect(Space + 0.03125f * 36, -Space * 8 - Tile * 3 - .03125f * 3, 1, 0), "Health Bar Style");
            CoreUIEditor.Instance.FlexibleImage(new Rect(Space + 0.03125f * 36, -Space * 8 - Tile * 3 - .03125f * 13, .5f, 0), "Mana Bar Style");

            CoreUIEditor.Instance.Slider(new Rect(Space * 2 + SmallSpace * 4 + SmallTile + Tile * 2, -Space - SmallSpace, 5.8f, 0), CoreUIOrientation.Vertical, "RPG Slider Style");
        }
    }
}
