using UnityEngine;
using TMPro;
using System.Collections;

public class SecurityCheckpointInteraction : MonoBehaviour
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
    public GameFlowLegendManager _LegendManager;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;

    public void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            _HasInteracted = true;
            StartCoroutine(ShowDialogueSecurityCheck());
        }

        if (_HasInteracted && _CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNewDialogueNarrator("He looks at faces longer."));
            }

            else if (_DialogueIndex == 2) 
            {
                StartCoroutine(ShowNewDialogueNarrator("At bags."));
            }

            else if (_DialogueIndex == 3) 
            {
                StartCoroutine(ShowNewDialogueNarrator("At phones."));
            }

            else if (_DialogueIndex == 4) 
            {
                StartCoroutine(ShowNewDialogueNarrator("You hesitate before stepping forward."));
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
        _LegendManager._Fear.SetActive(false);
    }

    IEnumerator ShowDialogueSecurityCheck()
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PlayerAnimator.enabled = false;

        _StoryText.text = "";
        _NpcName.text = string.Empty;
        _LegendManager._FearCount++;  
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.Save();

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
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";

        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Security Checkpoint") && !_HasInteracted)
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
        if (other.CompareTag("Security Checkpoint"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }
}
