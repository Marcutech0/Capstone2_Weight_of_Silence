using UnityEngine;
using TMPro;
using System.Collections;
public class FlyerWallInteraction : MonoBehaviour
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

    public void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            _HasInteracted = true;
            StartCoroutine(ShowDialogueDesk());
            _NpcName.text = string.Empty;
        }
    }

    IEnumerator ShowDialogueDesk()
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";

        _LegendManager._ReputationCount++;
        _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
        _LegendManager._Reputation.SetActive(true);

        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();

        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(1f);
        _DialoguePanel.SetActive(false);
        _LegendManager._Reputation.SetActive(false);

        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
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
