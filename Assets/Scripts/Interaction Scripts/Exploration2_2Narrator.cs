using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class Exploration2_2Narrator : MonoBehaviour
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
        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNarratorNewDialogue("No one sits where Raya used to."));
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNarratorNewDialogue("The capstone file opens. Her name is still on the first page."));
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
