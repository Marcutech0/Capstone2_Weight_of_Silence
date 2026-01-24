using UnityEngine;
using TMPro;
using System.Collections;
public class Deskinteraction : MonoBehaviour
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
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F)) 
        {
            _HasInteracted = true;
            StartCoroutine(ShowDialogueDesk());
        }

        if (_HasInteracted && _CanContinue && Input.GetKeyDown(KeyCode.E)) 
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
                StartCoroutine(ShowNewDialogueTextLiam("If I don’t mess this up, at least one thing stays on track."));
            else
                EndDialogue();
        }
    }
    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
    }

    IEnumerator ShowDialogueDesk() 
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);
        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

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
        if (other.CompareTag("Desk") && !_HasInteracted) 
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
        if (other.CompareTag("Desk")) 
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }
    
}
