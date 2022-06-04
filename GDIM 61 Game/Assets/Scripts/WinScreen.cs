using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{ 
    [SerializeField]
    public GameObject winScreenUI;
    public string MainMenu;
    

    private void Start()
    {
        winScreenUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
      
    }


    public void LoadMenu()
    {
        Time.timeScale = 1f;
        //load title screen
        Debug.Log("menu");
        SceneManager.LoadScene(MainMenu);
    }

    public void QuitGame()
    {
        //quit game completely
        Debug.Log("Quit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Pause();
            winScreenUI.SetActive(true);
        }
    }

    void Pause()
    {
        //pause game and show pause screen
        
        Time.timeScale = 0f;
        

    }
}
