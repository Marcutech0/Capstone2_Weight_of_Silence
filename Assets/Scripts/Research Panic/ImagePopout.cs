using NUnit.Framework;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ImagePopout : MonoBehaviour
{
    public RectTransform _CanvasRect;
    public List <GameObject> _PopoutImages;
    public float _PopInterval = 1f;
    public float _ActiveDuration = 0.5f;
    public ResearchTitleSpawner _Title;

    void Start()
    {
        StartCoroutine(PopOutRoutine());
    }

    IEnumerator PopOutRoutine() 
    {
        while (true)
        {
            if (_Title != null && _Title._CurrentTitle != null && _PopoutImages.Count > 0) 
            {
                int _Index = Random.Range(0, _PopoutImages.Count);
                GameObject _Image = _PopoutImages[_Index];
                RectTransform _Rect = _Image.GetComponent<RectTransform>();

                RectTransform _TitlePos = _Title._CurrentTitle.GetComponent<RectTransform>();
                _Rect.SetParent(_TitlePos, false);
                _Rect.anchoredPosition = Vector2.zero;
                _Rect.sizeDelta = _TitlePos.sizeDelta;
                _Rect.localScale = Vector3.one * 1.2f;
                _Rect.SetAsLastSibling();
                _Rect.localRotation = Quaternion.Euler(0, 0, Random.Range(-15f, 15f));
                _Image.SetActive(true);
                yield return new WaitForSeconds(_ActiveDuration);
                _Image.SetActive(false);
            }
            yield return new WaitForSeconds(_PopInterval);
        }
    }
}
