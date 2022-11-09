
using UnityEngine;

public class WordSelector : MonoBehaviour
{
    [SerializeField]
    private TextAsset _textFile;


    private string _chosenWord;

    public string GetChosenWord() => _chosenWord;
    private void ChoseWord()
    {
        var content = _textFile.text.Split("\n");
        _chosenWord = content[Random.Range(0, content.Length)].ToUpper();
        Debug.Log($"{_chosenWord}");
    }

    private void Awake()
    {
        ChoseWord();
    }
}
