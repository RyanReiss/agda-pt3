using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : ReloadableGun
{
    // Basic Player Gun class that is attached to a gun object

    /*[RangeAttribute(0,1)]
    public float timeBetweenShots;
    private float intervalBullet;   //The interval time until the next bullet can be fired
    private int shotsQueued; // The amount of shots queued after the current cooldown
    public Transform muzzle;   // Location of the muzzle of the gun
    GameObject bulletPrefab;*/

    float timeCount = 0f;

    public override void Start() {
        //Loads bullet prefab
        fireRate = 0.35f;
        // bulletPrefab = Resources.Load("Prefabs/Bullet/PistolBullet") as GameObject;

        //Ammo System init
        maxClipSize = 10;
        currentClip = maxClipSize;
        maxAmmo = 100;
        currentAmmoStored = 10;
        msReloadTime = 1000f; //1 second reload time
    }

    public override void UpdateWeapon() {
        if(Input.GetKeyDown(KeyCode.R)){
            ReloadGun();
        }
        Attack();
    }

    // Fires the player's gun if Left Mouse-Button is pressed
    public override void Attack()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= fireRate && Input.GetMouseButton(0) && (!isReloading || failSafe) && currentClip > 0)
        {
            //Deal with Ammo
            currentClip--;
            if(currentClip <= 0){
                ReloadGun();
            }
            GameObject aBullet = Instantiate(bulletPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
            timeCount = 0f;
        }
    }
}
