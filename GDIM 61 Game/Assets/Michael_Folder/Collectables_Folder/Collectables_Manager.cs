using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables_Manager : MonoBehaviour
{
    public static Collectables_Manager instance;

    [SerializeField] private GameObject screenObject;
    [SerializeField] private Transform[] parentTransforms;

    [SerializeField] private GameObject worldButtonPrefab;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private GameObject collectableUI_Prefab;

    [SerializeField] private Collectable_SO[] collectable_Database;

    private bool isOpen = false;

    private int numberOfWorlds;
    private List<int> numberOfLevelsPerWorld = new List<int>();

    private int selectedWorldNumber;
    private int selectedLevelNumber;

    private List<GameObject> collectables_L1_W1 = new List<GameObject>();
    private List<GameObject> collectables_L2_W1 = new List<GameObject>();

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
        HideCollectablesScreen();
        GetRefs();
        DestroyWorldChildren();
        DestroyLevelChildren();
        DestroyCollectableUIChildren();

        CreateWorldButtons();
        CreateCollectableUIs();
    }

    public static void GetRefs()
    {
        instance.numberOfWorlds = LevelSelect_Manager.GetWorldAmount();
        instance.numberOfLevelsPerWorld = LevelSelect_Manager.GetLevelAmountPerWorld();
    }

    public static void ShowCollectablesScreen()
    {
        instance.screenObject.SetActive(true);
        instance.isOpen = true;
    }

    public static void HideCollectablesScreen()
    {
        instance.screenObject.SetActive(false);
        instance.isOpen = false;
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

    public static void DestroyCollectableUIChildren()
    {
        foreach(Transform child in instance.parentTransforms[2])
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

    public static void CreateWorldButtons()
    {
        for(int i = 0; i < instance.numberOfWorlds; i++)
        {
            GameObject worldButton = GameObject.Instantiate(instance.worldButtonPrefab, instance.parentTransforms[0]);
            worldButton.GetComponent<C_WorldButton_Script>().SetWorldNumber(i + 1);
        }
    }

    public static void CreateLevelButtons()
    {
        DestroyLevelChildren();

        for (int i = 0; i < instance.numberOfLevelsPerWorld[instance.selectedWorldNumber - 1]; i++)
        {
            GameObject levelButton = GameObject.Instantiate(instance.levelButtonPrefab, instance.parentTransforms[1]);
            levelButton.GetComponent<C_LevelButton_Script>().SetLevelNumber(i + 1);
        }
    }

    public static void FoundCollectable(string collectableName)
    {
        for(int i = 0; i < instance.collectable_Database.Length; i++)
        {
            if(instance.collectable_Database[i].collectableName == collectableName)
            {
                switch (instance.collectable_Database[i].worldNumber)
                {
                    case 1:
                        switch (instance.collectable_Database[i].levelNumber)
                        {
                            case 1:
                                for(int ii = 0; ii < instance.collectables_L1_W1.Count; ii++)
                                {
                                    if (instance.collectables_L1_W1[ii].GetComponent<CollectableUI_Script>().GetCollectableName() == collectableName)
                                    {
                                        instance.collectables_L1_W1[ii].GetComponent<CollectableUI_Script>().SetInfo(instance.collectable_Database[i].collectableName, instance.collectable_Database[i].description, instance.collectable_Database[i].collectableName);
                                        break;
                                    }
                                }
                                break;
                        }
                        break;
                }

                break;
            }
        }
    }

    public static void HideCollectableUI()
    {
        for (int i = 0; i < instance.collectables_L1_W1.Count; i++)
        {
            instance.collectables_L1_W1[i].SetActive(false);
        }
    }

    public static void ShowCollectableUI()
    {
        HideCollectableUI();

        switch (instance.selectedWorldNumber)
        {
            case 1:
                switch (instance.selectedLevelNumber)
                {
                    case 1:
                        for(int i = 0; i < instance.collectables_L1_W1.Count; i++)
                        {
                            instance.collectables_L1_W1[i].SetActive(true);
                        }
                        break;
                }
                break;
        }
    }

    public static void CreateCollectableUIs()
    {
        for(int i = 0; i < instance.collectable_Database.Length; i++)
        {
            Collectable_SO currentData = instance.collectable_Database[i];
            GameObject collectableUI = GameObject.Instantiate(instance.collectableUI_Prefab, instance.parentTransforms[2]);
            collectableUI.SetActive(false);
            collectableUI.GetComponent<CollectableUI_Script>().SetInfo(currentData.collectableName, currentData.hint, "");

            switch (currentData.worldNumber)
            {
                case 1:
                    switch (currentData.levelNumber)
                    {
                        case 1:
                            instance.collectables_L1_W1.Add(collectableUI);
                            break;

                        case 2:
                            instance.collectables_L2_W1.Add(collectableUI);
                            break;
                    }
                    break;
            }

        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (instance.isOpen)
            {
                HideCollectablesScreen();
            }
            else
            {
                ShowCollectablesScreen();
            }
        }
    }

}
