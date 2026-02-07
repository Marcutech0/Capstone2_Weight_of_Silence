using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayasDoorInteraction : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RayasDoor"))
        {
            SceneManager.LoadScene("Cutscene1.6");
        }
    }
}
