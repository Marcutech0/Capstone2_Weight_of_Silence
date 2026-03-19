using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class Exploration2_1CourtyardDoorInteraction : MonoBehaviour
{
    public Exploration2_1BulletinBoardInteraction _BulletinBoardInteraction;
    public Exploration2_2TwoStudentsInteraction _TwoStudentsInteraction;
    public TextMeshProUGUI _InteractIndicator;
    public GameObject _Indicator;

    [SerializeField] private bool _IsNearDoor;
    [SerializeField] private bool _IsTriggered;



    private void Update()
    {
        if (_IsNearDoor && _BulletinBoardInteraction._HasInteracted && _TwoStudentsInteraction._HasInteracted) 
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
        if (other.CompareTag("CourtyardDoor"))
        {
            _Indicator.SetActive(true);
            _IsNearDoor = true;
            _InteractIndicator.text = "Please interact with the bulletin board and two students first";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CourtyardDoor"))
        {
            _Indicator.SetActive(false);
            _IsNearDoor = false;
        }
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Cutscene2.2");
    }
}
