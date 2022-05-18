using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Kill : MonoBehaviour
{
    private Player_Movement playerMovementRef;
    private Player_Collision playerCollisionRef;
    private SpriteRenderer playerSpriteRef;

    private void Awake()
    {
        playerMovementRef = gameObject.GetComponent<Player_Movement>();
        playerCollisionRef = gameObject.GetComponent<Player_Collision>();
        playerSpriteRef = gameObject.GetComponent<SpriteRenderer>();
    }

    public void KillPlayer()
    {
        playerMovementRef.SetVelocityToZero();
        EnableScripts(false);
        StartCoroutine(Respawn_Timer());
    }

    public void EnableScripts(bool incomingBool)
    {
        playerMovementRef.enabled = incomingBool;
        playerCollisionRef.enabled = incomingBool;
        playerSpriteRef.enabled = incomingBool;
    }

    IEnumerator Respawn_Timer()
    {
        yield return new WaitForSeconds(3f);

        Checkpoint_Manager.SpawnPlayerAtCheckpoint();
        EnableScripts(true);
    }
}
