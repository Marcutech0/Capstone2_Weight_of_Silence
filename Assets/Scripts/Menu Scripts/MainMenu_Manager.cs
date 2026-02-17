using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField] string[] scenes;
    public GameObject Settings_Menu;
    public GameObject CreditsPanel;

    public Vector2 hoverOffset = new Vector2(30f, 0f);
    public float slideSpeed = 10f;

    private RectTransform _rect;
    private Vector2 _originalPosition;
    private Vector2 _targetPosition;


    //Audio Section
    [SerializeField] private AudioClip ClickSfx;
    [SerializeField] private AudioSource AudioSource;

    void Start()
    {
        Settings_Menu.SetActive(false);
        CreditsPanel.SetActive(false);
    }

    //For playing Sfx
    private void PlayClickSFX()
    {
        AudioSource.PlayOneShot(ClickSfx);
    }


    public void PlayGame()
    {
        PlayClickSFX();
            SceneManager.LoadScene("PrologueScene");
    }

    public void ExitGame()
    {
            PlayClickSFX();
        Application.Quit();
    }

    //either Settings Menu in same scene or load new scene
    #region Settings Menu 
    public void openSettings()
    {
        PlayClickSFX();
        Settings_Menu.SetActive(true);

    }

    public void closeSettings()
    {
        PlayClickSFX();
        Settings_Menu.SetActive(false);

    }

    #endregion

    public void openCreditsPanel()
    {
        PlayClickSFX();
        CreditsPanel.SetActive(true);
    }

    public void closeCreditsPanel()
    {
        PlayClickSFX();
        CreditsPanel.SetActive(false);

    }

    public void HoverEnter()
    {
        _targetPosition = _originalPosition + hoverOffset;
    }

    public void HoverExit()
    {
        _targetPosition = _originalPosition;
    }


}

