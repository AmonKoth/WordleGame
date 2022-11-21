using System;
using System.Collections.Generic;
using UnityEngine;

public class RowManager : MonoBehaviour
{
    [SerializeField]
    private Row[] _rowList;
    [SerializeField]
    private int _wordLength = 5;
    [SerializeField]
    private WordSelector _wordSelector;

    private int _currentLength = 0;
    private int _correctCount = 0;
    private int _currentRow = 0;

    //For testing delet later all later
    private char[] _correctWord;
    private string guessedWord;
    private HashSet<char> hashSetofCorrectWord;
    /// Until here

    public event Action<Color, char> ColorChanged = delegate { };

    public void OnKeyPress(Keys key)
    {
        if (key == Keys.SUBMIT)
        {
            SubmitPressed();
            return;
        }
        if (key == Keys.BACK)
        {
            if (_currentLength == 0)
            {
                return;
            }
            _rowList[_currentRow].DeleteLetter(_currentLength - 1);
            guessedWord = guessedWord.Substring(0, _currentLength - 1);
            _currentLength--;
            return;
        }

        if (_currentLength == _wordLength)
        {
            return;
        }
        guessedWord += key.ToString();
        _rowList[_currentRow].AddLetter(key, _currentLength);
        Debug.Log($"{guessedWord}");
        _currentLength++;
    }

    private void SubmitPressed()
    {
        if (_currentLength != _wordLength)
        {
            return;
        }
        if (!_wordSelector.CheckValid(guessedWord))
        {
            Debug.Log($"Not a valid Word");
            return;
        }

        char[] guessArray = guessedWord.ToCharArray();

        for (int i = 0; i < _wordLength; i++)
        {
            if (guessArray[i] == _correctWord[i])
            {
                _rowList[_currentRow].ChangeColor(Color.green, i);
                ColorChanged(Color.green, guessArray[i]);
                _correctCount++;
            }
            else if (hashSetofCorrectWord.Contains(guessArray[i]))
            {
                _rowList[_currentRow].ChangeColor(Color.yellow, i);
                ColorChanged(Color.yellow, guessArray[i]);
            }
        }
        if (_correctCount == _wordLength)
        {
            Debug.Log($"YOU WON");
            return;
        }
        Debug.Log($"Wrong Word");
        if (_currentRow == _rowList.Length - 1)
        {
            LostTheGame();
        }
        NextRow();
    }
    private void InitializeCorrectWord()
    {
        _correctWord = _wordSelector.ChooseWord(_wordLength);
        hashSetofCorrectWord = new HashSet<char>(_wordLength);
        for (int i = 0; i < _wordLength; i++)
        {
            hashSetofCorrectWord.Add(_correctWord[i]);
        }
    }

    private void NextRow()
    {
        guessedWord = "";
        _currentLength = 0;
        _correctCount = 0;
        _currentRow++;
    }
    private void LostTheGame()
    {
        Debug.Log($"LOST THE GAME");
    }
    private void Awake()
    {
        InitializeCorrectWord();
    }
}
