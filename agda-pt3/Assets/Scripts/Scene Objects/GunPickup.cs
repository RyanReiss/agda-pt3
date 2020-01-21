using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
        //gunToGive = transform.Find(gunName).gameObject;
        //gunToGive.SetActive(false);
        // Instead of having a gun that you swap to be a child of the backpack GO, 
        // INSTANIATE a new gun when the item is picked up
        gunToGive = (GameObject)Resources.Load("Prefabs/Player/" + gunName, typeof(GameObject));
        if(((GameObject)Resources.Load("Prefabs/UI/LoadoutImages/" + gunName, typeof(GameObject))).GetComponent<Image>().sprite != null){
            this.GetComponent<SpriteRenderer>().sprite = ((GameObject)Resources.Load("Prefabs/UI/LoadoutImages/" + gunName, typeof(GameObject))).GetComponent<Image>().sprite;
        }
    }

    public override void Interact(){
        //GameObject temp = Instantiate(gunToGive);
        player.PickupGun(gunToGive, gunName);
        Destroy(this.gameObject);
    }
}
