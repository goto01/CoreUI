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

    private string _text =
            "~The quick brown fox jumps~"
        ;
    
    protected virtual void Start()
    {
        _window = CoreUIEditor.Instance.Window(new Rect(0, 0, _pixelSize * 300, _pixelSize * 300), "Item Window Style");
        var w = CoreUIEditor.Instance.Window(new Rect(5*_pixelSize, -5*_pixelSize, _pixelSize * 290, _pixelSize * 290), _window, "Item Window Style");
        var button = CoreUIEditor.Instance.Button(new Rect(0, -20 * _pixelSize, _pixelSize * 300, 0), w, Action);
        button.Id = 1;
        CoreUIEditor.Instance.Button(new Rect(0, -40*_pixelSize, _pixelSize*300, 0), w, Action).Id = 2;
        CoreUIEditor.Instance.Button(new Rect(0, -60*_pixelSize, _pixelSize*300, 0), w, Action).Id = 3;
        _label = CoreUIEditor.Instance.Label(new Rect(0, 0, 0, 0), 
            string.Empty,
            w,
            2, 1, 2, .3f, .3f, "Wave Font");
        _label.FontColor = Color.blue;
        StartCoroutine(Write());
        StartCoroutine(ChangeColor());
        button.Active = false;
    }

    private void Action(int id)
    {
        Debug.Log(id);
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