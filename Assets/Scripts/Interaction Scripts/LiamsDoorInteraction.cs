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
    public GameObject _PhoneButtonsCloseUI;
    public Deskinteraction _Desk;
    public MirrorInteraction _Mirror;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LiamsDoor"))
        {
            _InteractIndicator.SetActive(true);
            _PhoneNotif.text = "Please Interact with the Desk and Mirror to access your phone";

            if (_Desk._HasInteracted && _Mirror._HasInteracted)
            {
                _PhoneButtonsOpenUI.SetActive(true);
                _PhoneButtonsCloseUI.SetActive(true);
                _PhoneNotif.text = "You may now access your phone";

                if (_Phone._HasInteractedPhone && _Desk._HasInteracted && _Mirror._HasInteracted)
                {
                    _PhoneNotif.text = "Going to Campus Courtyard";
                    StartCoroutine(CallNextScene());
                }
            }
 
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LiamsDoor"))
        {
            _InteractIndicator.SetActive(false);
        }
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cutscene1.2");
    }
}
