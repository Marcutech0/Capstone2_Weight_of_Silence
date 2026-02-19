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
                StartCoroutine(ShowNewDialogueNarrator("Your name is called."));
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueNarrator("Applause rises—polite, practiced."));
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNewDialogueNarrator("You stand."));
            }

            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNewDialogueNarrator("Walk forward."));
            }

            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowNewDialogueNarrator("The capstone title appears on the screen:"));
            }

            else if (_DialogueIndex == 6)
            {
                StartCoroutine(ShowNewDialogueNarrator("Silence as Compliance: Student Expression in Institutional Spaces."));
            }

            else if (_DialogueIndex == 7)
            {
                StartCoroutine(ShowNewDialogueNarrator("For a moment, everything follows the script."));
            }

            else if (_DialogueIndex == 8)
            {
                StartCoroutine(ShowNewDialogueNarrator("Then you stop, The applause thins out, Someone coughs."));
            }

            else if (_DialogueIndex == 9)
            {
                StartCoroutine(ShowNewDialogueNarrator("You hear your own breathing, You look at the panel, Then the crowd. "));
            }
            else if (_DialogueIndex == 10)
            {
                StartCoroutine(ShowNewDialogueNarrator("You hesitate— Just long enough to remember her voice."));
            }
            else if (_DialogueIndex == 11)
            {
                StartCoroutine(ShowNewDialogueLiam("I wasn’t supposed to say anything here."));
            }

            else if (_DialogueIndex == 12)
            {
                StartCoroutine(ShowNewDialogueNarrator("A few heads lift."));
            }

            else if (_DialogueIndex == 13)
            {
                StartCoroutine(ShowNewDialogueLiam("That’s how this works. You submit. You graduate. You move on."));
            }

            else if (_DialogueIndex == 14)
            {
                StartCoroutine(ShowNewDialogueNarrator("A pause."));
            }

            else if (_DialogueIndex == 15)
            {
                StartCoroutine(ShowNewDialogueLiam("But my capstone wouldn’t exist without Raya."));
            }

            else if (_DialogueIndex == 16)
            {
                StartCoroutine(ShowNewDialogueNarrator("The room shifts, A murmur spreads—quiet, contained."));

            }

            else if (_DialogueIndex == 17)
            {
                StartCoroutine(ShowNewDialogueLiam("She did the research people told her not to do, She asked questions people warned her about."));

            }

            else if (_DialogueIndex == 18)
            {
                StartCoroutine(ShowNewDialogueNarrator("You swallow."));
            }

            else if (_DialogueIndex == 19)
            {
                StartCoroutine(ShowNewDialogueLiam("And when she disappeared from the room…, Everyone else learned how to be quiet."));

            }

            else if (_DialogueIndex == 20)
            {
                StartCoroutine(ShowNewDialogueNarrator("The panel doesn’t interrupt, No one tells you to stop, So you keep going."));
            }

            else if (_DialogueIndex == 21)
            {
                StartCoroutine(ShowNewDialogueLiam("Raya taught me that silence isn’t neutral, It’s a choice."));
                    
            }

            else if (_DialogueIndex == 22)
            {
                StartCoroutine(ShowNewDialogueNarrator("Your voice wavers, Then steadies."));

            }

            else if (_DialogueIndex == 23)
            {
                StartCoroutine(ShowNewDialogueLiam("She taught me how to speak, So I’m not stopping now."));

            }

            else if (_DialogueIndex == 24)
            {
                StartCoroutine(ShowNewDialogueNarrator("Silence, Not empty, Listening, Somewhere in the crowd—"));
            }

            else if (_DialogueIndex == 25)
            {
                StartCoroutine(ShowNewDialogueStudent("…Raya."));
            }

            else if (_DialogueIndex == 26)
            {
                StartCoroutine(ShowNewDialogueNarrator("Another voice follows, Then another, You don’t rush offstage, You don’t apologize, You stand there, Unprotected, Seen."));
 
            }

            else if (_DialogueIndex == 27) 
            {
                StartCoroutine(ShowNewDialogueNarrator("Days later, You’re back on campus, Then another, You don’t look away this time, You answer, Not because it’s safe, But because it’s learned."));
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

    IEnumerator ShowNewDialogueStudent(string _NewLine)
    {
        _StoryText.text = "";
        _NpcName.text = "Student";
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
