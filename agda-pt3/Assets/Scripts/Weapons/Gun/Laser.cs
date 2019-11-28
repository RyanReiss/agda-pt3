using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : ReloadableGun
{

    //still in progress
    float timeCount = 0f;
    int energyConsumePerSecond = 1;
    float weaponRange = 10;
    public LineRenderer line;
    public Transform lineEndPoint;
    public ParticleSystem hitEffect;
    

    void Start()
    {
        //Ammo System init
        maxClipSize = 10;
        currentClip = maxClipSize;
        maxAmmo = 100;
        currentAmmoStored = 10;
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
        timeCount += Time.deltaTime;
        if (timeCount >= fireRate && Input.GetMouseButton(0) && !isReloading && currentClip > 0)
        {
            //Deal with Ammo
            currentClip--;
            if (currentClip <= 0)
            {
                line.enabled = false;
                ReloadGun();
            }
            OnEnable();
            timeCount = 0f;
        }
    }


    void OnEnable()
    {
        line.enabled = true;
        line.SetPosition(0, transform.position);

        //Cast the laser
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.up.normalized, weaponRange);

        //If the laser hit something, then the object would be its endpoint
        if (raycastHit)
        {
            line.SetPosition(1, raycastHit.point);
        }

        else
        {
            line.SetPosition(1, lineEndPoint.position);
        }
    }

    void Update()
    {
        line.SetPosition(0, transform.position);

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, transform.up.normalized, weaponRange);
        if (raycastHit)
        {
            line.SetPosition(1, raycastHit.point);
            hitEffect.transform.position = raycastHit.point;
            
        }

        else
        {
            line.SetPosition(1, lineEndPoint.position);
            hitEffect.transform.position = lineEndPoint.position;
         
        }

    }
}
