using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exploration2_2GroupOfStudents : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _InteractIndicator;
    public TextMeshProUGUI _NpcName; // since this is a object npc name is empty
    public TextMeshProUGUI _StoryText;
    public TextMeshProUGUI _InteractText;
    public GameObject _ChoicePanel1;
    [TextArea] public string _Storyline;

    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public bool _IsInRange;
    public bool _HasInteracted;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public GameFlowLegendManager _LegendManager;
    public void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            _HasInteracted = true;
            StartCoroutine(ShowDialogueGroupOfStudents());
        }

        if (_HasInteracted && _CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNewDialogueGroupOfStudents("Just to be safe."));
                _ChoicePanel1.SetActive(true);
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNewDialogueLiam("Raya worked on this, her name deserves to stay on the paper."));
            }

            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNewDialogueNarrator("No one responds"));
            }

            else
            {
                EndDialogue();
                StartCoroutine(CallNextScene());
            }
        }
    }
    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _LegendManager._Anonymity.SetActive(false);
        _LegendManager._Reputation.SetActive(false);
    }

    public void Choice1() 
    {
        _LegendManager._FearCount--;
        _LegendManager._CourageCount--;
        _LegendManager._GuiltCount++;
        _LegendManager._AnonymityCount++;
        _LegendManager._ReputationCount++;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _LegendManager._AnonymityText.text = "Anonymity: " + _LegendManager._AnonymityCount;
        _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._Anonymity.SetActive(true);
        _LegendManager._Reputation.SetActive(true);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();
        _ChoicePanel1.SetActive(false);
        _CanContinue = false;
        StartCoroutine(EndDialogueDelay());
        StartCoroutine(CallNextScene());
    }

    public void Choice2() 
    {
        _LegendManager._FearCount++;
        _LegendManager._CourageCount++;
        _LegendManager._GuiltCount--;
        _LegendManager._AnonymityCount--;
        _LegendManager._ReputationCount--;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _LegendManager._AnonymityText.text = "Anonymity: " + _LegendManager._AnonymityCount;
        _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._Anonymity.SetActive(true);
        _LegendManager._Reputation.SetActive(true);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();
        _ChoicePanel1.SetActive(false);
        _DialogueIndex++;
    }

    IEnumerator ShowDialogueGroupOfStudents()
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";
        _NpcName.text = "Groupmate 1";
        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    IEnumerator ShowNewDialogueGroupOfStudents(string _NewLine)
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";
        _NpcName.text = "Groupmate 2";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    IEnumerator ShowNewDialogueLiam(string _NewLine)
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";
        _NpcName.text = "Liam";
        foreach (char c in _NewLine)
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
        _NpcName.text = string.Empty;
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Research Panic Chapter 2");
    }

    IEnumerator EndDialogueDelay()
    {
        yield return new WaitForSeconds(1f);
        _DialoguePanel.SetActive(false);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _LegendManager._Anonymity.SetActive(false);
        _LegendManager._Reputation.SetActive(false);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TwoStudents") && !_HasInteracted)
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
        if (other.CompareTag("TwoStudents"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }
}
