using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //PlayerInputActions playerInput;

    CursorController playerInput;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector2 stickMovementInput;

    bool isMovementPressed;
    bool isRunPressed;
    CharacterController characterController;

    float walkSpeed = 16.0f;
    float runSpeed = 24.0f;

    //gravity
    float gravity = -9.8f;
    //float gravity = -9f;
    float groundedGravity = -0.05f;

    //jumping variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 9.0f;
    float maxJumpTime = 0.7f;
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

    [SerializeField] Transform debugHitTransform;
    [SerializeField] Transform grapplerTransform;
    private State state;
    private Vector3 grapplePosition;


    private Vector2 characterVelocityMomentum;

    private float grappleSize;

    //wall jumping variables
    bool isContactingWall = false;
    bool isWallJumping = false;

    //slippery surfaces
    private bool isSlippery = false;

    //sticky surfaces
    private bool isSticky = false;


    //bouncy surfaces
    private bool isBouncy = false;


    bool isGrounded = false;

    //for debugging
    private bool isRespawnPressed = false;
    public Camera cam;
    Vector3 mousePos;
    [SerializeField]
    private RectTransform cursorTransform;

    [SerializeField]
    private CursorScript cursorScript;

    //particle system
    public ParticleSystem dust;
    private bool facingRight = true;
    private bool facingLeft = false;


    //character sprite
    [SerializeField]
    private GameObject characterSprite;

    //animations
    private Animator animator;


    private enum State
    {
        Normal, GrappleThrown, GrappleFlyingPlayer
    }


    private void Awake()
    {

        animator = characterSprite.GetComponent<Animator>();

        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;

        //playerInput = new PlayerInputActions();
        playerInput = new CursorController();

        characterController = GetComponent<CharacterController>();
        playerInput.Player.Move.started += OnMovementInput;
        playerInput.Player.Move.canceled += OnMovementInput;
        playerInput.Player.Move.performed += OnMovementInput;

        playerInput.Player.Run.started += OnRun;
        playerInput.Player.Run.canceled += OnRun;

        playerInput.Player.Grapple.started += OnGrapple;
        playerInput.Player.Grapple.canceled += OnGrapple;


        playerInput.Player.Jump.started += OnJump;
        playerInput.Player.Jump.canceled += OnJump;

        playerInput.Player.GrappleAim.started += OnAimInput;
        playerInput.Player.GrappleAim.canceled += OnAimInput;
        playerInput.Player.GrappleAim.performed += OnAimInput;


        playerInput.Player.Respawn.started += OnRespawn;
        playerInput.Player.Respawn.canceled += OnRespawn;


        state = State.Normal;
        SetupJumpVariables();
        grapplerTransform.gameObject.SetActive(false);
    }



    void OnRespawn(InputAction.CallbackContext context)
    {
        isRespawnPressed = context.ReadValueAsButton();
    }

    void HandleRespawn()
    {
        if (isRespawnPressed)
        {
            transform.position = new Vector3(0, 1.5f, 0);
        }
    }

    void OnAimInput(InputAction.CallbackContext context)
    {
        currentAimInput = context.ReadValue<Vector2>();

        m_AimSlider.value = Mathf.Sqrt((Mathf.Pow(currentAimInput.x, 2.0f) + Mathf.Pow(currentAimInput.y, 2.0f))) * 30.0f;
        //ArrowDirection.transform.Rotate(0, 0, 4.0f, Space.Self);

        //ArrowDirection.transform.eulerAngles = new Vector3(ArrowDirection.transform.eulerAngles.x, Mathf.Atan2(currentAimInput.x, currentAimInput.y) * Mathf.Rad2Deg, ArrowDirection.transform.eulerAngles.z);
    }


    void AimWithCursor()
    {
        //distance from player to camera
        float camToPlayerDist = Vector3.Distance(transform.position, Camera.main.transform.position);

        //world position of cursor/mouse
        //Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camToPlayerDist));


        Vector3 cursorPos = cursorScript.GetCursorPosition();
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(cursorPos.x, cursorPos.y, camToPlayerDist));

        // direction of cursor/mouse is pointing from player
        Vector2 direction = mouseWorldPosition - (Vector2)transform.position;

        //direction angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //rotating the transform based on angle
        ArrowDirection.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void HandleGrappleMovement()
    {
        Vector3 grappleDir = (grapplePosition - transform.position).normalized;
        float grappleSpeed = 60.0f;

        characterController.Move(grappleDir * grappleSpeed * Time.deltaTime);

        grappleSize -= grappleSpeed * Time.deltaTime;
        grapplerTransform.localScale = new Vector3(1, 1, grappleSize);


        //distance from character to position of hookshot target
        //float reachedGrapplePositionDistance = 2.0f;
        float reachedGrapplePositionDistance = 3.0f;
        if (Vector3.Distance(transform.position, grapplePosition) <= reachedGrapplePositionDistance)
        {

            isGrapplePressed = false;
            state = State.Normal;
            ResetGravity();
            grapplerTransform.gameObject.SetActive(false);
        }



        //Cancel Grapple with Jump Button
        if (isJumpPressed)
        {
            float momentumExtraSpeed = 2.0f;
            characterVelocityMomentum = grappleDir * grappleSpeed * momentumExtraSpeed;
            //characterVelocityMomentum = grappleDir * grappleSpeed;

            //float jumpSpeed = 1f;
            //characterVelocityMomentum += Vector2.up * jumpSpeed;

            state = State.Normal;

            bool isWalking = animator.GetBool("isMoving");
            if (isWalking)
            {
                animator.SetBool("isMoving", false);
            }

            ResetGravity();
            grapplerTransform.gameObject.SetActive(false);
        }

    }

    private void HandleGrappleThrow()
    {
        grapplerTransform.LookAt(grapplePosition);
        //float grappleThrowSpeed = 50f;
        //float grappleThrowSpeed = 60f;
        float grappleThrowSpeed = 80f;
        grappleSize += grappleThrowSpeed * Time.deltaTime;
        grapplerTransform.localScale = new Vector3(1, 1, grappleSize);

        if (grappleSize >= Vector3.Distance(transform.position,grapplePosition))
        {
            //animator.SetBool("isMoving", false);
            state = State.GrappleFlyingPlayer;
        }
    }

    void HandleGrapple()
    {
        if (!isJumping && isGrapplePressed)
        {

            //Debug.Log("Aiming...");
            if (Physics.Raycast(GrappleSpawn.transform.position, GrappleSpawn.transform.right, out RaycastHit hit))
            {
                debugHitTransform.position = hit.point;
                grapplePosition = hit.point;
                grappleSize = 0f;
                grapplerTransform.gameObject.SetActive(true);
                grapplerTransform.localScale = Vector3.zero;
                state = State.GrappleThrown;

            }
        }

        else if (!isGrapplePressed)
        {
            //Debug.Log("STOPPED GRAPPLING");
        }

    }

    void OnGrapple(InputAction.CallbackContext context)
    {
        isGrapplePressed = context.ReadValueAsButton();
    }


    void HandleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        //if ((!isJumping && characterController.isGrounded && isJumpPressed) ||(!characterController.isGrounded && isJumpPressed && isContactingWall))
        {

            CreateDustTrail();
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
            currentRunMovement.y = initialJumpVelocity;

            bool isJumpingPressed = animator.GetBool("isJumping");
            if (!isJumpingPressed)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isGrounded", false);
            }


        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        //Debug.Log("Jump" + context.phase);
    }


    void HandleGravity()
    {

        if (characterController.isGrounded)
        //if (isGrounded)
        {
            
            bool groundCheck = animator.GetBool("isGrounded");
            if (!groundCheck)
            {
                animator.SetBool("isGrounded", true);
                //animator.SetBool("isFalling", false);
            }
            
            

            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
            bool isJumpingPressed = animator.GetBool("isJumping");
            if (isJumpingPressed)
            {
                animator.SetBool("isJumping", false);
                
                
            }
        }

        else
        {
            
            bool groundCheck = animator.GetBool("isGrounded");
            if (groundCheck)
            {
                animator.SetBool("isGrounded", false);

            }
            

            

            if (isContactingWall && currentMovement.y < 0)
            {
                CreateDustTrail();
                currentMovement.y += gravity * 0.1f * Time.deltaTime;
                currentRunMovement.y += gravity * 0.1f * Time.deltaTime;

            }
            else {
                currentMovement.y += gravity * Time.deltaTime;
                currentRunMovement.y += gravity * Time.deltaTime;
                bool isFalling = animator.GetBool("isFalling");
                if (!isFalling && (currentMovement.y < 0f || currentRunMovement.y < 0f))
                {
                    animator.SetBool("isFalling", true);
                }
                else if (isFalling)
                {
                    animator.SetBool("isFalling", false);
                }

            }

        }
    }

    private void ResetGravity()
    {
        currentMovement.y = groundedGravity;
        currentRunMovement.y = groundedGravity;
    }


    void HandleMovement()
    {
        //Debug.Log(currentMovement.y);

        if (!isWallJumping)
        {
            currentMovement.x = currentMovementInput.x * walkSpeed; ;
            //currentMovement.z = currentMovementInput.y;
            currentRunMovement.x = currentMovementInput.x * runSpeed;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;

            //Apply Momentum
            currentMovement.x += characterVelocityMomentum.x;
            currentRunMovement.x += characterVelocityMomentum.x;
        }



        //animation
        bool isWalking = animator.GetBool("isMoving");


        if (!isWalking && stickMovementInput.x != 0f && characterController.isGrounded)
        {
            animator.SetBool("isMoving", true);
        }
        else if ((isWalking) && (stickMovementInput.x == 0f|| !characterController.isGrounded))
        {
            animator.SetBool("isMoving", false);
        }
        
        
        



        //dust particles when flipping directions (left and right)
        //if (characterController.isGrounded &&  currentMovementInput.x > 0f && facingLeft == true)
        if (!isWallJumping && currentMovementInput.x > 0f && facingLeft == true)
        {
            if (!isJumping)
            {
            CreateDustTrail();
            }
            Flip(false);
        }
        //else if (characterController.isGrounded && currentMovementInput.x < 0f && facingLeft != true)
        else if (!isWallJumping && currentMovementInput.x < 0f && facingLeft != true)
        {
            if (!isJumping)
            {
                CreateDustTrail();
            }

            Flip(true);

        }
        
        
        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
            //animator.SetBool("isMoving", true);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
            //animator.SetBool("isMoving", false);
        }
        

        Debug.Log("momentum:" + characterVelocityMomentum);
        
        //moving left
        if (isSlippery && currentMovementInput.x < 0f)
        {
            if (characterVelocityMomentum.x > 0f && characterController.isGrounded)
            {
                //characterVelocityMomentum = new Vector2(characterVelocityMomentum.x -(characterVelocityMomentum.x * .1f * Time.deltaTime), 0);
                //characterVelocityMomentum.x += characterVelocityMomentum.x * .4f * Time.deltaTime;
                characterVelocityMomentum.x -= 4f * Time.deltaTime;
                //characterVelocityMomentum.x *= 1.25f * Time.deltaTime;
            }
            else if (characterVelocityMomentum.x > 0f)
            {
                characterVelocityMomentum.x -= 40f * Time.deltaTime;
                if (characterVelocityMomentum.x < 0f)
                {
                    characterVelocityMomentum.x = 0f;
                }
            }
            else
            {
                //characterVelocityMomentum = new Vector2(characterVelocityMomentum.x - .3f, 0);
                //characterVelocityMomentum.x -= 10f * Time.deltaTime;
                characterVelocityMomentum.x -= 200f * Time.deltaTime;
                //characterVelocityMomentum.x *= 1.25f;
            }

            
            if (characterVelocityMomentum.x > 45f)
            {
                characterVelocityMomentum.x = 45f;
            }
            if (characterVelocityMomentum.x < -45f)
            {
                characterVelocityMomentum.x = -45f;
            }
            

        }

        //moving right
         if (isSlippery  && currentMovementInput.x > 0f)
        {

            if (characterVelocityMomentum.x < 0f && characterController.isGrounded)
            {
                //characterVelocityMomentum = new Vector2(characterVelocityMomentum.x + (-characterVelocityMomentum.x * .1f * Time.deltaTime), 0);
                //characterVelocityMomentum.x -= characterVelocityMomentum.x * .4f * Time.deltaTime;
                characterVelocityMomentum.x += 4f * Time.deltaTime;
            }

            else if (characterVelocityMomentum.x < 0f)
            {
                characterVelocityMomentum.x += 40f * Time.deltaTime;
                if (characterVelocityMomentum.x > 0f)
                {
                    characterVelocityMomentum.x = 0f;
                }
            }

            else
            {

                //characterVelocityMomentum = new Vector2(characterVelocityMomentum.x + 3f, 0);
                //characterVelocityMomentum.x += 3f * Time.deltaTime;
                //characterVelocityMomentum.x += 10f * Time.deltaTime;
                characterVelocityMomentum.x += 200f * Time.deltaTime;
                //characterVelocityMomentum.x *= 1.25f;
            }
            if (characterVelocityMomentum.x > 45f)
            {
                characterVelocityMomentum.x = 45f;
            }
            if (characterVelocityMomentum.x < -45f)
            {
                characterVelocityMomentum.x = -45f;
            }
        }


        //Dampen Momentum
        if (characterVelocityMomentum.magnitude >= 0f || isSlippery && currentMovementInput.x == 0f)
        {

            float momentumDrag = 3.25f;
            if ((characterController.isGrounded && currentMovementInput == Vector2.zero) && !isSlippery)
            {
                //momentumDrag = 30.25f;
                momentumDrag = 5.25f;
            }

            //float momentumDrag = 30.25f;
            characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
            if (characterVelocityMomentum.magnitude < .0f)
            {
                characterVelocityMomentum = Vector2.zero;
            }

            /*
            if ((characterController.isGrounded && currentMovementInput.x < 0f && characterVelocityMomentum.x > 0f))
            {
                characterVelocityMomentum.x += currentMovementInput.x;
            }
            */

        }
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        stickMovementInput = context.ReadValue<Vector2>();

    }

    void OnRun(InputAction.CallbackContext context)
    {

        isRunPressed = context.ReadValueAsButton();

    }


    void Flip(bool left)
    {
        /*if (characterController.isGrounded)
        {
            CreateDustTrail();
        } */

        facingLeft = left;

        characterSprite.GetComponent<SpriteRenderer>().flipX = (left);


    }

    void CreateDustTrail()
    {
        dust.Play();
    }


    private void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:

                
                HandleMovement();
                HandleGravity();
                HandleJump();
                HandleGrapple();
                AimWithCursor();

                HandleRespawn();

                if (characterController.collisionFlags == CollisionFlags.None)
                {
                    isContactingWall = false;
                }

                if (characterController.isGrounded)
                {
                    isContactingWall = false;
                    isWallJumping = false;
                }
                break;

            case State.GrappleThrown:
                HandleMovement();
                HandleGravity();
                HandleGrappleThrow();
                break;

            case State.GrappleFlyingPlayer:

                HandleGrappleMovement();
                break;

        }

    }

    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

         if (hit.collider.CompareTag("Wall") && !characterController.isGrounded && characterController.collisionFlags == CollisionFlags.Sides)
         {

            //Debug.Log("wall!");
            isContactingWall = true;
            Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);

            if (isJumpPressed)
            {
                CreateDustTrail();
                isJumping = true;
                isWallJumping = true;
                currentMovement.y = initialJumpVelocity;
                currentRunMovement.y = initialJumpVelocity;


                Flip(!facingLeft);


                currentMovement.x = hit.normal.x * 20f;
                currentRunMovement.x = hit.normal.x * 15f;
            }
         }
         if (hit.collider.CompareTag("Slippery") && characterController.isGrounded)
        {
            isSlippery = true;
        }
        else
        {
            if (characterController.isGrounded)
            {
                isSlippery = false;
            }
        }

        if (hit.collider.CompareTag("Sticky") && isSticky == false)
        {
            isSticky = true;
            characterVelocityMomentum.x = 0f;
            walkSpeed = 2.0f;
            runSpeed = 3.0f;
            maxJumpHeight = 2.0f;
            maxJumpTime = 0.35f;
            SetupJumpVariables();
        }
        else if( isSticky == true && !hit.collider.CompareTag("Sticky"))
        {
            isSticky = false;
            walkSpeed = 16.0f;
            runSpeed = 24.0f;
            maxJumpHeight = 9.0f;
            maxJumpTime = 0.7f;
            SetupJumpVariables();
        }
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
