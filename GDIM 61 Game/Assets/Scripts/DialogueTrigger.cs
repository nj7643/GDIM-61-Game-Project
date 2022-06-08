using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject dialogueCanvas;
    public GameObject eTextInput;
    public string player;


    private bool inDialogue = false;
    private bool isReadPressed = false;
    CursorController playerInput;

    private void Awake()
    {
        playerInput = new CursorController();

        playerInput.Player.Dialogue.started += OnRead;
        playerInput.Player.Dialogue.canceled += OnRead;
    }



    void OnRead(InputAction.CallbackContext context)
    {
        isReadPressed = context.ReadValueAsButton();
    }

    public void TriggerDialogue()
    {
        //use for buttons
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

  

    public void OnTriggerStay(Collider other)
    {
        //used on signs when collision with player 
        //bool inDialogue = false;

        Debug.Log("entered the sign");

        //only player can trigger the signs
        if (other.tag == player)
        {
            //activaes diagloue canvas 
           
            dialogueCanvas.SetActive(true);

            
             //while player is in the trigger they can use e to start dialogue
             if (inDialogue == false && isReadPressed)
             //if (inDialogue == false && Input.GetKeyDown(KeyCode.E))
             {
                 eTextInput.SetActive(false);
                 FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                inDialogue = true;
             }
            

            /*
            if (inDialogue == false)
            {
                eTextInput.SetActive(false);
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                inDialogue = true;
            }
            */

        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == player)
        {
            eTextInput.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        dialogueCanvas.SetActive(false);
        inDialogue = false;
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
