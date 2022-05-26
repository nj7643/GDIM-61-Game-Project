using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Checkpoint":
                Checkpoint_Info checkpointInfo = other.GetComponent<Checkpoint_Info>();

                if (checkpointInfo.GetHasBeenHit() == false)
                {
                    Checkpoint_Manager.SetCheckpoint(other.gameObject);
                    checkpointInfo.SetHasBeenHit(true);
                }
                else
                {
                    Debug.Log("Already passed this checkpoint!");
                }
                break;

            case "Collectable":
                Debug.Log("Collided with collectable!");
                Collectables_Manager.FoundCollectable(other.GetComponent<CollectableWorld_Script>().GetCollectableName());
                Destroy(other.gameObject);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            gameObject.GetComponent<Copy_PlayerKill>().KillPlayer();
        }
    }

}
