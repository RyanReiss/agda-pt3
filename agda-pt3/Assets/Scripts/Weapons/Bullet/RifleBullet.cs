﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = 60f;
        damageToGive = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        BulletPath(1f);
    }

    public override void BulletPath(float coefficient)
    {
        transform.position += transform.up.normalized * Time.deltaTime * bulletSpeed * coefficient;
        Destroy(this.gameObject, 2.0f);
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        Start();
        if (col.gameObject.GetComponent<Health>() != null)
        {
            col.gameObject.GetComponent<Health>().TakeDamage(damageToGive);
        }
        //Destroy the bullet if it collides with something
        if (col.transform.name != "Player")
        {
            Destroy(gameObject);
        }
    }
}