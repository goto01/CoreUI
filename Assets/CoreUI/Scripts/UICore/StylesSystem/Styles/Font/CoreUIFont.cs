using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UICore.StylesSystem.Styles.Font
{
    [Serializable]
    public class CoreUIFont : BaseStyle
    {
        [SerializeField] private int _pixelsInterval;
        [SerializeField] private int _pixelsSpace;
        [SerializeField] private int _pixelsHeight;
        [SerializeField] private Material _material;
        [SerializeField] private SymbolDescription[] _alphabet;
        private Dictionary<char, SymbolDescription> _alphabetDictionary;

        public float FontHeight { get { return _pixelsHeight * _pixelWidth; } }

        public Material Material
        {
            get { return _material; } 
            set { _material = value; }
        }

        public int PixelsHeight
        {
            get { return _pixelsHeight; }
            set { _pixelsHeight = value; }
        }

        public int PixelsInterval
        {
            get { return _pixelsInterval; }
            set { _pixelsInterval = value; }
        }

        public float Interval { get { return ToWorldCoords(_pixelsInterval);} }

        public int PixelsSpace
        {
            get { return _pixelsSpace; }
            set { _pixelsSpace = value; }
        }

        public float Space { get { return ToWorldCoords(_pixelsSpace); } }

        public SymbolDescription[] Alphabet { get { return _alphabet ?? (_alphabet = new SymbolDescription[]{});} }
        
        public SymbolDescription GetSymbol(char symbol)
        {
            if (_alphabetDictionary == null) InitSelf();
            if (_alphabetDictionary.ContainsKey(symbol)) return _alphabetDictionary[symbol];
            Debug.LogErrorFormat(this, "Symbol '{0}' doesn't exist in the font '{1}'", symbol, name); 
            return null;
        }

        public bool ContainsSymbol(char symbol)
        {
            if (_alphabetDictionary == null) InitSelf();
            if (_alphabetDictionary.ContainsKey(symbol)) return true;
            return false;
        }

        public void RemoveSymbol(int symbolIndex)
        {
            var list = _alphabet.ToList();
            list.RemoveAt(symbolIndex);
            _alphabet = list.ToArray();
            InitSelf();
        }

        public void CreateSymbol(char symbol)
        {
            var list = _alphabet.ToList();
            list.Add(new SymbolDescription()
            {
                Symbol = symbol,
            });
            _alphabet = list.ToArray();
            //InitSelf();
        }

        protected virtual void OnEnable()
        {
            InitSelf();
        }

        public void InitSelf()
        {
            if (_alphabet == null) return;
            _alphabetDictionary = new Dictionary<char, SymbolDescription>();
            for (var index = 0; index < _alphabet.Length; index++)
            {
                var symbol = _alphabet[index];
                if (_alphabetDictionary.ContainsKey(symbol.Symbol))
                {
                    Debug.LogErrorFormat("Symbol {0} already exists in the font - {1}", symbol.Symbol, name);
                    continue;
                }
                _alphabetDictionary[symbol.Symbol] = symbol;
                symbol.Init(_pixelWidth, _texture.width, _texture.height);
            }
        }

        private float ToWorldCoords(int value)
        {
            return value*_pixelWidth;
        }

        public bool CheckSymbolForDuplicate(char symbol)
        {
            var counter = 0;
            for (var index = 0; index < _alphabet.Length; index++) if (_alphabet[index].Symbol == symbol) counter++;
            return counter > 1;
        }
    }
}
