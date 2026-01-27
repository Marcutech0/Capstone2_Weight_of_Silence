using UnityEngine;
using TMPro;
using System.Collections;

public class CutScene1 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;

    [TextArea] public string _Storyline;
    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public bool _IsInRange;
    public bool _HasInteracted;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public GameFlowLegendManager _LegendManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _NpcName.text = "";
        StartCoroutine(ShowDialogue());
    }

    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E)) 
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
                StartCoroutine(ShowNewDialogueRaya("Don’t forget the paper, sleepyhead."));
            else if (_DialogueIndex == 2)
                StartCoroutine(ShowNewDialogueRaya("Also… can we talk later?"));
            else if (_DialogueIndex == 3)
                StartCoroutine(ShowNewInnerDialogue("Later keeps getting further away..."));
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

    IEnumerator ShowDialogue()
    {
        _DialoguePanel.SetActive(true);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";

        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    IEnumerator ShowNewDialogueRaya(string _NewLine)
    {
        _StoryText.text = "";
        _NpcName.text = "Raya";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;

    }

    IEnumerator ShowNewInnerDialogue(string _NewLine) 
    {
        _StoryText.text = "";
        _NpcName.text = "";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
                _CanContinue = true;

    }
}