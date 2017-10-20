using Assets.Scripts.UICore;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CoreUITest: MonoBehaviour
    {
        private const float Pixel = 1f/32;
        private const float Space = Pixel*2;
        private const float Tile = Pixel*48;
        private const float BackgroundWindowSize = Pixel*152;
        private const float SmallTile = Pixel*32;
        private const float SmallSpace = Pixel*9;

        protected virtual void Awake()
        {
            CoreUIEditor.Instance.Window(new Rect(-SmallSpace, SmallSpace, BackgroundWindowSize + SmallSpace * 2, BackgroundWindowSize + SmallSpace * 2), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(0, 0, BackgroundWindowSize, BackgroundWindowSize), "Background Window Style");

            CoreUIEditor.Instance.Window(new Rect(Space, - Space * 2 - Tile, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space * 2 + Tile, - Space * 2 - Tile, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space * 3 + Tile * 2, - Space * 2 - Tile, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space, -Space * 3 - Tile * 2, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space * 2 + Tile, -Space * 3 - Tile * 2, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space * 3 + Tile * 2, -Space * 3 - Tile * 2, Tile, Tile), "Item Window Style");

            CoreUIEditor.Instance.Window(new Rect(Space + SmallSpace * 2 + SmallTile, -Space, Tile, Tile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(Space * 2+ SmallSpace * 3 + SmallTile + Tile, -Space - SmallSpace, SmallTile, SmallTile), "Item Window Style");
            CoreUIEditor.Instance.Window(new Rect(SmallSpace, -Space - SmallSpace, SmallTile, SmallTile), "Item Window Style");
        }
    }
}
