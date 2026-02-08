using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class CutScene3_1 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    [TextArea] public string _Storyline;

    [SerializeField] private int _DialogueIndex;
    [SerializeField] bool _CanContinue;
    public Fade _FadeTransition;
    void Start()
    {
        _NpcName.text = "";
        StartCoroutine(ShowNarratorDialogue());
    }

    public void Update()
    {
        if (_CanContinue && Input.GetKeyDown(KeyCode.E))
        {
            _CanContinue = false;
            _DialogueIndex++;

            if (_DialogueIndex == 1)
            {
                StartCoroutine(ShowNewDialogueNarrator("Days pass."));
            }

            else if (_DialogueIndex == 2)
            {
                StartCoroutine(ShowNewDialogueNarrator("Classes continue."));
            }

            else if (_DialogueIndex == 3)
            {
                StartCoroutine(ShowNewDialogueNarrator("Posters fade."));
            }

            else if (_DialogueIndex == 4)
            {
                StartCoroutine(ShowNewDialogueNarrator("The space where Raya used to walk is still empty."));
            }

            else if (_DialogueIndex == 5)
            {
                StartCoroutine(ShowNewDialogueNarrator("No announcement is made."));
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
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;
    }

    IEnumerator ShowNewDialogueNarrator(string _NewLine)
    {
        _StoryText.text = "";
        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _CanContinue = true;

    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Exploration 3.1");
    }
}
