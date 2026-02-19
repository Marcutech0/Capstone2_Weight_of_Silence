using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu_Manager : MonoBehaviour
{
    [SerializeField] private GameObject Game_Settings;
    [SerializeField] private GameObject Menu_UI;

    //Audio Section
    [SerializeField] private AudioClip ClickSfx;
    [SerializeField] private AudioSource AudioSource;

    //Gameflow 
    [SerializeField] private GameFlowLegendManager GameFlowLegendManager;

    void Start()
    {
        GameFlowLegendManager = FindAnyObjectByType<GameFlowLegendManager>();
       Game_Settings.SetActive(false);
        Menu_UI.SetActive(false);
    }

    private void PlayClickSFX()
    {
        AudioSource.PlayOneShot(ClickSfx);
    }


    public void OpenSettings()
    {
        PlayClickSFX();
        Game_Settings.SetActive(true);
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene("StartMainMenu");

        //Save Variables for replayability
        PlayerPrefs.Save();
    }
    public void ExitGame()
    {
        PlayClickSFX();
        Application.Quit();

        //Reset Variables for replayability
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public void CallNextScene()
    {
        SceneManager.LoadScene("Cutscene1.3");
    }
}
