using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copy_PlayerKill : MonoBehaviour
{
    public void KillPlayer()
    {
        Manager_RespawnTimer.RespawnTimerFunction(gameObject);
    }

}
