using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : ReloadableGun
{
    float timeCount = 0f;

    void Start()
    {
        //Loads bullet prefab
        fireRate = 0.1f;
        // bulletPrefab = Resources.Load("Prefabs/Bullet/PistolBullet") as GameObject;

        //Ammo System init
        maxClipSize = 100;
        currentClip = maxClipSize;
        maxAmmo = 1000;
        currentAmmoStored = 500;
        msReloadTime = 1000f; //1 second reload time
    }

    public override void UpdateWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadGun();
        }
        Attack();
    }

    // Fires the player's gun if Left Mouse-Button is pressed
    public override void Attack()
    {
        /* 
        if (timeCount >= fireRate && Input.GetMouseButton(0) && !isReloading && currentClip > 0)
        {
            //Deal with Ammo
            currentClip--;
            if (currentClip <= 0)
            {
                ReloadGun();
            }
            GameObject aBullet = Instantiate(bulletPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
            timeCount = 0f;
        } */
        timeCount += Time.deltaTime;

        if (timeCount >= fireRate && Input.GetMouseButton(0) && !isReloading && currentClip > 0)
        {
            if (currentClip <= timeCount || currentClip <= 0)
            {
                // currentClip = 0;
                // timeCount = 0f;
                ReloadGun();
            }
            GameObject aBullet = Instantiate(bulletPrefab, spawnPos.position + (spawnPos.up * 2), spawnPos.rotation) as GameObject;
            currentClip--;
            timeCount = 0f;
        }

        // currentClip -= (int)timeCount;
        // timeCount = 0f;

    }
}
