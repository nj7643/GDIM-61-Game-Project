using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect_Manager : MonoBehaviour
{
    public static LevelSelect_Manager instance;

    [SerializeField] private GameObject screenObject;
    [SerializeField] private Transform[] parentTransforms;

    [SerializeField] private GameObject worldButtonPrefab;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private GameObject playButtonObject;

    private int numberOfWorlds = 3;
    private List<int> numberOfLevelsPerWorld = new List<int> { 2, 4, 6 };

    private bool isOpen = false;

    private int selectedWorldNumber = 1;
    private int selectedLevelNumber = 1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        InitialSetup();
    }

    public static void InitialSetup()
    {
        HideLevelSelectScreen();
        DestroyWorldChildren();
        DestroyLevelChildren();

        CreateWorldButtons();
        HidePlayButton();
    }

    public static int GetWorldAmount()
    {
        return instance.numberOfWorlds;
    }

    public static List<int> GetLevelAmountPerWorld()
    {
        return instance.numberOfLevelsPerWorld;
    }

    public static void ShowLevelSelectScreen()
    {
        instance.screenObject.SetActive(true);
        instance.isOpen = true;
    }

    public static void HideLevelSelectScreen()
    {
        instance.screenObject.SetActive(false);
        instance.isOpen = false;
    }

    public static void ShowPlayButton()
    {
        instance.playButtonObject.SetActive(true);
    }

    public static void HidePlayButton()
    {
        instance.playButtonObject.SetActive(false);
    }

    #region Destroy Children Functions
    public static void DestroyWorldChildren()
    {
        foreach (Transform child in instance.parentTransforms[0])
        {
            Destroy(child.gameObject);
        }
    }

    public static void DestroyLevelChildren()
    {
        foreach (Transform child in instance.parentTransforms[1])
        {
            Destroy(child.gameObject);
        }
    }
    #endregion

    public static void SetSelectedNumber(string incomingString, int incomingInt)
    {
        switch (incomingString)
        {
            case "World":
                instance.selectedWorldNumber = incomingInt;
                break;

            case "Level":
                instance.selectedLevelNumber = incomingInt;
                break;
        }
    }

    public static int GetSelectedWorldNumber()
    {
        return instance.selectedWorldNumber;
    }

    public static int GetSelectedLevelNumber()
    {
        return instance.selectedLevelNumber;
    }

    public static void CreateWorldButtons()
    {
        for(int i = 0; i < instance.numberOfWorlds; i++)
        {
            GameObject worldButton = GameObject.Instantiate(instance.worldButtonPrefab, instance.parentTransforms[0]);
            worldButton.GetComponent<LS_WorldButton_Script>().SetWorldNumber(i + 1);
        }

        Debug.Log("Created World Buttons!");
    }

    public static void CreateLevelButtons()
    {
        DestroyLevelChildren();

        for (int i = 0; i < instance.numberOfLevelsPerWorld[instance.selectedWorldNumber - 1]; i++)
        {
            GameObject levelButton = GameObject.Instantiate(instance.levelButtonPrefab, instance.parentTransforms[1]);
            levelButton.GetComponent<LS_LevelButton_Script>().SetLevelNumber(i + 1);
        }

        Debug.Log("Created Level Buttons!");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (instance.isOpen)
            {
                HideLevelSelectScreen();
            }
            else
            {
                ShowLevelSelectScreen();
            }
        }

    }



}
