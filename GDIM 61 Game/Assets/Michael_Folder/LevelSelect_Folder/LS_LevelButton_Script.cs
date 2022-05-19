using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LS_LevelButton_Script : MonoBehaviour
{
    [SerializeField] private Text textRef;

    private int number;
    
    public void Button_Pressed()
    {
        LevelSelect_Manager.SetSelectedNumber("Level", number);
        LevelSelect_Manager.ShowPlayButton();
    }

    public void SetLevelNumber(int incomingInt)
    {
        number = incomingInt;
        textRef.text = "Level " + number;
    }

}
