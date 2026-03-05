using UnityEngine;

public class ClosePhone : MonoBehaviour
{
    public GameObject _HomeUI;
    public GameObject _MessagesUI;
    public GameObject _ReplyChoice1;

   
    public void TogglePhone() 
    {
        bool _IsOpen = _HomeUI.activeSelf || _MessagesUI.activeSelf || _ReplyChoice1.activeSelf;

        if (_IsOpen)
        {
            _HomeUI.SetActive(false);
            _MessagesUI.SetActive(false);
            _ReplyChoice1.SetActive(false);
        }

        else 
        {
            _HomeUI.SetActive(true);
        }
    }
}
