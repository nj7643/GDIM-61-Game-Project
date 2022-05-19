using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leo_KillAndRespawn : MonoBehaviour
{
    [SerializeField] private Player_Kill script;

    private void OnTriggerEnter(Collider other)
    {
        script.KillPlayer();
    }
}
