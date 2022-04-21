using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CoreUI
{
    [Serializable]
    class FontEditorWindow : EditorWindow
    {
        protected const string StyleStaffPath = "CoreUI/FontEditorStaff/";
        private const float AlphabetGridWindowHorizontalDelta = .02f;
        private const float AlphabetGridWindowWidthDelta = .46f;
        private const float AlphabetGridWindowHeight = FontWindowHeight - SmallButtonSize * 2;
        private const float AlphabetGridWindowVerticalSpace = 20 + SmallButtonSize * 2;
        private const float FontWindowHorizontalDelta = .5f;
        private const float FontWindowWidthDelta = .48f;
        private const float FontWindowHeight = 290;
        private const float FontWindowVerticalSpace = 20;
        private const int SmallButtonSize = 30;
        private const int ScaleMaxValue = 20;
        private const float BoundsButtonSize = 10;
        private const float SymbolSelectionMarkHeight = 20;
        private const int AlphabetGridButtonSize = 20;
        private const char InitSymbol = '~';
        private const string SymbolSelectionMarkLabel = "Selected symbol";
        private static readonly Color SelectedSymbolColor = Color.yellow;

        [SerializeField] private CoreUIFont _font;
        [SerializeField] private int _textureScale = 2;
        [SerializeField] private Vector2 _scroll;
        [SerializeField] private int _selectedSymbolIndex;
        [SerializeField] private bool _firstBoundsPointModified;
        [SerializeField] private bool _secondBoundsPointModified;
        [SerializeField] private bool _verticalOffsetPointModified;
        [SerializeField] private Texture2D _boundsTexture;
        [SerializeField] private Texture2D _verticalOffsetTexture;
        [SerializeField] private Texture2D _symbolSelectionMarkTexture;

        private float WindowWidth { get { return position.width; } }
        private float WindowHeight { get { return position.height; } }
        
        private float AlphabetGridWindowVerticalPos { get { return WindowHeight - AlphabetGridWindowHeight - AlphabetGridWindowVerticalSpace; } }
        private float AlphabetGridWindowWidth{get { return WindowWidth * AlphabetGridWindowWidthDelta; }}
        private Rect AlphabetGridWindowRect{get{return new Rect(WindowWidth * AlphabetGridWindowHorizontalDelta,
            AlphabetGridWindowVerticalPos,
            AlphabetGridWindowWidth,
            AlphabetGridWindowHeight);}}
        private float FontWindowVerticalPos { get { return WindowHeight - FontWindowHeight - FontWindowVerticalSpace; } }
        private Rect FontWindowRect { get { return new Rect(WindowWidth * FontWindowHorizontalDelta,
            FontWindowVerticalPos,
            WindowWidth * FontWindowWidthDelta,
            FontWindowHeight);} }

        private Rect MinScaleRect { get { return new Rect(WindowWidth * (1 - FontWindowHorizontalDelta - FontWindowWidthDelta),
            FontWindowVerticalPos + FontWindowHeight - SmallButtonSize,
            SmallButtonSize, SmallButtonSize);} }
        private Rect MaxScaleRect { get { return new Rect(WindowWidth * (1 - FontWindowHorizontalDelta - FontWindowWidthDelta) + SmallButtonSize,
            FontWindowVerticalPos + FontWindowHeight - SmallButtonSize,
            SmallButtonSize, SmallButtonSize);} }

        private float TextureEditorWidth { get { return WindowWidth * _textureScale; } }
        private float TextureEditorHeight { get { return _font.Texture.height * TextureEditorWidth / _font.Texture.width; } }
        private float TextureEditorVerticalSpace { get { return EditorGUIUtility.singleLineHeight; } }
        private Rect TextureEditorRect { get { return new Rect(0, 0, TextureEditorWidth, TextureEditorHeight);} }
        private Rect TextureScrollRect { get { return new Rect(0, TextureEditorVerticalSpace, WindowWidth, TextureEditorHeight + TextureEditorVerticalSpace * 2); } }
        private Rect TextureViewEditorRect { get { return new Rect(0, 0, TextureEditorWidth, TextureEditorHeight);} }
        private bool AlphabetEmpty { get { return _font.Alphabet.Length == 0; } }

        private SymbolDescription SelectedSymbol
        {
            get
            {
                _selectedSymbolIndex = Mathf.Clamp(_selectedSymbolIndex, 0, _font.Alphabet.Count() - 1);
                return _font.Alphabet[_selectedSymbolIndex];
            }
        }

        public static void ShowSelf()
        {
            var window = EditorWindow.GetWindow<FontEditorWindow>(false, "Font Editor Window");
            window.Init();
        }

        protected virtual void Init()
        {
            _boundsTexture = EditorGUIUtility.Load(StyleStaffPath + "FontEditorBounds.png") as Texture2D;
            _verticalOffsetTexture = EditorGUIUtility.Load(StyleStaffPath + "FontEditorVerticalOffset.png") as Texture2D;
            _symbolSelectionMarkTexture = EditorGUIUtility.Load(StyleStaffPath + "SymbolSelectionMark.png") as Texture2D;
        }

        protected virtual void OnGUI()
        {
            DrawTextureEditor();
            DrawScaleEditor();
            DrawFontEditorWindow();
        }

        private void DrawFontEditorWindow()
        {
            BeginWindows();
            GUI.Window(0, AlphabetGridWindowRect, DrawAlphabetWindow, "AlphabetGrid");
            GUI.Window(1, FontWindowRect, DrawFontWindow, "Font Editor");
            EndWindows();
        }

        private void DrawAlphabetWindow(int id)
        {
            if (_font == null) return;
            EditorGUILayout.BeginHorizontal();
            var buttonsInLine = (int) ( AlphabetGridWindowWidth - 7) / AlphabetGridButtonSize;
            var line = 0;
            var style = EditorStyles.miniButtonLeft;
            for (var index = 0; index < _font.Alphabet.Length; index++)
            {
                if (index / buttonsInLine > line)
                {
                    line++;
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    style = EditorStyles.miniButtonLeft;
                }
                if (index == _font.Alphabet.Length - 1 || (index + 1) % buttonsInLine == 0) style = EditorStyles.miniButtonRight;
                if (index == _selectedSymbolIndex) GUI.color = SelectedSymbolColor;
                if (GUILayout.Button(_font.Alphabet[index].Symbol.ToString(), style, GUILayout.Width(AlphabetGridButtonSize))) _selectedSymbolIndex = index;
                GUI.color = Color.white;
                style = EditorStyles.miniButtonMid;
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawFontWindow(int id)
        {
            DrawGeneralFontInfo();

            if (_font != null) EditorUtility.SetDirty(_font);
        }

        private void DrawGeneralFontInfo()
        {
            _font = EditorGUILayout.ObjectField(_font, typeof(CoreUIFont), false) as CoreUIFont;
            if (_font == null) return;
            _font.Texture = EditorGUILayout.ObjectField(_font.Texture, typeof (Texture2D), false) as Texture2D;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Pixels interval", GUILayout.Width(90));
            _font.PixelsInterval = EditorGUILayout.IntField(_font.PixelsInterval, GUILayout.MinWidth(15));
            EditorGUILayout.LabelField("Pixels space", GUILayout.Width(90));
            _font.PixelsSpace = EditorGUILayout.IntField(_font.PixelsSpace, GUILayout.MinWidth(15));
            EditorGUILayout.LabelField("Pixels height", GUILayout.Width(90));
            _font.PixelsHeight = EditorGUILayout.IntField(_font.PixelsHeight, GUILayout.MinWidth(15));
            EditorGUILayout.EndHorizontal();
            DrawSymbolsSelector();
            if (AlphabetEmpty) return;
            DrawSymbolEditor();
        }

        private void DrawSymbolsSelector()
        {
            var symbols = _font.Alphabet.Select(x => x.Symbol.ToString()).ToArray();
            EditorGUILayout.BeginHorizontal();
            if (!AlphabetEmpty) _selectedSymbolIndex = EditorGUILayout.Popup(_selectedSymbolIndex, symbols);
            if (GUILayout.Button("Delete", EditorStyles.miniButtonLeft))
            {
                if (EditorUtility.DisplayDialog("SelectedSymbol deleting", "Are you sure?", "Yes", "Cancel")) _font.RemoveSymbol(_selectedSymbolIndex);
            }
            if (GUILayout.Button("Create", EditorStyles.miniButtonRight)) CreateSymbol();
            EditorGUILayout.EndHorizontal();
        }

        private void CreateSymbol()
        {
            _font.CreateSymbol(InitSymbol);
            if (_font.Alphabet.Length > 1)
                _font.Alphabet[_font.Alphabet.Length - 1].Sprite.HorizontalOffset =
                    _font.Alphabet[_font.Alphabet.Length - 2].Sprite.HorizontalOffset;
            _selectedSymbolIndex = _font.Alphabet.Length - 1;
        }

        private void DrawSymbolEditor()
        {
            EditorGUILayout.LabelField("SelectedSymbol settings", EditorStyles.boldLabel);
            var value = EditorGUILayout.TextField("SelectedSymbol", SelectedSymbol.Symbol.ToString());
            if (string.IsNullOrEmpty(value)) return;
            SelectedSymbol.Symbol = value[0];
            if (_font.CheckSymbolForDuplicate(SelectedSymbol.Symbol)) SelectedSymbol.Symbol = InitSymbol;
            SelectedSymbol.PixelsVerticalOffset = EditorGUILayout.IntField("Symbol vertical offset", SelectedSymbol.PixelsVerticalOffset);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Sprite info");
            SelectedSymbol.Sprite.HorizontalOffset = EditorGUILayout.IntField("Horizontal offset", SelectedSymbol.Sprite.HorizontalOffset);
            SelectedSymbol.Sprite.VerticalOffset = EditorGUILayout.IntField("Vertical offset", SelectedSymbol.Sprite.VerticalOffset);
            SelectedSymbol.Sprite.Width = EditorGUILayout.IntField("Width", SelectedSymbol.Sprite.Width);
            SelectedSymbol.Sprite.Height = EditorGUILayout.IntField("Height", SelectedSymbol.Sprite.Height);
        }

        private void DrawTextureEditor()
        {
            if (_font == null) return;
            _scroll = GUI.BeginScrollView(TextureScrollRect, _scroll, TextureViewEditorRect, true, false, GUI.skin.horizontalScrollbar, GUIStyle.none);
            GUI.Box(TextureEditorRect, string.Empty);
            GUI.DrawTexture(TextureEditorRect, _font.Texture);
            if (!AlphabetEmpty) DrawSelectedSymbolBounds();
            GUI.EndScrollView();
        }

        private void DrawSelectedSymbolBounds()
        {
            DrawSymbolBounds(SelectedSymbol);
            foreach (var symbolDescription in _font.Alphabet)
            {
                if (SelectedSymbol == symbolDescription) continue;
                DrawSymbolBounds(symbolDescription, false);
            }
        }

        private void DrawSymbolBounds(SymbolDescription symbol, bool drawButtons = true)
        {
            var point0 = GetLeftBottomBoundsRect(symbol, drawButtons);
            var point1 = GetRightTopBoundsRect(symbol, drawButtons);
            var boxRect = new Rect(GetActualLeftBottomBoundsRect(symbol).center, GetActualRightTopBoundsRect(symbol).center - GetActualLeftBottomBoundsRect(symbol).center);
            DrawBox(boxRect, symbol.Symbol);
            if (drawButtons) DrawSelectedMark(boxRect);
            DrawVerticalOffset(GetActualLeftBottomBoundsRect(symbol), GetActualRightTopBoundsRect(symbol), symbol, drawButtons);
            if (!drawButtons) return;
            _firstBoundsPointModified = GUI.Toggle(point0, _firstBoundsPointModified, string.Empty, GUI.skin.button);
            _secondBoundsPointModified = GUI.Toggle(point1, _secondBoundsPointModified, string.Empty, GUI.skin.button);
        }

        private void DrawSelectedMark(Rect rect)
        {
            rect.y -= SymbolSelectionMarkHeight - rect.height;
            rect.height = SymbolSelectionMarkHeight;
            GUI.DrawTexture(rect, _symbolSelectionMarkTexture);
            GUI.Label(rect, SymbolSelectionMarkLabel);
        }

        private void DrawVerticalOffset(Rect point0, Rect point1, SymbolDescription symbol, bool drawButtons)
        {
            var rect = GetVerticalOffsetRect(symbol, drawButtons);
            if (drawButtons) _verticalOffsetPointModified = GUI.Toggle(rect, _verticalOffsetPointModified, string.Empty, GUI.skin.button);
            GUI.DrawTexture(new Rect(point0.center, new Vector2(point1.x - point0.x, GetActualVerticalOffsetRect(symbol).y - point0.y)), _verticalOffsetTexture);
        }

        private void ApplyMousePositionToVerticalOffset(SymbolDescription symbol)
        {
            var pos = GetMouseSpriteTexturePosition();
            pos.y -= symbol.Sprite.VerticalOffset;
            symbol.PixelsVerticalOffset = Mathf.FloorToInt(pos.y);
        }

        private Rect GetLeftBottomBoundsRect(SymbolDescription symbol, bool applyValues)
        {
            if (_firstBoundsPointModified && applyValues)
            {
                ApplyMousePositionToFirstPoint(symbol);
                return GetMouseBoundsRect();
            }
            return GetActualLeftBottomBoundsRect(symbol);
        }

        private Rect GetRightTopBoundsRect(SymbolDescription symbol, bool applyValues)
        {
            if (_secondBoundsPointModified && applyValues)
            {
                ApplyMousePositionToSecondPoint(symbol);
                return GetMouseBoundsRect();
            }
            return GetActualRightTopBoundsRect(symbol);
        }

        private Rect GetVerticalOffsetRect(SymbolDescription symbol, bool applyValues)
        {
            if (_verticalOffsetPointModified && applyValues)
            {
                ApplyMousePositionToVerticalOffset(symbol);
                return GetMouseBoundsRect();
            }
            return GetActualVerticalOffsetRect(symbol);
        }

        private Rect GetActualVerticalOffsetRect(SymbolDescription symbol)
        {
            return new Rect((float)(symbol.Sprite.HorizontalOffset + symbol.Sprite.Width/2f)/ _font.Texture.width * TextureEditorWidth - BoundsButtonSize / 2,
                (1 - (float)(symbol.Sprite.VerticalOffset + symbol.PixelsVerticalOffset) / _font.Texture.height) * TextureEditorHeight - BoundsButtonSize / 2, BoundsButtonSize, BoundsButtonSize);
        }

        private Rect GetActualLeftBottomBoundsRect(SymbolDescription symbol)
        {
            return new Rect((float)symbol.Sprite.HorizontalOffset / _font.Texture.width * TextureEditorWidth - BoundsButtonSize/2,
                (1 - (float)symbol.Sprite.VerticalOffset / _font.Texture.height) * TextureEditorHeight - BoundsButtonSize/2, BoundsButtonSize, BoundsButtonSize);
        }
        
        private Rect GetActualRightTopBoundsRect(SymbolDescription symbol)
        {
            return new Rect((float)(symbol.Sprite.HorizontalOffset + symbol.Sprite.Width) / _font.Texture.width * TextureEditorWidth - BoundsButtonSize / 2,
                (1 - (float)(symbol.Sprite.VerticalOffset + symbol.Sprite.Height) / _font.Texture.height) * TextureEditorHeight - BoundsButtonSize / 2, BoundsButtonSize, BoundsButtonSize);
        }

        private Rect GetMouseBoundsRect()
        {
            return new Rect(Event.current.mousePosition - new Vector2(BoundsButtonSize/2, BoundsButtonSize/2), new Vector2(BoundsButtonSize, BoundsButtonSize));
        }

        private void ApplyMousePositionToFirstPoint(SymbolDescription symbol)
        {
            var pos = GetMouseSpriteTexturePosition();
            symbol.Sprite.HorizontalOffset = (int)pos.x;
            symbol.Sprite.VerticalOffset = (int)pos.y;
        }

        private void ApplyMousePositionToSecondPoint(SymbolDescription symbol)
        {
            var pos = GetMouseSpriteTexturePosition() - new Vector2(symbol.Sprite.HorizontalOffset, symbol.Sprite.VerticalOffset);
            symbol.Sprite.Width = (int)pos.x;
            symbol.Sprite.Height = (int)pos.y;
        }

        private Vector2 GetMouseSpriteTexturePosition()
        {
            Repaint();
            var mousePos = Event.current.mousePosition - new Vector2(BoundsButtonSize/2, BoundsButtonSize/2);
            mousePos.x /= TextureEditorWidth;
            mousePos.y = 1- mousePos.y / TextureEditorHeight;
            mousePos.x = Mathf.Floor(mousePos.x*_font.Texture.width);
            mousePos.y = Mathf.Floor(mousePos.y*_font.Texture.height);
            return mousePos;
        }

        private void DrawBox(Rect rect, char symbol)
        {
            GUI.DrawTexture(rect, _boundsTexture);
            rect.y += rect.height;
            rect.height = EditorGUIUtility.singleLineHeight;
            GUI.Label(rect, symbol.ToString());
        }

        private void DrawScaleEditor()
        {
            if (GUI.Button(MinScaleRect, "-", EditorStyles.miniButtonLeft)) _textureScale --;
            if (GUI.Button(MaxScaleRect, "+", EditorStyles.miniButtonRight)) _textureScale ++;
            _textureScale = Mathf.Clamp(_textureScale, 1, ScaleMaxValue);
        }
    }
}
