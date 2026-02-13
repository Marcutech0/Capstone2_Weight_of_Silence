using TMPro;
using UnityEngine;

public class UniversalPhone : MonoBehaviour
{
    public GameObject _PhonePanel;
    public GameObject _MessagesUI;
    public PhoneExploration1_1 _Phone;
    public void OpenPhone()
    {
        _PhonePanel.SetActive(true);
    }

    public void OpenMessages()
    {
        _MessagesUI.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {

            if (_MessagesUI.activeSelf)
            {
                _MessagesUI.SetActive(false);
                return;
            }

            if (_Phone._HomeUI.activeSelf)
            {
                _PhonePanel.SetActive(false);
                return;
            }
        }
    }

    public void Start()
    {
        int _Result = PlayerPrefs.GetInt("ChoiceResult", 0);

        if (_Result == 1)
        {
            _Phone._ReplyText.text = "I’m alive, promise.";
            _Phone._RayaReplyText.text = "Good. Don’t disappear on me today, okay?";
        }

        else if (_Result == 2)
        {
            _Phone._ReplyText.text = "Sure. We’ll talk later.";
            _Phone._RayaReplyText.text = "Later, then.";
        }

        else if (_Result == 3)
        {
            _Phone._LiamMessageBox.SetActive(false);
            _Phone._RayaMessageBox.SetActive(false);
        }
    }
}
