using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour
{

    public Vector2 hoverOffset = new Vector2(30f, 0f);
    public float slideSpeed = 10f;

    private RectTransform _rect;
    private Vector2 _originalPosition;
    private Vector2 _targetPosition;

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _originalPosition = _rect.anchoredPosition;
        _targetPosition = _originalPosition;
    }

    void Update()
    {
        _rect.anchoredPosition = Vector2.Lerp(
            _rect.anchoredPosition,
            _targetPosition,
            Time.deltaTime * slideSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("HOVER ENTER");
        _targetPosition = _originalPosition + hoverOffset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("HOVER EXIT");
        _targetPosition = _originalPosition;
    }
}
