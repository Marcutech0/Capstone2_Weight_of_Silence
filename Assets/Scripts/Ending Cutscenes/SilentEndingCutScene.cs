using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class SilentEndingCutScene : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    [TextArea] public string _Storyline;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public FadeText _FadeText;
    void Start()
    {
        _NpcName.text = "";
        StartCoroutine(ShowNarratorDialogue());
    }

    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNewDialogueNarrator("Your name is called."));
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueNarrator("Applause comes on cue."));
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNewDialogueNarrator("Your capstone appears on the screen."));
            }

            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNewDialogueNarrator("Clean."));
            }

            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowNewDialogueNarrator("Neutral."));
            }

            else if (_DialogueIndex == 6)
            {
                StartCoroutine(ShowNewDialogueNarrator("Raya’s name is gone."));
            }

            else if (_DialogueIndex == 7)
            {
                StartCoroutine(ShowNewDialogueNarrator("Later, you walk past the campus alone."));
            }

            else if (_DialogueIndex == 8)
            {
                StartCoroutine(ShowNewDialogueNarrator("Everything worked."));
            }

            else if (_DialogueIndex == 9)
            {
                StartCoroutine(ShowNewDialogueLiam("I did what I had to."));
            }

            else
            {
                EndDialogue();
                StartCoroutine(CallNextScene());
            }

        }
    }

    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);

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

    IEnumerator ShowNewDialogueNarrator(string _NewLine)
    {
        _StoryText.text = "";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;

    }

    IEnumerator ShowNewDialogueLiam(string _NewLine)
    {
        _StoryText.text = "";
        _NpcName.text = "Liam";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;

    }

    IEnumerator CallNextScene()
    {
         yield return (_FadeText.FadeRoutine());
        SceneManager.LoadScene("StartMainMenu");
    }
}
