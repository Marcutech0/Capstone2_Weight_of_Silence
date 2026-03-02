using TMPro;
using UnityEngine;

public class Exp1_2InteractionChecker : MonoBehaviour
{
    public NewsScreenInteract _NewsScreenInteraction;
    public FlyerWallInteraction _FlyerWallInteraction;
    public RayaInteractionLectureHall _RayaInteractionLectureHall;
    public GameObject _InteractIndicator;
    public TextMeshProUGUI _InteractText;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            if (!_NewsScreenInteraction._HasInteracted)
            {
                _InteractIndicator.SetActive(true);
                _InteractText.text = "Please Interact With the News Screen.";
            }
            else if (!_FlyerWallInteraction._HasInteracted)
            {
                _InteractIndicator.SetActive(true);
                _InteractText.text = "Please Interact With the Flyer Wall.";
            }
            else if (!_RayaInteractionLectureHall._HasInteracted)
            {
                _InteractIndicator.SetActive(true);
                _InteractText.text = "Please Interact With Raya.";
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        _InteractIndicator.SetActive(false);
    }
}
