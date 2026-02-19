using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class CutScene2_2 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public GameObject _NotifPanel1;
    public GameObject _NotifPanel2;
    public GameObject _NotifPanel3;
    public TextMeshProUGUI _NotifText1;
    public TextMeshProUGUI _NotifText2;
    public TextMeshProUGUI _NotifText3;
    public GameObject _HomeUI;
    [TextArea] public string _Storyline;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public Fade _FadeTransition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _NpcName.text = "";
        StartCoroutine(ShowNarratorDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E)) 
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1) 
            {
                _DialoguePanel.SetActive(false);
                StartCoroutine(PhoneNotifRoutine());
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNarratorNewDialogue("You scroll."));
                _HomeUI.SetActive(false);
            }

            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNarratorNewDialogue("Raya’s name is still there."));
                _HomeUI.SetActive(false);
            }

            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowNarratorNewDialogue("No explanation. Just a deadline."));
                _HomeUI.SetActive(false);
            }

            else 
            {
                 EndDialogue();
                _FadeTransition.FadeOut();
                 StartCoroutine(CallNextScene());
            }
        }
    }

    IEnumerator ShowNarratorDialogue()
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

    IEnumerator ShowNarratorNewDialogue(string _NewLine)
    {
        _DialoguePanel.SetActive(true);
        _StoryText.text = "";

        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Exploration 2.2");
    }

    IEnumerator PhoneNotifRoutine()
    {
        _HomeUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        _NotifPanel1.SetActive(true);
        _NotifText1.text = "CAPSTONE PROGRESS REVIEW";

        yield return new WaitForSeconds(2f);
        _NotifPanel1.SetActive(false);
        _NotifPanel2.SetActive(true);
        _NotifText2.text = "Midterm submission required.";

        yield return new WaitForSeconds(2f);
        _NotifPanel1.SetActive(false);
        _NotifPanel2.SetActive(false);
        _NotifPanel3.SetActive(true);
        _NotifText3.text = "All groups must present updated scope and documentation.";
        _DialogueIndex = 2;
        _CanContinue = true;
    }
}
