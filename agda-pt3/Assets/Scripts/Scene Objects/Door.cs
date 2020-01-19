using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{

    // Implementation of an Interactable object to make a primitive door script

    private bool state; // Door state: true = open, false = closed
    Animator anim;
    public string roomToShow; //shows a hidden room using the HiddenRoomController
    private bool needToShowRoom;
    private HiddenRoomController roomController;
    public bool lockedDoor;
    public string keyToUnlock;
    private PlayerController player;

    protected override void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        roomController = (HiddenRoomController)FindObjectOfType(typeof(HiddenRoomController));
        base.Start();
        if(roomToShow != ""){
            needToShowRoom = true;
        } else {
            needToShowRoom = false;
        }
        anim = GetComponent<Animator>();
    }

    public override void Interact(){
        if(!lockedDoor){
            if(state){
                //If the door is open, close it
                state = !state;
                anim.SetBool("doorState", state);
                // Hide the room if it needs to be hidden
            } else {
                //If the door is closed, open it
                state = !state;
                anim.SetBool("doorState", state);
                // If the room is currently hidden, unhide it!
                if(roomToShow != ""){
                    if(needToShowRoom){
                        roomController.ShowRoom(roomToShow);
                        needToShowRoom = false;
                    }
                }
            }
        } else if(player.InventoryContains(keyToUnlock)){
            lockedDoor = false;
            Interact();
        }
        
    }

    public bool GetState(){
        return state;
    }
}
