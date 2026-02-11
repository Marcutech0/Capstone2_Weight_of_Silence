using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class CutScene2_2 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    [TextArea] public string _Storyline;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public Fade _FadeTransition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _NpcName.text = "";
        StartCoroutine(ShowNarratorDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E)) 
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1) 
            {
                StartCoroutine(ShowNarratorNewDialogue("CAPSTONE PROGRESS REVIEW"));
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNarratorNewDialogue("Midterm submission required."));
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNarratorNewDialogue("All groups must present updated scope and documentation."));
            }

            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNarratorNewDialogue("You scroll."));
            }

            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowNarratorNewDialogue("Raya’s name is still there."));
            }

            else if (_DialogueIndex == 6)
            {
                StartCoroutine(ShowNarratorNewDialogue("No explanation. Just a deadline."));
            }

            else 
            {
                 EndDialogue();
                _FadeTransition.FadeOut();
                 StartCoroutine(CallNextScene());
            }
        }
    }

    IEnumerator ShowNarratorDialogue()
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

    IEnumerator ShowNarratorNewDialogue(string _NewLine)
    {
        _DialoguePanel.SetActive(true);
        _StoryText.text = "";

        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Exploration 2.2");
    }
}
