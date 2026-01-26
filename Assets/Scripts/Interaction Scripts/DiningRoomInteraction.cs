using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DiningRoomInteraction : MonoBehaviour
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

    public GameFlowLegendManager _LegendManager;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame

    void Start()
    {
        _NpcName.text = "Tita Liza";
        StartCoroutine(ShowDialogueTitaLiza());
    }
    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNewDialogueLiam("They’re just asking questions."));
                _NpcName.text = "Liam";
            }
            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueTextTitaLiza("Questions get people hurt."));
                _Choice1Panel.SetActive(true);
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
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
    }
    IEnumerator ShowDialogueTitaLiza()
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

        _CanContinue = true;    
    }

    IEnumerator ShowNewDialogueLiam(string _NewLine)
    {
        _StoryText.text = "";
        _NpcName.text = "Liam";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        _CanContinue = true;
    }

    IEnumerator ShowNewDialogueTextTitaLiza(string _NewLine)
    {
        _StoryText.text = "";
        _NpcName.text = "Tita Liza";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        _CanContinue = true;
    }
    public void Choice1DiningRoom() 
    {
        _LegendManager._CourageCount += 3;
        _LegendManager._FearCount += 4;
        _LegendManager._GuiltCount--;
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _Choice1Panel.SetActive(false);
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
    }

    public void Choice2DiningRoom() 
    {
        _LegendManager._CourageCount--;
        _LegendManager._FearCount--;
        _LegendManager._GuiltCount++;
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _Choice1Panel.SetActive(false);
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
    }

    public void Choice3DiningRoom() 
    {
        _LegendManager._FearCount++;
        _LegendManager._GuiltCount++;
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _Choice1Panel.SetActive(false);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
    }

}
