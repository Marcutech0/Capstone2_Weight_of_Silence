using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageArray : MonoBehaviour
{
    [Header("UI Target")]
    [SerializeField] private Image _targetImage;

    [Header("Images to Display")]
    [SerializeField] private Sprite[] _frameImages;

    [Header("Slideshow Settings")]
    [SerializeField] private float _frameInterval = 1f;

    private int _currentIndex = 0;
    private Coroutine _slideshowCoroutine;

    private void Start()
    {
        if (_frameImages.Length == 0 || _targetImage == null)
            return;

        _targetImage.sprite = _frameImages[_currentIndex];
        _slideshowCoroutine = StartCoroutine(SlideshowLoop());
    }

    private IEnumerator SlideshowLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_frameInterval);

            _currentIndex = (_currentIndex + 1) % _frameImages.Length;
            _targetImage.sprite = _frameImages[_currentIndex];
        }
    }

    // Optional manual controls (can be removed if not needed)
    public void ShowNext()
    {
        if (_frameImages.Length == 0) return;

        _currentIndex = (_currentIndex + 1) % _frameImages.Length;
        _targetImage.sprite = _frameImages[_currentIndex];
    }

    public void ShowPrevious()
    {
        if (_frameImages.Length == 0) return;

        _currentIndex = (_currentIndex - 1 + _frameImages.Length) % _frameImages.Length;
        _targetImage.sprite = _frameImages[_currentIndex];
    }

    private void OnDisable()
    {
        if (_slideshowCoroutine != null)
            StopCoroutine(_slideshowCoroutine);
    }
}
