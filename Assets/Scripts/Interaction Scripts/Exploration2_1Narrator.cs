using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Exploration2_1Narrator : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    [TextArea] public string _Storyline;

    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public Animator _PlayerAnimator;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;

    public 
    void Start()
    {
        _NpcName.text = "";
        StartCoroutine(ShowDialogue());
    }

    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNewDialogueNarrator("Everything looks the same. That’s what makes it worse."));
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueNarrator("Students pass by—laughing, checking phones, complaining about deadlines."));
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNewDialogueNarrator("Posters line the walls, but the ones you remember are gone. Torn down. Scribbled over."));
            }

            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNewDialogueNarrator("Replaced with announcements that feel aggressively normal."));
            }

            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowNewDialogueNarrator("At the security desk, IDs are checked longer than before."));
            }

            else if (_DialogueIndex == 6)
            {
                StartCoroutine(ShowNewDialogueNarrator("No one says her name."));
            }

            else if (_DialogueIndex == 7)
            {
                StartCoroutine(ShowNewDialogueNarrator("You keep walking."));
            }

            else
            {
                EndDialogue();
            }

        }
    }

    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _PlayerControls.enabled = true;
        _PlayerController.enabled = true;
        _PlayerAnimator.enabled = true;

    }

    IEnumerator ShowDialogue()
    {
        _DialoguePanel.SetActive(true);
        _StoryText.text = "";
        _PlayerControls.enabled = false;
        _PlayerController.enabled = false;
        _PlayerAnimator.enabled = false;
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
        _NpcName.text = string.Empty;
        _PlayerControls.enabled = false;
        _PlayerController.enabled = false;
        _PlayerAnimator.enabled = false;
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
        SceneManager.LoadScene("Exploration 2.1");
    }
}
