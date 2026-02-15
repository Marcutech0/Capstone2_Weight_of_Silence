using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public float _FillRate; 
    public TextMeshProUGUI _FillPercentText;
    public Slider _TahoBar;

    void Update()
    {
        PourSoy();
        UpdateUI();
    }

    void PourSoy()
    {
        // if no selected cup return if the player presses space fill the selected cup based on fill rate
        if (CupClickManager._CurrentlySelectedCup == null) return;

        if (Input.GetKey(KeyCode.Space))
        {
            CupClickManager._CurrentlySelectedCup._FillPercent += _FillRate * Time.deltaTime;
            CupClickManager._CurrentlySelectedCup._FillPercent = Mathf.Clamp(
                CupClickManager._CurrentlySelectedCup._FillPercent, 0f, 100f);
        }
    }

    public void UpdateUI() 
    {
        // Updates UI based on the currently selected cup fill amount data in correlation to the players fill rate
        if (CupClickManager._CurrentlySelectedCup == null) return;

        float _CurrentFill = CupClickManager._CurrentlySelectedCup._FillPercent;

        if (_TahoBar != null)
            _TahoBar.value = _CurrentFill;

        if (_FillPercentText != null)
            _FillPercentText.text = $"{_CurrentFill:0}%";
    }
}
