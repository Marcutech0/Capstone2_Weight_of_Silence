using TMPro;
using UnityEngine;
using System.Collections;
public class MirrorInteraction : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _InteractIndicator;
    public TextMeshProUGUI _NpcName; // since this is a object npc name is empty
    public TextMeshProUGUI _StoryText;
    public TextMeshProUGUI _InteractText;
    [TextArea] public string _Storyline;

    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public Animator _PlayerAnimator;
    public bool _IsInRange;
    public bool _HasInteracted;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;

    public void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            _HasInteracted = true;
            StartCoroutine(ShowDialogueMirror());
        }

        if (_HasInteracted && _CanContinue && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
                StartCoroutine(ShowNewDialogueTextLiam("That’s the problem."));
            else
                EndDialogue();
        }
    }
    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
        _PlayerAnimator.enabled = true;
    }

    IEnumerator ShowDialogueMirror()
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PlayerAnimator.enabled = false;

        _StoryText.text = "";
        _NpcName.text = string.Empty;
        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;

    }

    IEnumerator ShowNewDialogueTextLiam(string _NewLine)
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mirror") && !_HasInteracted)
        {
            _IsInRange = true;
            _InteractIndicator.SetActive(true);

            if (_HasInteracted)
                _InteractText.text = "Interacted!";
            else
                _InteractText.text = "Press F to Interact";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mirror"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }
}
