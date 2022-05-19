using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Manager : MonoBehaviour
{
    public static Checkpoint_Manager instance;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private GameObject[] startingCPs_W1;

    private GameObject previousCheckpoint;
    private GameObject currentCheckpoint;

    private GameObject savedCheckpoint;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static void SetCheckpoint(GameObject incomingObject)
    {
        //instance.previousCheckpoint = instance.currentCheckpoint;
        //instance.currentCheckpoint = incomingObject;

        instance.savedCheckpoint = incomingObject;
        Debug.Log("Set checkpoint!");
    }

    public static void SpawnPlayerAtCheckpoint()
    {
        instance.playerTransform.position = instance.savedCheckpoint.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
    }

    public static void SpawnPlayerAtBeginningOfLevel(int worldNumber, int levelNumber)
    {
        switch (worldNumber)
        {
            case 1:
                instance.playerTransform.position = instance.startingCPs_W1[levelNumber - 1].transform.position;
                SetCheckpoint(instance.startingCPs_W1[levelNumber - 1]);
                break;

            case 2:

                break;

            case 3:

                break;

        }
    }

}
