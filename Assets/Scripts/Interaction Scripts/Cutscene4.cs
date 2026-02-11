using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cutscene4 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _Choice1Panel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;

    [TextArea] public string _Storyline;

    public GameFlowLegendManager _LegendManager;
    public Fade _FadeTransition;

    private int _DialogueIndex;
    private bool _CanContinue;

    private string[] _CurrentLines;
    private string[] _CurrentSpeakers;
    private int _CurrentLineIndex;

    void Start()
    {
        _NpcName.text = "Liam";

        // First dialogue only
        _CurrentLines = new string[] { _Storyline };
        _CurrentSpeakers = new string[] { "Liam" };
        _CurrentLineIndex = 0;

        StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
    }

    void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _CurrentLineIndex++;

            if (_CurrentLineIndex < _CurrentLines.Length)
            {
                StartCoroutine(TypeLine(
                    _CurrentLines[_CurrentLineIndex],
                    _CurrentSpeakers[_CurrentLineIndex]
                ));
            }
            else
            {
                if (_DialogueIndex < 4)
                {
                    ContinueMainDialogue();
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    void ContinueMainDialogue()
    {
        _DialogueIndex++;

        if (_DialogueIndex == 1)
        {
            StartDialogueSet(
                new string[] { "Advocates warn that so-called red-tagging puts students at risk, but authorities insist these measures are for public safety." },
                new string[] { "News TV" }
            );
        }
        else if (_DialogueIndex == 2)
        {
            StartDialogueSet(
                new string[] { "Students like that bring danger." },
                new string[] { "Tita Liza" }
            );
        }
        else if (_DialogueIndex == 3)
        {
            StartDialogueSet(
                new string[] { "They’re just asking for answers." },
                new string[] { "Liam" }
            );
        }
        else if (_DialogueIndex == 4)
        {
            StartDialogueSet(
                new string[] { "Questions get people hurt." },
                new string[] { "Tita Liza" }
            );

            _Choice1Panel.SetActive(true);
        }
    }

    void StartDialogueSet(string[] _Lines, string[] _Speakers)
    {
        _CurrentLines = _Lines;
        _CurrentSpeakers = _Speakers;
        _CurrentLineIndex = 0;

        StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
    }

    IEnumerator TypeLine(string _Line, string _Speaker)
    {
        _StoryText.text = "";
        _NpcName.text = _Speaker;

        foreach (char c in _Line)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }

        _CanContinue = true;
    }

    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _FadeTransition.FadeOut();
        StartCoroutine(CallNextScene());
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cutscene1.5");
    }

    

    public void Choice1DiningRoom()
    {
        _LegendManager._CourageCount += 3;
        _LegendManager._FearCount += 4;
        _LegendManager._GuiltCount--;

        UpdateStatsUI();

        _Choice1Panel.SetActive(false);

        StartDialogueSet(
            new string[]
            {
                "That’s how they make it sound. But asking questions isn’t a crime.",
                "You think the people on TV will protect you if something happens?",
                "I’m just trying to keep you safe."
            },
            new string[]
            {
                "Liam",
                "Tita Liza",
                "Tita Liza"
            }
        );
    }

    public void Choice2DiningRoom()
    {
        _LegendManager._CourageCount--;
        _LegendManager._FearCount--;
        _LegendManager._GuiltCount++;

        UpdateStatsUI();

        _Choice1Panel.SetActive(false);

        StartDialogueSet(
            new string[]
            {
                "I know… it’s dangerous.",
                "Good. You’re smart. You know when to stay out of things."
            },
            new string[]
            {
                "Liam",
                "Tita Liza"
            }
        );
    }

    public void Choice3DiningRoom()
    {
        _LegendManager._FearCount++;
        _LegendManager._GuiltCount++;

        UpdateStatsUI();

        _Choice1Panel.SetActive(false);

        StartDialogueSet(
            new string[]
            {
                "I have class.",
                "Tita Liza watches you go.",
                "Just… be careful, ha?"
            },
            new string[]
            {
                "Liam",
                "Narration",
                "Tita Liza"
            }
        );
    }

    void UpdateStatsUI()
    {
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Guilt.SetActive(true);

        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;

        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
    }
}