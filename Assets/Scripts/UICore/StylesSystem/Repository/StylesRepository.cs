using System;
using Assets.Scripts.UICore.StylesSystem.Styles;
using UnityEngine;

namespace Assets.Scripts.UICore.StylesSystem.Repository
{
    [Serializable]
    public class StylesRepository : ScriptableObject
    {
        [SerializeField] private BaseStyle[] _styles;

        public BaseStyle[] Styles { get { return _styles; } }
    }
}
