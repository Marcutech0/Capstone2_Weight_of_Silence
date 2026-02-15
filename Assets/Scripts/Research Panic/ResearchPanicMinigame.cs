using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ResearchPanicMinigame : MonoBehaviour
{
    [Header("Timer")]
    public Slider _TimerSlider;
    public TextMeshProUGUI _TimerText;
    public float _MinigameDuration;
    public bool _StopTimer;
    public float _CurrentTime;
    public GameObject _TutorialPanel;
    [Header("Points System")]
    public int _CorrectPoints;
    public int _WrongPoints;
    public TextMeshProUGUI _CorrectPointsText;
    public TextMeshProUGUI _WrongPointsText;
    public TextMeshProUGUI _GameStatus;
    public GameObject _GameStatusObject;
    public ResearchTitleSpawner _CurrentTitleActivePolitics, _CurrentTitleActiveCulture, _CurrentTitleActiveEducation;

    public GameFlowLegendManager _LegendManager;

    void Start()
    {
        _StopTimer = false;
        _TimerSlider.maxValue = _MinigameDuration;
        _TimerSlider.value = _MinigameDuration;
        _CurrentTime = _MinigameDuration;
        _CorrectPoints = 0;
        _WrongPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_TutorialPanel.activeSelf) return;
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

    public void CloseTutorial() 
    {
        _TutorialPanel.SetActive(false);
    }

    public void OpenTutorial()
    {
        _TutorialPanel.SetActive(true);
    }

    private void WinAndFailCon()
    {
        if (_WrongPoints >= 3)
        {
            _CurrentTitleActivePolitics._CurrentTitle.SetActive(false);
            _CurrentTitleActiveCulture._CurrentTitle.SetActive(false);
            _CurrentTitleActiveEducation._CurrentTitle.SetActive(false);
            _StopTimer = true;
            _LegendManager._CourageCount--;
            _LegendManager._FearCount += 2;
            _LegendManager._GuiltCount++;
            PlayerPrefs.SetInt("Courage  Count", _LegendManager._CourageCount);
            PlayerPrefs.SetInt("Fear  Count", _LegendManager._FearCount);
            PlayerPrefs.SetInt("Guilt  Count", _LegendManager._GuiltCount);
            PlayerPrefs.Save();
            _GameStatus.text = "You failed! You only got partial Info";
            _GameStatusObject.SetActive(true);
            UpdateUI();
            StartCoroutine(CallNextScene());
        }

        else if (_StopTimer && _CorrectPoints == 0)
        {
            _CurrentTitleActivePolitics._CurrentTitle.SetActive(false);
            _CurrentTitleActiveCulture._CurrentTitle.SetActive(false);
            _CurrentTitleActiveEducation._CurrentTitle.SetActive(false);
            _LegendManager._CourageCount--;
            _LegendManager._FearCount += 2;
            _LegendManager._GuiltCount++;
            PlayerPrefs.SetInt("Courage  Count", _LegendManager._CourageCount);
            PlayerPrefs.SetInt("Fear  Count", _LegendManager._FearCount);
            PlayerPrefs.SetInt("Guilt  Count", _LegendManager._GuiltCount);
            PlayerPrefs.Save();
            _GameStatus.text = "You failed! You only got partial Info";
            _GameStatusObject.SetActive(true);
            UpdateUI();
            StartCoroutine(CallNextScene());
        }

        else if (_StopTimer && _WrongPoints < 3)
        {
            _CurrentTitleActivePolitics._CurrentTitle.SetActive(false);
            _CurrentTitleActiveCulture._CurrentTitle.SetActive(false);
            _CurrentTitleActiveEducation._CurrentTitle.SetActive(false);
            _LegendManager._CourageCount += 5;
            _LegendManager._ReputationCount += 3;
            _LegendManager._GuiltCount -= 2;
            PlayerPrefs.SetInt("Courage  Count", _LegendManager._CourageCount);
            PlayerPrefs.SetInt("Reputation  Count", _LegendManager._ReputationCount);
            PlayerPrefs.SetInt("Guilt  Count", _LegendManager._GuiltCount);
            PlayerPrefs.Save();
            _GameStatus.text = "You won! Evidence Gained";
            _GameStatusObject.SetActive(true);
            UpdateUI();
            StartCoroutine(CallNextScene());
        }
    }

    public void UpdateUI() 
    {
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Fear.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
        _LegendManager._Reputation.SetActive(true);
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _LegendManager._Reputation.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        _GameStatus.text = "Moving to Campus";
        SceneManager.LoadScene("Exploration 1.3");
    }
}
