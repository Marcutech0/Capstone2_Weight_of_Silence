using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cutscene5 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName; // since this is a object npc name is empty
    public TextMeshProUGUI _StoryText;
    [TextArea] public string _Storyline;

    public GameFlowLegendManager _LegendManager;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public Fade _FadeTransition;
    void Start()
    {
        _NpcName.text = "Liam";
        StartCoroutine(ShowDialoguePhone());
    }

    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
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
        _LegendManager._Guilt.SetActive(false);
    }

    IEnumerator ShowDialoguePhone()
    {
        _DialoguePanel.SetActive(true);

        _StoryText.text = "";
        _LegendManager._GuiltCount += 2;
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();

        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Exploration 1.4");
    }
}
