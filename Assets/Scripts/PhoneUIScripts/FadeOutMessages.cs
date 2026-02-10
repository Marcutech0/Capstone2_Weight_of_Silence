using System.Collections;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
public class FadeOutMessages : MonoBehaviour
{
    public GameObject _RayasMessageBox1;
    public GameObject _RayasMessageBox2;
    public GameObject _RayasMessageBox3;
    public TextMeshProUGUI _RayasMessage1;
    public TextMeshProUGUI _RayasMessage2;
    public TextMeshProUGUI _RayasMessage3;
    public GameObject _Phone;
    public CutScene1 _DialogueIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _RayasMessageBox1.SetActive(false);
        _RayasMessageBox2.SetActive(false);
        _RayasMessageBox3.SetActive(false);
    }

    private void Update()
    {
        
    }


    IEnumerator RayasMessageRoutineCutscene1() 
    {
        yield return new WaitForSeconds(1f);

        _RayasMessageBox1.SetActive(true);
        _RayasMessage1.text = "Don’t forget the paper, sleepyhead.";

        yield return new WaitForSeconds(1f);

        _RayasMessageBox2.SetActive(true);
        _RayasMessage2.text = "Also… can we talk later";

        yield return new WaitForSeconds(1f);

        _RayasMessageBox3.SetActive(true);
        _RayasMessage3.text = "hey, are you there?";

        yield return new WaitForSeconds(1.5f);
        _DialogueIndex._DialogueIndex = 2;
        _DialogueIndex._CanContinue = true;
    }

    public void RayaMessageRoutine()
    {
        StartCoroutine(RayasMessageRoutineCutscene1());
    }
}
