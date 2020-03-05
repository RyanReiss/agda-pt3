using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : InteractableObject
{
    DialogueController dialogueController;
    [SerializeField]
    public List<string> dialogueToGive;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        dialogueController = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<DialogueController>();
    }

    public override void Interact(){
        // Pass Dialogue Lines to Controller
        dialogueController.InteractWithTextBox(dialogueToGive);
    }
}
