using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    /// Until here

    public event Action<Color, char> ColorChanged = delegate { };
    public event Action<bool, char[]> GameWon = delegate { };

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
        char[] testArray = _correctWord;
        Dictionary<int, LetterPlace> colorTheGuess = new Dictionary<int, LetterPlace>();
        //Color assign part
        for (int i = 0; i < _wordLength; i++)
        {
            if (guessArray[i] == _correctWord[i])
            {
                testArray = testArray.Where(letter => letter != _correctWord[i]).ToArray();
                colorTheGuess[i] = LetterPlace.CORRECT;
                _correctCount++;
            }
        }

        HashSet<char> remainingLetters = new HashSet<char>(testArray.Length - 1);

        for (int i = 0; i < testArray.Length; i++)
        {
            remainingLetters.Add(testArray[i]);
        }

        for (int i = 0; i < guessArray.Length; i++)
        {
            if (remainingLetters.Contains(guessArray[i]))
            {
                colorTheGuess[i] = LetterPlace.WRONGPLACE;
            }
        }

        //Coloring Part
        for (int index = 0; index < _wordLength; index++)
        {
            LetterPlace isExists;
            colorTheGuess.TryGetValue(index, out isExists);
            if (isExists == LetterPlace.CORRECT)
            {
                _rowList[_currentRow].ChangeColor(Color.green, index);
                ColorChanged(Color.green, guessArray[index]);
            }
            else if (isExists == LetterPlace.WRONGPLACE)
            {
                _rowList[_currentRow].ChangeColor(Color.yellow, index);
                ColorChanged(Color.yellow, guessArray[index]);
            }
            else
            {
                _rowList[_currentRow].ChangeColor(Color.gray, index);
                ColorChanged(Color.gray, guessArray[index]);
            }
        }

        colorTheGuess.Clear();

        if (_correctCount == _wordLength)
        {

            Debug.Log($"YOU WON");
            GameWon(true, _correctWord);
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
        GameWon(false, _correctWord);
        Debug.Log($"LOST THE GAME");
    }
    private void Awake()
    {
        InitializeCorrectWord();
    }
}
