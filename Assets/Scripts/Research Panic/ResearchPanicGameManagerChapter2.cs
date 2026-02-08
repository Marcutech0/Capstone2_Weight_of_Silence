using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ResearchPanicGameManagerChapter2 : MonoBehaviour
{
    [Header("Timer")]
    public Slider _TimerSlider;
    public TextMeshProUGUI _TimerText;
    public float _MinigameDuration;
    public float _CurrentTime;
    public bool _StopTimer;
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
        _CurrentTime = _MinigameDuration;
        _TimerSlider.maxValue = _MinigameDuration;
        _TimerSlider.value = _MinigameDuration;
        _CorrectPoints = 0;
        _WrongPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_StopTimer) return;

        _CurrentTime -= Time.deltaTime;
        _CurrentTime = Mathf.Max(_CurrentTime, 0f);

        int _Minutes = Mathf.FloorToInt(_CurrentTime / 60);
        int _Seconds = Mathf.FloorToInt(_CurrentTime % 60);

        _TimerText.text = string.Format("{0:0}:{1:00}", _Minutes, _Seconds);
        _TimerSlider.value = _CurrentTime;

        if (_CurrentTime <= 0)
        {
            _StopTimer = true;
            WinAndFailCon();
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
            SceneManager.LoadScene("Exploration 2.3");
        }

        else if (_StopTimer && _CorrectPoints == 0)
        {
            _CurrentTitleActivePolitics._CurrentTitle.SetActive(false);
            _CurrentTitleActiveCulture._CurrentTitle.SetActive(false);
            _CurrentTitleActiveEducation._CurrentTitle.SetActive(false);
            _GameStatus.text = "You failed! no titles were categorized";
            _GameStatusObject.SetActive(true);
            SceneManager.LoadScene("Exploration 2.3");
        }

        else if (_StopTimer && _WrongPoints < 3)
        {
            _CurrentTitleActivePolitics._CurrentTitle.SetActive(false);
            _CurrentTitleActiveCulture._CurrentTitle.SetActive(false);
            _CurrentTitleActiveEducation._CurrentTitle.SetActive(false);
            _GameStatus.text = "You won! titles were categorized!";
            _GameStatusObject.SetActive(true);
            SceneManager.LoadScene("Exploration 2.3");
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
