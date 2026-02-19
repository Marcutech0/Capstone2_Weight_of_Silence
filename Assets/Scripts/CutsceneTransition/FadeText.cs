using UnityEngine;
using System.Collections;
using TMPro;
public class FadeText : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup _FadePanel;
    [SerializeField] private TextMeshProUGUI _FadeTextPanel;

    [Header("Settings")]
    [SerializeField] private float _FadeDuration = 2.0f;
    [SerializeField] private float _WaitTime = 2.0f;

    private void Awake()
    {
        if (_FadePanel != null)
            _FadePanel.alpha = 0f;

        if (_FadeTextPanel != null)
        {
            Color textColor = _FadeTextPanel.color;
            textColor.a = 0f;
            _FadeTextPanel.color = textColor;
        }
    }

    public void StartFade()
    {
        StartCoroutine(FadeRoutine());
    }

    public IEnumerator FadeRoutine()
    {
        yield return FadeInOut();
    }
    
    IEnumerator FadeInOut()
    {
        yield return FadeCanvasGroup(_FadePanel, 0f, 1f, _FadeDuration);

        yield return new WaitForSeconds(_WaitTime);

        yield return FadeTextAlpha(_FadeTextPanel, 0f, 1f, _FadeDuration);

        yield return new WaitForSeconds(_WaitTime);

        yield return FadeTextAlpha(_FadeTextPanel, 1f, 0f, _FadeDuration);

        yield return FadeCanvasGroup(_FadePanel, 1f, 0f, _FadeDuration);
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        if (canvasGroup == null) yield break;

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }


    IEnumerator FadeTextAlpha(TextMeshProUGUI text, float startAlpha, float endAlpha, float duration)
    {
        if (text == null) yield break;

        float elapsedTime = 0.0f;
        Color originalColor = text.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            originalColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            text.color = originalColor;
            yield return null;
        }

        originalColor.a = endAlpha;
        text.color = originalColor;
    }
}
