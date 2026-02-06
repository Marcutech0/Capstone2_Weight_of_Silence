using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [Header("Timer")]
    public Slider _TimerSlider;
    public TextMeshProUGUI _TimerText;
    public float _MinigameDuration;
    public bool _StopTimer;
    public float _TimeRemaining;

    [Header("Points System")]
    public int _CorrectPoints;
    public int _WrongPoints;
    public TextMeshProUGUI _CorrectPointsText;
    public TextMeshProUGUI _WrongPointsText;
    public TextMeshProUGUI _GameStatus;
    public GameObject _GameStatusObject;
    public ResearchTitleSpawner _CurrentTitleActivePolitics, _CurrentTitleActiveCulture, _CurrentTitleActiveEducation;

    public GameObject _TutorialPanel;
    void Start()
    {
        _StopTimer = false;
        _TimerSlider.maxValue = _MinigameDuration;
        _TimeRemaining = _MinigameDuration;
        _TimerSlider.value = _TimeRemaining;
        _CorrectPoints = 0;
        _WrongPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_StopTimer) 
        {
            _TimeRemaining -= Time.deltaTime;
        }
        int _Minutes = Mathf.FloorToInt(_TimeRemaining / 60);
        int _Seconds = Mathf.FloorToInt(_TimeRemaining - _Minutes * 60f);
        string _TextTime = string.Format("{0:0}:{1:00}", _Minutes, _Seconds);
        _TimerSlider.value = _TimeRemaining;

        if (_TimeRemaining <= 0)
        {
            _StopTimer = true;
        }

        if (_StopTimer == false)
        {
            _TimerText.text = _TextTime;
            _TimerSlider.value = _TimeRemaining;
        }

        if (_StopTimer == true && IsPaused()) 
        {
            _TimerText.text = _TextTime;
            _TimerSlider.value = _TimeRemaining;
        }

    }
    
    public void AddCorrectPoints() 
    {
        _CorrectPoints++;
        _CorrectPointsText.text = " Correct Categorized Title: " + _CorrectPoints;
        WinAndFailCon();
    }

    public void AddWrongPoints() 
    {
        _WrongPoints++;
        _WrongPointsText.text = " Incorrect Categorized Title: " + _WrongPoints;
        WinAndFailCon();
    }

    private void WinAndFailCon()
    {
        if (_WrongPoints >= 3)
        {
            _CurrentTitleActivePolitics._CurrentTitle.SetActive(false);
            _CurrentTitleActiveCulture._CurrentTitle.SetActive(false);
            _CurrentTitleActiveEducation._CurrentTitle.SetActive(false);
            _StopTimer = true;
            _GameStatus.text = "You failed! too many miscategorized titles";
            _GameStatusObject.SetActive(true);
            SceneManager.LoadScene("SampleScene");
        }

        else if (_StopTimer && _CorrectPoints == 0)
        {
            _CurrentTitleActivePolitics._CurrentTitle.SetActive(false);
            _CurrentTitleActiveCulture._CurrentTitle.SetActive(false);
            _CurrentTitleActiveEducation._CurrentTitle.SetActive(false);
            _GameStatus.text = "You failed! no titles were categorized";
            _GameStatusObject.SetActive(true);
            SceneManager.LoadScene("SampleScene");
        }

        else if (_StopTimer && _WrongPoints < 3)
        {
            _CurrentTitleActivePolitics._CurrentTitle.SetActive(false);
            _CurrentTitleActiveCulture._CurrentTitle.SetActive(false);
            _CurrentTitleActiveEducation._CurrentTitle.SetActive(false);
            _GameStatus.text = "You won! titles were categorized!";
            _GameStatusObject.SetActive(true);
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void OpenTutorialPanel()
    {
        _TutorialPanel.SetActive(true);
        IsPaused();
        _StopTimer = true;
    }

    public void CloseTutorialPanel()
    {
        _TutorialPanel.SetActive(false);
        _StopTimer = false;
    }
    bool IsPaused() 
    {
        return _TutorialPanel != null && _TutorialPanel.activeSelf;
    }
}
