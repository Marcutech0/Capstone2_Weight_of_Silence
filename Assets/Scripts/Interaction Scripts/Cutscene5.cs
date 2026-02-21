using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cutscene5 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName; // since this is a object npc name is empty
    public TextMeshProUGUI _StoryText;
    [TextArea] public string _Storyline;

    public GameFlowLegendManager _LegendManager;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public Fade _FadeTransition;
    void Start()
    {
        _NpcName.text = "Liam";
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
                StartCoroutine(ShowNewDialogueNarrator("The phone lights up."));
            }
            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueNarrator("No new message."));
            }
            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNewDialogueNarrator("Just the time."));
            }
            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNewDialogueLiam("She always replies by now."));
            }
            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowNewDialogueNarrator("Rain streaks down the glass."));
            }
            else if (_DialogueIndex == 6)
            {
                StartCoroutine(ShowNewDialogueLiam("Unless she’s busy."));
            }
            else if (_DialogueIndex == 7)
            {
                StartCoroutine(ShowNewDialogueLiam("..."));
            }
            else if (_DialogueIndex == 8)
            {
                StartCoroutine(ShowNewDialogueLiam("Unless something’s wrong."));
            }
            else if (_DialogueIndex == 9)
            {
                StartCoroutine(ShowNewDialogueLiam("I need to check on her."));
            }
            else if (_DialogueIndex == 10)
            {
                _FadeTransition.FadeOut();
                StartCoroutine(CallNextScene());
                EndDialogue();
            }
        }
    }

    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Exploration 1.4");
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

    IEnumerator ShowNewDialogueNarrator(string _NewLine)
    {
        _DialoguePanel.SetActive(true);

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
        _DialoguePanel.SetActive(true);

        _StoryText.text = "";

        _NpcName.text = "Liam";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }
}
