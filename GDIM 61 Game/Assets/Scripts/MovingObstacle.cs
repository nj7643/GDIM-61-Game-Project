using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{

    [SerializeField] private float directionTimer = 2.0f;
    [SerializeField] private float moveSpeed = 5.0f;
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
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
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


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            other.transform.SetParent(gameObject.transform);
            Debug.Log("Success!");
        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            other.transform.SetParent(null);
            Debug.Log("Exit!");
        }

    }
    


}
