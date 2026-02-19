using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.AI;
public class PhoneExploration1_1 : MonoBehaviour
{
    public GameObject _PhonePanel;
    public GameObject _MessagesUI;
    public GameObject _HomeUI;
    public GameFlowLegendManager _LegendManager;
    public GameObject _ReplyChoice1;
    public TextMeshProUGUI _ReplyText;
    public TextMeshProUGUI _RayaReplyText;
    public GameObject _RayaMessageBox;
    public GameObject _LiamMessageBox;
    public GameObject _SeenText;
    public GameObject _Messages;
    public LiamsDoorInteraction _DormDoor;
    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public LiamsMessages _LiamsMessages;
    public RayasMessages _RayasMessages;
    public int _ChoiceResult;
    public bool _HasInteractedPhone;
    public GameObject _ReplyButton;
    public void OpenPhone()
    {
        _HomeUI.SetActive(true);
        if (_MessagesUI.activeSelf) 
        {
            _HomeUI.SetActive(false);
        }
    }

    public void OpenMessages()
    {
        _MessagesUI.SetActive(true);
        _HomeUI.SetActive(false);
    }

    public void Reply()
    {
        _ReplyChoice1.SetActive(true);
    }

    public void Choice1_1() 
    {
        _PlayerControls.enabled = false;
        _PlayerController.enabled = false;
        _ReplyChoice1.SetActive(false);
        _LiamMessageBox.SetActive(true);
        _ReplyText.text = "I’m alive, promise.";
        _LegendManager._ReputationCount++;
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();
        StartCoroutine(RayasReplyDelayChoice1());
        _ChoiceResult = 1;
        PlayerPrefs.SetInt("ChoiceResult", _ChoiceResult);
        PlayerPrefs.Save();
        _HasInteractedPhone = true;
        _ReplyButton.SetActive(false);

    }

    public void Choice1_2() 
    {
        _PlayerControls.enabled = false;
        _PlayerController.enabled = false;
        _ReplyChoice1.SetActive(false);
        _LiamMessageBox.SetActive(true);
        _ReplyText.text = "Sure. We’ll talk later.";
        _LegendManager._GuiltCount++;
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        StartCoroutine(RayasReplyDelayChoice2());
        _ChoiceResult = 2;
        PlayerPrefs.SetInt("ChoiceResult", _ChoiceResult);
        PlayerPrefs.Save();
        _HasInteractedPhone = true;
        _ReplyButton.SetActive(false);
    }


    public void Choice1_3() 
    {
        _PlayerControls.enabled = false;
        _PlayerController.enabled = false;
        _ReplyChoice1.SetActive(false);
        _LegendManager._AnonymityCount++;
        _LegendManager._GuiltCount++;
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        StartCoroutine(SeenStatusDelay());
        _ChoiceResult = 3;
        PlayerPrefs.SetInt("ChoiceResult", _ChoiceResult);
        PlayerPrefs.Save();
        _HasInteractedPhone = true;
        _ReplyButton.SetActive(false);


    }

    public void ClosePhone() 
    {
        if (_HomeUI.activeSelf || _MessagesUI.activeSelf) 
        {
            _HomeUI.SetActive(false);
            _MessagesUI.SetActive(false);
        }
    }

    IEnumerator RayasReplyDelayChoice1()
    {
        yield return new WaitForSeconds(1f);
        _RayaMessageBox.SetActive(true);
        _RayaReplyText.text = "Good. Don’t disappear on me today, okay?";
        yield return new WaitForSeconds(1f);
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;

    }

    IEnumerator RayasReplyDelayChoice2()
    {
        yield return new WaitForSeconds(1f);
        _RayaMessageBox.SetActive(true);
        _RayaReplyText.text = "Later, then.";
        yield return new WaitForSeconds(1f);
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
    }

    IEnumerator SeenStatusDelay()
    {
        yield return new WaitForSeconds(1f);
        _SeenText.SetActive(true);
        yield return new WaitForSeconds(1f);
        _SeenText.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;
        _Messages.SetActive(false);
    }

    
}
