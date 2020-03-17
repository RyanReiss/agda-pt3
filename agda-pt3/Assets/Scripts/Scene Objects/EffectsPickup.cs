using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsPickup : Pickup {
    private PlayerController player;
    public string effectName;
    private List<GameObject> weaponList = new List<GameObject> ();

    protected override void Start () {
        base.Start ();
        player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

    }

    public override void Interact () {
        // weaponList = player.GetAllWeapons ();
        // foreach (GameObject weapon in weaponList) {
        //     weapon.setWeaponEffect(effectName);
        // }
        Debug.Log("Get Weapon!!!");
        GameObject weapon = player.GetCurrentWeapon();
        Debug.Log("Interaction!!!");
        weapon.GetComponent<ReloadableGun>().SetWeaponEffect(effectName);
        if(this.GetComponent<DestroyWhenPickedUp>()){
            this.GetComponent<DestroyWhenPickedUp>().PickUp();
        }
        List<string> messageToSend = new List<string>();
        messageToSend.Add("* picked up " + effectName.ToLower() + " bullet effect on " + weapon.GetComponent<ReloadableGun>().name.ToLower() + " *");
        DialogueController.Instance.InteractWithTextBox(messageToSend);
        Destroy(this.gameObject);
    }

    public override void SetPickupValue(float value){
        
    }
}