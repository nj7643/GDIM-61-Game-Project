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
                Debug.Log("Collided with checkpoint!");
                Checkpoint_Manager.SetCheckpoint(other.gameObject);
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
            gameObject.GetComponent<Player_Kill>().KillPlayer();
        }
    }

}
