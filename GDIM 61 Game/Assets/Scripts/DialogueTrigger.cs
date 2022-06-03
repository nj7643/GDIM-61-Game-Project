using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject dialogueCanvas;
    public GameObject eTextInput;
    public string player; 

   
    public void TriggerDialogue()
    {
        //use for buttons
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

  

    public void OnTriggerStay(Collider other)
    {
        //used on signs when collision with player 

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
    }



}
