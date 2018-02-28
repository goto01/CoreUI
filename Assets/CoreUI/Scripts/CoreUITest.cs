using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UICore;
using UICore.Controls;
using UICore.Controls.Containers;
using UICore.Controls.Text;
using UICore.StylesSystem.Styles.Font;
using UICore.UICoreMeshes.Meshes;
using UnityEngine;

internal class CoreUITest: MonoBehaviour
{
    private const float _pixelSize = 1/32f;
    private CoreUIContainer _window;
    private CoreUISlider _slider;
    private CoreUISlider _sliderHorizontal;
    private CoreUIScroll _scroll;
    private CoreUILabel _label;
    private bool _mode;
    private CoreUIWindow _secondWindow;
    private CoreUIFlexibleImage _barHorizontal;
    private CoreUIFlexibleImage _barVertical;
    private CoreUIWindow _window0;
    private CoreUIWindow _window1;
    private CoreUIWindow _window0Below;
    private CoreUIWindow _window1Below;
    
    private string _text =
            "~The quick brown fox jumps~"
        ;
    
    protected virtual void Start()
    {
        _window = CoreUIEditor.Instance.Window(new Rect(-150*_pixelSize, 0, _pixelSize * 300, _pixelSize * 300), "Item Window Style");
        var w = CoreUIEditor.Instance.Window(new Rect(5*_pixelSize, -5*_pixelSize, _pixelSize * 290, _pixelSize * 290), _window, "Item Window Style");
        var button = CoreUIEditor.Instance.Button(new Rect(5 * _pixelSize, -20 * _pixelSize, _pixelSize * 280, 0), w, Action);
        button.Id = 1;
        _label = CoreUIEditor.Instance.Label(new Rect(5 * _pixelSize, -5 * _pixelSize, 0, 0), 
            string.Empty,
            w,
            2, 1, 2, .3f, .3f, "Wave Font");
        _label.FontColor = Color.blue;
        StartCoroutine(Write());
        StartCoroutine(ChangeColor());

        _secondWindow = CoreUIEditor.Instance.Window(new Rect(_pixelSize * 155, 0, _pixelSize * 300, _pixelSize * 300), "Item Window Style");
        _secondWindow.Enabled = false;
        CoreUIEditor.Instance.Label(new Rect(5 * _pixelSize, -5*_pixelSize, 0, 0), "±The quick brown fox jumps±", _secondWindow, 2, 1, 2, .3f, .3f, "Wave Font");
        button = CoreUIEditor.Instance.Button(new Rect(5 * _pixelSize, -20 * _pixelSize, _pixelSize * 280, 0), _secondWindow, Action);
        _barHorizontal = CoreUIEditor.Instance.FlexibleImage(new Rect(5 * _pixelSize, -40 * _pixelSize, _pixelSize * 100, 0), _secondWindow, CoreUIOrientation.Horizontal ,"Health Bar Style");
        _barVertical = CoreUIEditor.Instance.FlexibleImage(new Rect(105 * _pixelSize, -40 * _pixelSize, _pixelSize * 100, 0), _secondWindow, CoreUIOrientation.Vertical ,"Health Bar Style");
        CoreUIEditor.Instance.Button(new Rect(5 * _pixelSize, -60 * _pixelSize, _pixelSize * 140, 0), _secondWindow, i =>
        {
            _barHorizontal.Value += .1f;
            _barVertical.Value += .1f;
        });
        CoreUIEditor.Instance.Button(new Rect(145 * _pixelSize, -60 * _pixelSize, _pixelSize * 140, 0), _secondWindow, i =>
        {
            _barHorizontal.Value -= .1f;
            _barVertical.Value -= .1f;
        });

        _window0 = CoreUIEditor.Instance.Window(new Rect(CoreUICameraHandler.Instance.LeftTopPosition, new Vector2(250 * _pixelSize, 200 * _pixelSize)), "Item Window Style");
        CoreUIEditor.Instance.Label(new Rect(10 * _pixelSize, -10 * _pixelSize, 0, 0), "Show right window", _window0, "Wave Font");
        CoreUIEditor.Instance.Button(new Rect(10 * _pixelSize, -25 * _pixelSize, 230 * _pixelSize, 0), _window0, ChangeVisibilityOfWindows);
        CoreUIEditor.Instance.Label(new Rect(10 * _pixelSize, -45 * _pixelSize, 0, 0), "Activate/Disable window below", _window0, "Wave Font");
        CoreUIEditor.Instance.Button(new Rect(10 * _pixelSize, -60 * _pixelSize, 230 * _pixelSize, 0), _window0, ChangeActivityOfWindow0Below);
        _window0Below = CoreUIEditor.Instance.Window(new Rect(10 * _pixelSize, -80 * _pixelSize, 230 * _pixelSize, 110 * _pixelSize), _window0, "Item Window Style");
        var window0BelowScrollSlider = CoreUIEditor.Instance.Slider(new Rect(213*_pixelSize, -7*_pixelSize, 96 * _pixelSize, 0), _window0Below, CoreUIOrientation.Vertical, "RPG Slider Style");
        var window0BelowScroll = CoreUIEditor.Instance.Scroll(new Rect(7 * _pixelSize, -7 * _pixelSize, 204 * _pixelSize, 200 * _pixelSize), 204 * _pixelSize, 96 * _pixelSize, null, window0BelowScrollSlider, _window0Below);
        CoreUIEditor.Instance.Label(new Rect(0, 0, 0, 0), "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n", window0BelowScroll, "Wave Font");
        
        _window1 = CoreUIEditor.Instance.Window(new Rect(CoreUICameraHandler.Instance.LeftTopPosition + new Vector2(250 * _pixelSize, 0), new Vector2(250 * _pixelSize, 200 * _pixelSize)), "Item Window Style");
        _window1.Enabled = false;
        CoreUIEditor.Instance.Label(new Rect(10 * _pixelSize, -10 * _pixelSize, 0, 0), "Show left window", _window1, "Wave Font");
        CoreUIEditor.Instance.Button(new Rect(10 * _pixelSize, -25 * _pixelSize, 230 * _pixelSize, 0), _window1, ChangeVisibilityOfWindows);
        CoreUIEditor.Instance.Label(new Rect(10 * _pixelSize, -45 * _pixelSize, 0, 0), "Activate/Disable window below", _window1, "Wave Font");
        CoreUIEditor.Instance.Button(new Rect(10 * _pixelSize, -60 * _pixelSize, 230 * _pixelSize, 0), _window1, ChangeActivityOfWindow1Below);
        _window1Below = CoreUIEditor.Instance.Window(new Rect(10 * _pixelSize, -80 * _pixelSize, 230 * _pixelSize, 110 * _pixelSize), _window1, "Item Window Style");
        window0BelowScrollSlider = CoreUIEditor.Instance.Slider(new Rect(7*_pixelSize, -95*_pixelSize, 216 * _pixelSize, 0), _window1Below, CoreUIOrientation.Horizontal, "RPG Slider Style");
        window0BelowScroll = CoreUIEditor.Instance.Scroll(new Rect(7 * _pixelSize, -7 * _pixelSize, 400 * _pixelSize, 90 * _pixelSize), 216 * _pixelSize, 86 * _pixelSize, window0BelowScrollSlider, null, _window1Below);
        CoreUIEditor.Instance.Label(new Rect(0, 0, 0, 0), "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n" +
                                                          "The quick brown fox jumps\nover the lazy dog\n", window0BelowScroll, "Wave Font");
    }

    private void ChangeVisibilityOfWindows(int i1)
    {
        _window0.Enabled = !_window0.Enabled;
        _window1.Enabled = !_window1.Enabled;
    }

    private void ChangeActivityOfWindow0Below(int i)
    {
        _window0Below.Active = !_window0Below.Active;
    }

    private void ChangeActivityOfWindow1Below(int i)
    {
        _window1Below.Active = !_window1Below.Active;
    }

    private void Action(int id)
    {
        _window.Enabled = !_window.Enabled;
        _secondWindow.Enabled = !_secondWindow.Enabled;
    }

    private IEnumerator Write()
    {
        _label.Text = _text;
        var index = 0;
        while (index < _text.Length)
        {
            _label.ShowSymbols(0, index+++1);
            yield return new WaitForSeconds(.1f); 
        }
    }

    private IEnumerator ChangeColor()
    {
        while (true)
        {
            _label.FontColor = Color.red;
            yield return new WaitForSeconds(.1f);
            _label.FontColor = Color.blue;
            yield return new WaitForSeconds(.1f);
        }
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _window.Active = !_window.Active;
    }
}