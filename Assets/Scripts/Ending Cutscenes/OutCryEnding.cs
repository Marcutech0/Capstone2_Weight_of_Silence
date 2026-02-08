using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class OutCryEnding : MonoBehaviour
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
                StartCoroutine(ShowNewDialogueNarrator("You stand where Raya used to sit."));
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueNarrator("The room feels too quiet."));
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNewDialogueNarrator("You take a breath."));
            }

            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNewDialogueLiam("My capstone wouldn’t exist without Raya."));
            }

            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowNewDialogueNarrator("Your voice wavers — then steadies."));
            }

            else if (_DialogueIndex == 6)
            {
                StartCoroutine(ShowNewDialogueLiam("She taught me how to speak. So I’m not stopping now."));
            }

            else if (_DialogueIndex == 7)
            {
                StartCoroutine(ShowNewDialogueNarrator("Days later."));
            }

            else if (_DialogueIndex == 8)
            {
                StartCoroutine(ShowNewDialogueNarrator("You’re back on campus."));
            }

            else if (_DialogueIndex == 9)
            {
                StartCoroutine(ShowNewDialogueNarrator("Someone says her name."));
            }
            else if (_DialogueIndex == 10)
            {
                StartCoroutine(ShowNewDialogueNarrator("Then another."));
            }
            else if (_DialogueIndex == 11)
            {
                StartCoroutine(ShowNewDialogueNarrator("You don’t look away this time."));
            }

            else if (_DialogueIndex == 12)
            {
                StartCoroutine(ShowNewDialogueNarrator("You answer."));
            }

            else if (_DialogueIndex == 13)
            {
                StartCoroutine(ShowNewDialogueNarrator("Not because it’s safe."));
            }

            else if (_DialogueIndex == 14)
            {
                StartCoroutine(ShowNewDialogueNarrator("But because it’s learned."));
            }

            else
            {
                EndDialogue();
                _FadeText.FadeRoutine();
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
        _NpcName.text = "";
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
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("");
    }
}
