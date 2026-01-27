using UnityEngine;
using TMPro;
using System.Collections;
public class PackedBagInteractionRayaDorm : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _Choice1Panel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    [TextArea] public string _Storyline;

    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;

    public GameFlowLegendManager _LegendManager;
    void Start()
    {
        _NpcName.text = string.Empty;
        StartCoroutine(ShowDialogueRaya());
    }

    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
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
        _LegendManager._Reputation.SetActive(false);
        _LegendManager._Anonymity.SetActive(false);
        _Choice1Panel.SetActive(false);
    }
   
    IEnumerator ShowDialogueRaya()
    {
        _DialoguePanel.SetActive(true);
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

    public void Choice1RayaDorm() 
    {
        _LegendManager._CourageCount += 2;
        _LegendManager._AnonymityCount += 5;
        _LegendManager._GuiltCount -= 2;
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Anonymity.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._AnonymityText.text = "Anonymity: " + _LegendManager._AnonymityCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        _CanContinue = true;
        
    }

    public void Choice2RayaDorm()
    {
        _LegendManager._FearCount += 2;
        _LegendManager._GuiltCount += 2;
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        _CanContinue = true;


    }
    public void Choice3RayaDorm()
    {
        _LegendManager._CourageCount += 4;
        _LegendManager._FearCount += 3;
        _LegendManager._ReputationCount--;
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Reputation.SetActive(true);
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();
        _CanContinue = true;

    }
}