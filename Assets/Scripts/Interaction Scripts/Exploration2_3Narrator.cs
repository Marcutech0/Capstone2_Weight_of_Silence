using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exploration2_3Narrator : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public GameObject _ChoicePanel1;
    public GameObject _PhonePanel;
    public GameObject _HomeUI;
    public GameObject _MessagesUI;
    public GameObject _RayaMessageBox;
    public GameObject _PhoneNotif;
    public GameObject _PhoneButtonsUI;
    public GameObject _MessagesApp;
    public TextMeshProUGUI _PhoneNotifText;

    [TextArea] public string _Storyline;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public Animator _PlayerAnimator;
    public GameFlowLegendManager _LegendManager;
    public PhoneExploration1_1 _Phone;
    void Start()
    {
        _NpcName.text = string.Empty;
        StartCoroutine(ShowNarratorDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNarratorNewDialogue("You're suppose to leave the room until your phone buzzes.", true));
            }

            else if (_DialogueIndex == 2)
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
                _CanContinue = true;
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNarratorNewDialogue("—or nothing at all.", false));
                _ChoicePanel1.SetActive(true);
            }

            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowNarratorNewDialogue("The portal updates", true));

            }

            else if (_DialogueIndex == 6)
            {
               _HomeUI.SetActive(true);
                _MessagesApp.SetActive(false);
               _MessagesUI.SetActive(false);
                StartCoroutine(PhoneNotifRoutine());
            }

            else if (_DialogueIndex == 7)
            {
                StartCoroutine(ShowNarratorNewDialogue("No feedback.", true));
                _HomeUI.SetActive(false);
            }

            else
            {
                EndDialogue();
                _PhoneButtonsUI.SetActive(true);
                _MessagesApp.SetActive(true);
            }
        }
    }

    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
        _PlayerAnimator.enabled = true;
       
    }

    public void Choice1()
    {
        _LegendManager._FearCount--;
        _LegendManager._GuiltCount++;
        _LegendManager._AnonymityCount++;
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.Save();
        StartCoroutine(LegendManagerDelay());
        _ChoicePanel1.SetActive(false);
        _DialogueIndex++;
        _HomeUI.SetActive(false);
        _CanContinue = true;
    }

    public void Choice2()
    {
        _LegendManager._FearCount--;
        _LegendManager._AnonymityCount++;
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.Save();
        StartCoroutine(LegendManagerDelay());
        _ChoicePanel1.SetActive(false);
        _DialogueIndex++;
        _HomeUI.SetActive(false);
        _CanContinue = true;
    }

    IEnumerator ShowNarratorDialogue()
    {
        _DialoguePanel.SetActive(true);
        _StoryText.text = "";
        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PlayerAnimator.enabled = false;

        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    IEnumerator ShowNarratorNewDialogue(string _NewLine, bool _AllowContinue)
    {
        _DialoguePanel.SetActive(true);
        _StoryText.text = "";
        _NpcName.text = string.Empty;
        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PlayerAnimator.enabled = false;
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = _AllowContinue;
    }

    IEnumerator ShowNewDialogueRaya(string _NewLine)
    {
        _DialoguePanel.SetActive(true);
        _StoryText.text = "";
        _NpcName.text = "Raya";
        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PlayerAnimator.enabled = false;
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    IEnumerator LegendManagerDelay()
    {
        yield return new WaitForSeconds(1f);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _LegendManager._Anonymity.SetActive(false);
    }

    IEnumerator CallNextScene()
    {
        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PhoneButtonsUI.SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cutscene2.3");
    }

    IEnumerator PhoneNotifRoutine()
    {
        _HomeUI.SetActive(true);
        _PhoneNotif.SetActive(true);
        _PhoneNotifText.text = "Submission received.";

        yield return new WaitForSeconds(1f);
        _PhoneNotif.SetActive(false);
        _CanContinue = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door")) 
        {
            StartCoroutine(CallNextScene());
            
        }
    }
}
