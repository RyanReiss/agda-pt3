using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : ReloadableGun
{
    // Start is called before the first frame update
    float timeCount = 0f;
    public float shotgunKnockback;
    public override void Start()
    {
        base.Start();
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
        if (timeCount >= fireRate && Input.GetMouseButton(0) && (!isReloading || failSafe) && currentClip > 0)
        {
            //Deal with Ammo System
            currentClip--;
            if(currentClip <= 0){
                ReloadGun();
            }
            GameObject muzzleFlash = PlayerEffectsController.Instance.GetEffect("yellowFlash");
            muzzleFlash.transform.position = spawnPos.position;
            muzzleFlash.transform.rotation = spawnPos.rotation;
            muzzleFlash.transform.position += muzzleFlash.transform.up*1.25f;
            muzzleFlash.GetComponent<MuzzleFlash>().transformToReturnTo = muzzleFlash.transform.parent;
            muzzleFlash.transform.parent = this.transform;
            muzzleFlash.SetActive(true);
            for (int i = 0; i < 6; i++)
            {
                GameObject aBullet = Instantiate(bulletPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
                aBullet.GetComponent<Bullet>().SetEffect(currentEffect);
                aBullet.transform.Rotate(0,0, Random.Range(-20, 20));
            }
            transform.parent.parent.parent.GetComponent<PlayerController>().ApplyGunKnockback(shotgunKnockback);
            timeCount = 0f;
        }
    }

}
