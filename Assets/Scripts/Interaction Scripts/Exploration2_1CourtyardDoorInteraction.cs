using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Exploration2_1CourtyardDoorInteraction : MonoBehaviour
{
    public Exploration2_1BulletinBoardInteraction _BulletinBoardInteraction;
    public Exploration2_2TwoStudentsInteraction _TwoStudentsInteraction;
    public TextMeshProUGUI _InteractIndicator;
    public GameObject _Indicator;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CourtyardDoor"))
        {
            if (_BulletinBoardInteraction._HasInteracted && _TwoStudentsInteraction._HasInteracted) 
            {
                SceneManager.LoadScene("Cutscene2.2");
            }

            else
            {
                _InteractIndicator.text = "The bulletin board and two students have not been interacted.";
                _Indicator.SetActive(true);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CourtyardDoor"))
        {
            _Indicator.SetActive(false);
        }
    }
}
