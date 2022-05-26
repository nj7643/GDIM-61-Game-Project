using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Info : MonoBehaviour
{
    private bool hasBeenHit = false;

    public bool GetHasBeenHit()
    {
        return hasBeenHit;
    }

    public void SetHasBeenHit(bool hasBeenHit)
    {
        this.hasBeenHit = hasBeenHit;
    }

}
