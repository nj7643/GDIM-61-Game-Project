using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_RespawnTimer : MonoBehaviour
{
    public static Manager_RespawnTimer instance;

    [SerializeField] private RespawnTimer respawnTimer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public static void RespawnTimerFunction(GameObject player)
    {
        instance.respawnTimer.StartRespawnTimer(player);
    }

}
