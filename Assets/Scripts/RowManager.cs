using System.Collections;
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
    private string _correctWord;
    char[] correctWord;
    List<string> _listOfCorrectness;
    /// Until here
    private Stack _guessedWord = new Stack();
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
            _guessedWord.Pop();
            _currentLength--;
            return;
        }
        if (_currentLength == _wordLength)
        {
            return;
        }
        _guessedWord.Push(key);
        _rowList[_currentRow].AddLetter(key, _currentLength);
        _currentLength++;
    }

    private void SubmitPressed()
    {

        if (_currentLength != _wordLength)
        {
            return;
        }

        Stack testStack = _guessedWord;
        Stack reverseStack = new Stack();
        //Nothing to see here just reversing the stack to print the word in correct order
        for (int i = 0; i < _wordLength; i++)
        {
            reverseStack.Push(testStack.Pop());
        }
        for (int i = 0; i < _wordLength; i++)
        {
            string testLetter = reverseStack.Pop().ToString();
            if (testLetter == correctWord[i].ToString())
            {
                _rowList[_currentRow].ChangeColor(Color.green, i);
                _correctCount++;
            }
            else if (_listOfCorrectness.Contains(testLetter))
            {
                _rowList[_currentRow].ChangeColor(Color.yellow, i);
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
        _correctWord = _wordSelector.GetChosenWord();
        correctWord = _correctWord.ToCharArray();
        _listOfCorrectness = new List<string>(_wordLength);
        for (int i = 0; i < correctWord.Length; i++)
        {
            _listOfCorrectness.Add(correctWord[i].ToString());
        }
    }

    private void NextRow()
    {
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
