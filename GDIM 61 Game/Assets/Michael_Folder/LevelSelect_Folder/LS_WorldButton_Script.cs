using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LS_WorldButton_Script : MonoBehaviour
{
    [SerializeField] private Text textRef;

    private int number;

    public void Button_Pressed()
    {
        LevelSelect_Manager.SetSelectedNumber("World", number);
        LevelSelect_Manager.CreateLevelButtons();
        LevelSelect_Manager.HidePlayButton();
    }

    public void SetWorldNumber(int incomingInt)
    {
        number = incomingInt;
        textRef.text = "World " + number;
    }

    //public int GetWorldNumber()
    //{
    //    return number;
    //}

}
