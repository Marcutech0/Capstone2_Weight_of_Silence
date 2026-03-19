using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class DuskDoorInteraction : MonoBehaviour
{
    public SecurityCheckpointInteraction _SecurityCheckpointInteraction;
    public MemorialStickersInteraction _MemorialStickersInteraction;
    public TextMeshProUGUI _InteractIndicator;
    public GameObject _Indicator;

    [SerializeField] private bool _IsNearDoor;
    [SerializeField] private bool _IsTriggered;



    private void Update()
    {
        if (_IsNearDoor && _SecurityCheckpointInteraction._HasInteracted && _MemorialStickersInteraction._HasInteracted) 
        {
            if (!_IsTriggered) 
            {
                _InteractIndicator.text = "Press F to Continue";

                if (Input.GetKeyDown(KeyCode.F) && !_IsTriggered)
                {
                    _IsTriggered = true;
                    _InteractIndicator.text = string.Empty;
                    StartCoroutine(CallNextScene());
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DuskDoor"))
        {
            _Indicator.SetActive(true);
            _IsNearDoor = true;
            _InteractIndicator.text = "Please interact with the Security and Memorial Stickers first";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DuskDoor"))
        {
            _Indicator.SetActive(false);
            _IsNearDoor = false;
        }
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cutscene1.4");
    }
}
