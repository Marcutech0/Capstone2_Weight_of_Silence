using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutScene2_3 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public GameObject _PhonePanel;
    public GameObject _RayaMessageBox;
    public TextMeshProUGUI _LiamMessageText;
    [TextArea] public string _Storyline;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public Fade _FadeTransition;
    public PhoneExploration1_1 _Phone;

    void Start()
    {
        _NpcName.text = "";
        StartCoroutine(ShowNarratorDialogue());
        _DialogueIndex = 0;
    }

    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {               
                StartCoroutine(ShowNewDialogueNarrator("A message is typed in phone."));
            }

            else if (_DialogueIndex == 2)
            {               
                StartCoroutine(ShowNewDialogueNarrator("Deleted."));
            }

            else if (_DialogueIndex == 3)
            {               
                StartCoroutine(ShowNewDialogueNarrator("Typed again."));
            }

            else if (_DialogueIndex == 4)
            {               
                StartCoroutine(ShowNewDialogueNarrator("The cursor blinks."));
            }

            else if (_DialogueIndex == 5)
            {               
                StartCoroutine(ShowNewDialogueNarrator("Campus noise fades."));
            }

            else if (_DialogueIndex == 6)
            {               
                StartCoroutine(ShowNewDialogueNarrator("The screen goes dark."));
            }

            else
            {
                EndDialogue();
                _FadeTransition.FadeOut();
                StartCoroutine(CallNextScene());
            }

        }
    }

    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);

    }

    IEnumerator ShowNarratorDialogue()
    {
        _DialoguePanel.SetActive(true);
        _StoryText.text = "";

        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        _CanContinue = true;
    }

    IEnumerator ShowNewDialogueNarrator(string _NewLine)
    {
        _StoryText.text = "";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        _CanContinue = true;

    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cutscene3.1");
    }

    IEnumerator PhoneTypeRoutine() 
    {
        _LiamMessageText.text = "Typing";
        yield return new WaitForSeconds(1.5f);

        _LiamMessageText.text = "";
        yield return new WaitForSeconds(1.5f);

        _LiamMessageText.text = "Typing";
    }
}
