using System;
using UnityEngine;

namespace Assets.Scripts.UICore.StylesSystem.Styles
{
    [Serializable]
    public class BaseStyle : ScriptableObject
    {
        [SerializeField] protected Texture2D _texture;
    }
}
