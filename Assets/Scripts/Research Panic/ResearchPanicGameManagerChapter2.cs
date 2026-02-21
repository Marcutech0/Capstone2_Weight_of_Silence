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
    public bool _StopTimer;
    public float _CurrentTime;
    [Header("Points System")]
    public int _CorrectPoints;
    public int _WrongPoints;
    public TextMeshProUGUI _CorrectPointsText;
    public TextMeshProUGUI _WrongPointsText;
    public TextMeshProUGUI _GameStatus;
    public GameObject _GameStatusObject;
    public ResearchTitleSpawner _CurrentTitleActivePolitics, _CurrentTitleActiveCulture, _CurrentTitleActiveEducation;

    public GameObject _TutorialPanel;
    public GameFlowLegendManager _LegendManager;
    public GameObject _LoseScreen;
    public GameObject _WinScreen;
    public GameObject _Continue;
    public GameObject _ImagePopper;
    public GameObject _Distractions;
    public ResearchTitleSpawner _TitleHolder;
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

        if ((_CorrectPoints + _WrongPoints) >= 9) 
        {
            _StopTimer = true;
            WinAndFailCon();
        }
        
        else if (_CorrectPoints >= 9) 
        {
            _StopTimer = true;
            WinAndFailCon();
        }
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
        if (_WrongPoints >= 3) // Lose
        {
            _StopTimer = true;
            _LegendManager._CourageCount--;
            _LegendManager._FearCount += 2;
            _LegendManager._GuiltCount++;
            PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
            PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
            PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
            PlayerPrefs.Save();
            _GameStatus.text = "You failed! You only got partial Info";
            _GameStatusObject.SetActive(true);
            _LoseScreen.SetActive(true);
            _Continue.SetActive(true);
            _ImagePopper.SetActive(false);
            _Distractions.SetActive(false);
            _TitleHolder._CurrentTitle.SetActive(false);
        }

        else if (_StopTimer && _CorrectPoints == 0) // Lose
        {
            _LegendManager._CourageCount--;
            _LegendManager._FearCount += 2;
            _LegendManager._GuiltCount++;
            PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
            PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
            PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
            PlayerPrefs.Save();
            _GameStatus.text = "You failed! You only got partial Info";
            _GameStatusObject.SetActive(true);
            _LoseScreen.SetActive(true);
            _Continue.SetActive(true);
            _ImagePopper.SetActive(false);
            _Distractions.SetActive(false);
            _TitleHolder._CurrentTitle.SetActive(false);
        }

        else if (_StopTimer && _WrongPoints < 3)// Win
        {
            _LegendManager._CourageCount += 5;
            _LegendManager._ReputationCount += 3;
            _LegendManager._GuiltCount -= 2;
            PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
            PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
            PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
            PlayerPrefs.Save();
            _GameStatus.text = "You won! Evidence Gained";
            _GameStatusObject.SetActive(true);
            _WinScreen.SetActive(true);
            _Continue.SetActive(true);
            _ImagePopper.SetActive(false);
            _Distractions.SetActive(false);
            _TitleHolder._CurrentTitle.SetActive(false);
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

    public void EndMinigame() 
    {
        StartCoroutine(CallNextScene());
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.Save();
    }

    IEnumerator CallNextScene()
    {
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _LegendManager._Reputation.SetActive(false);
        _Continue.SetActive(false);
        

        yield return new WaitForSeconds(1f);
        _GameStatus.text = "Moving to Campus";
        SceneManager.LoadScene("Exploration 2.3");
    }
}
