using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Controller : MonoBehaviour
{

    //private Rigidbody CapsuleRigidbody;
    PlayerInputActions playerInput;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    bool isMovementPressed;
    bool isRunPressed;
    CharacterController characterController;

    float walkSpeed = 5.0f;
    float runSpeed = 12.0f;

    //gravity
    float gravity = -9.8f;
    float groundedGravity = -0.05f;

    //jumping variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 3.0f;
    float maxJumpTime = 0.5f;
    bool isJumping = false;


    //grapple variables
    bool isGrappleAimPressed = false;


    private void Awake()
    {
        //CapsuleRigidbody = GetComponent<Rigidbody>();

        playerInput = new PlayerInputActions();
        characterController = GetComponent<CharacterController>();
        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Move.performed+= OnMovementInput;

        playerInput.Player.Run.started += OnRun;
        playerInput.Player.Run.canceled += OnRun;

        playerInput.Player.Grapple.started += OnGrappleAim;
        playerInput.Player.Grapple.canceled += OnGrappleAim;

        playerInput.Player.Jump.started += OnJump;
        playerInput.Player.Jump.canceled += OnJump;


        SetupJumpVariables();
    }



    void HandleGrappleAim()
    {

        if (!isJumping && characterController.isGrounded && isGrappleAimPressed)
        {

            Debug.Log("Aiming...");
            //isJumping = true;
            //currentMovement.y = initialJumpVelocity;
            //currentRunMovement.y = initialJumpVelocity;
        }

    }

    void OnGrappleAim(InputAction.CallbackContext context)
    {
        isGrappleAimPressed = context.ReadValueAsButton();
        //Debug.Log("Aiming..." + context.phase);
    }


    void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
            currentRunMovement.y = initialJumpVelocity;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime/2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        Debug.Log("Jump" + context.phase);
    }


    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }

        else
        {
            
            currentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;

        }
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * walkSpeed; ;
        //currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runSpeed;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void OnRun(InputAction.CallbackContext context)
    {

        isRunPressed = context.ReadValueAsButton();

    }

    private void Update()
    {

        
        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }

        HandleGravity();
        HandleJump();
        HandleGrappleAim();
    }

    /*
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump" + context.phase);
            //CapsuleRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }


    }
    */

    void OnEnable()
    {
        playerInput.Player.Enable();
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
    }

}
