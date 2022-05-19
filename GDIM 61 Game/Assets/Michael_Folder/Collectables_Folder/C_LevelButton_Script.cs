using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_LevelButton_Script : MonoBehaviour
{
    [SerializeField] private Text textRef;
    private int number;
    
    public void Button_Pressed()
    {
        Collectables_Manager.SetSelectedNumber("Level", number);
        Collectables_Manager.ShowCollectableUI();
    }

    public void SetLevelNumber(int incomingInt)
    {
        number = incomingInt;
        textRef.text = "Level " + number;
    }

}
