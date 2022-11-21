using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField]
    private Keys _key = Keys.A;
    [SerializeField]
    private Image _buttonBackground;

    private TextMeshProUGUI _textBox;

    public Keys GetKey() => _key;

    public event Action<Keys> KeyPressed = delegate { };

    private void InitializeButton()
    {
        _textBox = GetComponentInChildren<TextMeshProUGUI>();
        if (_textBox != null)
        {
            _textBox.text = _key.ToString();
        }
    }

    public void OnKeyPress()
    {
        KeyPressed(_key);
    }

    private void Awake()
    {
        InitializeButton();
    }
    public void ChangeColor(Color color)
    {
        if (_buttonBackground.color == Color.green)
        {
            return;
        }
        _buttonBackground.color = color;
    }

}
