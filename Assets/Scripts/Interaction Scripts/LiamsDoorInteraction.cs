using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LiamsDoorInteraction : MonoBehaviour
{
    public TextMeshProUGUI _PhoneNotif;
    public GameObject _InteractIndicator;
    public PhoneExploration1_1 _Phone;
    public GameObject _PhoneButtonsOpenUI;
    public Deskinteraction _Desk;
    public MirrorInteraction _Mirror;

    [SerializeField] private bool _IsNearDoor;
    [SerializeField] private bool _IsTriggered;

    private void Update()
    {
        if (_IsNearDoor && _Desk._HasInteracted && _Mirror._HasInteracted) 
        {
           _PhoneButtonsOpenUI.SetActive(true);

            if (!_IsTriggered) 
            {
                _PhoneNotif.text = "Reply to Raya";
            }

            if (_Phone._HasInteractedPhone)
            { 
                _PhoneNotif.text = "Press F to go to Campus Courtyard";

                if (Input.GetKeyDown(KeyCode.F) && !_IsTriggered)
                {
                    _IsTriggered = true;
                    _PhoneNotif.text = "Going to Campus Courtyard...";
                    StartCoroutine(CallNextScene());
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LiamsDoor"))
        {
            _InteractIndicator.SetActive(true);
            _IsNearDoor = true;
            _PhoneNotif.text = "Please interact with the desk and mirror first";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LiamsDoor"))
        {
            _InteractIndicator.SetActive(false);
            _IsNearDoor = false;
        }
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cutscene1.2");
    }
}
