using UnityEngine;

public class RowManager : MonoBehaviour
{
    [SerializeField]
    private Row[] _rowList;


    private int _currentRow = 0;

    public void OnKeyPress(Keys key)
    {
        if (key == Keys.SUBMIT)
        {
            SubmitPressed();
            return;
        }
        if (key == Keys.BACK)
        {
            _rowList[_currentRow].DeleteLetter();
            return;
        }
        _rowList[_currentRow].AddLetter(key.ToString());
    }

    private void SubmitPressed()
    {
        _currentRow++;
    }
}
