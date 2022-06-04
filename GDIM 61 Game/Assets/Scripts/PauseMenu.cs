using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    
    // Start is called before the first frame update
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingMenuUI;
    public string MainMenu;

    // Update is called once per frame
    void Update()
    {
        //pausing and resuming
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //resume and hide pause screen and settings
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        

    }

    void Pause()
    {
        //pause game and show pause screen
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void LoadMenu()
    {
        //load title screen
        Debug.Log("menu");
        SceneManager.LoadScene(MainMenu);
    }

    public void QuitGame()
    {
        //quit game completely
        Debug.Log("Quit");
    }

    public void openSettings()
    {
        //activates and shows settings (sound and controls)
        Debug.Log("Settings");
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(true);
    }

    public void closeSettings()
    {
        //closes and leads back to pause screen
        Debug.Log("Settings");
        settingMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
       
    }

}
