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
                StartCoroutine(ShowNewDialogueText("Let’s just get in class"));
                _NpcName.text = "Raya";
            }
            else 
            {
                StartCoroutine(CallNextScene());
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
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Anonymity.SetActive(false);
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
        _Choice1Panel.SetActive(true);
    }

    IEnumerator ShowNewDialogueText(string _NewLine)
    {
        _StoryText.text = "";

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
        SceneManager.LoadScene("Shared Notes");
    }
    public void Choice1LectureHall()
    {
        _LegendManager._FearCount++;
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.Save();
        _Choice1Panel.SetActive(false);
        _DialogueIndex = 1;
        _CanContinue = false;
        _NpcName.text = "Raya";
        StartCoroutine(ShowNewDialogueText("That's what they always call them."));
    }

    public void Choice2LectureHall()
    {
        _LegendManager._ReputationCount++;
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();
        _Choice1Panel.SetActive(false);
        _DialogueIndex = 1;
        _CanContinue = false;
        _NpcName.text = "Raya";
        StartCoroutine(ShowNewDialogueText("Yeah. It has..."));
    }

    public void Choice3LectureHall()
    {
        _LegendManager._AnonymityCount++;
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.Save();
        _Choice1Panel.SetActive(false);
        _DialogueIndex = 1;
        _CanContinue = false;
        _NpcName.text = "Raya";
        StartCoroutine(ShowNewDialogueText("Okay."));
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
}
