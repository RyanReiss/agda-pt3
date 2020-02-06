using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Pickup
{
    private PlayerController player;
    public string itemName;
    public List<string> linesToShowWhenItemPickedUp;
    DialogueController dialogueController;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        dialogueController = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<DialogueController>();
    }

    public override void Interact(){
        player.PickupItem(itemName, this.GetComponent<SpriteRenderer>().sprite);
        if(this.GetComponent<DestroyWhenPickedUp>() && this.GetComponent<DestroyWhenPickedUp>().identifier != ""){
            this.GetComponent<DestroyWhenPickedUp>().PickUp();
        }
        if(linesToShowWhenItemPickedUp.Count > 0){
            dialogueController.InteractWithTextBox(linesToShowWhenItemPickedUp);
        }
        Destroy(this.gameObject);
    }

    public override void SetPickupValue(float value){
        
    }
}
