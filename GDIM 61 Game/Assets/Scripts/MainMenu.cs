using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public string firstLevel;

    public GameObject OptionScreen;
    

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenOptions()
    {
        Debug.Log("pressed settings");
        OptionScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        Debug.Log("pressed close settings");
        OptionScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("pressed quit");
        Application.Quit();
    }
}
