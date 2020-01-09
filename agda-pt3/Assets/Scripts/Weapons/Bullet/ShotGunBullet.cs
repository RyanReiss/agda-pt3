using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunBullet : Bullet
{
    // Start is called before the first frame update
    
    void Start()
    {
        bulletSpeed = 150f;
        damageToGive = 1.5f;
    }

    void Update()
    {
        BulletPath(1f);
    }

    public override void BulletPath(float coefficient)
    {
        transform.position += transform.up.normalized * Time.deltaTime * bulletSpeed * coefficient;
        Destroy(this.gameObject, 0.8f);
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        Start(); // Added in case OnTriggerEnter2D is called before start is called
        if (col.gameObject.GetComponent<Health>() != null)
        {
            col.gameObject.GetComponent<Health>().TakeDamage(damageToGive);
        }
        if (col.transform.name != "Player" && col.transform.name != "ShotGunBullet(Clone)" && col.transform.tag != "TriggersToIgnore")
        {
            Destroy(gameObject);
        }
    }
}
