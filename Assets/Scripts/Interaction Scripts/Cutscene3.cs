using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Cutscene3 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _Choice1Panel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;

    [TextArea] public string _Storyline;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;

    public Fade _FadeTransition;
    public GameFlowLegendManager _LegendManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        _NpcName.text = "Raya";
        StartCoroutine(ShowDialogueRaya());
    }
    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 2)
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
        _LegendManager._Anonymity.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
    } 

    IEnumerator ShowDialogueRaya()
    {
        _DialoguePanel.SetActive(true);
        
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
    IEnumerator CallNextScene()
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
        _Choice1Panel.SetActive(false);
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
        _Choice1Panel.SetActive(false);
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
        _Choice1Panel.SetActive(false);
        StartCoroutine(ShowNewDialogueTextRaya("Someone has to."));
    }
}
