using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : Gun
{
    // Start is called before the first frame update
    float timeCount = 0f;

    void Start()
    {
        //Loads bullet prefab
        fireRate = 0.15f;
         bulletPrefab = Resources.Load("Prefabs/Bullet/RifleBullet") as GameObject;
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
            Debug.Log("instantiated");
            timeCount = 0f;
        }
    }
}
