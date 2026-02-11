using TMPro;
using UnityEngine;

public class UniversalPhone : MonoBehaviour
{
    public GameObject _PhonePanel;
    public GameObject _MessagesUI;
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

            if (_PhonePanel.activeSelf)
            {
                _PhonePanel.SetActive(false);
                return;
            }
        }
    }

    public void Start()
    {
        //public TextMeshProUGUI _ReplyText;
        //public TextMeshProUGUI _RayaReplyText;
    }
}
