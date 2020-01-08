using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : ReloadableGun
{
    // Start is called before the first frame update
    float timeCount = 0f;
    public float shotgunKnockback;
    void Start()
    {
        fireRate = 0.75f;

        //Ammo System init
        maxClipSize = 4;
        currentClip = maxClipSize;
        maxAmmo = 80;
        currentAmmoStored = 40;
        msReloadTime = 3000f; //1 second reload time
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
        if (timeCount >= fireRate && Input.GetMouseButton(0) && !isReloading && currentClip > 0)
        {
            //Deal with Ammo System
            currentClip--;
            if(currentClip <= 0){
                ReloadGun();
            }
            for (int i = 0; i < 6; i++)
            {
                GameObject aBullet = Instantiate(bulletPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
                aBullet.transform.Rotate(Random.Range(-40, 40), Random.Range(-40, 40), 0);
            }
            transform.parent.parent.GetComponent<PlayerController>().ApplyGunKnockback(shotgunKnockback);
            timeCount = 0f;
        }
    }

}
