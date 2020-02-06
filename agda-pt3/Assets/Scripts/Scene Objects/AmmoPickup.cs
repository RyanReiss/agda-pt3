using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    public string gunName;
    public int ammoToPickup;
    PlayerController player;
    AmmoPickupController ammoPickupController;
    DialogueController dialogueController;
    List<string> dontHaveGunMessage = new List<string>();
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ammoPickupController = (AmmoPickupController)FindObjectOfType(typeof(AmmoPickupController));
        dialogueController = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<DialogueController>();
        dontHaveGunMessage.Add("You need a " + gunName + " to pickup " + gunName + " ammo!");
    }

    public override void Interact(){
        if(this.GetComponent<DestroyWhenPickedUp>() && this.GetComponent<DestroyWhenPickedUp>().identifier != ""){
            this.GetComponent<DestroyWhenPickedUp>().PickUp();
        }
        if(ammoPickupController.CanPickupAmmo(gunName)){
            ammoPickupController.PickupAmmo(gunName,ammoToPickup);
            Destroy(this.gameObject);
        } else {
            dialogueController.InteractWithTextBox(dontHaveGunMessage);
        }
    }

    public override void SetPickupValue(float value){
        ammoToPickup = (int)value;
    }
}
