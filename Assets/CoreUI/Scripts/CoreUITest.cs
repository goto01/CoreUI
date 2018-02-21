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

    private string _text =
            "~The quick brown fox jumps~"
        ;
    
    protected virtual void Start()
    {
        //_window = CoreUIEditor.Instance.Window(new Rect(-_pixelSize*100, _pixelSize*50, _pixelSize*200, _pixelSize*200), "Item Window Style");
        //_slider = CoreUIEditor.Instance.Slider(new Rect(_pixelSize*6, -_pixelSize*6, _pixelSize*188, 0), _window, CoreUIOrientation.Vertical, "RPG Slider Style");
        //_scroll = CoreUIEditor.Instance.Scroll(new Rect(_pixelSize * 20, -_pixelSize * 6, _pixelSize * 300, _pixelSize * 300), _pixelSize * 150, _pixelSize * 150, _window);
        //_sliderHorizontal = CoreUIEditor.Instance.Slider( new Rect(_pixelSize*20, -_pixelSize*180, _pixelSize*180, 0), _window, CoreUIOrientation.Horizontal, "RPG Slider Style");
        var w = CoreUIEditor.Instance.Window(new Rect(0, 0, _pixelSize * 300, _pixelSize * 300), "Item Window Style");
        //CoreUIEditor.Instance.Button(new Rect(0, 0, _pixelSize*300, 0), _scroll, () => { Debug.Log("BUTTON1"); });
        _label = CoreUIEditor.Instance.Label(new Rect(0, 0, 0, 0), 
            string.Empty,
            w,
            2, 1, 2, .3f, .3f, "Wave Font");
        _label.Color = Color.blue;
        StartCoroutine(Write());
        StartCoroutine(ChangeColor());
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
            _label.Color = Color.red;
            yield return new WaitForSeconds(.1f);
            _label.Color = Color.blue;
            yield return new WaitForSeconds(.1f);
        }
    }
    
    protected virtual void Update()
    {
//        _scroll.ScrollVerticalValue = _slider.Value;
//        _scroll.ScrollHorizontalValue = _sliderHorizontal.Value;
//        if (Input.GetKeyDown(KeyCode.Space)) _mode = !_mode;
//        if (_mode) _window.Position = CoreUICameraHandler.Instance.PointerPosition;
    }
}