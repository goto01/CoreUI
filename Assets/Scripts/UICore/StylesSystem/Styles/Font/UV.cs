using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.UICore.StylesSystem.Styles.Font
{
    [Serializable]
    public class UV
    {
        public float X;
        public float Y;
        public float MaxX;
        public float MaxY;

        public UV(float x, float y, float maxX, float maxY)
        {
            X = x;
            Y = y;
            MaxX = maxX;
            MaxY = maxY;
        }
    }
}
