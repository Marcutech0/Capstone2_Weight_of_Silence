using UnityEngine;
using UnityEngine.SceneManagement;

public class DuskDoorInteraction : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DuskDoor"))
        {
            SceneManager.LoadScene("Cutscene1.4");
        }
    }
}
