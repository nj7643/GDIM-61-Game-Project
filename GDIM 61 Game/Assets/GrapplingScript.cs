using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingScript : MonoBehaviour
{


    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public GameObject targetObject;

    public Transform player;

    public GameObject p;

    private SpringJoint joint;


    private float maxDist = 100.0f;


    public bool isGPressed = false;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }



    private void Update()
    {
        if (isGPressed)
        {
            StartGrapple();
        }
        p.transform.Translate(new Vector3(2.0f, 0.0f, 0.0f) * Time.deltaTime, Space.World);
    }


    public void StartGrapple()
    {
        Vector3 forward = transform.TransformDirection(Vector3.right) * 20;
        

        RaycastHit hit;

        if (Physics.Raycast(this.gameObject.transform.position, this.gameObject.transform.right, out hit, maxDist, whatIsGrappleable))
        {

            Debug.DrawRay(transform.position, forward, Color.green);
            grapplePoint = hit.point;

            //player.gameObject.GetComponent<CharacterController>().enabled = false;
            //joint = player.gameObject.AddComponent<SpringJoint>();


            Vector3 targetPos = hit.point - transform.position;


            var step = 50.0f * Time.deltaTime; // calculate distance to move
            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            //player.position = Vector3.MoveTowards(transform.position, hit.point, step);
            //player.position = Vector3.MoveTowards(transform.position, targetPos, step);
            //player.position = Vector3.MoveTowards(transform.position, hit.point, step);
            player.Translate(new Vector3(5.0f, 0.0f, 0.0f) * Time.deltaTime, Space.World);

        }

    }


        public void StopGrapple()
    {
        //player.gameObject.GetComponent<CharacterController>().enabled = true;
    }

}