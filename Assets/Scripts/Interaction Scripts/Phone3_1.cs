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
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public GameObject _PhonePanel;
    public GameObject _RayaMessageBox;
    public PhoneExploration1_1 _Phone;

    [TextArea] public string _Storyline;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    [SerializeField] bool _EndingDialogueFinished;
    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public Animator _PlayerAnimator;
    public GameFlowLegendManager _LegendManager;

    
    void Update()
    {
        if (_BulletinBoard._HasInteracted && _Locker._HasInteracted && _Locker._DialogueFinished) 
        {
            _PhoneButtonUI.SetActive(true);
        }

        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
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
                _CanContinue = true;
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueNarrator("You slide your phone back into your pocket."));
                _PhonePanel.SetActive(false);
            }

            else if (_DialogueIndex == 3)
            {
                EndingRoute();
            }
        }
    }

    public void EndingProc() 
    {
        StartCoroutine(ShowDialoguePhone());
    }

    public void EndingRoute() 
    {
        if (_LegendManager._CourageCount >= 18) 
        {
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
