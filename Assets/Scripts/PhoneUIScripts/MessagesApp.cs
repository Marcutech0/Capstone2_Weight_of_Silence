using UnityEngine;

public class MessagesApp : MonoBehaviour
{
    public GameObject _MessageApp;
    public GameObject _HomeUI;
    public CutScene1 _DialogueIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void OpenMessageApp()
    {
        _MessageApp.SetActive(true);
        _HomeUI.SetActive(false);
        _DialogueIndex._DialogueIndex = 2;
        _DialogueIndex._CanContinue = true;
    }
}
