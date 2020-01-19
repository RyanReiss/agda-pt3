using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : InteractableObject
{
    private PlayerController player;
    public string itemName;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public override void Interact(){
        player.PickupItem(itemName, this.GetComponent<SpriteRenderer>().sprite);
        Destroy(this.gameObject);
    }
}
