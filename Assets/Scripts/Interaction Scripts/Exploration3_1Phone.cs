using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exploration3_1Phone : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _InteractIndicator;
    public TextMeshProUGUI _NpcName; // since this is a object npc name is empty
    public TextMeshProUGUI _StoryText;
    public TextMeshProUGUI _InteractText;
    public GameObject _PhonePanel;
    public GameObject _RayaMessageBox;
    [TextArea] public string _Storyline;

    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public bool _IsInRange;
    public bool _HasInteracted;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public PhoneExploration1_1 _Phone;
    public Exploration3_1BulletinBoard _Board;
    public Exploration3_1Locker _Locker;
    public GameFlowLegendManager _LegendManager;

    public void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            _HasInteracted = true;
            StartCoroutine(ShowDialoguePhone());
        }

        if (_HasInteracted && _CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                _PhonePanel.SetActive(true);
                int _Result = PlayerPrefs.GetInt("ChoiceResult", 0);
                if (_Result == 1)
                {
                    _Phone._ReplyText.text = "I’m alive, promise.";
                    _Phone._RayaReplyText.text = "Good. Don’t disappear on me today, okay?";
                }

                else if (_Result == 2)
                {
                    _Phone._ReplyText.text = "Sure. We’ll talk later.";
                    _Phone._RayaReplyText.text = "Later, then.";
                }

                else if (_Result == 3)
                {
                    _Phone._LiamMessageBox.SetActive(false);
                    _Phone._RayaMessageBox.SetActive(false);
                    _RayaMessageBox.transform.localPosition = new Vector3(-99.93086f, 159, 0);
                }
                _PhonePanel.SetActive(false);
                _CanContinue = true;
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueNarrator("You slide your phone back into your pocket."));
            }

            else
            {
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

        if (_Board._HasInteracted || _Locker._HasInteracted || _HasInteracted || _LegendManager._CourageCount >= 20) 
        {
            StartCoroutine(CallNextSceneEnding1());
        }

        else if (_Board._HasInteracted || _Locker._HasInteracted || _HasInteracted || _LegendManager._CourageCount <= 20) 
        {
            StartCoroutine(CallNextSceneEnding2());
        }
    }

    IEnumerator ShowDialoguePhone()
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

    IEnumerator ShowNewDialogueRaya(string _NewLine)
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";
        _NpcName.text = "Raya";
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Phone") && !_HasInteracted)
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
        if (other.CompareTag("Phone"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }

    IEnumerator CallNextSceneEnding1()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("OutcryEnding");
    }

    IEnumerator CallNextSceneEnding2()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SilentEnding");
    }
}
