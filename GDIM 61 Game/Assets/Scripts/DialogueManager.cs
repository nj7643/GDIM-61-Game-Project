using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.InputSystem;
using TMPro;





public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    //animation to enter the dialogue box into screen
    public Animator animator;

    private Queue<string> sentences;


    CursorController playerInput;
    //PlayerInputActions playerInput;
    private bool isTalkPressed;
    private float timeRemaining = 0;


    private void Awake()
    {
        playerInput = new CursorController();
        //playerInput = new PlayerInputActions();

        isTalkPressed = false;
        playerInput.Player.Dialogue.started += OnTalk;
        playerInput.Player.Dialogue.canceled += OnTalk;
        //playerInput.Player.Dialogue.performed += OnTalk;
    }
    

    
    void OnTalk(InputAction.CallbackContext context)
    {
        isTalkPressed = context.ReadValueAsButton();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialogue (Dialogue dialogue)
    {
        //activates the animation
        animator.SetBool("IsOpen", true);

        Debug.Log("START CONVO with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear();
        // checks sentence entered by triggerDialogue
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
           
        }
            DisplayNextSentence();
        
        
        
    }

    //for buttons (when continue is pressed)
    public void DisplayNextSentence()
    {
        //checks if there is no more sentence to type
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //goes through the sentence que and types
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        //types sentence to dialogue box per character
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        //closes the dialogue box
        Debug.Log("end of conversation");
        animator.SetBool("IsOpen", false);
    }

    private void Update()
    {
        //press tab to conibue the dialogue instead of clicking continue

        if(timeRemaining <=0 && isTalkPressed)
        //Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("next");
            DisplayNextSentence();
            timeRemaining = .35f;
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
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
