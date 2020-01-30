using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupController : MonoBehaviour
{
    // Storage class for all ammo pickups
    PlayerController player;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void PickupAmmo(string gunName,int ammoToPickup){
        if(gunName == "Pistol"){
            player.GetComponentInChildren<Pistol>(true).AddAmmoToAmmoStorage(ammoToPickup);
        } else if(gunName == "Rifle"){
            player.GetComponentInChildren<AutoRifle>(true).AddAmmoToAmmoStorage(ammoToPickup);
        } else if(gunName == "Shotgun"){
            player.GetComponentInChildren<ShotGun>(true).AddAmmoToAmmoStorage(ammoToPickup);
        } else if(gunName == "Flamethrower"){
            player.GetComponentInChildren<FlameThrower>(true).AddAmmoToAmmoStorage(ammoToPickup);
        }
    }

    public bool CanPickupAmmo(string gunName){
        if(gunName == "Pistol"){
            if(player.GetComponentInChildren<Pistol>(true)){
                return true;
            }
        } else if(gunName == "Rifle"){
            if(player.GetComponentInChildren<AutoRifle>(true)){
                return true;
            }
        } else if(gunName == "Shotgun"){
            if(player.GetComponentInChildren<ShotGun>(true)){
                return true;
            }
        } else if(gunName == "Flamethrower"){
            if(player.GetComponentInChildren<FlameThrower>(true)){
                return true;
            }
        }
        return false;
    }
}
