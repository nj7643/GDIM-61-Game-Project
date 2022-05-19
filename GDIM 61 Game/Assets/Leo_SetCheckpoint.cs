using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leo_SetCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Checkpoint_Manager.SetCheckpoint(gameObject);
        }
    }
}
