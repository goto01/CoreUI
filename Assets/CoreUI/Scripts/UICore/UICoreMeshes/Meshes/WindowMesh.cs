﻿using System.Collections.Generic;
using UICore.StylesSystem.Styles;
using UnityEngine;

namespace UICore.UICoreMeshes.Meshes
{
    public class WindowMesh : BaseCoreUIMesh
    {
        private float _borderWidth;

        public float BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }

        protected override void Generate(BaseStyle style)
        {
            if (!(style is WindowStyle))
            {
                Debug.LogErrorFormat("Can't generate window mesh because style {0} isn't a window's style", style.name);
                return;
            }
            Generate(style as WindowStyle);
        }

        private void Generate(WindowStyle style)
        {
            BorderWidth = style.BorderWidth;
            CreateMesh();
            if (style.Has9Tiles) ApplyUV9Laytout();
            else ApplyUV3Laytout();

            Triangles = new List<int>()
            {
                0, 1, 2 , 0, 2, 3,          // left botttom corner
                4, 5, 6, 4, 6 , 7,          // left top corner
                8, 9, 10, 8, 10, 11,        // right top corner
                12, 13, 14, 12, 14, 15,     // right bottom corner
                16, 17, 18, 16, 18, 19,     // left border
                20, 21, 22, 20, 22, 23,     // top border
                24, 25, 26, 24, 26, 27,     // right border
                28, 29, 30, 28, 30, 31,     // bottom border
                32, 33, 34, 32, 34, 35,     // content area
            };
        }

        private void CreateMesh()
        {
            PushVertice(0, - BorderWidth);
            PushVertice(0, 0);
            PushVertice(BorderWidth, 0);
            PushVertice(BorderWidth, - BorderWidth);
                        
            PushVertice(Width - BorderWidth, - BorderWidth);
            PushVertice(Width - BorderWidth, 0);
            PushVertice(Width, 0);
            PushVertice(Width, - BorderWidth);
                        
            PushVertice(0, 0 - Height);
            PushVertice(0, 0 - Height + BorderWidth);
            PushVertice(BorderWidth, - Height + BorderWidth);
            PushVertice(BorderWidth, - Height);
                        
            PushVertice(Width - BorderWidth, - Height);
            PushVertice(Width - BorderWidth, - Height + BorderWidth);
            PushVertice(Width, - Height + BorderWidth);
            PushVertice(Width, - Height);
                        
            PushVertice(0, - Height + BorderWidth);
            PushVertice(0, - BorderWidth);
            PushVertice(BorderWidth, - BorderWidth);
            PushVertice(BorderWidth, - Height + BorderWidth);
                        
            PushVertice(BorderWidth, - BorderWidth);
            PushVertice(BorderWidth, 0);
            PushVertice(Width - BorderWidth, 0);
            PushVertice(Width - BorderWidth, 0 - BorderWidth);
                        
            PushVertice(Width - BorderWidth, 0 - Height + BorderWidth);
            PushVertice(Width - BorderWidth, 0 - BorderWidth);
            PushVertice(Width, 0 - BorderWidth);
            PushVertice(Width, 0 - Height + BorderWidth);
                        
            PushVertice(BorderWidth, 0 - Height);
            PushVertice(BorderWidth, 0 - Height + BorderWidth);
            PushVertice(Width - BorderWidth, 0 - Height + BorderWidth);
            PushVertice(Width - BorderWidth, 0 - Height);
                        
            PushVertice(BorderWidth, 0 - Height + BorderWidth);
            PushVertice(BorderWidth, 0 - BorderWidth);
            PushVertice(Width - BorderWidth, 0 - BorderWidth);
            PushVertice(Width - BorderWidth, 0 - Height + BorderWidth);
        }

        private void ApplyUV3Laytout()
        {
            PushUV(.5f, .5f);
            PushUV(0, .5f);
            PushUV(0, 1);
            PushUV(.5f, 1f);

            PushUV(.5f, 1);
            PushUV(.5f, .5f);
            PushUV(0, .5f);
            PushUV(0, 1);

            PushUV(0, .5f);
            PushUV(0, 1);
            PushUV(.5f, 1);
            PushUV(.5f, .5f);

            PushUV(0, 1);
            PushUV(.5f, 1);
            PushUV(.5f, .5f);
            PushUV(0, .5f);

            PushUV(.5f, 1);
            PushUV(1, 1);
            PushUV(1, .5f);
            PushUV(.5f, .5f);
            
            PushUV(.5f, .5f);
            PushUV(.5f, 1);
            PushUV(1, 1);
            PushUV(1, .5f);
            
            PushUV(1, .5f);
            PushUV(.5f, .5f);
            PushUV(.5f, 1);
            PushUV(1, 1);
            
            PushUV(1, 1);
            PushUV(1, .5f);
            PushUV(.5f, .5f);
            PushUV(.5f, 1);
            
            PushUV(.5f, 0);
            PushUV(.5f, .5f);
            PushUV(1, .5f);
            PushUV(1, 0);
        }

        private void ApplyUV9Laytout()
        {
            PushUV(0, .6f);
            PushUV(0, .8f);
            PushUV(.5f, .8f);
            PushUV(0.5f, .6f);
            
            PushUV(0, .4f);
            PushUV(0, .6f);
            PushUV(.5f, .6f);
            PushUV(0.5f, .4f);

            PushUV(0, .8f);
            PushUV(0, 1f);
            PushUV(.5f, 1f);
            PushUV(0.5f, .8f);

            PushUV(0, .2f);
            PushUV(0, .4f);
            PushUV(.5f, .4f);
            PushUV(.5f, .2f);

            PushUV(.5f, .6f);
            PushUV(.5f, .8f);
            PushUV(1, .8f);
            PushUV(1, .6f);

            PushUV(.5f, .8f);
            PushUV(.5f, 1);
            PushUV(1, 1);
            PushUV(1, .8f);

            PushUV(.5f, .4f);
            PushUV(.5f, .6f);
            PushUV(1, .6f);
            PushUV(1, .4f);

            PushUV(.5f, .2f);
            PushUV(.5f, .4f);
            PushUV(1, .4f);
            PushUV(1, .2f);

            PushUV(.5f, 0);
            PushUV(.5f, .2f);
            PushUV(1, .2f);
            PushUV(1, 0);
        }
        
        protected override void ApplySize()
        {

        }
    }
}
