using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class Cutscene6 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _Choice1Panel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public GameObject _PhonePanel;
    public TextMeshProUGUI _RayasUnsentMessage;
    [TextArea] public string _Storyline;
    
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public GameFlowLegendManager _LegendManager;
    public Fade _FadeTransition;
    public PhoneExploration1_1 _Phone;


    public void Start()
    {
        _NpcName.text = string.Empty;
        StartCoroutine(ShowDialogueRaya());
    }

    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                _RayasUnsentMessage.text = "Liam, if anything happens—";
                _PhonePanel.SetActive(true);
                _Choice1Panel.SetActive(true);

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
                }
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNewDialogueNarrartor("Siren outside."));
                _PhonePanel.SetActive(false);
            }

            else 
            {
                _FadeTransition.FadeOut();
                StartCoroutine(CallNextScene());
                EndDialogue();
            }
        }
    }

    public void EndDialogue() 
    {
        _DialoguePanel.SetActive(false);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _LegendManager._Reputation.SetActive(false);
        _LegendManager._Anonymity.SetActive(false);
    }

    IEnumerator ShowDialogueRaya()
    {
        _DialoguePanel.SetActive(true);  

        _StoryText.text = "";

        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }

        _CanContinue = true;
    }

    IEnumerator ShowNewDialogueNarrartor(string _Newline)
    {
        _DialoguePanel.SetActive(true);

        _StoryText.text = "";

        foreach (char c in _Newline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }

        _CanContinue = true;
    }

    public void Choice1RayaDorm() 
    {
        _LegendManager._CourageCount += 2;
        _LegendManager._AnonymityCount += 5;
        _LegendManager._GuiltCount -= 2;
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        _Choice1Panel.SetActive(false);
        _DialogueIndex++;
        _CanContinue = true;

    }

    public void Choice2RayaDorm()
    {
        _LegendManager._FearCount += 2;
        _LegendManager._GuiltCount += 2;
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        _Choice1Panel.SetActive(false);
        _DialogueIndex++;
        _CanContinue = true;
    }
    public void Choice3RayaDorm()
    {
        _LegendManager._CourageCount += 4;
        _LegendManager._FearCount += 3;
        _LegendManager._ReputationCount--;
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();
        _Choice1Panel.SetActive(false);
        _DialogueIndex++;
        _CanContinue = true;
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cutscene2.1");
    }
}