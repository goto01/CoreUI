using System;
using UnityEngine;

namespace Assets.Scripts.UICore.StylesSystem.Styles
{
    [Serializable]
    public class BaseStyle : ScriptableObject
    {
        [SerializeField] protected Texture2D _texture;
        [SerializeField] protected float _pixelWidth = 1f / 32;
    }
}
