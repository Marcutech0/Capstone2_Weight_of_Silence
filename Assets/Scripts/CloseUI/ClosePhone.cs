using UnityEngine;

public class ClosePhone : MonoBehaviour
{
    public GameObject _HomeUI;
    public GameObject _MessagesUI;
    public GameObject _ReplyChoice1;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_ReplyChoice1.activeSelf)
            {
                _ReplyChoice1.SetActive(false);
            }

            else if (_MessagesUI.activeSelf)
            {
                _MessagesUI.SetActive(false);
            }
            else if (_HomeUI.activeSelf)
            {
                _HomeUI.SetActive(false);
            }
        }
    }
}
