using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Row : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _textObjects;
    private TextMeshProUGUI[] _textBox;

    private int _activeTextBox = 0;
    private void ClearTexts()
    {
        foreach (TextMeshProUGUI textBox in _textBox)
        {
            textBox.text = " ";
        }
    }

    private void InitializeTextFields()
    {
        _textBox = new TextMeshProUGUI[_textObjects.Length];
        for (int i = 0; i < _textObjects.Length; i++)
        {
            _textBox[i] = _textObjects[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void AddLetter(string letter)
    {
        _textBox[_activeTextBox].text = letter;
        _activeTextBox++;
    }

    public void DeleteLetter()
    {
        if (_activeTextBox == 0)
        {
            return;
        }
        _textBox[_activeTextBox].text = "";
        _activeTextBox--;
    }

    private void Start()
    {
        InitializeTextFields();
        ClearTexts();
    }
}
