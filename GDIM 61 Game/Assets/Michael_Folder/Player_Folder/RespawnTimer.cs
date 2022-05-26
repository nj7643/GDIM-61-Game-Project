using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTimer : MonoBehaviour
{
    [SerializeField] private float seconds;

    private GameObject player;

    public void StartRespawnTimer(GameObject player)
    {
        this.player = player;
        StartCoroutine(Respawn_Timer());
    }

    IEnumerator Respawn_Timer()
    {
        player.SetActive(false);

        yield return new WaitForSeconds(seconds);

        Checkpoint_Manager.SpawnPlayerAtCheckpoint();
        player.SetActive(true);
    }
}
