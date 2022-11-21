using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField]
    private RowManager _rowManager;

    private Button[] _buttons;
    private void InitializeKeyManager()
    {
        _buttons = GetComponentsInChildren<Button>();
        _rowManager.ColorChanged += HandleColorChange;
        foreach (Button button in _buttons)
        {
            button.KeyPressed += KeyPressed;
        }
    }

    private void Awake()
    {
        InitializeKeyManager();
    }

    private void KeyPressed(Keys key)
    {
        if (_rowManager == null)
        {
            return;
        }
        _rowManager.OnKeyPress(key);
    }

    private void HandleColorChange(Color color, char key)
    {
        foreach (Button button in _buttons)
        {
            if (button.GetKey().ToString() == key.ToString())
            {
                button.ChangeColor(color);
            }
        }
    }
}
