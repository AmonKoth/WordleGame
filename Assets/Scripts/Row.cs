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
        if (_activeTextBox == _textBox.Length)
        {
            Debug.Log($"{_activeTextBox}");
            return;
        }
        _textBox[_activeTextBox].text = letter;
        if (_activeTextBox != _textBox.Length - 1)
        {
            _activeTextBox++;
        }
    }

    public void DeleteLetter()
    {

        _textBox[_activeTextBox].text = "";
        if (_activeTextBox != 0)
        {
            _activeTextBox--;
        }
    }

    private void Start()
    {
        InitializeTextFields();
        ClearTexts();
    }
}
