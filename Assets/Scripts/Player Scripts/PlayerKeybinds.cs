using UnityEngine;

public class PlayerKeybinds : MonoBehaviour
{
    [SerializeField] private GameObject Tutorial_and_Menu;
    [SerializeField] private GameObject Game_Settings;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Open and close Menu with Escape Key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Ensures Settings Menu is closed when opening and closing the main menu
            Game_Settings.SetActive(false);
            Tutorial_and_Menu.SetActive(!Tutorial_and_Menu.activeSelf);
            Time.timeScale = Tutorial_and_Menu.activeSelf ? 0f : 1f;
        }
    }
}