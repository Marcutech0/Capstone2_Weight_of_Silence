using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;
public class MinigameManager : MonoBehaviour
{
    public static MinigameManager _Instance;
    [Header("UI")]
    public TextMeshProUGUI _TimerText;
    public TextMeshProUGUI _OverfillText;
    public TextMeshProUGUI _ReleasedText;
    public TextMeshProUGUI _GameStatusText;
    public CanvasGroup _TahoPanel;
    public GameObject _TutorialPanel;
    public Slider _TahoSlider;

    [Header("MinigameSettings")]
    public float _GameDuration;
    private float _RemainingTime;
    private bool _GameActive = false;
    public ReleaseManager _OverfillManager;
    public ReleaseManager _ReleaseManager;

    [Header("SpillSettings")]
    public float _MinSpillDelay;
    public float _MaxSpillDelay;
    public float _MinSpillAmount;
    public float _MaxSpillAmount;

    public GameFlowLegendManager _LegendManager;

    public void Awake()
    {
         // set the instance to this Manager
        _Instance = this;
    }

    public void Start()
    {
        // starts minigame
        StartCoroutine(StartMinigame());
    }

    public void UpdateFillSlider(float value)
    {
        if (_TahoSlider != null)
            _TahoSlider.value = value;
    }

    public void Update()
    {
        if (CupClickManager._CurrentlySelectedCup != null)
        {
            _TahoSlider.value = CupClickManager._CurrentlySelectedCup._FillPercent;
        }
    }

    IEnumerator StartMinigame()
    {
         // while minigame starts remaining time ticks down based on the given duration, if over fill count is 3 or remaining time is below 0, game over
        _RemainingTime = _GameDuration;
        _GameActive = true;

        StartCoroutine(SpillRoutine());

        while (_GameActive) 
        {
            if (IsPaused()) 
            {
                yield return null;
                continue;
            }

           _RemainingTime -= Time.deltaTime;
           _TimerText.text = $"{Mathf.Max(_RemainingTime, 0f):0}s";

            if (_OverfillManager._OverfillCount >= 3 || _RemainingTime < 0f) 
            {
                _GameActive = false;
                EndMiniGame();
                yield break;
            }

            yield return null;  
        }

    }

    IEnumerator SpillRoutine()
    {
        // during minigame spill the currently selected cup based on random spill amount with respect to time to spill 
        while (_GameActive)
        {
            if (IsPaused())
            {
                yield return null;
                continue;
            }

            float _WaitTime = Random.Range(_MinSpillDelay, _MaxSpillDelay);
            yield return new WaitForSeconds(_WaitTime);

            var _CurrentCup = CupClickManager._CurrentlySelectedCup;
            if (_CurrentCup != null)
            {
                float _SpillAmount = Random.Range(_MinSpillAmount, _MaxSpillAmount);
                _CurrentCup._FillPercent -= _SpillAmount;
                _CurrentCup._FillPercent = Mathf.Clamp(_CurrentCup._FillPercent, 0, 100);
            }
        }
    }

    public void AddOverFill() 
    {
        if (!_GameActive) return;
        _OverfillManager._OverfillCount++;
        _OverfillText.text = $"Overfilled Taho: {_OverfillManager._OverfillCount}";
    }

    public void AddRelease() 
    {
        if (!_GameActive) return;
        _ReleaseManager._ReleasedCount++;
        _ReleasedText.text = $"Released Taho: {_ReleaseManager._ReleasedCount}";
    }

    public void EndMiniGame() 
    {
        // lose win conditions
        _GameActive = false;
        if (_OverfillManager._OverfillCount >= 3) // Lose
        {
            _GameStatusText.text = "Game over! spilled to many cups!";
        }

        else // Win
        {
            _LegendManager._CourageCount++;
            _LegendManager._GuiltCount--;
            PlayerPrefs.SetInt("Courage  Count", _LegendManager._CourageCount);
            PlayerPrefs.SetInt("Guilt  Count", _LegendManager._GuiltCount);
            PlayerPrefs.Save();
            _GameStatusText.text = "Time's up, good job!";
        }
        UpdateUI();
        StartCoroutine(CallNextScene());
    }
    public bool _IsGameActive() 
    {
        // if active return minigame is active
        return _GameActive;
    }

    public  void OpenTutorialPanel() 
    {
        _TutorialPanel.SetActive(true);
        IsPaused();
    }

    public void CloseTutorialPanel()
    {
        _TutorialPanel.SetActive(false);
    }

    bool IsPaused() 
    {
        return _TutorialPanel != null && _TutorialPanel.activeSelf;
    }

    public void UpdateUI()
    {
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _LegendManager._Courage.SetActive(true);
        _LegendManager._Guilt.SetActive(true);
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Guilt.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        _GameStatusText.text = "Moving to Campus";
        SceneManager.LoadScene("Exploration 1.2");
    }
}
