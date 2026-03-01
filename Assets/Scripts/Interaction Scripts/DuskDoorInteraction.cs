using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DuskDoorInteraction : MonoBehaviour
{
    public SecurityCheckpointInteraction _SecurityCheckpointInteraction;
    public MemorialStickersInteraction _MemorialStickersInteraction;
    public TextMeshProUGUI _InteractIndicator;
    public GameObject _Indicator;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DuskDoor"))
        {
            if (_SecurityCheckpointInteraction._HasInteracted && _MemorialStickersInteraction._HasInteracted)
            {
                SceneManager.LoadScene("Cutscene1.4");
            }
            else
            {
                _InteractIndicator.text = "The security checkpoint and memorial stickers have not been interacted.";
                _Indicator.SetActive(true);
            }
        }        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DuskDoor"))
        {
            _Indicator.SetActive(false);
        }
    }
}
