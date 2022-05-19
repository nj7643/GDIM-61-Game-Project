using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_PlayButton_Script : MonoBehaviour
{
    public void Button_Pressed()
    {
        int selectedWorldNumber = LevelSelect_Manager.GetSelectedWorldNumber();
        int selectedLevelNumber = LevelSelect_Manager.GetSelectedLevelNumber();

        Checkpoint_Manager.SpawnPlayerAtBeginningOfLevel(selectedWorldNumber, selectedLevelNumber);
        LevelSelect_Manager.HideLevelSelectScreen();
    }
}
