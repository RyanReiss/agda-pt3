using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : InteractableObject
{
    private PlayerController player;
    public string gunName;
    public GameObject gunToGive;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gunToGive = transform.Find(gunName).gameObject;
        gunToGive.SetActive(false);
    }

    public override void Interact(){
        player.PickupGun(gunToGive, gunName);
        Destroy(this.gameObject);
    }
}
