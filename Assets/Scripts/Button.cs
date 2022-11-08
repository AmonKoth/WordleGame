using System;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    [SerializeField]
    private Keys _key = Keys.A;



    private TextMeshProUGUI _textBox;
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


}
