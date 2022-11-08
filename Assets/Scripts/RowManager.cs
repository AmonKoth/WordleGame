using System.Collections;
using UnityEngine;

public class RowManager : MonoBehaviour
{
    [SerializeField]
    private Row[] _rowList;
    [SerializeField]
    private int _wordLength = 5;

    private int _currentLength = 0;
    private string result;
    //For testing delet later
    [SerializeField]
    private string _correctWord = "POINT";
    private int _currentRow = 0;

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
            _rowList[_currentRow].DeleteLetter(_currentLength - 1);

            _guessedWord.Pop();
            _currentLength--;
            Debug.Log($"{_currentLength}");
            return;
        }
        if (_currentLength == _wordLength)
        {
            return;
        }
        _guessedWord.Push(key);
        _rowList[_currentRow].AddLetter(key, _currentLength);
        _currentLength++;
        Debug.Log($"{_currentLength}");
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
            result += reverseStack.Pop();
        }
        Debug.Log($"{result}");
        if (result == _correctWord)
        {
            Debug.Log($"Correct");
            return;
        }
        Debug.Log($"Wrong Word");
        if (_currentRow == _rowList.Length - 1)
        {
            LostTheGame();
        }
        NextRow();
    }

    private void NextRow()
    {
        _currentLength = 0;
        result = "";
        _currentRow++;
    }
    private void LostTheGame()
    {
        Debug.Log($"LOST THE GAME");
    }
}
