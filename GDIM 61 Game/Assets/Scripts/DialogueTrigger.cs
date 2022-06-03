using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public string player; 

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == player)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        
    }
}
