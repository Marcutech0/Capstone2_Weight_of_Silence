using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu_Manager : MonoBehaviour
{
    [SerializeField] private GameObject Game_Settings;
    [SerializeField] private GameObject Menu_UI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Game_Settings.SetActive(false);
        Menu_UI.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OpenSettings()
    {
        Game_Settings.SetActive(true);
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene("StartMainMenu");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // stop Play Mode in the Editor
        EditorApplication.isPlaying = false;
#else
        // quit in a build
        Application.Quit();
#endif
    }
}
