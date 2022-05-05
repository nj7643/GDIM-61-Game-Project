using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipScript : MonoBehaviour
{
    /*
    float speed = 6.0f; 
    float jumpSpeed = 8.0f; 
    float friction = 1.0f; // 0 means no friction; private var curVel = Vector3.zero; private var velY: float = 0; private var character: CharacterController;

    void Update()
    { 
        // get the CharacterController only the first time: if (!character) character = GetComponent(CharacterController); // get the direction from the controls: var dir = Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); // calculate the desired velocity: var vel = transform.TransformDirection(dir) * speed;

        // here's where the magic happens: curVel = Vector3.Lerp(curVel, vel, 5 friction friction * Time.deltaTime);

        // apply gravity and jump after the friction! if (character.isGrounded){ velY = 0; if (Input.GetKeyDown("Jump")){ velY = jumpSpeed; } velY -= gravity Time.deltaTime; } curVel.y = velY; character.Move(curVel Time.deltaTime); }

    void OnTriggerEnter(Collider other){
            if (other.name == "Ice") friction = 0.1f; // set low friction }

    void OnTriggerExit(Collider other){
                if (other.name == "Ice") friction = 1f; // restore regular friction } To define a slippery region, create a trigger covering the desired area and name it "Ice". You may have as many "Ice" objects you want.

               // NOTE: If you're using the standard First Person Controller, you must replace the input control script with the script below: save it in some Assets subfolder, add it to the First Person Controller and remove the original FPSInputController script:
            
var friction: float = 1.0;

private var motor : CharacterMotor; var curVel = Vector3.zero;

    // Use this for initialization function Awake () { motor = GetComponent(CharacterMotor); }

    // Update is called once per frame function Update () { // Get the input vector from keyboard or analog stick var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); var vel = transform.TransformDirection(dir); // Apply the friction factor: curVel = Vector3.Lerp(curVel, vel, 5*friction*friction*Time.deltaTime); motor.inputMoveDirection = curVel; motor.inputJump = Input.GetButton("Jump"); }

    function OnTriggerEnter(other: Collider)
    {
        if (other.name == "Ice") friction = 0.1; // set low friction }

        function OnTriggerExit(other: Collider){
            if (other.name == "Ice") friction = 1; // restore regular friction }



            // Start is called before the first frame update
            void Start()
    {
        
    }
    */

}
