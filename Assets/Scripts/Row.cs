using UnityEngine;
using TMPro;

public class Row : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _textObjects;
    private TextMeshProUGUI[] _textBox;

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

    public void AddLetter(Keys letter, int activeTextBox)
    {
        _textBox[activeTextBox].text = letter.ToString();
    }

    public void DeleteLetter(int activeTextBox)
    {
        _textBox[activeTextBox].text = "";
    }

    private void Start()
    {
        InitializeTextFields();
        ClearTexts();
    }
}
