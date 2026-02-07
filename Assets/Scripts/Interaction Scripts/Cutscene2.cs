using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Cutscene2 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _Choice2Panel;
    public GameObject _Choice3Panel;
    public TextMeshProUGUI _NpcName; 
    public TextMeshProUGUI _StoryText;

    [TextArea] public string _Storyline;   
    public GameFlowLegendManager _LegendManager;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public Fade _FadeTransition;

    private void Start()
    {
        _NpcName.text = "Raya";
        StartCoroutine(ShowDialogueRaya());
    }
    public void Update()
    {
        if ( _CanContinue && Input.GetKeyDown(KeyCode.E)) 
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueText("Hey… can we talk later? Just us."));
            }

            else if (_DialogueIndex == 3)
            {
                _Choice3Panel.SetActive(true);

            }

            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowNewDialogueTextRayaTaho("Let’s get taho first."));
                _NpcName.text = "Liam";

            }

            else if (_DialogueIndex == 6)
            {
                StartCoroutine(ShowNewDialogueTextRayaTaho("Yeah. Before the day ruins it."));
                _NpcName.text = "Raya";
                _FadeTransition.FadeOut();
                StartCoroutine(CallNextScene());
            }
            else
                StartCoroutine(EndDialogueLoadScene());
        }
    }

    IEnumerator ShowDialogueRaya()
    {
        _DialoguePanel.SetActive(true);

        _StoryText.text = "";

        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
            _Choice2Panel.SetActive(true);
    }

    IEnumerator ShowNewDialogueText(string _NewLine) 
    {
        _StoryText.text = "";

        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        _CanContinue = true;
    }

    IEnumerator ShowNewDialogueTextRayaTaho(string _NewLine)
    {
        _StoryText.text = "";

        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        _CanContinue = true;
    }


    IEnumerator EndDialogueLoadScene() 
    {
        yield return new WaitForSeconds(1f);
        _DialoguePanel.SetActive(false);
        _Choice3Panel.SetActive(false);
        _LegendManager._Reputation.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Taho!");
    }

    public void Choice4Dorm() 
    {
        _LegendManager._ReputationCount++;
        _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
        _LegendManager._Reputation.SetActive(true);
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();
        _Choice2Panel.SetActive(false);
        _DialogueIndex = 1;
        _CanContinue = false;
        _NpcName.text = "Raya";
        StartCoroutine(ShowNewDialogueText("Wow. New record."));
    }

    public void Choice5Dorm() 
    {
        _LegendManager._GuiltCount++;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _LegendManager._Guilt.SetActive(true);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        _Choice2Panel.SetActive(false);
        _DialogueIndex = 1;
        _CanContinue = false;
        _NpcName.text = "Raya";
        StartCoroutine(ShowNewDialogueText("You and that paper. One day it’s gonna thank you back."));
    }

    public void Choice6Dorm() 
    {
        _LegendManager._CourageCount++;
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._Courage.SetActive(true);
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.Save();
        _Choice2Panel.SetActive(false);
        _DialogueIndex = 1;
        _CanContinue = false;
        _NpcName.text = "Raya";
        StartCoroutine(ShowNewDialogueText("Didn’t sleep much."));
    }

    public void Choice7Dorm()
    {
        _LegendManager._CourageCount++;
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._Courage.SetActive(true);
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.Save();
        _DialogueIndex++;
        _CanContinue = false;
        _Choice3Panel.SetActive(false);
        _NpcName.text = "Raya";
        StartCoroutine(ShowNewDialogueText("Later. I promise."));
    }
    public void Choice8Dorm()
    {
        _LegendManager._FearCount++;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._Fear.SetActive(true);
        PlayerPrefs.SetInt("Courage Count", _LegendManager._FearCount);
        PlayerPrefs.Save();
        _DialogueIndex++;
        _CanContinue = false;
        _Choice3Panel.SetActive(false);
        _NpcName.text = "Raya";
        StartCoroutine(ShowNewDialogueText("Yeah. After class."));
    }
    public void Choice9Dorm()
    {
        _LegendManager._GuiltCount++;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _LegendManager._Guilt.SetActive(true);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        _DialogueIndex++;
        _CanContinue = false;
        _Choice3Panel.SetActive(false);
        _NpcName.text = "Raya";
        StartCoroutine(ShowNewDialogueText("Sorry. I didn’t mean to."));
    }
}
