using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{

    private float directionTimer = 2.0f;
    private float currentTime;

    private bool directionSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = directionTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (directionSwitch)
        {
            transform.Translate(5f * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(-5f * Time.deltaTime, 0, 0);
        }

        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            ChangeDirections();
            currentTime = directionTimer;
        }

        

    }

    void ChangeDirections()
    {
        if (directionSwitch)
        {
            directionSwitch = false;
        }
        else
        {
            directionSwitch = true;
        }
    }
}
