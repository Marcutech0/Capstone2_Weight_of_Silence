using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class RayaLibraryInteraction : MonoBehaviour
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
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;

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

        if (_HasInteracted && _CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 2)
            {
                StartCoroutine(ResearchPanicMinigame());
                EndDialogue();
            }
        }
    }
    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Anonymity.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
    } 

    IEnumerator ShowDialogueRaya()
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";
        _NpcName.text = "Raya";
        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        _Choice1Panel.SetActive(true);
    }

    IEnumerator ShowNewDialogueTextRaya(string _NewLine)
    {
        _StoryText.text = "";

        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        _CanContinue = true;
    }
    IEnumerator ResearchPanicMinigame() 
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Research Panic");
    }

    
    public void Choice1Library() 
    {
        _LegendManager._CourageCount++;
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._Courage.SetActive(true);
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.Save();
        _DialogueIndex = 1;
        _CanContinue = false;
        StartCoroutine(ShowNewDialogueTextRaya("I thought so."));
    }

    public void Choice2Library()
    {
        _LegendManager._AnonymityCount++;
        _LegendManager._FearCount++;
        _LegendManager._AnonymityText.text = "Anonymity: " + _LegendManager._AnonymityCount;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._Anonymity.SetActive(true);
        _LegendManager._Fear.SetActive(true);
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.Save();
        _DialogueIndex = 1;
        _CanContinue = false;
        StartCoroutine(ShowNewDialogueTextRaya("That's what most people do."));
    }

    public void Choice3Library()
    {
        _LegendManager._GuiltCount++;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _LegendManager._Guilt.SetActive(true);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        _DialogueIndex = 1;
        _CanContinue = false;
        StartCoroutine(ShowNewDialogueTextRaya("Someone has to."));
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raya Library Hall") && !_HasInteracted)
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
        if (other.CompareTag("Raya Library Hall"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }
}
