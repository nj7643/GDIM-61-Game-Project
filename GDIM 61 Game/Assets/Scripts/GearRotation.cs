using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    float rotationsPerMinute = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {
        //transform.Rotate(0f, 6.0f * rotationsPerMinute * Time.deltaTime, 0f);
        transform.Rotate(0f, 0f, 6.0f * rotationsPerMinute * Time.deltaTime);
    }
}
