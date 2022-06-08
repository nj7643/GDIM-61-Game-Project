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
             if (Input.GetKeyDown(KeyCode.E))
             {
                 eTextInput.SetActive(false);
                 FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
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



}
