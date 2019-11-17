using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    // Start is called before the first frame update
    float timeCount = 0f;
    void Start()
    {
        fireRate = 1f;
    }

    void FixedUpdate()
    {
        timeCount += Time.deltaTime;
    }

    // Fires the player's gun if Left Mouse-Button is pressed
    public override void Attack() 
    {
        if (timeCount >= fireRate && Input.GetMouseButton(0))
        {//if player clicks left mouse or there is a bullet queued
            for (int i = 0; i < 6; i++)
            {
                GameObject aBullet = Instantiate(bulletPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
                aBullet.transform.Rotate(Random.Range(-40, 40), Random.Range(-40, 40), 0);
            }
            timeCount = 0f;
        }
    }

}
