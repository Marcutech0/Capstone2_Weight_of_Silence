using UnityEngine;
using UnityEngine.UI;

public class CupData : MonoBehaviour
{
    // Cups Fill Data Amount
    public float _FillPercent = 0f;

    [Header("Visuals")]
    public Image _CupImage;
    public Sprite _EmptySprite;
    public Sprite _PouringSprite;

    public void SetPouring(bool isPouring)
    {
        if (_CupImage == null) return;

        _CupImage.sprite = isPouring ? _PouringSprite : _EmptySprite;
    }
}
