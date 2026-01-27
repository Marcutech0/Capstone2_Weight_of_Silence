using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DiningRoomInteraction : MonoBehaviour
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
        if (other.CompareTag("TitaLiza") && !_HasInteracted)
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
        if (other.CompareTag("TitaLiza"))
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
            yield return new WaitForSeconds(1f);
            StartCoroutine(ShowNewDialogueText("They’re just asking questions."));

            yield return new WaitForSeconds(2f);
            StartCoroutine(ShowNewDialogueTextTitaLiza("Questions get people hurt."));
            _Choice1Panel.SetActive(true);
        }
    }

    IEnumerator ShowNewDialogueText(string _NewLine)
    {
        _StoryText.text = "";
        _NpcName.text = "Liam";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
    }

    IEnumerator ShowNewDialogueTextTitaLiza(string _NewLine)
    {
        _StoryText.text = "";
        _NpcName.text = "Tita Liza";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
    }

    IEnumerator CloseUI() 
    {
        yield return new WaitForSeconds(1);
        _DialoguePanel.SetActive(false);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _Choice1Panel.SetActive(false);
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
    }


    public void Choice1DiningRoom() 
    {
        _LegendManager._CourageCount += 3;
        _LegendManager._FearCount += 4;
        _LegendManager._GuiltCount--;
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        StartCoroutine(CloseUI());
    }

    public void Choice2DiningRoom() 
    {
        _LegendManager._CourageCount--;
        _LegendManager._FearCount--;
        _LegendManager._GuiltCount++;
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        StartCoroutine(CloseUI());
    }

    public void Choice3DiningRoom() 
    {
        _LegendManager._FearCount++;
        _LegendManager._GuiltCount++;
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        StartCoroutine(CloseUI());
    }

}
