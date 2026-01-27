using UnityEngine;
using TMPro;
using System.Collections;
public class PackedBagInteractionRayaDorm : MonoBehaviour
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

    // Update is called once per frame
    public void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            _HasInteracted = true;
            StartCoroutine(ShowDialogueRaya());
            _NpcName.text = "Tita Liza";
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PackedBag") && !_HasInteracted)
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
        if (other.CompareTag("PackedBag"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
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

    IEnumerator CloseUI() 
    {
        yield return new WaitForSeconds(1f);
        _DialoguePanel.SetActive(false);
        _Choice1Panel.SetActive(false);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Anonymity.SetActive(false);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _LegendManager._Reputation.SetActive(false);
    }

    public void Choice1RayaDorm() 
    {
        _LegendManager._CourageCount += 2;
        _LegendManager._AnonymityCount += 5;
        _LegendManager._GuiltCount -= 2;
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Anonymity.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._AnonymityText.text = "Anonymity: " + _LegendManager._AnonymityCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        StartCoroutine(CloseUI());
    }

    public void Choice2RayaDorm()
    {
        _LegendManager._FearCount += 2;
        _LegendManager._GuiltCount += 2;
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        StartCoroutine(CloseUI());
    }
    public void Choice3RayaDorm()
    {
        _LegendManager._CourageCount += 4;
        _LegendManager._FearCount += 3;
        _LegendManager._ReputationCount--;
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Reputation.SetActive(true);
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();
        StartCoroutine(CloseUI());
    }
}