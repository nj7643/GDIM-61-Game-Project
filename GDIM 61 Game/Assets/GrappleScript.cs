using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{


    public bool foundTarget = false;

    public Vector3 targetPos;

    public GameObject targetObject;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.right) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        RaycastHit hit;


        //Vector2 end = hit.origin + hit.direction * distance;
        if (Physics.Raycast(this.gameObject.transform.position, this.gameObject.transform.right, out hit))
        {

            foundTarget = true;

            targetPos = hit.point;
            //Vector3 targetPos = hit.point - transform.position;
            targetObject = hit.transform.gameObject;

            //var step = 15.0f * Time.deltaTime; // calculate distance to move
            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            //transform.position = Vector3.MoveTowards(transform.position, hit.point, step);



            Debug.Log("RAYCAST");
        }
        else
        {
            Debug.Log("NO HIT");
            foundTarget = false;
        }

    }



    void StartGrapple()
    {



    }

}
