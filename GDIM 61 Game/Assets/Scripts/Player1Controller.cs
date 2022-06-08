using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    //float walkSpeed = 5.0f;
    //float runSpeed = 12.0f;
    float walkSpeed = 10.0f;
    float runSpeed = 16.0f;

    //gravity
    float gravity = -9.8f;
    float groundedGravity = -0.05f;

    //jumping variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    //float maxJumpHeight = 3.0f;
    float maxJumpHeight = 6.0f;
    float maxJumpTime = 0.5f;
    bool isJumping = false;


    //grapple variables
    bool isGrapplePressed = false;

    //grapple Aim variables
    Vector2 currentAimInput;
    public Slider m_AimSlider;
    public Canvas ArrowDirection;

    public float m_MinLaunchForce = 15f;
    public float m_MaxLaunchForce = 30f;
    private float m_CurrentLaunchForce;


    public GameObject GrappleSpawn;

    private bool canGrapple = false;


    private Vector3 grappleTarget;
    private bool grappleInProgress = false;
    private GameObject tObject;

    private void Awake()
    {

        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        //CapsuleRigidbody = GetComponent<Rigidbody>();

        playerInput = new PlayerInputActions();
        characterController = GetComponent<CharacterController>();
        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Move.performed+= OnMovementInput;

        playerInput.Player.Run.started += OnRun;
        playerInput.Player.Run.canceled += OnRun;

        playerInput.Player.Grapple.started += OnGrapple;
        playerInput.Player.Grapple.canceled += OnGrapple;

        //playerInput.Player.Grapple.performed += OnGrapple;

        playerInput.Player.Jump.started += OnJump;
        playerInput.Player.Jump.canceled += OnJump;

        playerInput.Player.GrappleAim.started += OnAimInput;
        playerInput.Player.GrappleAim.canceled += OnAimInput;
        playerInput.Player.GrappleAim.performed+= OnAimInput;


        SetupJumpVariables();
    }


    void OnAimInput(InputAction.CallbackContext context)
    {
        currentAimInput = context.ReadValue<Vector2>();


        //m_AimSlider.value = currentAimInput.x * 30.0f;
        m_AimSlider.value = Mathf.Sqrt((Mathf.Pow(currentAimInput.x, 2.0f) + Mathf.Pow(currentAimInput.y, 2.0f))) * 30.0f;
        //ArrowDirection.transform.Rotate(0, 0, 4.0f, Space.Self);

        //ArrowDirection.transform.eulerAngles = new Vector3(ArrowDirection.transform.eulerAngles.x, Mathf.Atan2(currentAimInput.x, currentAimInput.y) * Mathf.Rad2Deg, ArrowDirection.transform.eulerAngles.z);
        ArrowDirection.transform.eulerAngles = new Vector3(ArrowDirection.transform.eulerAngles.x, ArrowDirection.transform.eulerAngles.y, - Mathf.Atan2(currentAimInput.x, currentAimInput.y) * Mathf.Rad2Deg - 270);


    }


    void HandleHookShot()
    {

    }

    void HandleGrapple()
    {

        if (!isJumping && characterController.isGrounded && isGrapplePressed)
        {

            Debug.Log("Aiming...");
        }

        else if (!isGrapplePressed)
        {
            Debug.Log("STOPPED GRAPPLING");
        }

    }

    void OnGrapple(InputAction.CallbackContext context)
    {
        isGrapplePressed = context.ReadValueAsButton();
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

        //if (Physics.Raycast(this.gameObject.transform.position, this.gameObject.transform.up, out hit))
        if (GrappleSpawn.GetComponent<GrappleScript>().foundTarget && !canGrapple)
            {
            var step = 15.0f * Time.deltaTime; // calculate distance to move
            //Vector3 targetPos = hit.point - transform.position;

            Vector3 targetPos = GrappleSpawn.GetComponent<GrappleScript>().targetPos - transform.position;

            //grappleTarget = hit.point;
            grappleTarget = GrappleSpawn.GetComponent<GrappleScript>().targetPos;
            canGrapple = true;
            tObject = GrappleSpawn.GetComponent<GrappleScript>().targetObject;

        }

        

        //transform.Translate(new Vector3(10.0f, 0.0f, 0.0f) * Time.deltaTime, Space.World);

        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }


        if (!isGrapplePressed)
        {
            HandleGravity();
        }
        
        HandleJump();
        HandleGrapple();


        

        if (canGrapple && isGrapplePressed)
        {
            transform.position = Vector3.MoveTowards(transform.position, tObject.transform.position, 200.0f * Time.deltaTime);

            grappleInProgress = true;

        }
        

        //float grappleDist = Vector3.Distance(transform.position, grappleTarget);
        if (Vector3.Distance(transform.position, grappleTarget) <= 2.0f){
            grappleInProgress = false;
            isGrapplePressed = false;
            canGrapple = false;
            GrappleSpawn.GetComponent<GrappleScript>().foundTarget = false;
        }

        /*
        if (!gameObject.GetComponent<Player_Kill>().scriptsEnabled)
        {
            grappleInProgress = false;
            isGrapplePressed = false;
            canGrapple = false;
        }
        */
    }


    void OnEnable()
    {
        playerInput.Player.Enable();
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
    }

}
