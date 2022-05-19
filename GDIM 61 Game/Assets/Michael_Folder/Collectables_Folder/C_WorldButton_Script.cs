using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_WorldButton_Script : MonoBehaviour
{
    [SerializeField] private Text textRef;
    private int number;

    public void Button_Pressed()
    {
        Collectables_Manager.SetSelectedNumber("World", number);
        Collectables_Manager.CreateLevelButtons();
        Collectables_Manager.HideCollectableUI();
    }

    public void SetWorldNumber(int incomingInt)
    {
        number = incomingInt;
        textRef.text = "World " + number;
    }
}
