using UnityEngine;
using System.Collections;
using TMPro;
public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI _FadeTextPanel;
    public float _FadeDuration = 2.0f;

    
    IEnumerator FadeCanvasGroup(TextMeshProUGUI _Text, float _Start, float _End, float _Duration)
    {
        float _Elapsedtime = 0.0f;
        while (_Elapsedtime < _FadeDuration)
        {
            _Elapsedtime += Time.deltaTime;
            _Text.alpha = Mathf.Lerp(_Start, _End, _Elapsedtime / _Duration);
            yield return null;
        }
        _Text.alpha = _End;
    }

    IEnumerator FadeInOut()
    {
        yield return StartCoroutine(FadeCanvasGroup(_FadeTextPanel, _FadeTextPanel.alpha, 1f, _FadeDuration));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(FadeCanvasGroup(_FadeTextPanel, _FadeTextPanel.alpha, 0f, _FadeDuration));
    }

    public IEnumerator FadeRoutine() 
    {
         yield return StartCoroutine(FadeInOut());
    }



}
