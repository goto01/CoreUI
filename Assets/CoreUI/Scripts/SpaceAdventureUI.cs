using UICore;
using UICore.Controls;
using UICore.Controls.Containers;
using UICore.Controls.Text;
using UICore.UICoreMeshes.Meshes;
using UnityEngine;
using UnityEngine.UIElements;

namespace CoreUI.Scripts
{
	public class SpaceAdventureUI : MonoBehaviour
	{
		private readonly Vector2 StartPoint = new Vector2(-7f, 4f);
		private const float PixelSize = 1 / 32f;
		private CoreUIWindow _bottomInfoWindow;
		private CoreUIWindow _topInfoWindow;
		private CoreUIButton _buttonShowBottomWindow;
		[SerializeField] private Texture2D _verticalSeparatorTexture;
		[SerializeField] private Texture2D[] _icons;
		private CoreUILabel _healthLabel0;
		private CoreUILabel _healthLabel1;
		private CoreUILabel _energyLabel0;
		private CoreUILabel _energyLabel1;
		private CoreUIButton _showLeftWindowButton;
		private CoreUISlider _leftSlider;
		private CoreUISlider _rightMiniSlider;
		private float _healthTimer;
		private float _scrollTimer0;
		private int _scrollLeftInv = 1;
		private CoreUIFlexibleImage _progress;
		
		protected virtual void Start()
		{
			_topInfoWindow = CoreUIEditor.Instance.Window(new Rect(StartPoint.x, StartPoint.y, 128f * PixelSize, 160 * PixelSize), "SpaceAdventureWindow1Style");
			_progress = CoreUIEditor.Instance.FlexibleImage(new Rect(0, 30 * PixelSize, 100 * PixelSize, 0), _topInfoWindow);
			_buttonShowBottomWindow = CoreUIEditor.Instance.Button(new Rect((64f - 7f) * PixelSize, (- 160f) * PixelSize, PixelSize * 14f, PixelSize * 7f), _topInfoWindow, TopInfoWindowButtonAction,
				"SpaceAdventureBottomArrowButtonStyle");
			CoreUIEditor.Instance.Button(new Rect( - 1 * PixelSize, - 80f * PixelSize, 6 * PixelSize, 14 * PixelSize), _topInfoWindow, LeftButtonHideWindow, "SpaceAdventureArrowRightButtonStyle");

			var w = CoreUIEditor.Instance.Window(new Rect(5 * PixelSize, -12 * PixelSize, 118f * PixelSize, 146 * PixelSize), _topInfoWindow, "SpaceAdventureContentWindowStyle");
			w.Color = new Color(25f / 255, 38f / 255, 56f / 255);;
			CoreUIEditor.Instance.Image(new Rect(4 * PixelSize, -4 * PixelSize, 0, 0), w, _icons[3]);
			
			_buttonShowBottomWindow.Enabled = false;
			_bottomInfoWindow = CoreUIEditor.Instance.Window(new Rect(0, - 160 * PixelSize, 128f * PixelSize, 44 * PixelSize), _topInfoWindow, "SpaceAdventureWindow0");
			CoreUIEditor.Instance.Button(new Rect((64f - 7f) * PixelSize, (- 44f + 6f) * PixelSize, PixelSize * 14f, PixelSize * 7f), _bottomInfoWindow, Action,
				"SpaceAdventureArrowButtonStyle");
			
			var contentWindow0 = CoreUIEditor.Instance.Window(new Rect(6 * PixelSize, -3 * PixelSize, 116f * PixelSize, 35 * PixelSize), _bottomInfoWindow, "SpaceAdventureContentWindowStyle");
			contentWindow0.Color = new Color(25f / 255, 38f / 255, 56f / 255);
			var w0 = CoreUIEditor.Instance.Window(new Rect(4 * PixelSize, -3 * PixelSize, 28f * PixelSize, 28 * PixelSize), contentWindow0, "SpaceAdventureContentWindowStyle");
			w0.Color = new Color(42f / 255, 63f / 255, 91f / 255);
			var w1 = CoreUIEditor.Instance.Window(new Rect(41 * PixelSize, -3 * PixelSize, 28f * PixelSize, 28 * PixelSize), contentWindow0, "SpaceAdventureContentWindowStyle");
			w1.Color = new Color(42f / 255, 63f / 255, 91f / 255);
			var w2 = CoreUIEditor.Instance.Window(new Rect(78 * PixelSize, -3 * PixelSize, 33 * PixelSize, 28 * PixelSize), contentWindow0, "SpaceAdventureContentWindowStyle");
			w2.Color = new Color(42f / 255, 63f / 255, 91f / 255);
			CoreUIEditor.Instance.Image(new Rect(35 * PixelSize, -3 * PixelSize, 0, 0), contentWindow0, _verticalSeparatorTexture);
			CoreUIEditor.Instance.Image(new Rect(72 * PixelSize, -3 * PixelSize, 0, 0), contentWindow0, _verticalSeparatorTexture);
			_healthLabel0 = CoreUIEditor.Instance.Label(new Rect(3 * PixelSize, - 17 * PixelSize, 0, 0), "100/100", w0, "SpaceAdventureFont");
			_healthLabel0.Color = Color.black;
			_healthLabel1 = CoreUIEditor.Instance.Label(new Rect(3 * PixelSize, - 16 * PixelSize, 0, 0), "100/100", w0, "SpaceAdventureFont");
			_energyLabel0 = CoreUIEditor.Instance.Label(new Rect(3 * PixelSize, - 17 * PixelSize, 0, 0), "100/100", w1, "SpaceAdventureFont");
			_energyLabel0.Color = Color.black;
			_energyLabel1 = CoreUIEditor.Instance.Label(new Rect(3 * PixelSize, - 16 * PixelSize, 0, 0), "100/100", w1, "SpaceAdventureFont");
			CoreUIEditor.Instance.Label(new Rect(4 * PixelSize, - 17 * PixelSize, 0, 0), "999,999", w2, "SpaceAdventureFont").Color = Color.black;
			CoreUIEditor.Instance.Label(new Rect(4 * PixelSize, - 16 * PixelSize, 0, 0), "999,999", w2, "SpaceAdventureFont");
			CoreUIEditor.Instance.Image(new Rect(9 * PixelSize, -2 * PixelSize, 0, 0), w0, _icons[0]);
			CoreUIEditor.Instance.Image(new Rect(9 * PixelSize, -2 * PixelSize, 0, 0), w1, _icons[1]);
			CoreUIEditor.Instance.Image(new Rect(14 * PixelSize, -2 * PixelSize, 0, 0), w2, _icons[2]);
			InventoryWindow();
		}

		private void InventoryWindow()
		{
			var window = CoreUIEditor.Instance.Window(new Rect(StartPoint.x + 128f * PixelSize, StartPoint.y, 347 * PixelSize, 264 * PixelSize), "SpaceAdventureInventoryWindow");
			CoreUIEditor.Instance.Toggle(new Rect(7 * PixelSize, -9 * PixelSize, 57 * PixelSize, 29 * PixelSize), false, window, null, "SpaceAdventureTabToggleStyle");
			CoreUIEditor.Instance.Toggle(new Rect( 64 * PixelSize, -9 * PixelSize, 57 * PixelSize, 29 * PixelSize), true, window, null, "SpaceAdventureTabToggleStyle");
			CoreUIEditor.Instance.Image(new Rect(7 * PixelSize, -9 * PixelSize, 0, 0), window, _icons[12]);
			var insideWindow = CoreUIEditor.Instance.Window(new Rect(7 * PixelSize, -38 * PixelSize, 333 * PixelSize, 220 * PixelSize), window, "SpaceAdventureInventoryInsideWindowStyle");
			_showLeftWindowButton = CoreUIEditor.Instance.Button(new Rect(-6 * PixelSize, -100 * PixelSize, 6 * PixelSize, 14 * PixelSize), window, ShowLeftWindow, "SpaceAdventureLeftArrowButtonStyle");
			_showLeftWindowButton.Enabled = false;
			InventoryLeftPanel(insideWindow);
			InventoryRightPancel(insideWindow);
		}

		private void InventoryLeftPanel(CoreUIWindow window)
		{
			CoreUIEditor.Instance.Toggle(new Rect(4 * PixelSize, -4 * PixelSize, 24f * PixelSize, 17f * PixelSize), false, window, null, "SpaceAdventureTabCheckStyle");
			CoreUIEditor.Instance.Toggle(new Rect(29 * PixelSize, -4 * PixelSize, 24f * PixelSize, 17f * PixelSize), false, window, null, "SpaceAdventureTabCheckStyle");
			CoreUIEditor.Instance.Toggle(new Rect(54 * PixelSize, -4 * PixelSize, 24f * PixelSize, 17f * PixelSize), false, window, null, "SpaceAdventureTabCheckStyle");
			CoreUIEditor.Instance.Toggle(new Rect(79 * PixelSize, -4 * PixelSize, 24f * PixelSize, 17f * PixelSize), false, window, null, "SpaceAdventureTabCheckStyle");
			CoreUIEditor.Instance.Toggle(new Rect(104 * PixelSize, -4 * PixelSize, 24f * PixelSize, 17f * PixelSize), false, window, null, "SpaceAdventureTabCheckStyle");
			CoreUIEditor.Instance.Image(new Rect(4 * PixelSize, -4 * PixelSize, 0, 0), window, _icons[5]);
			CoreUIEditor.Instance.Window(new Rect(134 * PixelSize, -8 * PixelSize, 196 * PixelSize, 7 * PixelSize), window, "SpaceAdventureContentWindowStyle")
				.Color = new Color(57f / 255, 89f / 255, 131f / 255);
			var textField = CoreUIEditor.Instance.TextField(new Rect(135 * PixelSize, -9 * PixelSize, 182 * PixelSize, 9 * PixelSize), "100/999", Color.black, 
				new Color(42f / 255, 63f / 255, 91f / 255), window, "TextFieldCursorFlexibleImageStyle", "SpaceAdventureFont");
			textField.Color = Color.white;
			CoreUIEditor.Instance.Button(new Rect(317 * PixelSize, -5 * PixelSize, 13 * PixelSize, 13 * PixelSize), window, null, "SpaceAdventureSearchButtonStyle");
			CoreUIEditor.Instance.Image(new Rect(317 * PixelSize, -5 * PixelSize, 0, 0), window, _icons[4]);
			var w = CoreUIEditor.Instance.Window(new Rect(4 * PixelSize, -21 * PixelSize, 177 * PixelSize, 195 * PixelSize), window, "SpaceAdventuryLeftPancelWindowStyle");
			Scroll(w);
		}

		private void Scroll(CoreUIWindow window)
		{
			_leftSlider = CoreUIEditor.Instance.Slider(new Rect(157 * PixelSize, -15 * PixelSize, 161 * PixelSize, 13 * PixelSize), window, CoreUIOrientation.Vertical, 
			"SpaceAdventureScrollSliderStyle");
			var scroll = CoreUIEditor.Instance.Scroll(new Rect(7 * PixelSize, -7 * PixelSize, 146 * PixelSize, 500 * PixelSize), 146 * PixelSize, 181 * PixelSize, 
				null, _leftSlider, window,
				"SpaceAdventureScrollStyle");
			for (var row = 0; row < 18; row++)
			for (var column = 0; column < 5; column++)
			{
				CoreUIEditor.Instance.Image(new Rect((1 + 27 * column + column) * PixelSize , (-1 - 27 *row) * PixelSize, 0, 0), scroll, _icons[6]);
			}
			CoreUIEditor.Instance.Image(new Rect(1 * PixelSize, -1 * PixelSize, 0, 0), scroll, _icons[7]);
		}
		
		private void InventoryRightPancel(CoreUIWindow window)
		{
			var w = CoreUIEditor.Instance.Window(new Rect(185 * PixelSize, -21 * PixelSize, 145 * PixelSize, 195 * PixelSize), window, "SpaceAdventuryRightPancelWindowStyle");
			CoreUIEditor.Instance.Image(new Rect(10 * PixelSize, -5 * PixelSize, 0, 0), w, _icons[8]);
			_rightMiniSlider = CoreUIEditor.Instance.Slider(new Rect(131 * PixelSize, -72 * PixelSize, 94 * PixelSize, 5 * PixelSize), w, CoreUIOrientation.Vertical, "SpaceAdventureMiniSliderStyle");
			var scroll = CoreUIEditor.Instance.Scroll(new Rect(9 * PixelSize, -71 * PixelSize, 121 * PixelSize, 200 * PixelSize), 121 * PixelSize, 96 * PixelSize, null,
				_rightMiniSlider, w, "SpaceAdventureScrollStyle");
			CoreUIEditor.Instance.Image(new Rect(1 * PixelSize, -1 * PixelSize, 0, 0), scroll, _icons[9]);
			CoreUIEditor.Instance.Button(new Rect(55 * PixelSize, -175 * PixelSize, 8 * PixelSize, 14 * PixelSize), w, null, "SpaceAdventureInventoryLeftArrowButtonStyle");
			CoreUIEditor.Instance.Button(new Rect(88 * PixelSize, -175 * PixelSize, 8 * PixelSize, 14 * PixelSize), w, null, "SpaceAdventureInventoryRightArrowButtonStyle");
			CoreUIEditor.Instance.Image(new Rect(63 * PixelSize, -175 * PixelSize, 0, 0), w, _icons[10]);
			CoreUIEditor.Instance.Button(new Rect(98 * PixelSize, -175 * PixelSize, 40 * PixelSize, 14 * PixelSize), w, "SpaceAdventureBuildButtonStyle");
			CoreUIEditor.Instance.Image(new Rect(103 * PixelSize, -177 * PixelSize, 0, 0), w, _icons[11]);
		}

		private void LeftButtonHideWindow(int obj)
		{
			_topInfoWindow.Enabled = false;
			_showLeftWindowButton.Enabled = true;
		}

		private void ShowLeftWindow(int obj)
		{
			_topInfoWindow.Enabled = !_topInfoWindow.Enabled;
			_showLeftWindowButton.Enabled = false;
		}

		private void TopInfoWindowButtonAction(int obj)
		{
			_bottomInfoWindow.Enabled = true;
			_buttonShowBottomWindow.Enabled = false;
		}

		private void Action(int obj)
		{
			_bottomInfoWindow.Enabled = false;
			_buttonShowBottomWindow.Enabled = true;
		}

		private void Update()
		{
			_healthTimer += Time.deltaTime;
			if (_healthTimer > .1f)
			{
				_healthTimer = 0;
				_healthLabel0.Text = _healthLabel1.Text = string.Format("100/{0}", Random.Range(0, 2) == 0 ? 99 : 100);
				_energyLabel0.Text = _energyLabel1.Text = string.Format("100/{0}", Random.Range(0, 2) == 0 ? 99 : 100);
			}
			_scrollTimer0 += Time.deltaTime;
			_leftSlider.Value = Mathf.Abs((_scrollLeftInv + 1) / 2f - Mathf.Pow(_scrollTimer0 / 2f, 3f));
			_rightMiniSlider.Value = 1f - _leftSlider.Value;
			_progress.Value = _leftSlider.Value;
			if (_scrollTimer0 > 2f)
			{
				_scrollTimer0 = 0;
				_scrollLeftInv *= -1;
			}
		}
	}
}