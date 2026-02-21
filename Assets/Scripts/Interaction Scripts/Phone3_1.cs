using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
public class Phone3_1 : MonoBehaviour
{
    [Header("UI")]
    public Exploration3_1BulletinBoard _BulletinBoard;
    public Exploration3_1Locker _Locker;
    public GameObject _PhoneButtonUI;
    public GameObject _DialoguePanel;
    public GameObject _PhonePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public GameObject _RayaMessageBox;
    public GameObject _RayaMessageBox2;
    public PhoneExploration1_1 _Phone;

    [TextArea] public string _Storyline;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] private bool _CanContinue;
    [SerializeField] private bool _EndingDialogueFinished;
    [SerializeField] private bool _PhoneButtonOn;
    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public Animator _PlayerAnimator;
    public GameFlowLegendManager _LegendManager;

    
    void Update()
    {
        if (!_PhoneButtonOn && _BulletinBoard._HasInteracted && _Locker._HasInteracted && _Locker._DialogueFinished) 
        {
            _PhoneButtonUI.SetActive(true);
            _PhoneButtonOn = true;
        }

        if (_CanContinue && Input.GetKeyDown(KeyCode.Mouse0))
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
                    _RayaMessageBox.SetActive(true);
                    _RayaMessageBox2.SetActive(true);
                }

                else if (_Result == 2)
                {
                    _Phone._ReplyText.text = "Sure. We’ll talk later.";
                    _Phone._RayaReplyText.text = "Later, then.";
                    _RayaMessageBox.SetActive(true);
                    _RayaMessageBox2.SetActive(true);
                }

                else if (_Result == 3)
                {
                    _Phone._LiamMessageBox.SetActive(false);
                    _Phone._RayaMessageBox.SetActive(false);
                    _RayaMessageBox.transform.localPosition = new Vector3(-62f, -85f, 0);
                    _RayaMessageBox2.transform.localPosition = new Vector3(-62f, -146f, 0);
                    _RayaMessageBox.SetActive(true);
                    _RayaMessageBox2.SetActive(true);
                }
                _CanContinue = true;
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueNarrator("You slide your phone back into your pocket."));
                _PhoneButtonUI.SetActive(false);
            }

            else if (_DialogueIndex == 3)
            {
                EndingRoute();
                _PhonePanel.SetActive(false);
            }
        }
    }

    public void EndingProc() 
    {
        StartCoroutine(ShowDialoguePhone());
    }

    public void ClosePhoneButton() 
    {
        if (_PhoneButtonUI.activeSelf) 
        {
            _PhoneButtonUI.SetActive(false);
        }
    }

    public void EndingRoute() 
    {
        if (_LegendManager._CourageCount >= 18) 
        {
            _DialoguePanel.SetActive(false);
            StartCoroutine(CallNextSceneEnding1());
        }

        else 
        {
            StartCoroutine(CallNextSceneEnding2());
        }
    }

    IEnumerator ShowDialoguePhone()
    {
        _DialoguePanel.SetActive(true);
        _PhoneButtonUI.SetActive(false);
        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PlayerAnimator.enabled = false;

        _StoryText.text = "";
        _NpcName.text = string.Empty;
        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;

    }

    IEnumerator ShowNewDialogueNarrator(string _NewLine)
    {
        _DialoguePanel.SetActive(true);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;
        _PlayerAnimator.enabled = false;

        _StoryText.text = "";
        _NpcName.text = string.Empty;
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
        _EndingDialogueFinished = true;

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
