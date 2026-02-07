using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public CanvasGroup _FadePanel;
    public float _FadeDuration = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FadeIn();
    }

    IEnumerator FadeCanvasGroup(CanvasGroup _Cutscene, float _Start, float _End, float _Duration) 
    {
        float _Elapsedtime = 0.0f;
        while (_Elapsedtime < _FadeDuration) 
        {
            _Elapsedtime += Time.deltaTime;
            _Cutscene.alpha = Mathf.Lerp(_Start, _End, _Elapsedtime / _Duration);
            yield return null;
        }
        _Cutscene.alpha = _End;
    }

    public void FadeIn() 
    {
        StartCoroutine(FadeCanvasGroup(_FadePanel, _FadePanel.alpha, 0, _FadeDuration));
    }

    public void FadeOut() 
    {
        StartCoroutine(FadeCanvasGroup(_FadePanel, _FadePanel.alpha, 1, _FadeDuration));
    }
}
