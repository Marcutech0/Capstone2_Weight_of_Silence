using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RayaInteractionLectureHall : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _InteractIndicator;
    public GameObject _Choice1Panel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public TextMeshProUGUI _InteractText;

    [TextArea] public string _Storyline;
    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public bool _IsInRange;
    public bool _HasInteracted;

    public GameFlowLegendManager _LegendManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            _HasInteracted = true;
            StartCoroutine(ShowDialogueRaya());
            _NpcName.text = " ";
        }
    }

    IEnumerator ShowDialogueRaya()
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";

        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        if (_StoryText.text == _Storyline)
        {
            _Choice1Panel.SetActive(true);
        }

    }

    public void Choice1LectureHall()
    {
        _LegendManager._FearCount++;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._Fear.SetActive(true);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.Save();
        _Choice1Panel.SetActive(false);
        _DialoguePanel.SetActive(false);
        StartCoroutine(CloseLegendIndicator());
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
    }

    public void Choice2LectureHall()
    {
        _LegendManager._ReputationCount++;
        _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
        _LegendManager._Reputation.SetActive(true);
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();
        _Choice1Panel.SetActive(false);
        _DialoguePanel.SetActive(false);
        StartCoroutine(CloseLegendIndicator());
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
    }

    public void Choice3LectureHall()
    {
        _LegendManager._AnonymityCount++;
        _LegendManager._AnonymityText.text = "Anonymity: " + _LegendManager._AnonymityCount;
        _LegendManager._Anonymity.SetActive(true);
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.Save();
        _Choice1Panel.SetActive(false);
        _DialoguePanel.SetActive(false);
        StartCoroutine(CloseLegendIndicator());
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raya Lecture Hall") && !_HasInteracted)
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
        if (other.CompareTag("Raya Lecture Hall"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }

    IEnumerator CloseLegendIndicator() 
    {
        yield return new WaitForSeconds(1f);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Anonymity.SetActive(false);
        _LegendManager._Reputation.SetActive(false);
    }
}
