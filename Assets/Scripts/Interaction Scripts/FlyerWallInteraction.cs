using UnityEngine;
using TMPro;
using System.Collections;
public class FlyerWallInteraction : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _InteractIndicator;
    public GameObject _NPC;
    public TextMeshProUGUI _NpcName; // since this is a object npc name is empty
    public TextMeshProUGUI _StoryText;
    public TextMeshProUGUI _InteractText;
    [TextArea] public string _Storyline;

    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public bool _IsInRange;
    public bool _HasInteracted;
    public GameFlowLegendManager _LegendManager;
    public NewsScreenInteract _FlyerWallInteraction;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    [SerializeField] bool _HasInteractedNewsScreen;

    public void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            _HasInteracted = true;
            StartCoroutine(ShowDialogueFlyerWall());
        }

        if (_HasInteracted && _CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNewDialogueLiam("Someone didn’t want this seen."));
                _LegendManager._ReputationCount++;
                _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
                _LegendManager._Reputation.SetActive(true);
                PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
                PlayerPrefs.Save();
            }
            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueLiam("They said someone got tagged last night."));
                _NpcName.text = "Overheard Students";
                _LegendManager._FearCount++;
                _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
                _LegendManager._Fear.SetActive(true);
                PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
                PlayerPrefs.Save();
            }
            else
                EndDialogue();
        }
    }
    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
        _LegendManager._Reputation.SetActive(false);
        _LegendManager._Fear.SetActive(false);
    }

    IEnumerator ShowDialogueFlyerWall()
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flyer Wall") && !_HasInteracted)
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
        if (other.CompareTag("Flyer Wall"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }
}
