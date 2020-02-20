using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReloadableGun : Gun {

    // Ammo System
    public int currentAmmoStored; // The current amount of ammo that the player is holding (that isnt in currentClip)
    protected int maxAmmo; // The maximum amount of ammo the player can hold.
    public int maxClipSize; // The max ammo that can be stored in a clip
    protected int currentClip; // The current ammo inside the player's clip
    protected bool isReloading = false; // True if the player is currently reloading their gun
    public float msReloadTime; // The time it takes for the player to reload the gun in milliseconds
    protected bool failSafe; // A bool used to fix a weird error. (see below)
    private float startTime;

    public AudioObject fireAudio;
    public AudioObject reloadAudio;

    public string currentEffect;
    
    /*
    //Sample Start Method initialization of all reload variables
    // This would be for a gun that has a 10 bullet clip, that starts with 10 bullets.
    void Start() {
        maxClipSize = 10;
        currentClip = maxClipSize;
        maxAmmo = 100; //Starting Ammo
        currentAmmoStored = 0;
        msReloadTime = 1000f; //1 second reload time
    }
    */

    public virtual void Start(){
        currentEffect = "NoEffect";
    }

    protected void ReloadGun(){
        // Check to make sure the gun isnt currently reloading...
        //Debug.Log("Current ammo left: "+ currentAmmoStored);
        if(!isReloading){
            // Check to make sure the current clip isnt already fully reloaded
            if(currentClip != maxClipSize){
                //Debug.Log("Reloading...");
                currentAmmoStored += currentClip;
                currentClip = 0;
                startTime = Time.time;
                StartCoroutine(WaitToReload(msReloadTime));
            }
        } else if(failSafe) {
            failSafe = false;
            isReloading = false;
            if(currentClip != maxClipSize){
                //Debug.Log("Reloading...");
                currentAmmoStored += currentClip;
                currentClip = 0;
                startTime = Time.time;
                StartCoroutine(WaitToReload(msReloadTime));
            }
        }
    }

    protected IEnumerator WaitToReload(float msWaitTime){
        isReloading = true;
        // Check to make sure the clip is emptied before reloading
        failSafe = true;
        if (reloadAudio != null) {
            reloadAudio.PlayAll(msWaitTime);
        }
        yield return new WaitForSeconds(msWaitTime/1000f); // Wait to reload the gun
        if(currentClip != 0){
            if(currentAmmoStored + currentClip > maxAmmo){
                currentAmmoStored = maxAmmo;
            } else {
                currentAmmoStored += currentClip;
            }
            currentAmmoStored += currentClip;
            currentClip = 0;
        }
        // Does the player have enough ammo to fully reload a clip?
        if(currentAmmoStored >= maxClipSize){ //Yes
            //Reload full clip
            currentAmmoStored -= maxClipSize;
            currentClip = maxClipSize;
        } else { //No
            // Reload partial clip
            currentClip = currentAmmoStored;
            currentAmmoStored = 0;
        }
        failSafe = false;
        isReloading = false;
    }

    public int GetCurrentAmmoStored(){
        return currentAmmoStored;
    }

    public int GetCurrentClipSize(){
        return currentClip;
    }

    public float GetAmmoRatio(){
        if(isReloading){
            //Debug.Log(((Time.time-startTime) / ((Time.time-startTime) + msReloadTime/1000f))*2f);
            return ((Time.time-startTime) / ((Time.time-startTime) + msReloadTime/1000f))*2f;
        } else {
            return (float)currentClip / (float)maxClipSize;
        }
    }

    public void AddAmmoToAmmoStorage(int ammoToAdd){
        currentAmmoStored+=ammoToAdd;
        if(currentAmmoStored > maxAmmo){
            currentAmmoStored = maxAmmo;
        }
    }

    public bool IsGunReloading(){
        return isReloading;
    }

    public void SetWeaponEffect (string effectKey) {
        Debug.Log ("Setting Weapon Effect...");
        Debug.Log("effectKey in weapon: " + effectKey);
        this.currentEffect = effectKey;
        //bulletPrefab.GetComponent<Bullet> ().SetBulletEffect (effectKey);
    }
    
}
