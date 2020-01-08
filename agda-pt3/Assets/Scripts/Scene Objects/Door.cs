using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{

    // Implementation of an Interactable object to make a primitive door script

    private bool state; // Door state: true = open, false = closed
    Animator anim;

    protected override void Start() {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override void Interact(){
        if(state){
            //If the door is open, close it
            state = !state;
            anim.SetBool("doorState", state);
        } else {
            //If the door is closed, open it
            state = !state;
            anim.SetBool("doorState", state);
        }
    }
}
