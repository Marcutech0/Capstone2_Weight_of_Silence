using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutScene1 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public GameObject _Phone;
    [TextArea] public string _Storyline;

    public int _DialogueIndex;
    public bool _CanContinue;
    public Fade _FadeTransition;
    public FadeOutMessages _RayasMessages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _NpcName.text = "";
        StartCoroutine(ShowDialogue());
    }

    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1) 
            {
                _Phone.SetActive(true);
                _DialoguePanel.SetActive(false);
                _RayasMessages.RayaMessageRoutine();
            }

            else if (_DialogueIndex == 3) 
            {
                _Phone.SetActive(false);
                _DialoguePanel.SetActive(true);
                StartCoroutine(ShowNewNarratorDialogue("Liam sits up, rubs his eyes. He stares at the second message longer than the first."));
            }

            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNewInnerDialogue("Later keeps getting further away."));
            }
            else
            {
                EndDialogue();
               _FadeTransition.FadeOut();
                StartCoroutine(CallNextScene());
            }

        }
    }

    public void EndDialogue() 
    {
        _DialoguePanel.SetActive(false);
        
    }

    IEnumerator ShowDialogue()
    {
        _DialoguePanel.SetActive(true);


        _StoryText.text = "";

        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    IEnumerator ShowNewInnerDialogue(string _NewLine) 
    {
        _StoryText.text = "";
        _NpcName.text = "";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
                _CanContinue = true;
    }

    IEnumerator ShowNewNarratorDialogue(string _NewLine)
    {
        _StoryText.text = "";
        _NpcName.text = "";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    

    IEnumerator CallNextScene() 
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Exploration 1.1");
    }
}