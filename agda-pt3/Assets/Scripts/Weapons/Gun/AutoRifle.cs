using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : ReloadableGun
{
    // Start is called before the first frame update
    float timeCount = 0f;

    void Start()
    {
        //Loads bullet prefab
        fireRate = 0.15f;
        bulletPrefab = Resources.Load("Prefabs/Bullets/RifleBullet") as GameObject;

        // Ammo init
        maxClipSize = 20;
        currentClip = maxClipSize;
        maxAmmo = 400; // Starting Ammo
        currentAmmoStored = 100;
        msReloadTime = 2000f; //2 second reload time
    }

    public override void UpdateWeapon()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            ReloadGun();
        }
        Attack();
    }

    // Fires the player's gun if Left Mouse-Button is pressed
    public override void Attack()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= fireRate && Input.GetMouseButton(0) && !isReloading && currentClip > 0)
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
