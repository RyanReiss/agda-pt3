using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosions : Bullet
{
    // Start is called before the first frame update
    public float size;
    void Start()
    {
        bulletSpeed = 0f;
        damageToGive = 3.0f;
        timeToDie = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, timeToDie);
    }
    public override void BulletPath(float coefficient)
    {
    }
    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Health>() != null)
        {
            col.gameObject.GetComponent<Health>().TakeDamage(damageToGive);
        }
        //Destroy the bullet if it collides with something
        
        // Destroy(gameObject, timeToDie);
        
    }
}
