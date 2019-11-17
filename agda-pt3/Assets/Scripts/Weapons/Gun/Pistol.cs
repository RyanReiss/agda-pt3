using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    // Basic Player Gun class that is attached to a gun object

    /*[RangeAttribute(0,1)]
    public float timeBetweenShots;
    private float intervalBullet;   //The interval time until the next bullet can be fired
    private int shotsQueued; // The amount of shots queued after the current cooldown
    public Transform muzzle;   // Location of the muzzle of the gun
    GameObject bulletPrefab;*/

    float timeCount = 0f;

    void Start()
    {
        //Loads bullet prefab
        fireRate = 0.35f;
        // bulletPrefab = Resources.Load("Prefabs/Bullet/PistolBullet") as GameObject;
    }

    void FixedUpdate()
    {
        timeCount += Time.deltaTime;
    }

    // Fires the player's gun if Left Mouse-Button is pressed
    public override void Attack()
    {
        if (timeCount >= fireRate && Input.GetMouseButton(0))
        {
            GameObject aBullet = Instantiate(bulletPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
            timeCount = 0f;
        }
    }
}
