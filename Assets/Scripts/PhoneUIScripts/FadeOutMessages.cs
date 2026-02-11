using System.Collections;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
public class FadeOutMessages : MonoBehaviour
{
    public CanvasGroup _RayaMessageInputFieldUI1;
    public CanvasGroup _RayaMessageInputFieldUI2;
    public CanvasGroup _RayaMessageInputFieldUI3;
    public TMP_InputField _RayaMessageInput1;
    public TMP_InputField _RayaMessageInput2;
    public TMP_InputField _RayaMessageInput3;
    public GameObject _Phone;
    public CutScene1 _DialogueIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _RayaMessageInputFieldUI1.alpha = 0f;
        _RayaMessageInputFieldUI2.alpha = 0f;
        _RayaMessageInputFieldUI3.alpha = 0f;
    }

    private void Update()
    {
        
    }


    IEnumerator RayasMessageRoutineCutscene1() 
    {
        yield return new WaitForSeconds(1f);

        _RayaMessageInputFieldUI1.alpha = 1f;
        _RayaMessageInput1.text = "Don’t forget the paper, sleepyhead.";

        yield return new WaitForSeconds(1f);

        _RayaMessageInputFieldUI2.alpha = 1f;
        _RayaMessageInput2.text = "Also… can we talk later";

        yield return new WaitForSeconds(1f);

        _RayaMessageInputFieldUI3 .alpha = 1f;
        _RayaMessageInput3.text = "hey, are you there?";

        yield return new WaitForSeconds(1.5f);
        _DialogueIndex._DialogueIndex = 4;
        _DialogueIndex._CanContinue = true;
    }

    public void RayaMessageRoutine()
    {
        StartCoroutine(RayasMessageRoutineCutscene1());
    }
}
