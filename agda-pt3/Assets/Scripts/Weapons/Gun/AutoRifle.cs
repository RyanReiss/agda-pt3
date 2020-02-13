using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : ReloadableGun
{
    // Start is called before the first frame update
    float timeCount = 0f;

    public override void Start()
    {
        base.Start();
        //Loads bullet prefab
        fireRate = 0.15f;
        // bulletPrefab = Resources.Load("Prefabs/Bullets/RifleBullet") as GameObject;

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
        if (timeCount >= fireRate && Input.GetMouseButton(0) && (!isReloading || failSafe) && currentClip > 0)
        {
            //Deal with Ammo
            currentClip--;
            if(currentClip <= 0){
                ReloadGun();
            }
            GameObject muzzleFlash = PlayerEffectsController.Instance.GetEffect("greenFlash");
            muzzleFlash.transform.position = spawnPos.position;
            muzzleFlash.transform.rotation = spawnPos.rotation;
            muzzleFlash.transform.position += muzzleFlash.transform.up*.75f;
            muzzleFlash.GetComponent<MuzzleFlash>().transformToReturnTo = muzzleFlash.transform.parent;
            muzzleFlash.transform.parent = this.transform;
            muzzleFlash.SetActive(true);
            GameObject aBullet = Instantiate(bulletPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
            aBullet.GetComponent<Bullet>().SetEffect(currentEffect);
            if(this.GetComponent<Animator>()){
                this.GetComponent<Animator>().SetTrigger("Shoot");
            }
            timeCount = 0f;
        }
    }
}
