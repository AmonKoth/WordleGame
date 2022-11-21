using System.Collections.Generic;
using UnityEngine;

public class WordSelector : MonoBehaviour
{
    [SerializeField]
    private TextAsset _textFile;

    private HashSet<string> _wordList;
    private char[] _chosenWord;

    private bool _hasWordSelected = false;
    public bool CheckValid(string word) => _wordList.Contains(word);
    public char[] ChooseWord(int wordLength)
    {
        if (_hasWordSelected)
        {
            return _chosenWord;
        }
        _chosenWord = new char[wordLength];
        string[] content = _textFile.text.Split("\n");
        _wordList = new HashSet<string>();

        for (int i = 0; i < content.Length; i++)
        {
            _wordList.Add(content[i].Substring(0, wordLength).ToUpper());
        }

        string randomSelect = content[Random.Range(0, content.Length)].ToUpper();

        // delet LAter
        Debug.Log($"{randomSelect}");

        _chosenWord = randomSelect.ToCharArray();
        _hasWordSelected = true;

        return _chosenWord;
    }





}
