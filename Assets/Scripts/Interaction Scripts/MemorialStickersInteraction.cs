using UnityEngine;
using TMPro;
using System.Collections;
public class MemorialStickersInteraction : MonoBehaviour
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
            StartCoroutine(ShowDialogueMemorialStickers());
        }

        if (_HasInteracted && _CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;
            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNewDialogueNarrator("Names written in paint."));
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueNarrator("Some crossed out."));
            }

            else if (_DialogueIndex == 3) 
            {
                StartCoroutine(ShowNewDialogueNarrator("Some half-erased."));
            }
            else if (_DialogueIndex == 4)
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
    }

    IEnumerator ShowDialogueMemorialStickers()
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";
        _NpcName.text = "";


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
        _NpcName.text = "";

        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Memorial Stickers") && !_HasInteracted)
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
        if (other.CompareTag("Memorial Stickers"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }
}
