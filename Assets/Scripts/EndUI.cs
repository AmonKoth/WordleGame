using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndUI : MonoBehaviour
{
    [SerializeField]
    private RowManager _rowManager;

    [SerializeField]
    private TextMeshProUGUI _winText;

    [SerializeField]
    private TextMeshProUGUI _correctTextBox;

    [SerializeField]
    private GameObject _UIElemets;

    private string _correctWord;

    private GameObject[] _childs;
    private void Awake()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        _rowManager.GameWon += GameEnd;
        _UIElemets.SetActive(false);

    }

    private void GameEnd(bool won, char[] correctWord)
    {
        _UIElemets.SetActive(true);
        if (won)
        {
            _winText.text = " YOU WON!!";
        }
        else
        {
            _winText.text = "YOU LOST!!";
        }
        foreach (char letter in correctWord)
        {
            _correctWord += letter;
        }
        _correctTextBox.text = _correctWord;
    }

    public void OnRestartPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
