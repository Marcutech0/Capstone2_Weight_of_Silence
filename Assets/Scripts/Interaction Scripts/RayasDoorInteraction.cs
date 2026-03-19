using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RayasDoorInteraction : MonoBehaviour
{
    public NoticeBoardInteractionRayaDorm _NoticeBoard;
    public TextMeshProUGUI _InteractIndicator;
    public GameObject _Indicator;

    [SerializeField] private bool _IsNearDoor;
    [SerializeField] private bool _IsTriggered;


    private void Update()
    {
        if (_IsNearDoor && _NoticeBoard._HasInteracted) 
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
        if (other.CompareTag("RayasDoor"))
        {
            _Indicator.SetActive(true);
            _IsNearDoor = true;
            _InteractIndicator.text = "Please interact with the notice board first";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RayasDoor"))
        {
            _Indicator.SetActive(false);
            _IsNearDoor = false;
        }
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cutscene1.6");
    }
}
