using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayasDoorInteraction : MonoBehaviour
{
    public NoticeBoardInteractionRayaDorm _NoticeBoard;
    public TextMeshProUGUI _InteractIndicator;
    public GameObject _Indicator;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RayasDoor"))
        {
            if (_NoticeBoard._HasInteracted)
            {
                SceneManager.LoadScene("Cutscene1.6");
            }
            else
            {
                _InteractIndicator.text = "The notice board has not been interacted.";
                _Indicator.SetActive(true);
            }
        }      
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RayasDoor"))
        {
            _Indicator.SetActive(false);
        }
    }
}
