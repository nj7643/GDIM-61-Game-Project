using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    Cursor cursorlook;
    // Start is called before the first frame update
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingMenuUI;
    public string MainMenu;

    // Update is called once per frame
    void Update()
    {
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
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void LoadMenu()
    {
        Debug.Log("menu");
        SceneManager.LoadScene(MainMenu);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
    }

    public void openSettings()
    {
        Debug.Log("Settings");
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(true);
    }

    public void closeSettings()
    {
        Debug.Log("Settings");
        settingMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
       
    }

}
