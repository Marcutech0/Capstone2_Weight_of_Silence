using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class Exploration3_1Narrator : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    [TextArea] public string _Storyline;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public Animator _PlayerAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _NpcName.text = "";
        StartCoroutine(ShowNarratorDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNarratorNewDialogue("No one sits where Raya used to."));
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNarratorNewDialogue("Fluorescent lights hum overhead."));
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNarratorNewDialogue("Lockers line the walls."));
            }

            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNarratorNewDialogue("Students pass you in tight clusters."));
            }


            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowStudentsNewDialogue("We still have capstone later."));
            }

            else if (_DialogueIndex == 6)
            {
                StartCoroutine(ShowNarratorNewDialogue("Someone laughs."));
            }

            else if (_DialogueIndex == 7)
            {
                StartCoroutine(ShowNarratorNewDialogue("Footsteps echo."));
            }

            else if (_DialogueIndex == 8)
            {
                StartCoroutine(ShowNarratorNewDialogue("You slow down."));
            }

            else if (_DialogueIndex == 9)
            {
                StartCoroutine(ShowNarratorNewDialogue("This hallway hasn’t changed."));
            }

            else if (_DialogueIndex == 10)
            {
                StartCoroutine(ShowNarratorNewDialogue("Only you have."));
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
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
        _PlayerAnimator.enabled = true;
    }

    IEnumerator ShowNarratorDialogue()
    {
        _DialoguePanel.SetActive(true);
        _StoryText.text = "";
        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PlayerAnimator.enabled = false;

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
        _NpcName.text = string.Empty;
        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PlayerAnimator.enabled = false;
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    IEnumerator ShowStudentsNewDialogue(string _NewLine)
    {
        _DialoguePanel.SetActive(true);
        _StoryText.text = "";
        _NpcName.text = "Students";
        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PlayerAnimator.enabled = false;
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }
}
