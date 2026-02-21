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

    [SerializeField] private int _DialogueIndex;
    [SerializeField] private bool _CanContinue;

    [SerializeField] private string[] _CurrentLines;
    [SerializeField] private string[] _CurrentSpeakers;
    [SerializeField] private int _CurrentLineIndex;
    [SerializeField] private bool _DialougeEnd;

    void Start()
    {
        _NpcName.text = "Liam";

        _CurrentLines = new string[] { _Storyline };
        _CurrentSpeakers = new string[] { "Liam" };
        _CurrentLineIndex = 0;

        StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
    }

    void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.Mouse0))
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
                if (_DialougeEnd) 
                {
                    _DialougeEnd = false;
                    EndDialogue();
                }

                else if (_DialogueIndex <= 4) 
                {
                    ContinueMainDialogue();
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

    void StartDialogueSet(string[] lines, string[] speakers)
    {
        _CurrentLines = lines;
        _CurrentSpeakers = speakers;
        _CurrentLineIndex = 0;

        StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
    }

    IEnumerator TypeLine(string line, string speaker)
    {
        _StoryText.text = "";
        _NpcName.text = speaker;

        foreach (char c in line)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }

        _CanContinue = true;
    }

    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _FadeTransition.FadeOut();
        StartCoroutine(CallNextScene());
    }

  

    public void Choice1DiningRoom()
    {
        _LegendManager._CourageCount += 3;
        _LegendManager._FearCount += 4;
        _LegendManager._GuiltCount--;

        UpdateStatsUI();

        _Choice1Panel.SetActive(false);
        _DialougeEnd = true;

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
        _DialougeEnd = true;

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
        _DialougeEnd = true;

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
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cutscene1.5");
    }
}
