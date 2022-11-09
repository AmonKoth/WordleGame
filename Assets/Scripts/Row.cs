using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Row : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _textObjects;

    private TextMeshProUGUI[] _textBox;
    private Image[] _textBoxImages;

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
        _textBoxImages = new Image[_textObjects.Length];
        for (int i = 0; i < _textObjects.Length; i++)
        {
            _textBox[i] = _textObjects[i].GetComponentInChildren<TextMeshProUGUI>();
            _textBoxImages[i] = _textObjects[i].GetComponent<Image>();
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
    public void ChangeColor(Color color, int index)
    {
        _textBoxImages[index].color = color;
    }

    private void Start()
    {
        InitializeTextFields();
        ClearTexts();
    }
}
