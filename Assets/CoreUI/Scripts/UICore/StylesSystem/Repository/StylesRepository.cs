using System;
using System.Collections.Generic;
using System.Linq;
using UICore.StylesSystem.Styles;
using UnityEngine;

namespace UICore.StylesSystem.Repository
{
    [Serializable]
    public class StylesRepository : ScriptableObject
    {
        [SerializeField] private BaseStyle[] _styles;
        private Dictionary<string, BaseStyle> _stylesDictionary;

        public void AddStyle(BaseStyle style)
        {
            var styles = _styles.ToList();
            styles.Add(style);
            _styles = styles.ToArray();
        }
        
        public BaseStyle GetStyle(string styleName)
        {
            if (_stylesDictionary.ContainsKey(styleName)) return _stylesDictionary[styleName];
            Debug.LogErrorFormat("Style with name {0} doesn't exist in the StylesRepository", styleName);
            return null;
        }

        public void Init()
        {
            if (_styles.Any(x => x == null))
            {
                Debug.LogError("Styles Repository has some none styles");
                return;
            }
            _stylesDictionary = new Dictionary<string, BaseStyle>();
            for (var index = 0; index < _styles.Length; index++)
            {
                var style = _styles[index];
                if (_stylesDictionary.ContainsKey(style.name))
                {
                    Debug.LogErrorFormat("You have several styles with name - {0}", style.name);
                    return;
                }
                _stylesDictionary[style.name] = style;
            }
        }
    }
}
