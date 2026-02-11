using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LiamsDoorInteraction : MonoBehaviour
{
    public TextMeshProUGUI _PhoneNotif;
    public GameObject _InteractIndicator;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LiamsDoor"))
        {
            _InteractIndicator.SetActive(true);
            _PhoneNotif.text = "Please Check your Phone First and reply to Raya";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LiamsDoor"))
        {
            _InteractIndicator.SetActive(false);
        }
    }
}
