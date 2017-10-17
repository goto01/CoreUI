using System;
using UnityEngine;

namespace Assets.Scripts.UICore.StylesSystem.Styles
{
    [Serializable]
    public class WindowStyle : BaseStyle
    {
        [SerializeField] private bool _has9Tiles;

        public bool Has9Tiles { get { return _has9Tiles;} }
    }
}
