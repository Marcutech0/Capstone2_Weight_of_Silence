using UnityEngine;
using UnityEngine.SceneManagement;

public class LiamsDoorInteraction : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LiamsDoor"))
        {
            SceneManager.LoadScene("Cutscene1.2");
        }
    }
}
