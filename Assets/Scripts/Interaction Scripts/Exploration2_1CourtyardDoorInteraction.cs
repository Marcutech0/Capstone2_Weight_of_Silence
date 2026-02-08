using UnityEngine;
using UnityEngine.SceneManagement;
public class Exploration2_1CourtyardDoorInteraction : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CourtyardDoor"))
        {
            SceneManager.LoadScene("Cutscene2.2");
        }
    }
}
