using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    [Tooltip("Target aspect ratio (width / height). 16:9 ≈ 1.7777")]
    public float targetAspect = 16f / 9f;

    private void Start()
    {
        ApplyAspect();
    }

    private void OnEnable()
    {
        ApplyAspect();
    }

    private void ApplyAspect()
    {
        Camera cam = GetComponent<Camera>();

        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1f)
        {
            // Letterbox (black bars top/bottom)
            cam.rect = new Rect(0f, (1f - scaleHeight) / 2f, 1f, scaleHeight);
        }
        else
        {
            // Pillarbox (black bars left/right)
            float scaleWidth = 1f / scaleHeight;
            cam.rect = new Rect((1f - scaleWidth) / 2f, 0f, scaleWidth, 1f);
        }
    }
}
